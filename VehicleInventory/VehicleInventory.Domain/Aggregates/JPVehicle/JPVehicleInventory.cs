using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Exceptions;
using VehicleInventory.Domain.ValueObjects;

namespace VehicleInventory.Domain.Aggregates.JPVehicle
{
    public class JPVehicleInventory
    {
        public Guid Id { get; private set; }
        public Guid VehicleId { get; private set; }
        public JPLocationId Location { get; private set; }
        public int Quantity { get; private set; }
        public DateTime LastUpdated { get; private set; }

        private JPVehicleInventory()
        {
            Location = new JPLocationId("Unknown");
        }

        public JPVehicleInventory(Guid vehicleId, JPLocationId location, int quantity)
        {
            if (vehicleId == Guid.Empty)
                throw new JPDomainException("Vehicle Id is required.");

            if (quantity < 0)
                throw new JPDomainException("Quantity could not be Negative.");

            Id = Guid.NewGuid();
            VehicleId = vehicleId;
            Location = location;
            Quantity = quantity;
            LastUpdated = DateTime.UtcNow;
        }

        public void UpdateLocation(JPLocationId newLocation)
        {
            Location = newLocation;
            LastUpdated = DateTime.UtcNow;
        }

        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity < 0)
                throw new JPDomainException("Quantity can not be Negative.");

            Quantity = newQuantity;
            LastUpdated = DateTime.UtcNow;
        }

    }
}
