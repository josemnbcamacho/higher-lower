namespace HigherOrLower.API.Exceptions;

public class PlayerNotFoundException : Exception
{
    public PlayerNotFoundException(string playerNotFound) : base(playerNotFound)
    {
    }
}