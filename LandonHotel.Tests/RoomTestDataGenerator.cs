using System;
using System.Collections;
using System.Collections.Generic;
using Bogus;
using LandonHotel.Data;

namespace LandonHotel.Tests
{
    public class RoomTestDataGenerator : IEnumerable<object[]>
    {
        public readonly IEnumerable<object[]> FakeRooms;
        public RoomTestDataGenerator()
        {
            Randomizer.Seed = new Random(42);

            var roomIds = 0;
            var fakerRooms = new Faker<Room>()
                .RuleFor(r => r.Id, _ => ++roomIds)
                .RuleFor(r => r.Name, f => f.Address.City())
                .RuleFor(r => r.ArePetsAllowed, f => f.Random.Bool())
                .RuleFor(r => r.Capacity, f => f.Random.Int(1, 6))
                .RuleFor(r => r.Rate, f => f.Random.Int(100, 800));

            FakeRooms = new []{fakerRooms.Generate(10).ToArray()};
        }

        public IEnumerator<object[]> GetEnumerator() => FakeRooms.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}