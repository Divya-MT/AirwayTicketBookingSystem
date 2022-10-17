using System;
using System.Collections.Generic;

namespace AirWayTicketBooking.Models
{
    public partial class Registration
    {
        public Registration()
        {
            Logins = new HashSet<Login>();
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }

        public virtual ICollection<Login> Logins { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
