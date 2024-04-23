using Application.DTOs;

namespace Application.Contracts;

public interface IRoomService
{
    Task<IEnumerable<RoomDTO>> GetFilteredRooms(FiltersDTO filters);
    Task<RoomDTO> GetById(long roomId);
    Task<bool> CreateRoom(RoomDTO newRoom, long hotelId);
    Task<bool> UpdateRoom(RoomDTO updatedRoom);
    Task<bool> DeleteRoom(long roomId);
}
