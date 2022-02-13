namespace HigherOrLower.API.Models.Entities.Mappings;

public class SuitMapping
{
    public static readonly Dictionary<Suit, string> CardSuitsNames = new()
    {
        {Suit.Clubs, "Clubs"},
        {Suit.Diamonds, "Diamonds"},
        {Suit.Hearts, "Hearts"},
        {Suit.Spades, "Spades"}
    };
    
    public static readonly Dictionary<string, Suit> CardSuitValues = 
        CardSuitsNames.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
}