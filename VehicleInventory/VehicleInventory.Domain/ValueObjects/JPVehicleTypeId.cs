using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Exceptions;

namespace VehicleInventory.Domain.ValueObjects
{
    public class JPVehicleTypeId
    {
        public string TypeName { get; private set; }

        private JPVehicleTypeId()
        {
            TypeName = string.Empty;
        }

        public JPVehicleTypeId(string typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName))
                throw new JPDomainException("Vehicle type cannot be empty.");

            if (typeName.Length > 50)
                throw new JPDomainException("Vehicle type cannot be longer than 50 characters.");

            TypeName = typeName.Trim();
        }


    }
}
