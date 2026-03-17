using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInventory.Application.DTOs
{
    public class JPAddInventoryRecordDto
    {
        public string Location { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
