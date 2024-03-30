using Application.DTOs;

namespace Application.Contracts;

public interface IHotelService
{
    Task<IEnumerable<HotelDTO>> GetAll();
}
