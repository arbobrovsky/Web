using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entityes
{
    public class Contact
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime TimeWorkStart { get; set; }
        public DateTime TimeWorkDown { get; set; }
        public string WorkWeek { get; set; }
        public double GeoLong { get; set; } // долгота - для карт google
        public double GeoLat { get; set; } // широта - для карт google

    }
}
