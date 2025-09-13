using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkillBridges.Models;
using SkillBridges.ViewModels;

namespace SkillBridges.Controllers
{
    public class ProfessionalController : Controller
    {
        private readonly IProfessionalRepository _professionalRepository;
        private readonly IMapper _mapper;
        public ProfessionalController(IProfessionalRepository professionalRepository,IMapper mapper)
        {
            _professionalRepository = professionalRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var profiles=_professionalRepository.GetAll();
            return View(profiles);
        }
        public IActionResult Details(string id)
        {
            var model=_professionalRepository.GetById(id);
            return View(model);
        }
        [HttpGet]
        public IActionResult Create(string userId)
        {
            var professionalProfile=new ProfessionalProfile { UserId=userId};
            return View(professionalProfile);
        }
        [HttpPost]
        public IActionResult Create(ProfessionalProfile profile)
        {
            _professionalRepository.insert(profile);
            return RedirectToAction("Details", "Home", new {id=profile.UserId});
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var user = _professionalRepository.GetByUserId(id);
            if (user == null)
            {
                return RedirectToAction("Create", new { userId = id });
            }
            var model=_mapper.Map<ProfessionalEditViewModel>(user);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(ProfessionalEditViewModel profile)
        {
            if (!ModelState.IsValid)
            {
                return View(profile);
            }
             var existing= _professionalRepository.GetById(profile.ProfessionalProfileId);
            _mapper.Map(profile, existing);
            _professionalRepository.update(existing);
            return RedirectToAction("Home","Details",new {id=existing.UserId});

        }
    }
}
