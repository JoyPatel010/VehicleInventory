using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInventory.Application.DTOs
{
    public class JPCreateVehicleDto
    {
        [Required]
        public string VehicleCode { get; set; }

        [Required]
        public string LocationId { get; set; }

        [Required]
        public string VehicleType { get; set; }
    }
}
