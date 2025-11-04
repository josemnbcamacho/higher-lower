using HigherOrLower.API.DTOs.Response;
using HigherOrLower.API.Errors;
using HigherOrLower.API.Models.Entities;
using HigherOrLower.API.Repository.Interfaces;
using OneOf;

namespace HigherOrLower.API.Services;

public class HigherOrLowerGameManager : IHigherOrLowerGameManager
{
    private const int MinPlayers = 2;
    private const int MaxPlayers = 12;

    private readonly IHigherLowerGameRepository _gameRepository;
    private readonly IDeckRepository _deckRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IDeckCreator _deckCreator;
    private readonly IPlayerCreator _playerCreator;

    public HigherOrLowerGameManager(IHigherLowerGameRepository gameRepository, IDeckRepository deckRepository,
        IPlayerRepository playerRepository, IDeckCreator deckCreator, IPlayerCreator playerCreator)
    {
        _gameRepository = gameRepository;
        _deckRepository = deckRepository;
        _playerRepository = playerRepository;
        _deckCreator = deckCreator;
        _playerCreator = playerCreator;
    }

    public async Task<OneOf<GameStartedResult, InvalidNumberOfPlayersError>> StartGameAsync(int numPlayers)
    {
        // validate the number of requested players
        if (numPlayers < MinPlayers || numPlayers > MaxPlayers)
        {
            return new InvalidNumberOfPlayersError();
        }
        
        var shuffledDeck = _deckCreator.CreateShuffledDeck();
        var newGuid = Guid.NewGuid();
        var players = _playerCreator.CreatePlayers(numPlayers).ToList();
        
        // create a new higher or lower game
        var game = new HigherOrLowerGame()
        {
            Id = newGuid,
            Deck = shuffledDeck,
            Players = players
        };

        // get a new card from the deck
        var card = game.Deck.DrawCard();
        
        // set the new drawn card as the last card
        game.LastCard = card;

        // save the game
        await _gameRepository.AddAsync(game);
        
        // save the changes to the database
        await _gameRepository.SaveChangesAsync();

        return new GameStartedResult()
        {
            GameId = game.Id,
            DrawnCardName = card.ToString(),
            Players = game.Players.Select(p => new PlayerResult(){
                Id = p.Id,
                Name = p.Name
            }).ToList()
        };
    }

    public async Task<OneOf<TurnResult, GameNotFoundError, GameEndedError, DeckNotFoundError, NoCardDrawnError, PlayerNotFoundError>> PlayTurnAsync(Guid gameId, Guid playerId, Guess guess)
    {
        // get the game from the repository
        var currentGame = await _gameRepository.GetByIdAsync(gameId);

        // check if game exists
        if (currentGame == null)
            return new GameNotFoundError();

        // check if game has already ended
        if (currentGame.HasGameEnded)
            return new GameEndedError();

        // check if deck exists
        if (currentGame.Deck == null)
            return new DeckNotFoundError();

        // check if a card has been drawn
        if (currentGame.LastCard == null)
            return new NoCardDrawnError();

        // validate if the player is part of the game
        var player = currentGame.Players.FirstOrDefault(p => p.Id == playerId);

        if (player == null)
            return new PlayerNotFoundError();

        // get a new card from the deck
        var drawnCard = currentGame.Deck.DrawCard();

        // check if the guess is correct
        // if the card had the same face value, it counts as a win
        var correctGuess = guess switch
        {
            Guess.Higher => drawnCard.Value >= currentGame.LastCard.Value,
            Guess.Lower => drawnCard.Value <= currentGame.LastCard.Value,
            _ => throw new ArgumentOutOfRangeException(nameof(guess), guess, "Invalid guess")
        };
        
        if (correctGuess)
            player.Score++;
        
        // set the new drawn card as the last card
        currentGame.LastCard = drawnCard;

        // save the game
        _gameRepository.Update(currentGame);
        
        // save player changes to the database
        _playerRepository.Update(player);
        
        // save the updated deck
        _deckRepository.Update(currentGame.Deck);
        
        await _deckRepository.SaveChangesAsync();

        var turnResult = new TurnResult()
        {
            CorrectGuess = correctGuess,
            DrawnCardName = drawnCard.ToString(),
            GameEnded = currentGame.HasGameEnded
        };
        
        return turnResult;
    }

    public async Task<OneOf<GameStateResult, GameNotFoundError>> GetGameStateAsync(Guid gameId)
    {
        // get the game from the repository
        var currentGame = await _gameRepository.GetByIdAsync(gameId);

        if (currentGame == null)
            return new GameNotFoundError();

        var gameStateResult = new GameStateResult()
        {
            GameEnded = currentGame.HasGameEnded,
            PlayerScores = currentGame.Players.Select(p => new PlayerScoreResult()
            {
                Name = p.Name,
                Score = p.Score
            }).ToList()
        };

        return gameStateResult;
    }
}