using System.ComponentModel.DataAnnotations;

namespace HigherOrLower.API.DTOs.Request;

public class StartGameRequest
{
    [Range(2, 12, ErrorMessage = "Only a value between 2 and 12 players is allowed")]
    public int NumberOfPlayers { get; set; }
}