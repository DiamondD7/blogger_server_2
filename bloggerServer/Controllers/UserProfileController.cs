using bloggerServer.Data;
using bloggerServer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace bloggerServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UserProfileController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ActionName("GetProfileData")]
        public async Task<IEnumerable<UserProfile>> GetProfileData()
        {
            return await _context.UserProfiles.ToListAsync();
        }

        [HttpGet("{id}")]
        [ActionName("GetAProfileData")]
        public async Task<ActionResult<UserProfile>> GetAProfileData(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            /*var findUserId = await _context.UserProfiles.FindAsync(userId);*/
            var findUserId = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserId == id);

            if (findUserId == null)
            {
                return NotFound();
            }

            return findUserId;
        }

        [HttpPost]
        [ActionName("AddProfileContents")]
        public async Task<ActionResult<UserProfile>> AddProfileContents(UserProfile userProfile)
        {
            var newUserProfile = new UserProfile();
            newUserProfile.UserId = userProfile.UserId;
            newUserProfile.Description = userProfile.Description;
            newUserProfile.PinnedOne = userProfile.PinnedOne;
            newUserProfile.PinnedTwo = userProfile.PinnedTwo;
            newUserProfile.PinnedThree = userProfile.PinnedThree;

            _context.UserProfiles.Add(newUserProfile);
            await _context.SaveChangesAsync();
            return Ok("Added");
        }

        [HttpPut("{id}")]
        [ActionName("EditProfileContents")]
        public async Task<ActionResult<UserProfile>> EditProfileContents(int id,UserProfile userProfile)
        {
            try
            {

                var findUser = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserId == id);
                if (findUser == null)
                {
                    return NotFound();
                }

                findUser.Description = userProfile.Description;
                findUser.PinnedOne = userProfile.PinnedOne;
                findUser.PinnedTwo = userProfile.PinnedTwo;
                findUser.PinnedThree = userProfile.PinnedThree;


                _context.Entry(findUser).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok("Updated");
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        [ActionName("DeleteProfileContent")]
        public async Task<ActionResult<UserProfile>> DeleteProfileContent(int id)
        {
            var findId = await _context.UserProfiles.FindAsync(id);
            if(findId == null)
            {
                return NotFound();
            }
            _context.UserProfiles.Remove(findId);
            await _context.SaveChangesAsync();
            return Ok("Deleted");
        }
    }
}
