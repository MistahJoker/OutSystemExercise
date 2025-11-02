using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBookingSystem.EventSeating
{
    public class ReservedSeatType : ISeatingType
    {
        public void ManageSeating()
        {
            Console.WriteLine("Managing reserved seating arrangements.");
        }
    }
}