using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HigherOrLower.API.Models.Entities;

public class Deck
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public List<Card> Cards { get; set; }

    public Deck(List<Card> cards)
    {
        Cards = cards;
    }

    // draws a card from the deck
    public Card DrawCard()
    {
        if (Cards.Count == 0)
        {
            throw new Exception("No cards left in deck");
        }
        
        var card = Cards[0];
        Cards.RemoveAt(0);
        
        return card;
    }
}