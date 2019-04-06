using System;
using System.Collections.Generic;

namespace coreUserPanel.Models
{
    public partial class Movies
    {
        public Movies()
        {
            BookingDetails = new HashSet<BookingDetails>();
        }

        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public double MoviePrice { get; set; }
        public string MovieImage { get; set; }
        public string MovieDuration { get; set; }
        public string MovieDescription { get; set; }
        public int MultiplexId { get; set; }

        public Multiplexes Multiplex { get; set; }
        public MovieDetails MovieDetails { get; set; }
        public Reviews Reviews { get; set; }
        public ICollection<BookingDetails> BookingDetails { get; set; }
    }
}
