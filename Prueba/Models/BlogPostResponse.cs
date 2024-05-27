namespace Prueba.Models;

public class BlogPostResponse
{
    public List<BlogPost> BlogPosts { get; set; } = [];

    public int Pages { get; set; }

    public int PageSize { get; set; }

    public int Page { get; set; }
}
