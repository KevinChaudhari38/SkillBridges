using Microsoft.AspNetCore.Mvc;

namespace SkillBridges.Controllers
{
    public class SkillController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
