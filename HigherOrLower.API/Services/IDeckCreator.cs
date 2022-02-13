using HigherOrLower.API.Models.Entities;

namespace HigherOrLower.API.Services;

public interface IDeckCreator
{
    public Deck CreateShuffledDeck();
}