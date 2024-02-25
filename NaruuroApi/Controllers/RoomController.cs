using Microsoft.AspNetCore.Mvc;
using NaruuroApi.Model;
using NaruuroApi.Model.Interface;
using NaruuroApi.Model.Repository;
using System;
using System.Collections.Generic;

namespace NaruuroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;

        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Room>> GetAllRooms()
        {
            try
            {
                var rooms = _roomRepository.GetAllRooms();
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{roomId}")]
        public ActionResult<Room> GetRoomById(int roomId)
        {
            try
            {
                var room = _roomRepository.GetRoomById(roomId);
                if (room == null)
                    return NotFound();

                return Ok(room);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult InsertRoom([FromBody] Room room)
        {
            try
            {
                _roomRepository.InsertRoom(room);
                return Ok("Room inserted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateRoom([FromBody] Room room)
        {
            try
            {
                _roomRepository.UpdateRoom(room);
                return Ok("Room updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{roomId}")]
        public IActionResult DeleteRoom(int roomId)
        {
            try
            {
                _roomRepository.DeleteRoom(roomId);
                return Ok("Room deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
