namespace HigherOrLower.API.Exceptions;

public class GameNotFoundException : Exception
{
    public GameNotFoundException(string gameNotFound)
        : base(gameNotFound)
    {
    }
}