using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using House_TARgv20.Core.Dtos;
using House_TARgv20.Core.ServiceInterface;
using House_TARgv20.Data;
using House_TARgv20.Models.House;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
//using House.Models.House;

namespace House_TARgv20.Controllers
{
    public class HouseController : Controller
    {
        private readonly HouseDbContext _context;
        private readonly IHouseService _houseService;

        public HouseController
            (
                HouseDbContext context,
                IHouseService houseService
            )
        {
            _context = context;
            _houseService = houseService;
        }


        public IActionResult Index()
        {
            var result = _context.House
                .Select(x => new HouseListViewModel //OR Create HouseListViewModel
                {
                    Id = x.Id,
                    HouseAddress = x.HouseAddress,
                    ApartmentNumber = x.ApartmentNumber,
                    FloorNumber = x.FloorNumber,
                    NumberOfRooms = x.NumberOfRooms,
                    ConditionOfTheApartment = x.ConditionOfTheApartment,
                    Price = x.Price,
                    Description = x.Description
                }).ToList();
            
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var house = await _houseService.Delete(id);

            if (house == null)
            {
                RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Add()
        {
            HouseViewModel model = new HouseViewModel();

            return View("Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(HouseViewModel model)
        {
            var dto = new HouseDto()
            {
                Id = model.Id,
                HouseAddress = model.HouseAddress,
                ApartmentNumber = model.ApartmentNumber,
                FloorNumber = model.FloorNumber,
                NumberOfRooms = model.NumberOfRooms,
                ConditionOfTheApartment = model.ConditionOfTheApartment,
                Price = model.Price,
                Description = model.Description,
                CreatedAt = model.CreatedAt,
                ModifiedAt = model.ModifiedAt
            };

            var result = await _houseService.Add(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var house = await _houseService.Edit(id);
            if (house == null)
            {
                return NotFound();
            }

            var model = new HouseViewModel();

            model.Id = house.Id;
            model.HouseAddress = house.HouseAddress;
            model.ApartmentNumber = house.ApartmentNumber;
            model.FloorNumber = house.FloorNumber;
            model.NumberOfRooms = house.NumberOfRooms;
            model.ConditionOfTheApartment = house.ConditionOfTheApartment;
            model.Price = house.Price;
            model.Description = house.Description;
            model.CreatedAt = house.CreatedAt;
            model.ModifiedAt = house.ModifiedAt;

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Update(HouseViewModel model)
        {
            var dto = new HouseDto()
            {
                Id = model.Id,
                HouseAddress = model.HouseAddress,
                ApartmentNumber = model.ApartmentNumber,
                FloorNumber = model.FloorNumber,
                NumberOfRooms = model.NumberOfRooms,
                ConditionOfTheApartment = model.ConditionOfTheApartment,
                Price = model.Price,
                Description = model.Description,
                CreatedAt = model.CreatedAt,
                ModifiedAt = model.ModifiedAt
            };

            var result = await _houseService.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), model);

        }

        // trying to do select list for ConditionOfApartmens
        //public ActionResult SelectCondition()
        //{

        //    List<SelectListItem> items = new List<SelectListItem>();

        //    items.Add(new SelectListItem { Text = "The best", Value = "0" });
        //    items.Add(new SelectListItem { Text = "Good", Value = "1", Selected = true });
        //    items.Add(new SelectListItem { Text = "Bad", Value = "2", });

        //    ViewBag.MovieType = items;

        //    return View();
        //}
    }
}
