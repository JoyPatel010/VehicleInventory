using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using VehicleInventory.Domain.Enums;

namespace VehicleInventory.Application.DTOs
{
    public class JPUpdateVehicleStatusDto
    {
        [Required]
        public JPVehicleStatus Status { get; set; }
    }
}
