using System;
using System.Collections.Generic;

namespace AirWayTicketBooking.Models
{
    public partial class Payment
    {
        public Payment()
        {
            BookingDetails = new HashSet<BookingDetail>();
        }

        public int Id { get; set; }
        public int FlightId { get; set; }
        public int UserId { get; set; }
        public int PaymentType { get; set; }
        public string PaymentDetails { get; set; } = null!;
        public string Payee { get; set; } = null!;
        public int PassengerCount { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PaiedOn { get; set; }

        public virtual Flight Flight { get; set; } = null!;
        public virtual Registration User { get; set; } = null!;
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    }
}
