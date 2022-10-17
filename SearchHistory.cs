using System;
using System.Collections.Generic;

namespace AirWayTicketBooking.Models
{
    public partial class SearchHistory
    {
        public int Id { get; set; }
        public bool? TripType { get; set; }
        public string FromSource { get; set; } = null!;
        public string Destination { get; set; } = null!;
        public DateTime DepatureDate { get; set; }
        public DateTime? ReturnTripDate { get; set; }
        public int PassengersCount { get; set; }
    }
}
