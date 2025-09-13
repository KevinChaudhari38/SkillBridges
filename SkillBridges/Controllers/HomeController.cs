using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using SkillBridges.Models;
using SkillBridges.ViewModels;
using System.Diagnostics;

namespace SkillBridges.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProfessionalRepository _professionalRepository;
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger,IUserRepository userRepository,IMapper mapper,IClientRepository clientRepository,IProfessionalRepository professionRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
            _clientRepository = clientRepository;
            _professionalRepository = professionRepository;
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
                ModelState.AddModelError(string.Empty,"Invalid Email or Password");
                return View(model);
            }
        }

        
        public IActionResult Details(string id)
        {
            var user = _userRepository.GetById(id);
            if (user == null) return NotFound();
            var role = user.Role;
            if (user.Role == Models.UserRole.Client)
            {
                return RedirectToAction("ClientDetails",new {id=user.Id});
            }
            else
            {
                return RedirectToAction("ProfessionalDetails", new { id = user.Id });
            }
        }
        public IActionResult ClientDetails(string id)
        {
            var vm= _clientRepository.GetByUserId(id);
            if(vm == null)
            {
                return RedirectToAction("Create", "Client", new { userId = id });
            }
            var model=_mapper.Map<ClientDetailsViewModel>(vm);
            return View(model);
        }
        public IActionResult ProfessionalDetails(string id)
        {
            var vm=_professionalRepository.GetByUserId(id);
            if (vm == null)
            {
                return RedirectToAction("Create", "Professional", new { userId = id });
            }
            var model = _mapper.Map<ProfessionalDetailsViewModel>(vm);
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var vm = new UserCreateViewModel
            {
                Roles = _userRepository.GetRoles().ToList()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserCreateViewModel vm)
        {
            
            if (!ModelState.IsValid)
            {
               
                vm.Roles = _userRepository.GetRoles().ToList(); 
                return View(vm);
            }
            
            var user=_mapper.Map<User>(vm);
            try
            {
                _userRepository.insert(user);
                return RedirectToAction("Details", new { id = user.Id });
            }
            catch
            {
                var model = new UserCreateViewModel
                {
                    Roles = _userRepository.GetRoles().ToList()
                };
                ModelState.AddModelError("Email", "This email is already registered.");
                return View(model);
            }
            
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var user=_userRepository.GetById(id);
            if(user == null) return NotFound();
            var vm=_mapper.Map<UserEditViewModel>(user);
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserEditViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var user = _userRepository.GetById(vm.Id);
            if(user == null) return NotFound(); 

            _mapper.Map(vm,user);
            _userRepository.update(user);
            return RedirectToAction("Details", new { id=user.Id});
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var model=_userRepository.GetById(id);
            if (model == null) return NotFound();
            var vm = _mapper.Map<UserViewModel>(model);
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(UserViewModel vm)
        {
            try
            {
                var model = _userRepository.GetById(vm.Id);
                if (model == null) return NotFound();
                _userRepository.delete(model);
                return RedirectToAction("Login");
            }
            catch
            {
                return View(vm);
            }
            
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
