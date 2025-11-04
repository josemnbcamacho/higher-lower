using HigherOrLower.API.DTOs.Response;
using HigherOrLower.API.Errors;
using HigherOrLower.API.Models.Entities;
using OneOf;

namespace HigherOrLower.API.Services;

public interface IHigherOrLowerGameManager
{
    Task<OneOf<GameStartedResult, InvalidNumberOfPlayersError>> StartGameAsync(int numPlayers);
    Task<OneOf<TurnResult, GameNotFoundError, GameEndedError, DeckNotFoundError, NoCardDrawnError, PlayerNotFoundError>> PlayTurnAsync(Guid gameId, Guid playerId, Guess guess);
    Task<OneOf<GameStateResult, GameNotFoundError>> GetGameStateAsync(Guid gameId);
}