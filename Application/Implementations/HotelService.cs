using Application.Contracts;
using Application.DTOs;
using Domain.Contracts;
using Domain.Entities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Implementations;

public class HotelService(IRepository<Hotel> hotelsRepo) : IHotelService
{
    private readonly IRepository<Hotel> _hotelsRepo = hotelsRepo;

    public async Task<IEnumerable<HotelDTO>> GetFilteredHotels(FiltersDTO filter)
    {
        var query = await _hotelsRepo.Query
            .Include(x => x.Rooms)
            .Where(x => filter.City == null || x.City == filter.City)
            .Select(x => HotelDTO.MapFromDomainEntity(x))
            .ToListAsync();

        if (filter.City is not null)
        {
            return query;
        }

        return filter.Asc
            ? query.OrderBy(x => x.City)
            : query.OrderByDescending(x => x.City);
    }

    public async Task<HotelDTO> GetById(long hotelId)
    {
        var hotel = await _hotelsRepo.Query
            .Include(x => x.Rooms)
            .AsNoTracking()
            .Where(x => x.Id == hotelId)
            .FirstOrDefaultAsync()
            ?? throw new ApplicationException("No hotel found");

        return HotelDTO.MapFromDomainEntity(hotel);
    }

    public async Task<bool> Create(NewHotelDTO newHotelInfo)
    {
        await _hotelsRepo.Add(newHotelInfo.MapToDomainEntity());
        return await _hotelsRepo.SaveChanges();
    }
}
