using System;
using System.Collections.Generic;

namespace Sclbus.Models
{
    public partial class Ride
    {
        public Ride()
        {
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public int DriverId { get; set; }

        public virtual Driver Driver { get; set; } = null!;
        public virtual ICollection<Student>? Students { get; set; }
    }
}
