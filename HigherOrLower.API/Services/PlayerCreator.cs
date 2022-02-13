using HigherOrLower.API.Models.Entities;

namespace HigherOrLower.API.Services;

public class PlayerCreator : IPlayerCreator
{
    public IEnumerable<Player> CreatePlayers(int numberOfPlayers)
    {
        for (var i = 0; i < numberOfPlayers; i++)
        {
            yield return new Player()
            {
                Id = Guid.NewGuid(),
                Name = $"Player {i + 1}",
                Score = 0
            };
        }
    }
}

public interface IPlayerCreator
{
    IEnumerable<Player> CreatePlayers(int numberOfPlayers);
}