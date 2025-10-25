using System.ComponentModel.DataAnnotations;

namespace HigherOrLower.API.Models.Entities;

public class Player
{
    [Key]
    public Guid Id { get; init; }
    
    public string Name { get; set; } = string.Empty;
    
    public int Score { get; set; }
}