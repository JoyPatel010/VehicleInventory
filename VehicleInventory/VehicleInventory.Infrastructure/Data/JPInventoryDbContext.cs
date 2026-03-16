using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Aggregates.JPVehicle;
using VehicleInventory.Domain.ValueObjects;

namespace VehicleInventory.Infrastructure.Data
{
    public class JPInventoryDbContext: DbContext
    {
        public JPInventoryDbContext(DbContextOptions<JPInventoryDbContext> options)
           : base(options)
        {}  

        public DbSet<JPVehicle> Vehicles { get; set; }
        public DbSet<JPVehicleInventory> VehicleInventories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JPVehicle>(entity =>
            {

                entity.HasKey(v => v.Id);

                entity.Property(v => v.VehicleCode)
                    .HasConversion(v => v.Code, v => new JPVehicleCode(v))
                    .IsRequired();

                entity.Property(v => v.LocationId)
                    .HasConversion(v => v.Location, v => new JPLocationId(v))
                    .IsRequired();

                entity.Property(v => v.VehicleType)
                    .HasConversion(v => v.TypeName, v => new JPVehicleTypeId(v))
                    .IsRequired();

                entity.HasMany(v => v.InventoryRecords)
                    .WithOne()
                    .HasForeignKey(i => i.VehicleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<JPVehicleInventory>(entity =>
            {

                entity.HasKey(i => i.Id);

                entity.Property(i => i.Location)
                    .HasConversion(v => v.Location, v => new JPLocationId(v))
                    .IsRequired();


                entity.Property(i => i.Quantity).IsRequired();

                entity.Property(i => i.LastUpdated).IsRequired();
            });
        }

    }
}
