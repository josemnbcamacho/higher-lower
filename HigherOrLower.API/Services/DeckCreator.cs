using HigherOrLower.API.Models.Entities;

namespace HigherOrLower.API.Services;

public class DeckCreator : IDeckCreator
{
    public Deck CreateShuffledDeck()
    {
        var cardsList = CreateCardsList();
        var shuffledCards = ShuffleCards(cardsList);
        return new Deck(shuffledCards);
    }
    
    // create a list of cards
    private static List<Card> CreateCardsList()
    {
        var suits = Enum.GetValues(typeof(Suit)).Cast<Suit>();
        var ranks = Enum.GetValues(typeof(Rank)).Cast<Rank>();

        var cards = new List<Card>();
        foreach (var suit in suits)
        {
            foreach (var rank in ranks)
            {
                var card = new Card {Suit = suit, Rank = rank};
                cards.Add(card);
            }
        }

        return cards;
    }
    
    // shuffle a list of cards with Fisher-Yates algorithm
    private static List<Card> ShuffleCards(List<Card> cards)
    {
        var random = new Random();
        
        for (var i = cards.Count - 1; i > 0; i--)
        {
            var j = random.Next(i + 1);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }

        return cards;
    }
}