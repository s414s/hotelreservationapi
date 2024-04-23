﻿using Domain.Entities;
using Domain.Enum;

namespace Application.DTOs;

public class RoomDTO
{
    public long Id { get; set; }
    public int Storey { get; set; }
    public RoomTypes Type { get; set; }
    public int Capacity { get; set; }

    public static RoomDTO MapFromDomainEntity(Room room)
    {
        return new RoomDTO
        {
            Id = room.Id,
            Storey = room.Storey,
            Type = room.Type,
            Capacity = room.GetCapacity(),
        };
    }

    public Room MapToDomainEntity() => new(Storey, Type);
}
