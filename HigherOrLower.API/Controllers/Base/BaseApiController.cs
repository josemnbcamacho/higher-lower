using Microsoft.AspNetCore.Mvc;
using Nito;

namespace HigherOrLower.API.Controllers.Base;

public class BaseApiController : ControllerBase
{
    /// <summary>
    /// Matches a Try Monad and creates a IActionResult 
    /// </summary>
    /// <param name="t"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns>The corresponding IActionResult</returns>
    protected IActionResult MatchTryResult<T>(Try<T> t)
    {
        var actionResult = t.Match<IActionResult>(
            exception => Problem(exception.Message),
            result => Ok(result));
        
        return actionResult;
    }
}