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
    public class SecurityQuestionController : ControllerBase
    {
        private readonly AppDbContext _context;
        public SecurityQuestionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ActionName("GetAllData")]
        public async Task<IEnumerable<SecurityQuestion>> GetAllData()
        {
            return await _context.SecurityTable.ToListAsync();
        } 

        [HttpGet("{id}")]
        [ActionName("GetSecurityDetails")]
        public async Task<ActionResult<SecurityQuestion>> GetSecurityDetails(int id)
        {
            var userSecDetails = await _context.SecurityTable.FirstOrDefaultAsync(x => x.UserId == id);
            if(userSecDetails == null)
            {
                return NotFound();
            }
            return userSecDetails;
        }

        [HttpPost("{id}")]
        [ActionName("CheckSecurityAnswers")]
        public async Task<ActionResult<SecurityQuestion>> CheckSecurityAnswers(int id, SecurityQuestion securityQuestion)
        {
            bool isValidated = false;
            var userInputQuestionOne = HashPassword(securityQuestion.SecurityAnswerOne);
            var userInputQuestionTwo = HashPassword(securityQuestion.SecurityAnswerTwo);
            var userInputQuestionThree = HashPassword(securityQuestion.SecurityAnswerThree);

            var findUserDetails = await _context.SecurityTable.FirstOrDefaultAsync(x => x.UserId == id);


            if (findUserDetails.SecurityAnswerOne == userInputQuestionOne && findUserDetails.SecurityAnswerTwo == userInputQuestionTwo && findUserDetails.SecurityAnswerThree == userInputQuestionThree)
            {
                isValidated = true;
            }


            if(isValidated == false)
            {
                return NotFound(false);
            }

            return Ok(true);
            
        }


        [HttpPost]
        [ActionName("AddSecurityData")]
        public async Task<ActionResult<SecurityQuestion>> AddSecurityData(SecurityQuestion securityQuestion)
        {
            try
            {
                var hashedAnswerOne = HashPassword(securityQuestion.SecurityAnswerOne);
                var hashedAnswerTwo = HashPassword(securityQuestion.SecurityAnswerTwo);
                var hashedAnswerThree = HashPassword(securityQuestion.SecurityAnswerThree);
                var secuQues = new SecurityQuestion();
                secuQues.UserId = securityQuestion.UserId;
                secuQues.SecurityQuestionOne = securityQuestion.SecurityQuestionOne;
                secuQues.SecurityQuestionTwo = securityQuestion.SecurityQuestionTwo;
                secuQues.SecurityQuestionThree = securityQuestion.SecurityQuestionThree;
                secuQues.SecurityAnswerOne = hashedAnswerOne;
                secuQues.SecurityAnswerTwo = hashedAnswerTwo;
                secuQues.SecurityAnswerThree = hashedAnswerThree;

                _context.SecurityTable.Add(secuQues);
                await _context.SaveChangesAsync();
                return Ok("Added");

            }
            catch (Exception ex)
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
