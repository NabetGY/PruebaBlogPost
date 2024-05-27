using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Prueba;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext( DbContextOptions options ): base(options)
    {
    }

    public DbSet<BlogPost> BlogPosts { get; set; }

    public DbSet<PostComment> PostComments { get; set; }

}
