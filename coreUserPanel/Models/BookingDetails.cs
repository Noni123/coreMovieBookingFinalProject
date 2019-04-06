using System;
using System.Collections.Generic;

namespace coreUserPanel.Models
{
    public partial class BookingDetails
    {
        public int BookingId { get; set; }
        public int MovieId { get; set; }
        public int QtySeats { get; set; }

        public Bookings Booking { get; set; }
        public Movies Movie { get; set; }
    }
}
