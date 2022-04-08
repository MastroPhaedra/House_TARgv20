using System;

namespace House_TARgv20.Core.Domain
{
    public class HouseDomain
    {
        public Guid? Id { get; set; }
        public string HouseAddress { get; set; }
        public int ApartmentNumber { get; set; }
        public int FloorNumber { get; set; }
        public int NumberOfRooms { get; set; }
        public string ConditionOfTheApartment { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
