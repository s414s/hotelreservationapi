﻿using Application.DTOs;
using Domain.Enum;

namespace Application.Contracts;

public interface IHotelService
{
    IEnumerable<HotelDTO> GetFilteredHotels(Cities? city);
}
