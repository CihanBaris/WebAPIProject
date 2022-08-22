using Microsoft.AspNetCore.Mvc;
using WebAPIProject.Entity.Data;
using WebAPIProject.Entity.Models;

namespace WebAPIProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly WebAPIContext _dbContext;

        public VehiclesController(WebAPIContext dbContext) => _dbContext = dbContext;

        [HttpGet("cars")]
        public ActionResult GetCarByColor(string color) => GetVehicleByColor(color, VehicleType.Car);

        [HttpGet("buses")]
        public ActionResult GetBusByColor(string color) => GetVehicleByColor(color, VehicleType.Bus);

        [HttpGet("boat")]
        public ActionResult GetBoatByColor(string color) => GetVehicleByColor(color, VehicleType.Boat);

        [HttpDelete("{id}")]
        public ActionResult DeleteCar(int? id)
        {
            var car = GetCarById(id).Value;
            _dbContext.Cars.Remove(car);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult ToggleCarHeadlights(int? id)
        {
            var car = GetCarById(id).Value;

            if (car == null)
                return NotFound();

            car.Headlights = !car.Headlights;
            _dbContext.Update(car);
            _dbContext.SaveChanges();

            return Ok();
        }

        [NonAction]
        public ActionResult GetVehicleByColor(string color, VehicleType? vehicleType)
        {
            if (string.IsNullOrEmpty(color) || vehicleType == null)
                return BadRequest();
            
            object vehicles = new();

            switch (vehicleType)
            {
                case VehicleType.Car:
                    vehicles = _dbContext.Cars.Where(c => c.Color == color).ToList();
                    break;

                case VehicleType.Bus:
                    vehicles = _dbContext.Buses.Where(b => b.Color == color).ToList();
                    break;

                case VehicleType.Boat:
                    vehicles = _dbContext.Boats.Where(b => b.Color == color).ToList();
                    break;
            }

            if (vehicles == null)
                return NotFound();

            return Ok(vehicles);
        }

        [NonAction]
        public ActionResult<Car> GetCarById(int? id)
        {
            if (id == null)
                return BadRequest();

            var car = _dbContext.Cars.FirstOrDefault(c => c.Id == id);

            if (car == null)
                return NotFound();

            return car;
        }
    }

    public enum VehicleType { Car, Bus, Boat }
}
