using System;
using System.Collections.Generic;

namespace coreUserPanel.Models
{
    public partial class MovieDetails
    {
        public int MovieDetailId { get; set; }
        public int MovieId { get; set; }
        public string Casting { get; set; }
        public string Director { get; set; }
        public string Producer { get; set; }
        public string MovieType { get; set; }
        public string MovieImage { get; set; }
        public string MovieDuration { get; set; }

        public Movies Movie { get; set; }
    }
}
