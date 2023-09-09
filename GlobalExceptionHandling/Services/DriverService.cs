﻿using GlobalExceptionHandling.Data;
using GlobalExceptionHandling.Models;
using Microsoft.EntityFrameworkCore;

namespace GlobalExceptionHandling.Services
{
    public class DriverService : IDriverService
    {
        private readonly AppDbContext _context;
        public DriverService(AppDbContext context)
        {
            _context = context;
        }
        public  async Task<Driver> AddDriver(Driver driver)
        {
            var result = await _context.Drivers.AddAsync(driver);

            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteDriver(int id)
        {
            var driver = await GetDriverById(id);
            if (driver == null)
            {
                return false;
            }
            _context.Drivers?.Remove(driver);
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<Driver?> GetDriverById(int id)
        {
            return await _context.Drivers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Driver>> GetDrivers()
        {
            return await _context.Drivers.ToListAsync();
        }

        public async Task<Driver> UpdateDriver(Driver driver)
        {
            var result = _context.Drivers.Update(driver);
            await _context.SaveChangesAsync();

            return result.Entity;
        }
    }
}
