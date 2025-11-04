namespace HigherOrLower.API.Errors;

public record GameEndedError(string Message = "Game has already ended");
