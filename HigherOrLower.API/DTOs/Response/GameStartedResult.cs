namespace HigherOrLower.API.DTOs.Response;

public class GameStartedResult
{
    public Guid GameId { get; set; }
    
    public string? DrawnCardName { get; set; }
    
    public List<PlayerResult>? Players { get; set; }
}