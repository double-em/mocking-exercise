using System;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using LandonHotel.Data;
using LandonHotel.Repositories;
using LandonHotel.Services;
using Xunit;

namespace LandonHotel.Tests
{
    public class BookingServiceTests
    {
        private readonly IRoomsRepository _roomsRepository; 
        public BookingServiceTests()
        {
            _roomsRepository = A.Fake<IRoomsRepository>();
            
            var rooms = new RoomTestDataGenerator();
            A.CallTo(() => _roomsRepository.GetRooms()).Returns(((Room[])rooms.FakeRooms.First()).ToList());
        }

        [Theory, ClassData(typeof(RoomTestDataGenerator))]
        public void CalculateBookingCost_ShouldBeIncreasedWhenSmoking(params Room[] roomsObj)
        {
            var rooms = roomsObj;
            var smokerBooking = new Booking
            {
                Id = 1,
                HasPets = false,
                IsSmoking = true,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now
            };
            
            var sut = new BookingService(_roomsRepository);

            foreach (var room in rooms)
            {
                smokerBooking.RoomId = room.Id;
                sut.CalculateBookingCost(smokerBooking).Should().Be(room.Rate * 2);
            }
        }

        [Theory, ClassData(typeof(RoomTestDataGenerator))]
        public void CalculateBookingCost_ShouldNotBeIncreased(Room room)
        {
            var booking = new Booking
            {
                Id = 1,
                HasPets = false,
                IsSmoking = false,
                RoomId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now
            };
            
            var sut = new BookingService(_roomsRepository);

            sut.CalculateBookingCost(booking).Should().Be(room.Rate);
        }
    }
}