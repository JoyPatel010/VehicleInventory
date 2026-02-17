using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Enums;

namespace VehicleInventory.Application.DTOs
{
    public class JPVehicleDto
    {
        public Guid Id { get; set; }
        public string VehicleCode { get; set; }
        public string LocationId { get; set; }
        public string VehicleType { get; set; }
        public JPVehicleStatus Status { get; set; }
    }
}
