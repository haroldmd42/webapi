using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace webapi.Models;

public class AddHistory
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public string? SprintName { get; set; } = string.Empty;
    [Required]
    public Guid EstimationId { get; set; } = Guid.NewGuid();
    public string AccessCode { get; set; } = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
    public List<HistoryItem> Items { get; set; } = new();
}

public class HistoryItem
{
    [Required]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime Date { get; set; } = DateTime.UtcNow;

}
