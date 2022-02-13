namespace HigherOrLower.API.DTOs.Response;

public class TurnResult
{
    public bool CorrectGuess { get; set; }
    
    public string? DrawnCardName { get; set; }
    
    public bool GameEnded { get; set; }
}