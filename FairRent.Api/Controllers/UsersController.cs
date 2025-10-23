using Microsoft.AspNetCore.Mvc;

namespace FairRent.Api.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
