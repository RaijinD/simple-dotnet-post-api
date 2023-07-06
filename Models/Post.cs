namespace simple_dotnet_post_api.Models;
public class Post : CoreEntity
{
    public string Title { get; set; } = null!;

    public int OwnerId { get; set; } 

    public string content { get; set; } = null!;

}
