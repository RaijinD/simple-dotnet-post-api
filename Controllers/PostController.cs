using simple_dotnet_post_api.Context;
using simple_dotnet_post_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("post")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly DataContext _context;

    public PostController(DataContext context)
    {
        _context = context;
    }

    [Route("get")]
    [HttpGet]
    public async Task<ActionResult<Post>> GetPost(int id)
    {
        var post = await _context.Posts.Where(u => u.Id == id).FirstAsync();
        return post;
    }
    [Route("get-all")]
    [HttpGet]
    public async Task<ActionResult<Post[]>> GetAllPost(int page = 0)
    {
        var posts = _context.Posts.Skip(page).Take(10);
        return await posts.ToArrayAsync();
    }

    [Route("create")]
    [HttpPost]
    public async Task<ActionResult<Post>> CreatePost([FromBody] Post post)
    {
        _context.Add(post);
        await _context.SaveChangesAsync();
        return await GetPost(post.Id);
    }

    [Route("edit")]
    [HttpPut]
    public async Task<ActionResult<Post>> EditPost([FromBody] int userId, int postId, string content)
    {
        var post = await _context.Posts.Where(u => u.Id == postId && u.OwnerId == userId).FirstAsync();
        if (post != null)
        {
            post.content = content;
            post.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return await GetPost(postId);
        }
        return BadRequest("Error: Post not found");

    }
    [Route("delete")]
    [HttpPost]
    public ActionResult DeletePost([FromBody] int userId, int postId)
    {
        var post = _context.Posts.Where(u => u.Id == postId && u.OwnerId == userId).Count();
        if (post == 1)
        {
            var temp = new Post() { Id = postId };
            _context.Posts.Attach(temp);
            _context.Remove(temp);
            return Ok();
        }
        return BadRequest("Error: Post not found");
    }
}
