using NaruuroApi.Model.Interface;
using NaruuroApi.Model.Repository;
using NaruuroApi.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace NaruuroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaff _staffRepo;

        public StaffController(IStaff staffRepo)
        {
            _staffRepo = staffRepo;
        }

        [HttpGet]
        public ActionResult<List<StaffM>> GetAllStaff()
        {
            var staffList = _staffRepo.GetAllStaff();
            return Ok(staffList);
        }

        [HttpPost]
        public IActionResult AddStaff([FromBody] StaffM staff)
        {
            _staffRepo.AddStaff(staff);
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateStaff(int id, [FromBody] StaffM staff)
        {
            var existingStaff = _staffRepo.GetStaffById(id);
            if (existingStaff == null)
            {
                return NotFound();
            }

            existingStaff.Name = staff.Name;
            existingStaff.Telephone = staff.Telephone;
            existingStaff.Address = staff.Address;
            existingStaff.Gender = staff.Gender;
            existingStaff.RoleId = staff.RoleId;
            existingStaff.RegisteredDate = staff.RegisteredDate;
            existingStaff.UpdatedAt = staff.UpdatedAt;

            _staffRepo.UpdateStaff(existingStaff);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStaff(int id)
        {
            var existingStaff = _staffRepo.GetStaffById(id);
            if (existingStaff == null)
            {
                return NotFound();
            }

            _staffRepo.DeleteStaff(id);

            return Ok();
        }
        [HttpGet("{id}")]
        public ActionResult<StaffM> GetStaffById(int id)
        {
            var staff = _staffRepo.GetStaffById(id);
            if (staff == null)
            {
                return NotFound();
            }

            return Ok(staff);
        }


        // API endpoints will be implemented here
    }

}
