using bloggerServer.Data;
using bloggerServer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bloggerServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HashtagsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public HashtagsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ActionName("ListAllHashtags")]
        public async Task<IEnumerable<Hashtags>> ListAllHashtags() //temporary, please delete soon.
        {
            return await _context.Hashtags.ToListAsync();
        }

        [HttpGet("{id}")]
        [ActionName("PostHashtags")]
        public async Task<ActionResult<List<Hashtags>>> PostHashtags(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var hashTags = await _context.Hashtags.Where(a => a.PostId == id).ToListAsync();

            return hashTags;
        }


        [HttpPost]
        [ActionName("AddHashtags")]
        public async Task<ActionResult<Hashtags>> AddHashtags(Hashtags hashtags)
        {
            _context.Hashtags.Add(hashtags);
            await _context.SaveChangesAsync();
            return Ok("Added");
        }


        [HttpDelete("{id}")]
        [ActionName("DeleteBunch")]
        public async Task<ActionResult<Hashtags>> DeleteBunch(int id)
        {

            try
            {
                var findHashtags = await _context.Hashtags.Where(x => x.PostId == id).ToListAsync();
                if (findHashtags == null || findHashtags.Count == 0)
                {
                    return NotFound();
                }

                _context.Hashtags.RemoveRange(findHashtags);
                await _context.SaveChangesAsync();

                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }
}
