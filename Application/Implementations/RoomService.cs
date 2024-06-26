﻿using Application.Contracts;
using Application.DTOs;
using Domain.Contracts;
using Domain.DomainServices;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Implementations;

public class RoomService(IRepository<Hotel> hotelsRepo, IRepository<Room> roomsRepo) : IRoomService
{
    private readonly IRepository<Hotel> _hotelsRepo = hotelsRepo;
    private readonly IRepository<Room> _roomsRepo = roomsRepo;

    public async Task<bool> CreateRoom(NewRoomDTO newRoom, long hotelId)
    {
        var hotel = await _hotelsRepo.GetByID(hotelId)
            ?? throw new ApplicationException("the hotel does not exist");

        var room = newRoom.MapToDomainEntity();
        room.HotelId = hotel.Id;

        return await _roomsRepo.Add(room);
    }

    public async Task<bool> DeleteRoom(long roomId)
    {
        var room = await _roomsRepo.GetByID(roomId)
            ?? throw new ApplicationException("the room does not exist");

        return await _roomsRepo.Delete(room.Id);
    }

    public async Task<RoomDTO> GetById(long roomId)
    {
        var room = await _roomsRepo.GetByID(roomId)
            ?? throw new ApplicationException("the room does not exist");

        return RoomDTO.MapFromDomainEntity(room);
    }

    public async Task<IEnumerable<RoomDTO>> GetFilteredRooms(FiltersDTO filters)
    {
        var rooms = await _roomsRepo.Query
            .Include(r => r.Bookings)
            .Where(r => filters.HotelId == null || r.HotelId == filters.HotelId)
            .ToListAsync();

        if (filters.From is DateOnly from && filters.Until is DateOnly until)
        {
            rooms = rooms.Where(r => AvailabilityService.IsRoomAvailableBetweenDates(r, from, until)).ToList();
        }

        return rooms.Select(x => RoomDTO.MapFromDomainEntity(x));
    }

    public async Task<bool> UpdateRoom(long roomId, NewRoomDTO updatedRoom)
    {
        var room = await _roomsRepo.GetByID(roomId)
            ?? throw new ApplicationException("the room does not exist");

        room.Storey = updatedRoom.Storey;
        room.Type = updatedRoom.Type;
        room.Price = updatedRoom.Price;

        return await _roomsRepo.Update(room);
    }
}
