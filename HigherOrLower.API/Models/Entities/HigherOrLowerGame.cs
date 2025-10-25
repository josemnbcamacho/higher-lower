namespace HigherOrLower.API.Models.Entities;

public class HigherOrLowerGame : Game
{
    public Card LastCard { get; set; } = null!;
}