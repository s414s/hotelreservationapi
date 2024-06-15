using Domain.Entities;

namespace Domain.DomainServices;

public static class PriceService
{
    public static decimal CalculateTotalBookingPrice(Booking b)
        => b.Room?.Price ?? 0 * (b.End.DayNumber - b.Start.DayNumber);
}
