using System.ComponentModel.DataAnnotations;

namespace HigherOrLower.API.Models.Entities;

public class Game
{
    [Key]
    public Guid Id { get; init; }
    
    public Deck Deck { get; init; }
    
    public List<Player> Players { get; init; }

    public bool HasGameEnded => !Deck.Cards.Any();
}