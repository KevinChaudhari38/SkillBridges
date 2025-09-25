using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkillBridges.Models;
using SkillBridges.ViewModels;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace SkillBridges.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Login()
        {
            return View();
        }
        
        public IActionResult Index()
        {
            var models = _unitOfWork.Users.GetAll();
            var vm=_mapper.Map<List<UserViewModel>>(models);
            return View(vm);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _unitOfWork.Users.GetByEmailAndPassword(model.Email, model.Password);
            if (user != null)
                return RedirectToAction("Details", new { id = user.Id });

            ModelState.AddModelError(string.Empty, "Invalid Email or Password");
            return View(model);
        }

        public IActionResult Details(string id)
        {
            var user = _unitOfWork.Users.GetById(id);
            if (user == null) return NotFound();

            if (user.Role == Models.UserRole.Client)
                return RedirectToAction("ClientDetails", new { id = user.Id });
            else if(user.Role == Models.UserRole.Admin)
                return RedirectToAction("Index");

           return RedirectToAction("ProfessionalDetails", new { id = user.Id });
        }

        public IActionResult ClientDetails(string id)
        {
            var vm = _unitOfWork.Clients.GetByUserId(id);
            if (vm == null)
                return RedirectToAction("Create", "Client", new { userId = id });

            var model = _mapper.Map<ClientDetailsViewModel>(vm);
            return View(model);
        }

        public IActionResult ProfessionalDetails(string id)
        {
            var vm = _unitOfWork.Professionals.GetByUserId(id);
            if (vm == null)
                return RedirectToAction("Create", "Professional", new { userId = id });

            var model = _mapper.Map<ProfessionalDetailsViewModel>(vm);
            
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new UserCreateViewModel
            {
                Roles = _unitOfWork.Users.GetRoles().ToList()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Roles = _unitOfWork.Users.GetRoles().ToList();
                return View(vm);
            }
            var existingUser = _unitOfWork.Users.GetByEmail(vm.Email);

            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "This email is already registered.");
                vm.Roles = _unitOfWork.Users.GetRoles().ToList();
                return View(vm);
            }
            var user = _mapper.Map<User>(vm);
        
            _unitOfWork.Users.insert(user);
            _unitOfWork.Save(); 
             return RedirectToAction("Details", new { id = user.Id });
            
            
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var user = _unitOfWork.Users.GetById(id);
            if (user == null) return NotFound();

            var vm = _mapper.Map<UserEditViewModel>(user);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserEditViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = _unitOfWork.Users.GetById(vm.Id);
            if (user == null) return NotFound();

            _mapper.Map(vm, user);
            _unitOfWork.Users.update(user);
            _unitOfWork.Save(); 
            return RedirectToAction("Details", new { id = user.Id });
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var model = _unitOfWork.Users.GetById(id);
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
                var model = _unitOfWork.Users.GetById(vm.Id);
                if (model == null) return NotFound();

                _unitOfWork.Users.delete(model);
                _unitOfWork.Save(); 
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
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
