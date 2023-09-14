using Microsoft.AspNetCore.Mvc;

namespace ShoppingAssistant.Controllers
{
    public class StoreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
