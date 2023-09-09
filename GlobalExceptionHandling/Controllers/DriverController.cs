using GlobalExceptionHandling.Models;
using GlobalExceptionHandling.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalExceptionHandling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;
        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }


        [HttpGet("DriversList")]

        private  async Task<IActionResult> GetAllDrivers()
        {
            var drivers = await _driverService.GetDrivers();

            return Ok(drivers);
        }

        [HttpPost]
        private async Task<IActionResult> AddDrivers(Driver driver)
        {
            var result = await _driverService.AddDriver(driver);

            return Ok(result);
        }


        [HttpGet("GetDriverById")]

        private async Task<IActionResult> GetDriverById(int id)
        {
            var driver = await _driverService.GetDriverById(id);
            if(driver == null)
            {
                return NotFound();
            }
            return Ok(driver);
        }


        [HttpDelete]
        private async Task<IActionResult> DeleteDriver(int id)
        {
            var driver = await _driverService.DeleteDriver(id);

            return Ok(driver);
        }
    }
}
