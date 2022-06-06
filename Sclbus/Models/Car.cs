using System;
using System.Collections.Generic;

namespace Sclbus.Models
{
    public partial class Car
    {
        public Car()
        {
            Drivers = new HashSet<Driver>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Number { get; set; } = null!;
        public int SeatCount { get; set; }
        public int? DriverId { get; set; }

        public virtual Driver? Driver { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }
    }
}
