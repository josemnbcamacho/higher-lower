namespace HigherOrLower.API.DTOs.Response;

public class GameStateResult
{
    public List<PlayerScoreResult>? PlayerScores { get; set; }
    
    public bool GameEnded { get; set; }
}