using HigherOrLower.API.Models.Entities;

namespace HigherOrLower.API.Repository.Interfaces;

public interface IPlayerRepository
{
    Task<IEnumerable<Player>> GetAllAsync();
    Task<Player?> GetByIdAsync(Guid id);
    Task AddAsync(Player entity);
    void Update(Player entity);
    void Delete(Guid id);
    Task SaveChangesAsync();
    Task AddRangeAsync(IEnumerable<Player> entities);
    void UpdateRange(IEnumerable<Player> entities);
}