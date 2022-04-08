using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using House_TARgv20.Core.Domain;
using House_TARgv20.Core.Dtos;
using House_TARgv20.Core.ServiceInterface;
using House_TARgv20.Data;

namespace House_TARgv20.ApplicationServices.Services
{
    public class HouseServices : IHouseService
    {
        private readonly HouseDbContext _context;

        public HouseServices
            (
                HouseDbContext context
            )
        {
            _context = context;
        }

        public async Task<HouseDomain> Delete(Guid id)
        {
            var houseId = await _context.House.FirstOrDefaultAsync(x => x.Id == id); // Takes "Home" from HouseDbContext
            // 
            //_context.ExistingFilePath.RemoveRange(carId.ExistingFilePaths);
            _context.House.Remove(houseId);
            await _context.SaveChangesAsync();

            return houseId;
        }

        public async Task<HouseDomain> Add(HouseDto dto)
        {
            HouseDomain house = new HouseDomain();

            house.Id = Guid.NewGuid();
            house.HouseAddress = dto.HouseAddress;
            house.ApartmentNumber = dto.ApartmentNumber;
            house.FloorNumber = dto.FloorNumber;
            house.NumberOfRooms = dto.NumberOfRooms;
            house.ConditionOfTheApartment = dto.ConditionOfTheApartment;
            house.Price = dto.Price;
            house.Description = dto.Description;
            house.ModifiedAt = DateTime.Now;
            house.CreatedAt = DateTime.Now;

            await _context.House.AddAsync(house);
            await _context.SaveChangesAsync();

            return house;
        }

        public async Task<HouseDomain> Edit(Guid id)
        {
            var result = await _context.House
                .FirstOrDefaultAsync(x => x.Id == id); // Takes "Home" from HouseDbContext

            return result;
        }

        public async Task<HouseDomain> Update(HouseDto dto)
        {
            HouseDomain house = new HouseDomain();

            house.Id = dto.Id;
            house.HouseAddress = dto.HouseAddress;
            house.ApartmentNumber = dto.ApartmentNumber;
            house.FloorNumber = dto.FloorNumber;
            house.NumberOfRooms = dto.NumberOfRooms;
            house.ConditionOfTheApartment = dto.ConditionOfTheApartment;
            house.Price = dto.Price;
            house.Description = dto.Description;
            house.ModifiedAt = dto.ModifiedAt;
            house.CreatedAt = dto.ModifiedAt;

            _context.House.Update(house);
            await _context.SaveChangesAsync();

            return house;
        }

        // For test
        public async Task<HouseDomain> GetAsync(Guid id)
        {
            var result = await _context.House
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }
    }
}
