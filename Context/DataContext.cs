using Microsoft.EntityFrameworkCore;
using simple_dotnet_post_api.Models;

namespace simple_dotnet_post_api.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<Post> Posts { get; set; }
}
