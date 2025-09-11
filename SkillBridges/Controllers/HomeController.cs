using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkillBridges.Models;
using SkillBridges.ViewModels;
using System.Diagnostics;

namespace SkillBridges.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger,IUserRepository userRepository,IMapper mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = _userRepository.GetByEmailAndPassword(model.Email, model.Password);
            if (user != null)
            {
                return RedirectToAction("Details", new {id=user.Id});
            }
            else
            {
                return RedirectToAction("Create");
            }
        }

        
        public IActionResult Details(int id)
        {
            var user = _userRepository.GetById(id);
            return View(user);
        }
       
        [HttpGet]
        public IActionResult Create()
        {
            var vm = new ProfessionalUserCreateViewModel
            {
                Roles = _userRepository.GetRoles().ToList()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProfessionalUserCreateViewModel vm)
        {
            
            if (!ModelState.IsValid)
            {
               
                vm.Roles = _userRepository.GetRoles().ToList(); 
                return View(vm);
            }
            
            var user=_mapper.Map<User>(vm);
            _userRepository.insert(user);

            return RedirectToAction("Details", new {id=user.Id});
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
