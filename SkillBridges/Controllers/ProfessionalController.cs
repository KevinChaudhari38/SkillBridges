using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillBridges.Models;
using SkillBridges.Repositories;
using SkillBridges.ViewModels;

namespace SkillBridges.Controllers
{
    public class ProfessionalController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProfessionalController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var profiles = _unitOfWork.Professionals.GetAll();
            return View(profiles);
        }

        public IActionResult Details(string id)
        {
            var model = _unitOfWork.Professionals.GetById(id);
            var vm = _mapper.Map<ProfessionalDetailsViewModel>(model);
            return View(vm);
        }

        [HttpGet]
        public IActionResult Create(string userId)
        {
            var professionalProfile = new ProfessionalProfile { UserId = userId };
            return View(professionalProfile);
        }

        [HttpPost]
        public IActionResult Create(ProfessionalProfile profile)
        {
            _unitOfWork.Professionals.insert(profile);
            _unitOfWork.Save(); 
            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Professional")]
        public IActionResult Edit(string id)
        {
            var user = _unitOfWork.Professionals.GetByUserId(id);
            if (user == null)
            {
                return RedirectToAction("Create", new { userId = id });
            }
            var model = _mapper.Map<ProfessionalEditViewModel>(user);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ProfessionalEditViewModel profile)
        {
            if (!ModelState.IsValid)
            {
                return View(profile);
            }

            var existing = _unitOfWork.Professionals.GetById(profile.ProfessionalProfileId);
            _mapper.Map(profile, existing);
            _unitOfWork.Professionals.update(existing);
            _unitOfWork.Save(); 

            return RedirectToAction("Details", "Home", new { id = existing.UserId });
        }
    }
}
