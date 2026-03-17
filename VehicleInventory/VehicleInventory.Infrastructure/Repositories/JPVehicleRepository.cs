using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Application.Interfaces;
using VehicleInventory.Domain.Aggregates.JPVehicle;
using VehicleInventory.Domain.ValueObjects;
using VehicleInventory.Infrastructure.Data;

namespace VehicleInventory.Infrastructure.Repositories
{
    //implements EF Core persistence for Vehicles
    public class JPVehicleRepository: JPIVehicleRepository
    {

        private readonly JPInventoryDbContext _context;

        public JPVehicleRepository(JPInventoryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(JPVehicle vehicle)
        {
            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task<JPVehicle> GetByIdAsync(Guid id)
        {
            return await _context.Vehicles.Include(v => v.InventoryRecords).FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<List<JPVehicle>> GetAllAsync()
        {
            return await _context.Vehicles.Include(v => v.InventoryRecords).ToListAsync();
        }

        public async Task UpdateAsync(JPVehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(JPVehicle vehicle)
        {
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AddInventoryRecordAsync(Guid vehicleId, string location, int quantity)
        {
            var vehicle = await _context.Vehicles
                .Include(v => v.InventoryRecords)
                .FirstOrDefaultAsync(v => v.Id == vehicleId);

            if (vehicle == null)
                return false;

            var locationObj = new JPLocationId(location);

            vehicle.AddInventoryRecord(locationObj, quantity);

            
            var newRecord = vehicle.InventoryRecords.Last();

            await _context.VehicleInventories.AddAsync(newRecord);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
