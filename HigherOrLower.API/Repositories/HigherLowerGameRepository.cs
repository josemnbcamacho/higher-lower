using HigherOrLower.API.Models.Entities;
using HigherOrLower.API.Repository.Base;
using HigherOrLower.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HigherOrLower.API.Repository;

public class HigherLowerGameRepository : GenericRepository<HigherOrLowerGame>, IHigherLowerGameRepository
{
    public HigherLowerGameRepository(HigherLowerDbContext context) : base(context)
    {
    }

    public override Task<HigherOrLowerGame?> GetByIdAsync(Guid id)
    {
        var higherOrLowerGame = DbSet.Include(g => g.Players)
            .Include(g => g.Deck)
            .FirstOrDefault(g => g.Id == id);
        
        return Task.FromResult(higherOrLowerGame);
    }

    // public void Add(HigherOrLowerGame entity)
    // {
    //     _context.HigherOrLowerGames.Add(entity);
    // }
    //
    // public HigherOrLowerGame? GetById(Guid id)
    // {
    //     return _context.Set<HigherOrLowerGame>()
    //         .Include(g => g.Players)
    //         .Include(g => g.Deck)
    //         .FirstOrDefault(g => g.Id == id);
    // }
    //
    // public void UpdateGame(HigherOrLowerGame game)
    // {
    //     _context.Entry(game).State = EntityState.Modified;
    // }
    //
    // // update a deck
    // public void UpdateDeck(Deck deck)
    // {
    //     _context.Entry(deck).State = EntityState.Modified;
    // }
    //
    // // public void Update(HigherOrLowerGame item)
    // // {
    // //     // var entity = _context.Set<HigherOrLowerGame>()
    // //     //     .Include(g => g.Players)
    // //     //     .Include(g => g.Deck)
    // //     //     .FirstOrDefault(g => g.Id == item.Id);
    // //     
    // //     _context.Set<HigherOrLowerGame>().Attach(item);
    // //     var entry = _context.Entry(item);
    // //     entry.State = EntityState.Modified;
    // //     
    // //     // if (entity == null)
    // //     // {
    // //     //     return;
    // //     // }
    // //
    // //     // _context.Entry(entity).CurrentValues.SetValues(item);
    // // }
    // //
    // // public void Update(Deck deck)
    // // {
    // //     // update the deck
    // //     _context.Set<Deck>().Attach(deck);
    // // }
    //
    // public void Save()
    // {
    //     _context.SaveChanges();
    // }
}