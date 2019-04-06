using System;
using System.Collections.Generic;

namespace coreUserPanel.Models
{
    public partial class Payments
    {
        public int PaymentId { get; set; }
        public double Pay { get; set; }
        public int BookingId { get; set; }

        public Bookings Booking { get; set; }
    }
}
