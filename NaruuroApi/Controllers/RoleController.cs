using NaruuroApi.Model;
using NaruuroApi.Model.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Group_A_E_Commerce_Website.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRole _roleRepo;

        public RoleController(IRole roleRepo)
        {
            _roleRepo = roleRepo;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var role = _roleRepo.GetById(id);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var roles = _roleRepo.GetAllRole();

            return Ok(roles);
        }

        [HttpPost]
        public IActionResult AddRole(Role role)
        {
            _roleRepo.AddRole(role);

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRole(int id, Role role)
        {
            var existingRole = _roleRepo.GetById(id);

            if (existingRole == null)
            {
                return NotFound();
            }

            existingRole.Title = role.Title;

            _roleRepo.UpdateRole(existingRole);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRole(int id)
        {
            var role = _roleRepo.GetById(id);

            if (role == null)
            {
                return NotFound();
            }

            _roleRepo.DeleteRole(id);

            return Ok();
        }
    }
}
