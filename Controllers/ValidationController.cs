using Microsoft.AspNetCore.Mvc;
using OpenTable.Models;
using OpenTable.Models.DataLayer;

namespace OpenTable.Controllers
{
    public class ValidationController : Controller
    {
        private OpenTableDbContext context;
        public ValidationController(OpenTableDbContext ctx) => context = ctx;

        public JsonResult CheckEmail(string emailAddress)
        {
            string msg = Check.EmailExists(context, emailAddress);
            if (string.IsNullOrEmpty(msg))
            {
                TempData["okEmail"] = true;
                return Json(true);
            }
            else return Json(msg);
        }
    }
}
