using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HigherOrLower.API.Errors;
using HigherOrLower.API.Models.Entities;
using HigherOrLower.API.Repository.Interfaces;
using HigherOrLower.API.Services;
using Moq;
using Xunit;

namespace HigherOrLower.Tests;

public class GameManagerTests
{
    private readonly Mock<IHigherLowerGameRepository> _mockGameRepository = new();
    private readonly Mock<IDeckRepository> _mockDeckRepository = new();
    private readonly Mock<IPlayerRepository> _mockPlayerRepository = new();
    private readonly DeckCreator _deckCreator = new();
    private readonly PlayerCreator _playerCreator = new();

    [Theory]
    [InlineData(2)]
    [InlineData(12)]
    public async Task StartGameAsync_ShouldReturnGame(int numberOfPlayers)
    {
        // Arrange
        _mockGameRepository.Setup(e => e.AddAsync(It.IsAny<HigherOrLowerGame>()));

        var gameManager = new HigherOrLowerGameManager(_mockGameRepository.Object, _mockDeckRepository.Object, _mockPlayerRepository.Object, _deckCreator, _playerCreator);

        // Act
        var result = await gameManager.StartGameAsync(numberOfPlayers);

        // Assert
        Assert.True(result.IsT0); // Success case
        var gameResult = result.AsT0;
        Assert.NotNull(gameResult.DrawnCardName);
        Assert.Equal(numberOfPlayers, gameResult.Players.Count);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(-1)]
    [InlineData(100)]
    public async Task StartGameAsync_ShouldReturnError(int numberOfPlayers)
    {
        // Arrange
        _mockGameRepository.Setup(e => e.AddAsync(It.IsAny<HigherOrLowerGame>()));

        var gameManager = new HigherOrLowerGameManager(_mockGameRepository.Object, _mockDeckRepository.Object, _mockPlayerRepository.Object, _deckCreator, _playerCreator);

        // Act
        var result = await gameManager.StartGameAsync(numberOfPlayers);

        // Assert
        Assert.True(result.IsT1); // Error case
        Assert.IsType<InvalidNumberOfPlayersError>(result.AsT1);
    }
    
    [Fact]
    public async Task PlayTurnsAsync_InvalidGameId_ShouldReturnError()
    {
        // Arrange
        _mockGameRepository.Setup(e => e.GetByIdAsync(It.IsAny<Guid>()));

        var gameManager = new HigherOrLowerGameManager(_mockGameRepository.Object, _mockDeckRepository.Object, _mockPlayerRepository.Object, _deckCreator, _playerCreator);

        // Act
        var result = await gameManager.PlayTurnAsync(Guid.NewGuid(), Guid.NewGuid(), Guess.Higher);

        // Assert
        Assert.True(result.IsT1); // Error case
        Assert.IsType<GameNotFoundError>(result.AsT1);
    }
    
    [Fact]
    public async Task PlayTurnsAsync_InvalidPlayerId_ShouldReturnError()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var deck = _deckCreator.CreateShuffledDeck();
        var player1 = new Player { Id = Guid.NewGuid() };
        var players = new List<Player> { player1 };

        var higherOrLowerGame = new HigherOrLowerGame() { Id = gameId, Deck = deck, Players = players };
        higherOrLowerGame.LastCard = higherOrLowerGame.Deck.DrawCard();

        _mockGameRepository.Setup(e => e.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(higherOrLowerGame);

        var gameManager = new HigherOrLowerGameManager(_mockGameRepository.Object, _mockDeckRepository.Object, _mockPlayerRepository.Object, _deckCreator, _playerCreator);

        // Act
        var result = await gameManager.PlayTurnAsync(gameId, Guid.NewGuid(), Guess.Higher);

        // Assert
        Assert.True(result.IsT5); // PlayerNotFoundError is the 6th type (index 5)
        Assert.IsType<PlayerNotFoundError>(result.AsT5);
    }
    
    [Fact]
    public async Task PlayTurnsAsync_Success_ShouldReturnResult()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var deck = _deckCreator.CreateShuffledDeck();
        var player1 = new Player { Id = Guid.NewGuid() };
        var players = new List<Player> { player1 };

        var higherOrLowerGame = new HigherOrLowerGame() { Id = gameId, Deck = deck, Players = players };
        higherOrLowerGame.LastCard = higherOrLowerGame.Deck.DrawCard();

        _mockGameRepository.Setup(e => e.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(higherOrLowerGame);

        var gameManager = new HigherOrLowerGameManager(_mockGameRepository.Object, _mockDeckRepository.Object, _mockPlayerRepository.Object, _deckCreator, _playerCreator);

        // Act
        var result = await gameManager.PlayTurnAsync(gameId, player1.Id, Guess.Higher);

        // Assert
        Assert.True(result.IsT0); // Success case
        var turnResult = result.AsT0;
        Assert.NotNull(turnResult.DrawnCardName);
    }

}