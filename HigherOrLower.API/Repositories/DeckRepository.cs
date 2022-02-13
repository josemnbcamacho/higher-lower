using HigherOrLower.API.Models.Entities;
using HigherOrLower.API.Repository.Base;
using HigherOrLower.API.Repository.Interfaces;

namespace HigherOrLower.API.Repository;

public class DeckRepository : GenericRepository<Deck>, IDeckRepository
{
    public DeckRepository(HigherLowerDbContext dbContext) : base(dbContext)
    {
    }
}