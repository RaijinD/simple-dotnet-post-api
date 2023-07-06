using System.ComponentModel.DataAnnotations;

namespace simple_dotnet_post_api.Models;

public class CoreEntity
{
    [Key]
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
}
