using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBookingSystem.ExternalAPIs  
{
    public class PaymentGetway :IPaymentGeteway
    {
        public PaymentResult ProcessPayment(string CardNumber)
        {
            Console.WriteLine("Processing payment with card number: " + CardNumber);
            return new PaymentResult("12345"){ IsSuccessful = true }; ;
        }
    }
}