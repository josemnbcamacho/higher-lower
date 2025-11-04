using HigherOrLower.API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace HigherOrLower.API.Controllers.Base;

public class BaseApiController : ControllerBase
{
    /// <summary>
    /// Converts an error to an appropriate IActionResult
    /// </summary>
    protected IActionResult ToActionResult(object error)
    {
        return error switch
        {
            GameNotFoundError e => NotFound(new { error = e.Message }),
            PlayerNotFoundError e => NotFound(new { error = e.Message }),
            InvalidNumberOfPlayersError e => BadRequest(new { error = e.Message }),
            GameEndedError e => BadRequest(new { error = e.Message }),
            DeckNotFoundError e => Problem(e.Message, statusCode: 500),
            NoCardDrawnError e => Problem(e.Message, statusCode: 500),
            _ => Problem("An unexpected error occurred", statusCode: 500)
        };
    }
}