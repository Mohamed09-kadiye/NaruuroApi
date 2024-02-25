using System;
namespace NaruuroApi.Model.Interface
{
    public interface IRoomRepository
    {
        List<Room> GetAllRooms();
        Room GetRoomById(int roomId);
        void InsertRoom(Room room);
        void UpdateRoom(Room room);
        void DeleteRoom(int roomId);
    }

}

