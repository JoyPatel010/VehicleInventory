using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Exceptions;

namespace VehicleInventory.Domain.ValueObjects
{
    public class JPLocationId
    {
        public string Location { get; private set; }

        private JPLocationId()
        {
            Location = string.Empty;
        }

        public JPLocationId(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
                throw new JPDomainException("Location cannot be empty.");

            if (location.Length > 100)
                throw new JPDomainException("Location cannot be longer than 100 characters.");

            Location = location.Trim();
        }


      
    }
}
