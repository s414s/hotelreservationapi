using Application.DTOs;
using Domain.Enum;

namespace Application.Contracts;

public interface IHotelService
{
    Task<IEnumerable<HotelDTO>> GetFilteredHotels(FiltersDTO filter);
    Task<HotelDTO> GetById(long hotelId);
    Task<bool> Create(NewHotelDTO newHotelInfo);
}
