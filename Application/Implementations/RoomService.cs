using Application.Contracts;
using Application.DTOs;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Implementations;

public class RoomService : IRoomService
{
    private readonly IRepository<Hotel> _hotelsRepo;
    private readonly IRepository<Room> _roomsRepo;

    public RoomService(IRepository<Hotel> hotelsRepo, IRepository<Room> roomsRepo)
    {
        _hotelsRepo = hotelsRepo;
        _roomsRepo = roomsRepo;
    }

    public async Task<bool> CreateRoom(RoomDTO newRoom, long hotelId)
    {
        var hotel = await _hotelsRepo.GetByID(hotelId)
            ?? throw new ApplicationException("the hotel does not exist");

        var room = newRoom.MapToDomainEntity();
        room.HotelId = hotel.Id;

        await _roomsRepo.Add(room);
        return await _roomsRepo.SaveChanges();
    }

    public async Task<bool> DeleteRoom(long roomId)
    {
        var room = await _roomsRepo.GetByID(roomId)
            ?? throw new ApplicationException("the room does not exist");

        await _roomsRepo.Delete(room.Id);
        return await _roomsRepo.SaveChanges();
    }

    //public async Task<IEnumerable<RoomDTO>> GetFilteredRooms(DateOnly? from, DateOnly? until, long? hotelId, bool? isAvailable)
    public async Task<IEnumerable<RoomDTO>> GetFilteredRooms(FiltersDTO filters)
    {
        var rooms = await _roomsRepo.Query
            .Include(x => x.Bookings)
            .Where(x => filters.HotelId == null || x.HotelId == filters.HotelId)
            .ToListAsync();

        return rooms
            .Where(x => (filters.IsAvailable == null || filters.From == null || filters.Until == null)
                || x.IsAvailableBetweenDates((DateOnly)filters.From, (DateOnly)filters.Until) == filters.IsAvailable)
            .Select(x => RoomDTO.MapFromDomainEntity(x));
    }

    public async Task<bool> UpdateRoom(RoomDTO updatedRoom)
    {
        var room = await _roomsRepo.GetByID(updatedRoom.Id)
            ?? throw new ApplicationException("the room does not exist");

        room.Storey = updatedRoom.Storey;
        room.Type = updatedRoom.Type;

        return await _roomsRepo.Update(room);
    }
}
