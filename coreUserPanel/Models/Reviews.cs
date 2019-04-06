using System;
using System.Collections.Generic;

namespace coreUserPanel.Models
{
    public partial class Reviews
    {
        public int UserDetailId { get; set; }
        public int MovieId { get; set; }
        public string Comment { get; set; }

        public Movies Movie { get; set; }
        public UserDetails UserDetail { get; set; }
    }
}
