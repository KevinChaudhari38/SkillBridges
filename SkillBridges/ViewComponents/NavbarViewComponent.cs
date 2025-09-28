using Microsoft.AspNetCore.Mvc;

namespace SkillBridges.Components
{
    public class NavbarViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            if (User.IsInRole("Admin"))
                return View("Admin");

            else if (User.IsInRole("Professional"))
                return View("Professional");

            else if (User.IsInRole("Client"))
                return View("Client");

            else
                return View("Default");
        }
    }
}
