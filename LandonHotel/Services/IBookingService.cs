using LandonHotel.Data;

namespace LandonHotel.Services
{
    public interface IBookingService
    {
        int CalculateBookingCost(int roomId, Booking booking);
    }
}