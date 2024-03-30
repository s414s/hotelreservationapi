using Application.Contracts;
using Application.DTOs;
using Domain.Contracts;
using Domain.Entities;
using Domain.Enum;

namespace Application.Implementations;

public class HotelService : IHotelService
{
    private readonly IRepository<Hotel> _hotelsRepo;

    public HotelService(IRepository<Hotel> hotelsRepo)
    {
        _hotelsRepo = hotelsRepo;
    }

    public Task<IEnumerable<HotelDTO>> GetFilteredHotels(Cities? city)
    {
        throw new NotImplementedException();
    }
}
