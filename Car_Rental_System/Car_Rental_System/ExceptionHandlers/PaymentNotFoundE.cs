using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental_System.ExceptionHandlers
{
    public class PaymentNotFoundE : Exception
    {
        public PaymentNotFoundE(string message) : base(message)
        {
        }
    }
}
