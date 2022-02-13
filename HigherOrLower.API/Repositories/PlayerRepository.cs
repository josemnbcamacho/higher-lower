using HigherOrLower.API.Models.Entities;
using HigherOrLower.API.Repository.Base;
using HigherOrLower.API.Repository.Interfaces;

namespace HigherOrLower.API.Repository;

public class PlayerRepository : GenericRepository<Player>, IPlayerRepository
{
    public PlayerRepository(HigherLowerDbContext context) : base(context)
    {
    }
}