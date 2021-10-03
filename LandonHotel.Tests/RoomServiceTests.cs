using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using LandonHotel.Data;
using LandonHotel.Repositories;
using LandonHotel.Services;
using Xunit;

namespace LandonHotel.Tests
{
    public class RoomServiceTests
    {
        private readonly IRoomsRepository _roomsRepository; 
        public RoomServiceTests()
        {
            _roomsRepository = A.Fake<IRoomsRepository>();
        }
        
        [Fact]
        public void GetAllRooms_ShouldReturnRooms()
        {
            var roomGenerator = new RoomTestDataGenerator();

            var rooms = (Room[])roomGenerator.FakeRooms.First();
            
            var sut = new RoomService(_roomsRepository);
            A.CallTo(() => _roomsRepository.GetRooms()).Returns(rooms);
    
            sut.GetAllRooms().Count.Should().Be(rooms.Length);
        }

        [Fact]
        public void GetAllRooms_ShouldCallRepoOnlyOnce()
        {
            var sut = new RoomService(_roomsRepository);
            sut.GetAllRooms();
            
            A.CallTo(() => _roomsRepository.GetRooms()).MustHaveHappenedOnceExactly();
        }

        // [Theory, ClassData(typeof(RoomTestDataGenerator))]
        // public void GetAllRooms_RoomsDataMatchesFormat(Room room)
        // {
        //     
        // }
    }
}

