using HigherOrLower.API.Controllers.Base;
using HigherOrLower.API.DTOs.Request;
using HigherOrLower.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace HigherOrLower.API.Controllers;

[ApiController]
[Route("[controller]")]
public class HigherOrLowerGameController : BaseApiController
{
    private readonly IHigherOrLowerGameManager _gameManager;

    public HigherOrLowerGameController(IHigherOrLowerGameManager gameManager)
    {
        _gameManager = gameManager;
    }

    /// <summary>
    /// Starts a new game
    /// </summary>
    /// <returns>A game started result</returns>
    [HttpPost]
    public async Task<IActionResult> StartGame([FromBody] StartGameRequest request)
    {
        var result = await _gameManager.StartGameAsync(request.NumberOfPlayers);

        return result.Match<IActionResult>(
            success => Ok(success),
            error => ToActionResult(error)
        );
    }

    /// <summary>
    /// Plays a guess
    /// </summary>
    /// <returns>A turn result</returns>
    [HttpPost("guess")]
    public async Task<IActionResult> Guess([FromBody] GuessRequest request)
    {
        var result = await _gameManager.PlayTurnAsync(request.GameId, request.PlayerId, request.Guess);

        return result.Match<IActionResult>(
            success => Ok(success),
            error => ToActionResult(error)
        );
    }

    /// <summary>
    /// Gets the game state
    /// </summary>
    /// <returns>The game state</returns>
    [HttpGet("{gameId}")]
    public async Task<IActionResult> GetGameState(Guid gameId)
    {
        var result = await _gameManager.GetGameStateAsync(gameId);

        return result.Match<IActionResult>(
            success => Ok(success),
            error => ToActionResult(error)
        );
    }
}