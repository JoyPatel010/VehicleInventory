using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleInventory.Domain.Entities;

namespace VehicleInventory.Infrastructure.Data
{
    public class JPInventoryDbContext: DbContext
    {
        public JPInventoryDbContext(DbContextOptions<JPInventoryDbContext> options)
           : base(options)
        {}

        public DbSet<JPVehicle> Vehicles { get; set; }

    }
}
