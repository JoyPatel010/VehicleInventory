using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Entities;

namespace VehicleInventory.Application.Interfaces
{
    public interface JPIVehicleRepository
    {
        Task AddAsync(JPVehicle vehicle);
        Task<JPVehicle> GetByIdAsync(Guid id);
        Task<List<JPVehicle>> GetAllAsync();
        Task UpdateAsync(JPVehicle vehicle);
        Task DeleteAsync(JPVehicle vehicle);
    }
}
