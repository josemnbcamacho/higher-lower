namespace HigherOrLower.API.Models.Entities.Mappings;

public static class RankMapping
{
    public static readonly Dictionary<Rank, string> CardRankNames = new()
    {
        {Rank.Ace, "Ace"},
        {Rank.Two, "Two"},
        {Rank.Three, "Three"},
        {Rank.Four, "Four"},
        {Rank.Five, "Five"},
        {Rank.Six, "Six"},
        {Rank.Seven, "Seven"},
        {Rank.Eight, "Eight"},
        {Rank.Nine, "Nine"},
        {Rank.Ten, "Ten"},
        {Rank.Jack, "Jack"},
        {Rank.Queen, "Queen"},
        {Rank.King, "King"}
    };
    
    public static readonly Dictionary<string, Rank> CardRankValues = 
        CardRankNames.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
}