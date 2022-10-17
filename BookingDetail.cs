using System;
using System.Collections.Generic;

namespace AirWayTicketBooking.Models
{
    public partial class BookingDetail
    {
        public int Id { get; set; }
        public int PaymentId { get; set; }
        public string PassengerName { get; set; } = null!;
        public int PassengerAge { get; set; }

        public virtual Payment Payment { get; set; } = null!;
    }
}
