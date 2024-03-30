﻿using Application.Contracts;
using Application.DTOs;
using Domain.Contracts;
using Domain.Entities;

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

    public IEnumerable<RoomDTO> GetFilteredRooms(DateOnly from, DateOnly until, long? hotelId, bool? isAvailable)
    {
        return _roomsRepo.Query
            .Where(x => hotelId == null || x.Id == hotelId)
            .Where(x => isAvailable == null || x.IsAvailableBetweenDates(from, until) == isAvailable)
            .Select(x => RoomDTO.MapFromDomainEntity(x))
            ;
    }
}
