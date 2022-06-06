using System;
using System.Collections.Generic;

namespace Sclbus.Models
{
    public partial class Driver
    {
        public Driver()
        {
            Cars = new HashSet<Car>();
            Rides = new HashSet<Ride>();            
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;        
        public string Phone { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string License { get; set; } = null!;
        public int? CarId { get; set; }

        public virtual Car? Car { get; set; }        
        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<Ride> Rides { get; set; }
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

    }
}
