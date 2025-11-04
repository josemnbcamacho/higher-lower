namespace HigherOrLower.API.Errors;

public record InvalidNumberOfPlayersError(string Message = "Number of players must be between 2 and 12");
