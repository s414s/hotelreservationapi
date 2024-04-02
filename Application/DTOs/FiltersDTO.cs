﻿using Domain.Enum;

namespace Application.DTOs;

public class FiltersDTO
{
    public DateOnly? From { get; set; }
    public DateOnly? Until { get; set; }
    public Cities? City { get; set; }
    public long? HotelId { get; set; }
    public long? ClientId { get; set; }
    public string? GuestDNI { get; set; }
    public bool? IsAvailable { get; set; }
}