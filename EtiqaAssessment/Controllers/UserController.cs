using EtiqaAssessment.Data;
using EtiqaAssessment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EtiqaAssessment.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApiDBContext _dbContext;

        public UserController(ApiDBContext apiDBContext)
        {
            _dbContext = apiDBContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_dbContext.Users);
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {

            // Error handling - user return null value
            if (user == null)
            {
                return BadRequest();
            }

            //Error handling - username already exist
            var userExist = _dbContext.Users.Find(user.Username);

            if (userExist != null)
            {
                return BadRequest("User exist");
            }

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return Ok(user);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string username)
        {
            // Error handling - check DB if username exist
            var user = await _dbContext.Users.FindAsync(username);
            if (user == null)
            {
                return NotFound("Username not found");
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(User user)
        {

            // Error handling - check DB if username exist
            var currentUser = _dbContext.Users.Find(user.Username);
            if (currentUser == null)
            {
                return NotFound("Username not found");
            }

            // Repopulate user model properties
            currentUser.Username = user.Username;
            currentUser.PhoneNumber = user.PhoneNumber;
            currentUser.Mail = user.Mail;
            currentUser.Skillset = user.Skillset;
            currentUser.Hobby  = user.Hobby;

            _dbContext.Users.Update(currentUser);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        //public bool UsernameExist(string username)
        //{
        //    return (_dbContext.Users?.Any(e => e.Username == username)).GetValueOrDefault();
        //}
    }
}
