using Application.Contracts;
using Application.DTOs;
using Domain.Contracts;
using Domain.Entities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace Application.Implementations;

public class HotelService : IHotelService
{
    private readonly IRepository<Hotel> _hotelsRepo;

    public HotelService(IRepository<Hotel> hotelsRepo)
    {
        _hotelsRepo = hotelsRepo;
    }

    public IEnumerable<HotelDTO> GetFilteredHotels(Cities? city)
    {
        return _hotelsRepo.Query
            .Include(x => x.Rooms)
            .Where(x => city == null || x.City == city)
            .Select(x => HotelDTO.MapFromDomainEntity(x));
    }
}
