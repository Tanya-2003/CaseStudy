using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental_System.ExceptionHandlers
{
    public class LeaseNotFoundE : Exception
    {
        public LeaseNotFoundE(string message) : base(message)
        {
        }
    }
}
