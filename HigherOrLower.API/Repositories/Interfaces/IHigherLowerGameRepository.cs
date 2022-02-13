using HigherOrLower.API.Models.Entities;

namespace HigherOrLower.API.Repository.Interfaces;

public interface IHigherLowerGameRepository
{
    Task<IEnumerable<HigherOrLowerGame>> GetAllAsync();
    Task<HigherOrLowerGame?> GetByIdAsync(Guid id);
    Task AddAsync(HigherOrLowerGame entity);
    void Update(HigherOrLowerGame entity);
    void Delete(Guid id);
    Task SaveChangesAsync();
}