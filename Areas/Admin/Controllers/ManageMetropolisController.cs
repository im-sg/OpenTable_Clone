using Microsoft.AspNetCore.Mvc;
using OpenTable.Models;
using Microsoft.EntityFrameworkCore;
using OpenTable.Models.DataLayer;
using Microsoft.AspNetCore.Authorization;

namespace OpenTable.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManageMetropolisController : Controller
    {
        private readonly OpenTableDbContext _context;

        public ManageMetropolisController(OpenTableDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> List()
        {
            var metropolises = await _context.Metropolis
                .OrderBy(m => m.Name)
                .ToListAsync();

            return View(metropolises);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new Metropolis());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var metropolis = await _context.Metropolis.FindAsync(id);
            if (metropolis == null)
            {
                return NotFound();
            }

            ViewBag.Action = "Edit";
            return View(metropolis);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Metropolis metropolis)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Action = metropolis.MetropolisId == 0 ? "Add" : "Edit";
                return View(metropolis);
            }

            try
            {
                if (metropolis.MetropolisId == 0)
                {
                    _context.Metropolis.Add(metropolis);
                    TempData["SuccessMessage"] = $"{metropolis.Name} added successfully.";
                }
                else
                {
                    _context.Metropolis.Update(metropolis);
                    TempData["SuccessMessage"] = $"{metropolis.Name} updated successfully.";
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError(ex.Message, "An error occurred while saving. Please try again.");
                ViewBag.Action = metropolis.MetropolisId == 0 ? "Add" : "Edit";
                return View(metropolis);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var metropolis = await _context.Metropolis
                .FirstOrDefaultAsync(m => m.MetropolisId == id);

            if (metropolis == null)
            {
                return NotFound();
            }

            return View(metropolis);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(Metropolis metropolis)
        {
            _context.Metropolis.Remove(metropolis);
            TempData["SuccessMessage"] = $"{metropolis.Name} Deleted Successfully";
            _context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}