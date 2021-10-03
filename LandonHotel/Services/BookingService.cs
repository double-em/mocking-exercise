using LandonHotel.Data;
using LandonHotel.Repositories;

namespace LandonHotel.Services
{
    public class BookingService : IBookingService
    {
        private IRoomsRepository _roomsRepository;
        public BookingService(IRoomsRepository roomsRepository)
        {
            _roomsRepository = roomsRepository;
        }

        public int CalculateBookingCost(Booking booking)
        {
            var roomRate = _roomsRepository.GetRoom(booking.RoomId).Rate;
            
            if (booking.IsSmoking) roomRate *= 2;
            
            return roomRate ;
        }
    }
}
