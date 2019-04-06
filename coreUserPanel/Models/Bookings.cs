using System;
using System.Collections.Generic;

namespace coreUserPanel.Models
{
    public partial class Bookings
    {
        public Bookings()
        {
            BookingDetails = new HashSet<BookingDetails>();
        }

        public int BookingId { get; set; }
        public double BookingAmount { get; set; }
        public DateTime BookingDate { get; set; }
        public string ShowTiming { get; set; }
        public string AudiName { get; set; }
        public int UserDetailId { get; set; }

        public UserDetails UserDetail { get; set; }
        public Payments Payments { get; set; }
        public ICollection<BookingDetails> BookingDetails { get; set; }
    }
}
