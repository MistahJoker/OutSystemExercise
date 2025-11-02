using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBookingSystem.ExternalAPIs  
{
    public interface IPaymentGeteway
    {
        public PaymentResult ProcessPayment(string CardNumber);
    }
}