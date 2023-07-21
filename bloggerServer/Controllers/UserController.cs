using bloggerServer.Data;
using bloggerServer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace bloggerServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ActionName("GetAllUserData")]
        public async Task<IEnumerable<User>> GetAllUserData()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        [ActionName("GetaUserData")]
        public async Task<ActionResult<User>> GetaUserData(int id)
        {
            try
            {
                var findId = await _context.Users.FindAsync(id);
                if(findId == null)
                {
                    return BadRequest();
                }

                return findId;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetUserWithPara")]
        public async Task<ActionResult<User>> GetUserWithPara([FromQuery]string uname)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserUserName == uname);
            if(user == null)
            {
                return NotFound();
            }

            return user;
        }


        [HttpPost]
        [ActionName("CheckPassword")]
        public async Task<ActionResult<User>> CheckPassword(User user)
        {
            var hashedPassword = HashPassword(user.UserPassword);
            var loginUser = await _context.Users.FirstOrDefaultAsync(x => x.UserPassword == hashedPassword);
            if (loginUser == null)
            {
                return NotFound(false);
            }
            else if(loginUser.UserUserName != user.UserUserName)
            {
                return NotFound(false);
            }

            return loginUser;
        }

        [HttpPost]
        [ActionName("AddImage")]
        public IActionResult AddImage([FromForm] UploadImage upload)
        {
            if (upload != null && upload.UserProfilePicture != null && upload.UserProfilePicture.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.UserProfilePicture.FileName);
                var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/userprofilepics");
                var filePath = Path.Combine("wwwroot/userprofilepics", fileName);

                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    upload.UserProfilePicture.CopyTo(fileStream);
                }

                var imageUrl = $"/userprofilepics/{fileName}";
                var imagePathName = fileName;

                return Ok(new { imageUrl, imagePathName });
            }
            else
            {
                return BadRequest("No file");
            }
        }

        [HttpPut]
        [ActionName("SaveUserProfilePicture")]
        public async Task<ActionResult<User>> SaveUserProfilePicture(User user)
        {
            var newUser = new User();
            newUser.UserProfilePicture = user.UserProfilePicture;
            newUser.UserFirstName = user.UserFirstName;
            newUser.UserLastName = user.UserLastName;
            newUser.UserEmail = user.UserEmail;
            newUser.UserGender = user.UserGender;
            newUser.UserMobileNumber = user.UserMobileNumber;
            newUser.UserUserName = user.UserUserName;
            newUser.UserPassword = user.UserPassword;
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return Ok(newUser);
        }


        [HttpPost]
        [ActionName("AddUserData")]
        public async Task<ActionResult<User>> AddUserData(User user)
        {
            try
            {
                var hashedPW = HashPassword(user.UserPassword);
                var newUser = new User();
                newUser.UserFirstName = user.UserFirstName;
                newUser.UserLastName = user.UserLastName;
                newUser.UserEmail = user.UserEmail;
                newUser.UserGender = user.UserGender;
                newUser.UserMobileNumber = user.UserMobileNumber;
                newUser.UserUserName = user.UserUserName;
                newUser.UserPassword = hashedPW;

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
                return Ok("Added");
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpPut("{id}")]
        [ActionName("UpdateUserData")]
        public async Task<IActionResult> UpdateUserData(User user, int id)
        {
            try
            {
                if(id != user.UserId)
                {
                    return BadRequest();
                }

                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok("Updated");
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        [ActionName("DeleteUserData")]
        public async Task<ActionResult<User>> DeleteUserData(int id)
        {
            try
            {
                var findId = await _context.Users.FindAsync(id);
                if(findId == null)
                {
                    return NotFound();
                }

                _context.Users.Remove(findId);
                await _context.SaveChangesAsync();
                return Ok("Deleted");

            }
            catch(Exception ex)
            {
                throw;
            }
        }





        public string HashPassword(string password)
        {
            SHA256 hash = SHA256.Create();
            var passwordBytes = Encoding.Default.GetBytes(password);
            var hashedPassword = hash.ComputeHash(passwordBytes);

            return Convert.ToHexString(hashedPassword);
             
        }
    }
}
