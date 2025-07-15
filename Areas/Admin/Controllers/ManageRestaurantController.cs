using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenTable.Models;
using OpenTable.Models.DataLayer;

namespace OpenTable.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManageRestaurantController : Controller
    {
        private OpenTableDbContext context { get; set; }
        public ManageRestaurantController(OpenTableDbContext ctx) => context = ctx;
        [HttpPost]
        public RedirectToActionResult FilterValues(string[] filter)
        {
            string ids = string.Join('-', filter);
            return RedirectToAction("List", new { ID = ids });
        }
        public ViewResult List(string id)
        {
            var filter = new Filter(id);
            ViewBag.Filter = filter;
            ViewBag.Cuisines = context.Cuisines.ToList();
            ViewBag.PriceRange = context.PriceRange.ToList();
            ViewBag.Metropolis = context.Metropolis.ToList();

            IQueryable<Restaurant> query = context.Restaurant
                .Include(t => t.Metropolis)
                .Include(t => t.Cuisines)
                .Include(t => t.PriceRange);

            if (filter.HasCuisines)
            {
                query = query.Where(t => t.CuisinesId == int.Parse(filter.CuisinesID));
            }
            if (filter.HasPriceRanges)
            {
                query = query.Where(t => t.PriceRangeId == int.Parse(filter.PriceRangeID));
            }
            if (filter.HasMetropolis)
            {
                query = query.Where(t => t.MetropolisId == int.Parse(filter.MetropolisID));
            }
            var restaurants = query.OrderBy(t => t.RestaurantName).ToList();
            var restaurantViewModel = new RestaurantViewModel
            {
                Cuisines = ViewBag.Cuisines,
                PriceRange = ViewBag.PriceRange,
                Metropolis = ViewBag.Metropolis,
                Restaurant = restaurants
            };
            return View(restaurantViewModel);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.action = "Add";
            ViewBag.Cuisines = context.Cuisines.OrderBy(g => g.Cuisine).ToList();
            ViewBag.PriceRange = context.PriceRange.OrderBy(g => g.PriceRanges).ToList();
            ViewBag.Metropolis = context.Metropolis.OrderBy(g => g.Name).ToList();
            return View("Edit", new Restaurant());
        }

        [HttpPost]
        public IActionResult Edit(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                if (restaurant.RestaurantId == 0)
                {
                    context.Restaurant.Add(restaurant);
                    TempData["SuccessMessage"] = $"{restaurant.RestaurantName} Added Successfully";
                }
                else
                {
                    context.Restaurant.Update(restaurant);
                    TempData["SuccessMessage"] = $"{restaurant.RestaurantName} Updated Successfully";
                }
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                ViewBag.action = (restaurant.RestaurantId == 0) ? "Add" : "Edit";
                ViewBag.Cuisines = context.Cuisines.OrderBy(g => g.Cuisine).ToList();
                ViewBag.PriceRange = context.PriceRange.OrderBy(g => g.PriceRanges).ToList();
                ViewBag.Metropolis = context.Metropolis.OrderBy(g => g.Name).ToList();
                return View("Edit", restaurant);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.action = "Edit";
            ViewBag.Cuisines = context.Cuisines.OrderBy(g => g.Cuisine).ToList();
            ViewBag.PriceRange = context.PriceRange.OrderBy(g => g.PriceRanges).ToList();
            ViewBag.Metropolis = context.Metropolis.OrderBy(g => g.Name).ToList();
            Restaurant restaurant = context.Restaurant
               .Include(p => p.Cuisines)
            .Include(p => p.PriceRange)
               .FirstOrDefault(p => p.RestaurantId == id) ?? new Restaurant();
            return View("Edit", restaurant);
        }


        [HttpPost]
        public IActionResult Delete(Restaurant restaurant)
        {
            context.Restaurant.Remove(restaurant);
            TempData["SuccessMessage"] = $"{restaurant.RestaurantName} Deleted Successfully";
            context.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Restaurant restaurant = context.Restaurant
                .FirstOrDefault(p => p.RestaurantId == id) ?? new Restaurant();
            return View(restaurant);
        }
    }
}
