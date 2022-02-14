using System.Text.Json;
using HigherOrLower.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HigherOrLower.API.Repository;

public class HigherLowerDbContext : DbContext
{
    private const string DfFolderName = "Database";
    private const string DbFileName = "HigherOrLower.db";

    private readonly string _dbPath;
    
    public DbSet<HigherOrLowerGame> HigherOrLowerGames { get; set; }

    public HigherLowerDbContext()
    {
        // TODO: (JMBC)
        // temporary solution to get the path of the db file
        // should use options to configure the db path
        
        var contentRootPath = Directory.GetCurrentDirectory();
        _dbPath = Path.Combine(contentRootPath, DfFolderName, DbFileName);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={_dbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Card>();
        
#pragma warning disable CS8600
#pragma warning disable CS8603
        
        modelBuilder.Entity<Deck>()
            .Property(d => d.Cards)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<Card>>(v, (JsonSerializerOptions)null));
        
        modelBuilder.Entity<HigherOrLowerGame>()
            .Property(g => g.LastCard)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<Card>(v, (JsonSerializerOptions)null));
        
#pragma warning restore CS8600
#pragma warning restore CS8603

        base.OnModelCreating(modelBuilder);
    }
}