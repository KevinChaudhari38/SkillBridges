using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SkillBridges.Models;
using SkillBridges.Repositories;
using SkillBridges.ViewModels;
using System.Security.Claims;

namespace SkillBridges.Controllers
{
    [Authorize]
    public class SkillController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SkillController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
           
            _mapper = mapper;

        }
        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            var skills = _unitOfWork.Skills.GetAll();
            var vm = _mapper.Map<List<SkillViewModel>>(skills);
            return View(vm);
        }
        [Authorize(Roles ="Professional,Admin")]
        public IActionResult IndexForProfessional(string professionalId)
        {
            if (string.IsNullOrEmpty(professionalId)) professionalId = User.FindFirstValue("ProfessionalProfileId");
            var skills = _unitOfWork.Skills.GetByProfessionalId(professionalId);
            Console.WriteLine("Professional Id :- " + professionalId);
            Console.WriteLine("Skill count :- " + skills.ToList().Count);
            if (!skills.Any())
            {
                return RedirectToAction("Assign", new { professionalId });
            }
            var vm = _mapper.Map<List<SkillViewModel>>(skills);
            vm.First().ProfessionalProfileId = professionalId;
            
            return View(vm);
        }

        [Authorize(Roles ="Admin")] 
        public IActionResult Details(string id)
        {
            var skill = _unitOfWork.Skills.GetById(id);
            var vm = _mapper.Map<SkillViewModel>(skill);
            return View(vm);

        }
        [HttpGet]
        [Authorize(Roles ="Professional")]
        public IActionResult CreateForProfessional(string professionalProfileId)
        {
            Console.WriteLine("Create for Professional Hit");
            Console.WriteLine("Professional Id :- " + professionalProfileId);
            var vm = new SkillCreateViewModel
            {
                ProfessionalProfileId = professionalProfileId,
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult CreateForProfessional(SkillCreateViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = _unitOfWork.Professionals.GetById(vm.ProfessionalProfileId);
            if (user == null) return NotFound();
           
            var skill = _mapper.Map<Skill>(vm);
            _unitOfWork.Skills.Insert(skill);
            _unitOfWork.Save();
            
            user.Skills.Add(new ProfessionalSkill
            {
                ProfessionalProfileId = vm.ProfessionalProfileId,
                SkillId = skill.SkillId,
            });
            _unitOfWork.Save();
            return RedirectToAction("IndexForProfessional", new {professionalId=vm.ProfessionalProfileId});
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SkillCreateViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            
            var skill = _mapper.Map<Skill>(vm);
            _unitOfWork.Skills.Insert(skill);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Edit(string id)
        {
            var skill = _unitOfWork.Skills.GetById(id);
            var vm = _mapper.Map<SkillViewModel>(skill);
            return View(vm);
        }
        [HttpPost]
        public IActionResult Edit(SkillViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var model = _unitOfWork.Skills.GetById(vm.SkillId);
            if (model == null) return NotFound();
            model.Name = vm.Name;
            model.Description = vm.Description;
            _unitOfWork.Skills.Update(model);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        [Authorize(Roles ="Professional,Admin")]
        public IActionResult Assign(string professionalId)
        {
            if (string.IsNullOrEmpty(professionalId)) professionalId = User.FindFirstValue("ProfessionalProfileId");
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
        [Authorize(Roles ="Admin")]
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
