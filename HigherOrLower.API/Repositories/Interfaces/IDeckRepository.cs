using HigherOrLower.API.Models.Entities;

namespace HigherOrLower.API.Repository.Interfaces;

public interface IDeckRepository
{
    Task<IEnumerable<Deck>> GetAllAsync();
    Task<Deck?> GetByIdAsync(Guid id);
    void Update(Deck entity);
    void Delete(Guid id);
    Task SaveChangesAsync();
}