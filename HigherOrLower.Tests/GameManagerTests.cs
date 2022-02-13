using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HigherOrLower.API.Exceptions;
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
        Assert.NotNull(result);
        Assert.NotNull(result.DrawnCardName);
        Assert.Equal(numberOfPlayers, result.Players.Count);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(-1)]
    [InlineData(100)]
    public async Task StartGameAsync_ShouldThrowException(int numberOfPlayers)
    {
        // Arrange
        _mockGameRepository.Setup(e => e.AddAsync(It.IsAny<HigherOrLowerGame>()));

        var gameManager = new HigherOrLowerGameManager(_mockGameRepository.Object, _mockDeckRepository.Object, _mockPlayerRepository.Object, _deckCreator, _playerCreator);

        // Act and Assert
        await Assert.ThrowsAsync<ArgumentException>(() => gameManager.StartGameAsync(numberOfPlayers));
    }
    
    [Fact]
    public async Task PlayTurnsAsync_InvalidGameId_ShouldThrowException()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var deck = _deckCreator.CreateShuffledDeck();
        var players = new List<Player>();

        _mockGameRepository.Setup(e => e.GetByIdAsync(It.IsAny<Guid>()));

        var gameManager = new HigherOrLowerGameManager(_mockGameRepository.Object, _mockDeckRepository.Object, _mockPlayerRepository.Object, _deckCreator, _playerCreator);

        // Act and Assert
        await Assert.ThrowsAsync<GameNotFoundException>(() =>
            gameManager.PlayTurnAsync(Guid.NewGuid(), Guid.NewGuid(), Guess.Higher));
    }
    
    [Fact]
    public async Task PlayTurnsAsync_InvalidPlayerId_ShouldThrowException()
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
        
        // Act and Assert
        await Assert.ThrowsAsync<PlayerNotFoundException>(() =>
            gameManager.PlayTurnAsync(gameId, Guid.NewGuid(), Guess.Higher));
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
        Assert.NotNull(result);
        Assert.NotNull(result.DrawnCardName);
    }

}