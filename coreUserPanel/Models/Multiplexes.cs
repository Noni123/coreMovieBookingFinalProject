using System;
using System.Collections.Generic;

namespace coreUserPanel.Models
{
    public partial class Multiplexes
    {
        public Multiplexes()
        {
            Auditoriums = new HashSet<Auditoriums>();
            Movies = new HashSet<Movies>();
        }

        public int MultiplexId { get; set; }
        public string MultiplexName { get; set; }
        public string MultiplexDescription { get; set; }
        public int LocationId { get; set; }
        public string MultiplexImage { get; set; }

        public Locations Location { get; set; }
        public ICollection<Auditoriums> Auditoriums { get; set; }
        public ICollection<Movies> Movies { get; set; }
    }
}
