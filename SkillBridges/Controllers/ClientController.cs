using Microsoft.AspNetCore.Mvc;

namespace SkillBridges.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
