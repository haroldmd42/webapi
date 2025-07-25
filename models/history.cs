using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace webapi.Models;

public class History
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public string? SprintName { get; set; }
    [Required]
    public Guid EstimationId { get; set; }
    public string AccessCode { get; set; } = string.Empty;
}