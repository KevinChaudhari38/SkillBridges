using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkillBridges.Models;
using SkillBridges.Repositories;
using SkillBridges.ViewModels;

namespace SkillBridges.Controllers
{
    public class RatingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RatingController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper= mapper;
        }
        public IActionResult Index(string ProfessionalProfileId)
        {
            var vm=_unitOfWork.Ratings.GetByProfessionalId(ProfessionalProfileId);
            var model = _mapper.Map<List<RatingViewModel>>(vm);
            return View(model);
        }
        public IActionResult Create(string TaskId)
        {
            var task=_unitOfWork.Tasks.GetById(TaskId);
            if(task == null || task.Status!=Models.TaskStatus.Completed)
            {
                return BadRequest("Task Not Completed or not found");
            }
            var vm=new RatingCreateViewModel
            {
                TaskId = TaskId,
                ProfessionalProfileId=task.ProfessionalProfileId,
                ClientProfileId=task.ClientProfileId,
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Create(RatingCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var vm = _mapper.Map<Rating>(model);
            _unitOfWork.Ratings.Insert(vm);
            var task=_unitOfWork.Tasks.GetById(model.TaskId);
            if (task == null) return NotFound();
            task.Status = Models.TaskStatus.Done;
            _unitOfWork.Save();
            return RedirectToAction("Index", "Task", new {clientId=model.ClientProfileId});
        }
    }
}
