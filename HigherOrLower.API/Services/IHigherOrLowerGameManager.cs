using HigherOrLower.API.DTOs.Response;
using HigherOrLower.API.Models.Entities;

namespace HigherOrLower.API.Services;

public interface IHigherOrLowerGameManager
{
    Task<GameStartedResult> StartGameAsync(int numPlayers);
    Task<TurnResult> PlayTurnAsync(Guid gameId, Guid playerId, Guess guess);
    Task<GameStateResult> GetGameStateAsync(Guid gameId);    
}