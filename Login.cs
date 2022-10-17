using System;
using System.Collections.Generic;

namespace AirWayTicketBooking.Models
{
    public partial class Login
    {
        public int LoginId { get; set; }
        public string? Password { get; set; }
        public string? EmailId { get; set; }
        public int? Id { get; set; }

        public virtual Registration? IdNavigation { get; set; }
    }
}
