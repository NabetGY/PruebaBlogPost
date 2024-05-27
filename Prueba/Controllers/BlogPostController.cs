using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Prueba.Controllers;

[ApiController]
[Route("api/blogpost")]
public class BlogPostController: ControllerBase
{

    private readonly ApplicationDbContext _context;

    public BlogPostController(ApplicationDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<BlogPost>>> Get( string? title, int page = 1, int pageSize = 3 )
    {
        
        try
        {
            List<BlogPost> blogPosts;

            if( title is not null )
            {
                var query = title.ToLower();

                blogPosts = await _context.BlogPosts
                    .Where( post => post.Title.ToLower().Contains(query) )
                    .Skip( (page -1) * pageSize )
                    .Take( pageSize )
                    .ToListAsync();

                return Ok(blogPosts);
            } 

            blogPosts = await _context.BlogPosts
                .Skip( (page -1) * pageSize )
                .Take( pageSize )
                .ToListAsync();

            return Ok(blogPosts);
            
        }
        catch 
        {
            return BadRequest();
        }
    }
}
