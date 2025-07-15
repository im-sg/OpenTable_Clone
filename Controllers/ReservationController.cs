using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenTable.Models;
using OpenTable.Models.DataLayer;
using System.Text.Json;

namespace OpenTable.Controllers
{
    public class ReservationController : Controller
    {
        private OpenTableDbContext _context;
        public ReservationController(OpenTableDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult FindATable() => Content("Manage Reservation");
        public ViewResult Reservations(ReservationViewModel reservationView)
        {
            var ActiveFilters = new Filter($"{reservationView.ActiveMetropolis}-{reservationView.ActivePriceRange}-{reservationView.ActiveCuisines}");
            var session = new Sessions(HttpContext.Session);
            session.SetActiveMetropolis(reservationView.ActiveMetropolis);
            session.SetActivePriceRange(reservationView.ActivePriceRange);
            session.SetActiveCuisines(reservationView.ActiveCuisines);
            IQueryable<Restaurant> query = _context.Restaurant
               .Include(t => t.Metropolis)
               .Include(t => t.Cuisines)
               .Include(t => t.PriceRange);
            if (reservationView.ActivePriceRange != "all")
            {
                query = query.Where(r => r.PriceRange.PriceRangeId.ToString() == reservationView.ActivePriceRange.ToLower());
            }
            if (reservationView.ActiveMetropolis != "all")
            {
                query = query.Where(r => r.Metropolis.MetropolisId.ToString() == reservationView.ActiveMetropolis.ToLower());
            }
            if (reservationView.ActiveCuisines != "all")
            {
                query = query.Where(r => r.Cuisines.CuisinesId.ToString() == reservationView.ActiveCuisines.ToLower());
            }
            ViewBag.Filter = ActiveFilters;
            ViewBag.Cuisines = _context.Cuisines.ToList();
            ViewBag.PriceRange = _context.PriceRange.ToList();
            ViewBag.Metropolis = _context.Metropolis.ToList();
            var restaurants = query.OrderBy(t => t.RestaurantName).ToList();
            ReservationViewModel ViewModel = new ReservationViewModel
            {
                Restaurants = restaurants,
                Metropolis = ViewBag.Metropolis,
                PricesRange = ViewBag.PriceRange,
                Cuisines = ViewBag.Cuisines,
                ActiveMetropolis = session.GetActiveMetropolis(),
                ActivePriceRange = session.GetActivePriceRange(),
                ActiveCuisines = session.GetActiveCuisines(),
            };
            return View(ViewModel);
        }
        public IActionResult Detail(int id, DateTime? date)
        {
            var restaurant = GetRestaurantDetails(id);
            if (restaurant == null) return NotFound();

            var selectedDate = date ?? DateTime.Today;

            SetViewBagData(restaurant, selectedDate);

            var viewModel = BuildReservationViewModel(restaurant);

            return View(viewModel);
        }

        private Restaurant? GetRestaurantDetails(int id)
        {
            return _context.Restaurant
                .Include(r => r.Cuisines)
                .Include(r => r.Metropolis)
                .Include(r => r.PriceRange)
                .FirstOrDefault(r => r.RestaurantId == id);
        }

        private void SetViewBagData(Restaurant restaurant, DateTime selectedDate)
        {
            ViewBag.AvailableDates = Enumerable.Range(0, 7)
                .Select(offset => DateTime.Today.AddDays(offset))
                .ToList();

            ViewBag.AvailableSlots = GetAvailableTimeSlots(restaurant.OpenHours, restaurant.RestaurantId, selectedDate);
            ViewBag.SelectedDate = selectedDate;
        }

        private ReservationViewModel BuildReservationViewModel(Restaurant restaurant)
        {
            var session = new Sessions(HttpContext.Session);

            return new ReservationViewModel
            {
                Restaurant = restaurant,
                Metro = restaurant.Metropolis,
                Cuisine = restaurant.Cuisines,
                PriceRangs = restaurant.PriceRange,
                ActiveMetropolis = session.GetActiveMetropolis(),
                ActivePriceRange = session.GetActivePriceRange(),
                ActiveCuisines = session.GetActiveCuisines()
            };
        }

        [HttpPost]
        public IActionResult TableReservationHold(int restaurantId, DateTime date, int timeHour, int guests)
        {
            var session = new Sessions(HttpContext.Session);

            // Add reservation to session cart
            var cart = HttpContext.Session.GetObject<List<Reservation>>("Cart") ?? new List<Reservation>();
            var newReservation = CreateReservation(restaurantId, date, timeHour, guests);
            cart.Add(newReservation);
            HttpContext.Session.SetObject("Cart", cart);

            // Update cookie cart
            var cookieCart = GetCookieCart();
            cookieCart.Add(new ReservationItem
            {
                RestaurantId = restaurantId,
                Date = date,
                Time = timeHour,
                People = guests
            });
            SetCookieCart(cookieCart);

            TempData["SuccessMessage"] = "Table held successfully!";

            return RedirectToAction("Reservations", "Reservation", new
            {
                ActiveMetropolis = session.GetActiveMetropolis(),
                ActivePriceRange = session.GetActivePriceRange(),
                ActiveCuisines = session.GetActiveCuisines()
            });
        }

        private Reservation CreateReservation(int restaurantId, DateTime date, int timeHour, int guests)
        {
            return new Reservation
            {
                RestaurantId = restaurantId,
                ReservationDate = date,
                TimeSlot = new TimeSpan(timeHour, 0, 0),
                NumberOfGuests = guests,
                Status = "Held"
            };
        }

        private List<ReservationItem> GetCookieCart()
        {
            if (Request.Cookies.TryGetValue("ReservationStagingArea", out var cookieData))
            {
                return JsonSerializer.Deserialize<List<ReservationItem>>(cookieData) ?? new List<ReservationItem>();
            }
            return new List<ReservationItem>();
        }

        private void SetCookieCart(List<ReservationItem> cookieCart)
        {
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                IsEssential = true
            };
            Response.Cookies.Append("ReservationStagingArea", JsonSerializer.Serialize(cookieCart), options);
        }

        public IActionResult Cart()
        {
            var session = new Sessions(HttpContext.Session);

            var viewModel = new ReservationViewModel
            {
                Reservation = LoadCartFromCookies(),
                ActiveMetropolis = session.GetActiveMetropolis(),
                ActiveCuisines = session.GetActiveCuisines(),
                ActivePriceRange = session.GetActivePriceRange()
            };

            return View(viewModel);
        }

        private List<Reservation> LoadCartFromCookies()
        {
            var cart = new List<Reservation>();

            if (!Request.Cookies.TryGetValue("ReservationStagingArea", out var cookieData))
                return cart;

            var cookieCart = JsonSerializer.Deserialize<List<ReservationItem>>(cookieData) ?? new List<ReservationItem>();

            foreach (var item in cookieCart)
            {
                var restaurant = _context.Restaurant
                    .Include(r => r.Metropolis)
                    .Include(r => r.Cuisines)
                    .Include(r => r.PriceRange)
                    .FirstOrDefault(r => r.RestaurantId == item.RestaurantId);

                if (restaurant == null) continue;

                cart.Add(new Reservation
                {
                    RestaurantId = item.RestaurantId,
                    ReservationDate = item.Date,
                    TimeSlot = new TimeSpan(item.Time, 0, 0),
                    NumberOfGuests = item.People,
                    Status = "Held",
                    Restaurant = restaurant
                });
            }

            HttpContext.Session.SetObject("Cart", cart);
            return cart;
        }

        public IActionResult ClearCartItem(int id)
        {
            var cart = HttpContext.Session.GetObject<List<Reservation>>("Cart") ?? new List<Reservation>();
            var itemToRemove = cart.FirstOrDefault(r => r.RestaurantId == id);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                HttpContext.Session.SetObject("Cart", cart);
            }
            DeleteFromCookieCart(id);

            return RedirectToAction("Cart");
        }

        private void DeleteFromCookieCart(int restaurantId)
        {
            if (!Request.Cookies.TryGetValue("ReservationStagingArea", out var cookieCartRaw)) return;

            var cookieCart = JsonSerializer.Deserialize<List<ReservationItem>>(cookieCartRaw)
                            ?? new List<ReservationItem>();

            var cookieItem = cookieCart.FirstOrDefault(r => r.RestaurantId == restaurantId);
            if (cookieItem != null)
            {
                cookieCart.Remove(cookieItem);

                var options = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(7),
                    IsEssential = true
                };

                Response.Cookies.Append("ReservationStagingArea", JsonSerializer.Serialize(cookieCart), options);
            }
        }


        [HttpPost]
        public IActionResult CompleteReservation()
        {
            var cart = HttpContext.Session.GetObject<List<Reservation>>("Cart");
            var session = new Sessions(HttpContext.Session);
            if (cart is null || cart.Count == 0)
            {
                TempData["SuccessMessage"] = "No reservations to confirm.";
                return RedirectToAction("Cart");
            }

            foreach (var reservation in cart)
            {
                reservation.Status = "Confirmed";
                reservation.ReservationMadeAt = DateTime.Now;
                reservation.Restaurant = null!;
                _context.Reservations.Add(reservation);
            }

            _context.SaveChanges();
            HttpContext.Session.Remove("Cart");
            Response.Cookies.Delete("ReservationStagingArea");

            TempData["SuccessMessage"] = "Reservations confirmed successfully!";
            return RedirectToAction("Reservations", "Reservation", new
            {
                ActiveMetropolis = session.GetActiveMetropolis(),
                ActivePriceRange = session.GetActivePriceRange(),
                ActiveCuisines = session.GetActiveCuisines()
            });
        }

        public List<string> GetAvailableTimeSlots(string openHours, int restaurantId, DateTime selectedDate)
        {
            if (!TryParseOpenHours(openHours, out var start, out var end))
                return new List<string>();

            var bookedTimes = GetBookedTimeSlots(restaurantId, selectedDate);

            var availableSlots = new List<string>();
            for (var time = start; time < end; time = time.Add(TimeSpan.FromHours(1)))
            {
                if (!bookedTimes.Contains(time))
                    availableSlots.Add(time.ToString(@"hh\:mm"));
            }

            return availableSlots;
        }

        private bool TryParseOpenHours(string openHours, out TimeSpan start, out TimeSpan end)
        {
            start = end = default;

            if (string.IsNullOrWhiteSpace(openHours)) return false;

            var parts = openHours.Split('-');
            return parts.Length == 2 &&
                   TimeSpan.TryParse(parts[0], out start) &&
                   TimeSpan.TryParse(parts[1], out end);
        }

        private HashSet<TimeSpan> GetBookedTimeSlots(int restaurantId, DateTime selectedDate)
        {
            return _context.Reservations
                .Where(r => r.RestaurantId == restaurantId && r.ReservationDate.Date == selectedDate.Date)
                .Select(r => r.TimeSlot)
                .ToHashSet();
        }
    }
}
