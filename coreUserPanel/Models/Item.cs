using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreUserPanel.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public DateTime ItemAddedDate { get; set; }
        public Movies Movies { get; set; }
    }
}
