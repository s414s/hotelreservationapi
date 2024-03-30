using Application.DTOs;

namespace Application.Contracts;

public interface IRoomService
{
    Task<IEnumerable<RoomDTO>> GetAvailableRoomsOnDate(DateOnly from, DateOnly until, long? hotelId);
    Task<bool> CreateRoom(RoomDTO newRoom, long hotelId);
    Task<bool> DeleteRoom(long roomId);
}
