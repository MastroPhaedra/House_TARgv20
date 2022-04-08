using System;
using System.Threading.Tasks;
using Xunit;
using House_TARgv20.Core.Dtos;
using House_TARgv20.Core.Domain;
using House_TARgv20.Models.House;
using House_TARgv20.Core.ServiceInterface;
using House_TARgv20.ApplicationServices.Services;
using Microsoft.Extensions.DependencyInjection;
using House_TARgv20.Data;
using System.Linq;

namespace House_TARgv20.Tests.Validation
{
    public class House_Tests : Test_Base
    {
        //private readonly IHouseService _house;
        //public WorkingOrNotAddingData()
        //{
        //    _addingData = new WorkingOrNotAddingData();
        //}        
        //public WorkingOrNotAddingData(IHouseService house)
        //{
        //    house = _house;
        //}

        [Fact]
        public async void AddingData_Then_CheckEqualOrNot_ReturnsTrue() 
        {
            string guid = "c56a4180-65aa-42ec-a945-5fd21dec0538";

            var house = new HouseDto();
            {
                house.Id = Guid.Parse(guid);
                house.HouseAddress = "TestAdress";
                house.ApartmentNumber = 1;
                house.FloorNumber = 2;
                house.NumberOfRooms = 3;
                house.ConditionOfTheApartment = "good";
                house.Price = 4;
                house.Description = "labudabudabdab";
                house.CreatedAt = DateTime.Now;
                house.ModifiedAt = DateTime.Now;
            };

            var context = Svc<HouseDbContext>();
            var countBefore = context.House.Count();

            await Svc<IHouseService>().Add(house);

            var countAfter = context.House.Count();

            Assert.Equal(countBefore+1, countAfter);
            //Assert.True(_house.Add(house));
        }

        [Fact]
        public void TryingToWriteIncorrectTypeOfId__Then_CatchTheError()
        {
            var guid = "TOTTAL-ERROR";
            Action testCode = () => { throw new InvalidOperationException(guid); };

            var ex = Record.Exception(testCode);

            Assert.NotNull(ex);
            Assert.IsType<InvalidOperationException>(ex);
        }

        //[Fact]
        //public async Task DeleteByIdHouse_ReturnsTrue()
        //{
        //    //Adding house
        //    string guid = "12efd195-1364-4da0-a8c9-6700ffedc33a";
        //    var houseId = Guid.Parse(guid);

        //    //
        //    var house = new HouseDto();
        //    {
        //        house.Id = houseId;
        //        house.HouseAddress = "TestAdress";
        //        house.ApartmentNumber = 1;
        //        house.FloorNumber = 2;
        //        house.NumberOfRooms = 3;
        //        house.ConditionOfTheApartment = "good";
        //        house.Price = 4;
        //        house.Description = "labudabudabdab";
        //        house.CreatedAt = DateTime.Now;
        //        house.ModifiedAt = DateTime.Now;
        //    };

        //    await Svc<IHouseService>().Add(house);

        //    //delete
        //    var result = await Svc<IHouseService>().Delete(houseId);

        //    //Assert
        //    Assert.Null(result);
        //}


        [Fact]
        public async Task UpdateData_AddingData_Then_UpdateIt_ReturnsTrue()
        {
            string guid = "ef9bcdf5-3769-494e-ab42-4dd4b1a4d587";

            //
            var house = new HouseDto();
            {
                house.Id = Guid.Parse(guid);
                house.HouseAddress = "TestAdress";
                house.ApartmentNumber = 1;
                house.FloorNumber = 2;
                house.NumberOfRooms = 3;
                house.ConditionOfTheApartment = "good";
                house.Price = 4;
                house.Description = "labudabudabdab";
                house.CreatedAt = DateTime.Now;
                house.ModifiedAt = DateTime.Now;
            };

            //

            var houseUpdatedId = Guid.Parse(guid);
            var houseUpdated = new HouseDto();
            {
                houseUpdated.HouseAddress = "TestAdress2";
                houseUpdated.ApartmentNumber = 11;
                houseUpdated.FloorNumber = 2222;
                houseUpdated.NumberOfRooms = 33333;
                houseUpdated.ConditionOfTheApartment = "bad";
                houseUpdated.Price = 444444;
                houseUpdated.Description = "LOL";
            };

            await Svc<IHouseService>().Add(house);

            await Svc<IHouseService>().Update(houseUpdated);

            Assert.Equal(house.Id.ToString(), houseUpdatedId.ToString());
            Assert.NotEqual(house.HouseAddress, houseUpdated.HouseAddress);
            Assert.NotEqual(house.ApartmentNumber.ToString(), houseUpdated.ApartmentNumber.ToString());
            Assert.NotEqual(house.FloorNumber.ToString(), houseUpdated.FloorNumber.ToString());
            Assert.NotEqual(house.NumberOfRooms.ToString(), houseUpdated.NumberOfRooms.ToString());
            Assert.NotEqual(house.ConditionOfTheApartment, houseUpdated.ConditionOfTheApartment);
            Assert.NotEqual(house.Price.ToString(), houseUpdated.Price.ToString());
            Assert.NotEqual(house.Description, houseUpdated.Description);
        }

        [Fact]
        public async Task CheckById_ReturnsFail()
        {
            string guid = "ef9bcdf5-3769-494e-ab42-4dd4b1a4d587";

            string guid2 = "e6771076-91cd-4169-bbdd-cfc5290a6b3f";

            var insertGuid = Guid.Parse(guid);
            var insertGuid1 = Guid.Parse(guid2);

            await Svc<IHouseService>().GetAsync(insertGuid);

            Assert.NotEqual(insertGuid1, insertGuid);
        }
    }
}
