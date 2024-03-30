using Application.DTOs;
using Domain.Enum;

namespace Application.Contracts;

public interface IHotelService
{
    Task<IEnumerable<HotelDTO>> GetFilteredHotels(Cities? city);
}
