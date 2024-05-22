using Application.DTOs;

namespace Application.Contracts;

public interface IRoomService
{
    Task<IEnumerable<RoomDTO>> GetFilteredRooms(FiltersDTO filters);
    Task<RoomDTO> GetById(long roomId);
    Task<bool> CreateRoom(NewRoomDTO newRoom, long hotelId);
    Task<bool> UpdateRoom(long roomId, NewRoomDTO updatedRoom);
    Task<bool> DeleteRoom(long roomId);
}
