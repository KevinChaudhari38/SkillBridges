using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkillBridges.Models;
using SkillBridges.ViewModels;

namespace SkillBridges.Controllers
{
    public class SkillController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SkillController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
           
            _mapper = mapper;

        }
        public IActionResult Index()
        {
            var skills = _unitOfWork.Skills.GetAll();
            var vm = _mapper.Map<List<SkillViewModel>>(skills);
            return View(vm);
        }
        public IActionResult IndexForProfessional(string professionalId)
        {
            var skills = _unitOfWork.Skills.GetByProfessionalId(professionalId);
            var vm = _mapper.Map<List<SkillViewModel>>(skills);
            return View(vm);
        }
        
        public IActionResult Details(string id)
        {
            var skill = _unitOfWork.Skills.GetById(id);
            var vm = _mapper.Map<SkillViewModel>(skill);
            return View(vm);

        }
        [HttpGet]
       
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SkillViewModel vm)
        {
            
            var skill = _mapper.Map<Skill>(vm);
            _unitOfWork.Skills.Insert(skill);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Assign(string professionalId)
        {
            var allSkills = _unitOfWork.Skills.GetAll();
            Console.WriteLine(professionalId);
            foreach(var skill in allSkills)
            {
                Console.WriteLine(skill);
            }
            var vm = new AssignSkillViewModel
            {
                ProfessionalProfileId = professionalId,
                AvailableSkills = _mapper.Map<List<SkillViewModel>>(allSkills)
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Assign(AssignSkillViewModel vm)
        {
            

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                var professional = _unitOfWork.Professionals.GetById(vm.ProfessionalProfileId);
                    

                if (professional == null)
                {
                    return NotFound();
                }

                var selectedSkills = vm.SelectedSkillIds ?? new List<string>();
                
                var skills = _unitOfWork.Skills.GetByProfessionalId(vm.ProfessionalProfileId);
                var existingSkills = skills.Select(s=>s.SkillId).ToList();
                
                var toAdd = selectedSkills.Except(existingSkills).ToList();
                
                var toRemove = existingSkills.Except(selectedSkills).ToList();
                
                foreach (var skillId in toRemove)
                {
                    var toDelete = professional.Skills.FirstOrDefault(ps => ps.SkillId == skillId);
                    if (toDelete != null)
                    {
                        professional.Skills.Remove(toDelete);
                    }
                }
                Console.WriteLine("Skills removed successfully");

                foreach (var skillId in toAdd)
                {
                    professional.Skills.Add(new ProfessionalSkill
                    {
                        ProfessionalProfileId = vm.ProfessionalProfileId,
                        SkillId = skillId
                    });
                }

         
                _unitOfWork.Save();
                var user=_unitOfWork.Professionals.GetById(vm.ProfessionalProfileId).User;
                return RedirectToAction("ProfessionalDetails", "Home", new { id = user.Id});
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in Assign: {ex.Message}");
                return View(vm);
            }
        }

        public IActionResult Delete(string id)
        {
            var skill = _unitOfWork.Skills.GetById(id);
            var vm = _mapper.Map<SkillViewModel>(skill);
            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var skill = _unitOfWork.Skills.GetById(id);
            if (skill != null)
            {
                _unitOfWork.Skills.Delete(skill);
                _unitOfWork.Save();
            }
            return RedirectToAction("Index");
        }

    }
}
