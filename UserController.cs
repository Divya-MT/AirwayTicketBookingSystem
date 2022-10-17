using AirWayTicketBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NuGet.Protocol.Plugins;
using Microsoft.EntityFrameworkCore;

namespace AirWayTicketBooking.Controllers
{
    public class UserController : Controller
    {
        private readonly AirWayTicketBookContext _db;
        private readonly ISession session;
        public UserController(AirWayTicketBookContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;

            session = httpContextAccessor.HttpContext.Session;
        }
        public IActionResult Index()
        {
            return RedirectToAction("SearchHistory", "User");
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Logout()
        {
            return RedirectToAction("Login");
        }
        [HttpPost]
        public IActionResult Userlogin(Login login)
        {
            var result = (from i in _db.Registrations
                          where i.Password == login.Password && i.Email == login.EmailId
                          select i).SingleOrDefault();
            if (result != null)
            {
                HttpContext.Session.SetString("username", result.Email);
                HttpContext.Session.SetInt32("UserId", result.Id);
                return RedirectToAction("SearchHistory", "User");
            }
            return null;

        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Registration registration)
        {
            _db.Registrations.Add(registration);
            _db.SaveChanges();
            return RedirectToAction("Login");
        }

        public IActionResult Flight()
        {
            ViewBag.BookedSeat = 1;
            //ViewBag.Payments = _db.Payments.ToList();
            return View(_db.Flights.Include(i=>i.Payments).ToList());
        }
        [HttpGet]
        public IActionResult TicketBook(int id, int bookedseats)
        {
            ViewBag.TotalFare = 0;
            ViewBag.BookedSeat = bookedseats;
            ViewBag.FlightId = id;
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId"); ;
            var flight = _db.Flights.Find(id);
            if (flight == null) return NotFound();
            else
            {
                decimal totalRate = Convert.ToDecimal(flight.Rate * bookedseats);
                decimal taxAmount = Convert.ToDecimal((flight.Tax * totalRate) / 100);
                decimal totalAmount = Convert.ToDecimal(totalRate + taxAmount + flight.Charges);
                ViewBag.TotalFare = decimal.Round(totalAmount, 2, MidpointRounding.AwayFromZero);
                return View(flight);
            }
        }

        [HttpGet]
        public IActionResult SearchHistory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchResult(SearchHistory search)
        {
            _db.SearchHistories.Add(search);
            _db.SaveChanges();

            ViewBag.BookedSeat = 1;

            //var flights = (from i in _db.Flights
            //               where i.LeaveFrom == search.FromSource && i.GoingTo == search.Destination && i.DateOfDepature.Date == search.DepatureDate.Date
            //               select i).ToList();
            var flights = _db.Flights.Include(i=>i.Payments).Where(w => w.LeaveFrom == search.FromSource && w.GoingTo == search.Destination
            && w.DateOfDepature.Date == search.DepatureDate.Date).ToList();
            if (flights != null && flights.Count > 0)
            {
                ViewBag.BookedSeat = search.PassengersCount;
                return View("Flight", flights);
            }
            else
                return RedirectToPage("/Shared/Error");

        }
        public IActionResult Payment(int passengerCount,decimal total, int tax, 
            decimal charges,decimal rate, int userId, int flightId)
        {
            ViewBag.PassengerCount = passengerCount;
            ViewBag.Total = total;
            ViewBag.Tax = tax;
            ViewBag.Charges = charges;
            ViewBag.Rate = rate;
            ViewBag.UserId = userId;
            ViewBag.FlightId = flightId;
            return View();
        }

        [HttpPost]
        public IActionResult Payment(Payment payment)
        {
            _db.Payments.Add(payment);
            _db.SaveChanges();
            int id = payment.Id;
            ViewBag.PaymentId = id;
            ViewBag.PassengerCount=payment.PassengerCount;
            HttpContext.Session.SetInt32("PassengerCount", payment.PassengerCount);
            HttpContext.Session.SetInt32("PaymentId", id);
            return RedirectToAction("PassengerDetails");
          
        }
        public IActionResult PassengerDetails(int paymentId)
        {
            ViewBag.PaymentId = HttpContext.Session.GetInt32("PaymentId");
            ViewBag.PassengerCount = HttpContext.Session.GetInt32("PassengerCount");
            return View();
        }

        [HttpPost]
        public IActionResult PassengerDetails(BookingDetail detail)
        {
            _db.BookingDetails.Add(detail);
            _db.SaveChanges();
            int passengerCount = HttpContext.Session.GetInt32("PassengerCount").Value;
            HttpContext.Session.SetInt32("PaymentId", detail.PaymentId);
            if (passengerCount > 0)
            {
                return RedirectToAction("PassengerDetails");
            }
            else
                return View("BookedMessage");

        }

        public IActionResult BookedDetails(int id)
        {
            Flight details= _db.Flights.Include(i => i.Payments).FirstOrDefault(w => w.FlightId==id);
            //_db.Flights.Include(i=>i.Payments).Where(x => x.FlightId == flightId).FirstOrDefault();
            return View(details);
        }
        public IActionResult Cancel(int id, int flightId)
        {
            List<BookingDetail> booking = _db.BookingDetails.Where(w => w.PaymentId == id).ToList(); 
            if(booking!=null && booking.Count>0)
            {
                foreach(BookingDetail detail in booking)
                {
                    _db.BookingDetails.Remove(detail);
                    _db.SaveChanges();
                }
            }
            Payment payment = _db.Payments.Find(id);
            _db.Payments.Remove(payment);
            _db.SaveChanges();
            Flight details = _db.Flights.Include(i => i.Payments).Where(x => x.FlightId == flightId).FirstOrDefault();
            return View("BookedDetails",details);
        }

    }
}
