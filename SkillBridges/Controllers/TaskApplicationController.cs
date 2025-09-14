using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkillBridges.Models;
using SkillBridges.ViewModels;

namespace SkillBridges.Controllers
{
    public class TaskApplicationController : Controller
    {
        private readonly IUnitOfWork2 _unitOfWork;
        private readonly IMapper _mapper;
        public TaskApplicationController(IUnitOfWork2 unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IActionResult Index(string professionalProfileId)
        {
            var model=_unitOfWork.TaskApplications.GetByProfessionalId(professionalProfileId);
            var vm=_mapper.Map<List<TaskApplicationViewModel>>(model);
            return View(vm);
        }
        public IActionResult ClientRequests(string taskId)
        {
            var model = _unitOfWork.TaskApplications.GetByTaskId(taskId);
            var vm = _mapper.Map<List<TaskApplicationViewModel>>(model);
            return View(vm);
        }
        public IActionResult Create(string taskId,string clientId,string professionalId)
        {
            var vm = new TaskApplicationCreateViewModel
            {
                TaskId = taskId,
                ClientProfileId = clientId,
                ProfessionalProfileId = professionalId
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Create(TaskApplicationCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var model = _mapper.Map<TaskApplication>(vm);
            model.Status = ApplicationStatus.Pending;
            _unitOfWork.TaskApplications.Insert(model);
            _unitOfWork.Save();
            return RedirectToAction("IndexForClient", "Task", new { professionalProfileId = vm.ProfessionalProfileId });
        }

        public IActionResult Accept(string id)
        {
            var model = _unitOfWork.TaskApplications.GetById(id);
            var vm = _unitOfWork.Tasks.GetById(model.TaskId);
            if(model==null) return NotFound();

            model.Status=ApplicationStatus.Accepted;
            vm.ProfessionalProfileId = model.ProfessionalProfileId;
            vm.Status = Models.TaskStatus.Assigned;
            _unitOfWork.Save();
            return RedirectToAction("ClientRequests", new {taskId=model.TaskId});
        }
        public IActionResult Reject(string id)
        {
            var model = _unitOfWork.TaskApplications.GetById(id);
            if (model == null) return NotFound();
            model.Status = ApplicationStatus.Rejected;
            _unitOfWork.Save();
            return RedirectToAction("ClientRequests", new { taskId = model.TaskId });
        }
    }
}
