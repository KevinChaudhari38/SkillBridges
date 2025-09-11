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
        public IActionResult Details(int id)
        {
            var model=_professionalRepository.GetById(id);
            return View(model);
        }
        [HttpGet]
        public IActionResult Create(int userId)
        {
            var professionalProfile=new ProfessionalProfile { UserId=userId};
            return View(professionalProfile);
        }
        [HttpPost]
        public IActionResult Create(ProfessionalProfile profile)
        {
            _professionalRepository.insert(profile);
            return RedirectToAction("Details", new {id=profile.UserId});
        }
        [HttpGet]
        public IActionResult Edit(int id) {
            var model = _professionalRepository.GetById(id);
            var vm=_mapper.Map<UserViewModel>(model);
            return View(vm);
        }
    }
}
