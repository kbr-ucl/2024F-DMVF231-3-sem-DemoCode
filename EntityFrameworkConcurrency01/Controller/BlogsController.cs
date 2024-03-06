using System.Data;
using EntityFrameworkConcurrency01.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkConcurrency01.Controller;

[Route("api/[controller]")]
[ApiController]
public class BlogsController : ControllerBase
{
    private readonly BloggingContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public BlogsController(BloggingContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    // GET: api/Blogs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Blog>>> GetBlogs()
    {
        return await _context.Blogs.ToListAsync();
    }

    // GET: api/Blogs/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Blog>> GetBlog(int id)
    {
        var blog = await _context.Blogs.FindAsync(id);

        if (blog == null) return NotFound();

        return blog;
    }

    // PUT: api/Blogs/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBlog(int id, Blog updatedBlog)
    {
        if (id != updatedBlog.Id) return BadRequest();

        try
        {
            _unitOfWork.BeginTransaction();
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null) return NotFound();

            blog.Url = updatedBlog.Url;
            _context.Entry(blog).OriginalValues["RowVersion"] = updatedBlog.RowVersion;
            await _context.SaveChangesAsync();
            _unitOfWork.Commit();



        }
        catch (DbUpdateConcurrencyException e)
        {
            _unitOfWork.Rollback();
            if (!BlogExists(id))
                return NotFound();
            return BadRequest(e.Message);
        }
        catch (Exception ex)
        {
            _unitOfWork.Rollback();
            return BadRequest(ex.Message);
        }

        return NoContent();
    }

    // POST: api/Blogs
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Blog>> PostBlog(Blog blog)
    {
        _context.Blogs.Add(blog);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetBlog", new { id = blog.Id }, blog);
    }

    // DELETE: api/Blogs/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBlog(int id)
    {
        var blog = await _context.Blogs.FindAsync(id);
        if (blog == null) return NotFound();

        _context.Blogs.Remove(blog);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool BlogExists(int id)
    {
        return _context.Blogs.Any(e => e.Id == id);
    }
}