using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Exceptions;

namespace VehicleInventory.Domain.ValueObjects
{
    public class JPVehicleCode
    {
        public string Code { get; private set; }

        private JPVehicleCode()
        {
            Code = string.Empty;
        }

        public JPVehicleCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new JPDomainException("Vehicle code cannot be empty.");

            if (code.Length < 3)
                throw new JPDomainException("Vehicle code must be at least 3 characters long.");

            if (code.Length > 20)
                throw new JPDomainException("Vehicle code cannot be longer than 20 characters.");

            Code = code.Trim().ToUpper();
        }

        

    }
}
