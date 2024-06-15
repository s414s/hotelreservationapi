using Domain.Entities;

namespace Domain.DomainServices;

public static class AvailabilityService
{
    public static bool IsRoomAvailableBetweenDates(Room room, DateOnly start, DateOnly end)
        => room.Bookings?.Any(x => x.Start >= start && x.End <= end) == false;
}
