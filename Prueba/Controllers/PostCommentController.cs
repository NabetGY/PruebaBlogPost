using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Prueba.Controllers;

[ApiController]
[Route("PostComment")]
public class PostCommentController: ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PostCommentController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostComment>>> Get( string blogPostId )
    {
        try
        {
            var postComments = await _context.PostComments
                .Where( postComment => postComment.BlogPostId.ToString() == blogPostId )
                .ToListAsync();

            if(postComments is null)
            {
                return NotFound();
            }

            return Ok(postComments);
                
        }
        catch 
        {
            return BadRequest();
        }
    }


    [HttpPost]
    public async Task<ActionResult<PostComment>> Post( string blogPostId, [FromBody] PostCommentDTO postCommentDTO)
    {

        try {
            var blogpost = await _context.BlogPosts
            .FirstOrDefaultAsync( blogpost => blogpost.Id.ToString() == blogPostId);

            if( blogpost is null ){
                return NotFound();
            }

            var postComment = new PostComment
            {
                Id = Guid.NewGuid(),
                BlogPostId = blogpost.Id,
                UserFullName = postCommentDTO.UserFullName,
                Comment = postCommentDTO.Comment
            };

            _context.Add(postComment);

            await _context.SaveChangesAsync();

            return Ok(postComment);
        }
        catch 
        {
            return BadRequest();
        }
    }
}
