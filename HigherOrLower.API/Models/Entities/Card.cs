namespace HigherOrLower.API.Models.Entities;

public record Card
{
    public Rank Rank { get; init; }
    
    public Suit Suit { get; init; }

    public int Value => (int)Rank;
    
    public override string ToString() => $"{Rank} of {Suit}";
}