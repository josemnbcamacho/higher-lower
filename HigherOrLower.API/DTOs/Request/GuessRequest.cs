using System.ComponentModel.DataAnnotations;
using HigherOrLower.API.Models.Entities;

namespace HigherOrLower.API.DTOs.Request;

public class GuessRequest
{
    [Required]
    public Guid GameId { get; set; }
    
    [Required]
    public Guid PlayerId { get; set; }
    
    [Required]
    [EnumDataType(typeof(Guess))]
    public Guess Guess { get; set; }
}