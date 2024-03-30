using Application.DTOs;

namespace Application.Contracts;

public interface IRoomService
{
    IEnumerable<RoomDTO> GetFilteredRooms(DateOnly from, DateOnly until, long? hotelId, bool? isAvailable);
    Task<bool> CreateRoom(RoomDTO newRoom, long hotelId);
    Task<bool> DeleteRoom(long roomId);
}
