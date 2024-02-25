using NaruuroApi.Model;
using NaruuroApi.Model.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace NaruuroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userRepo;

        public UserController(IUser userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        public ActionResult<List<UserM>> GetAllUsers()
        {
            var userList = _userRepo.GetAllUsers();
            return Ok(userList);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] UserM user)
        {
            _userRepo.Add(user);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserM user)
        {
            var existingUser = _userRepo.GetUsersbyid(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Stafid = user.Stafid;
            existingUser.Roleid = user.Roleid;
            existingUser.UserName = user.UserName;
            existingUser.Password = user.Password;
            existingUser.Created_at = user.Created_at;
            existingUser.Updated_at = user.Updated_at;

            _userRepo.UpdateUsers(existingUser);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            _userRepo.DeleteUsers(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<UserM> GetUserById(int id)
        {
            var user = _userRepo.GetUsersbyid(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }



    }
}
