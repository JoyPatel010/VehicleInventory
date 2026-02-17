using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Enums;
using VehicleInventory.Domain.Exceptions;

namespace VehicleInventory.Domain.Entities
{
    public class JPVehicle
    {

        public Guid Id { get; private set; }
        public string VehicleCode { get; private set; }
        public string LocationId { get; private set; }
        public string VehicleType { get; private set; }
        public JPVehicleStatus Status { get; private set; }

        private JPVehicle() { }

        public JPVehicle(string vehicleCode, string locationId, string vehicleType)
        {
            if (string.IsNullOrWhiteSpace(vehicleCode))
                throw new ArgumentException("Vehicle code is required.");

            if (string.IsNullOrWhiteSpace(locationId))
                throw new ArgumentException("Location is required.");

            if (string.IsNullOrWhiteSpace(vehicleType))
                throw new ArgumentException("Vehicle type is required.");

            Id = Guid.NewGuid();
            VehicleCode = vehicleCode;
            LocationId = locationId;
            VehicleType = vehicleType;
            Status = JPVehicleStatus.Available;
        }

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
