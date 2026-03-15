using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Enums;
using VehicleInventory.Domain.Exceptions;
using VehicleInventory.Domain.ValueObjects;

namespace VehicleInventory.Domain.Aggregates.JPVehicle
{
    //oversees all lifecycle regulations and represents the vehicle aggregate
    public class JPVehicle
    {

        public Guid Id { get; private set; }
        public JPVehicleCode VehicleCode { get; private set; }
        public JPLocationId LocationId { get; private set; }
        public JPVehicleTypeId VehicleType { get; private set; }
        public JPVehicleStatus Status { get; private set; }

        private readonly List<JPVehicleInventory> _inventoryRecords = new();
        public IReadOnlyCollection<JPVehicleInventory> InventoryRecords => _inventoryRecords.AsReadOnly();

        private JPVehicle() 
        {
            VehicleCode = new JPVehicleCode("UNKNOWN");
            LocationId = new JPLocationId("UNKNOWN");
            VehicleType = new JPVehicleTypeId("UNKNOWN");
        }

        public JPVehicle(string vehicleCode, string locationId, string vehicleType)
        {
            
            VehicleCode = new JPVehicleCode(vehicleCode);
            LocationId = new JPLocationId(locationId);
            VehicleType = new JPVehicleTypeId(vehicleType);

            Id = Guid.NewGuid();
            Status = JPVehicleStatus.Available;
        }

        
        public void AddInventoryRecord(JPLocationId location, int quantity)
        {
            var record = new JPVehicleInventory(Id, location, quantity);
            _inventoryRecords.Add(record);
        }

        //Verifies and changes the car's status to rented
        public void MarkRented()
        {
            if (Status == JPVehicleStatus.Rented)
                throw new JPDomainException("Vehicle is already rented.");

            if (Status == JPVehicleStatus.Reserved)
                throw new JPDomainException("Reserved vehicle cannot be rented.");

            if (Status == JPVehicleStatus.UnderService)
                throw new JPDomainException("Vehicle under service cannot be rented.");

            Status = JPVehicleStatus.Rented;
        }

        public void MarkReserved()
        {
            if (Status == JPVehicleStatus.Rented)
                throw new JPDomainException("Rented vehicle cannot be reserved.");

            if (Status == JPVehicleStatus.UnderService)
                throw new JPDomainException("Vehicle under service cannot be reserved.");

            Status = JPVehicleStatus.Reserved;
        }

        public void MarkServiced()
        {
            if (Status == JPVehicleStatus.Rented)
                throw new JPDomainException("Rented vehicle cannot be sent to service.");

            Status = JPVehicleStatus.UnderService;
        }

        public void MarkAvailable()
        {
            if (Status == JPVehicleStatus.Reserved)
                throw new JPDomainException("Reserved vehicle cannot be made available without release.");

            Status = JPVehicleStatus.Available;
        }
    }
}
