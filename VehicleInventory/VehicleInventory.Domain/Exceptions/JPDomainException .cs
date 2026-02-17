using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInventory.Domain.Exceptions
{
    public class JPDomainException: Exception
    {
        public JPDomainException(string message) : base(message){}
    }
}
