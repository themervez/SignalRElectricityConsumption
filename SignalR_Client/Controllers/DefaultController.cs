using Microsoft.AspNetCore.Mvc;

namespace SignalR_Client.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
