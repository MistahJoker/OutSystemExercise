using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBookingSystem.ExternalAPIs
{
    public class PaymentResult
    {
        public bool IsSuccessful { get; set; }
        public string PaymentId { get; set; }

        public PaymentResult(string paymentId)
        {
            PaymentId = paymentId;
        }
    }

}