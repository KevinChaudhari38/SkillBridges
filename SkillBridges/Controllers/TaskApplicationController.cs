using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkillBridges.Models;
using SkillBridges.ViewModels;
using System.Data;

namespace SkillBridges.Controllers
{
    public class TaskApplicationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TaskApplicationController(IUnitOfWork unitOfWork, IMapper mapper)
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
        public IActionResult Create(TaskApplicationCreateViewModel vm,IFormFile file)
        {
            Console.WriteLine(vm.ProfessionalProfileId);
            if (!ModelState.IsValid)
            {
                foreach(var state in ModelState)
                {
                    foreach(var error in state.Value.Errors)
                    {
                        Console.WriteLine($"ModelState Error :- Key: {state.Key},Error : {error.ErrorMessage}");
                    }
                }
                return View(vm);
            }
            var model = _mapper.Map<TaskApplication>(vm);

            if (file != null && file.Length > 0)
            {
                var fileName=Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
                var filePath=Path.Combine("wwwroot/uploads/proofs", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                model.File = "/uploads/proofs/" + fileName;
            }
            
            model.Status = ApplicationStatus.Pending;
            _unitOfWork.TaskApplications.Insert(model);
            _unitOfWork.Save();
            return RedirectToAction("IndexByCategory", "Task", new { professionalProfileId = vm.ProfessionalProfileId });
        }

        public IActionResult Accept(string id)
        {
            var model = _unitOfWork.TaskApplications.GetById(id);
            var vm = _unitOfWork.Tasks.GetById(model.TaskId);
            if(model==null) return NotFound();
            var ToReject = _unitOfWork.TaskApplications.GetByTaskId(model.TaskId);
            foreach(var x in ToReject)
            {
                x.Status = ApplicationStatus.Rejected;
            }
            
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

        public IActionResult Delete(string id) { 
            var model=_unitOfWork.TaskApplications.GetById(id);
            if (model==null) return NotFound();
            var vm = _mapper.Map<TaskApplicationViewModel>(model);
            return View(vm);
        }
        [HttpPost]
        public IActionResult Delete(TaskApplicationViewModel model)
        {
            try
            {
                var vm = _unitOfWork.TaskApplications.GetById(model.TaskApplicationId);
                if (vm==null) return NotFound();
                _unitOfWork.TaskApplications.Delete(vm);
                _unitOfWork.Save();
                return RedirectToAction("IndexForProfessional", "Task", new { professionalProfileId = model.ProfessionalProfileId });
                
            }
            catch
            {
                return View(model);
            }
        }
        public IActionResult DeleteForClient(string id) {
            var model = _unitOfWork.TaskApplications.GetById(id);
            if (model == null) return NotFound();
            var vm = _mapper.Map<TaskApplicationViewModel>(model);
            return View(vm);
        }
        [HttpPost]
        public IActionResult DeleteForClient(TaskApplicationViewModel model)
        {
            try
            {
                var vm = _unitOfWork.TaskApplications.GetById(model.TaskApplicationId);
                if (vm == null) return NotFound();
                _unitOfWork.TaskApplications.Delete(vm);
                _unitOfWork.Save();
                return RedirectToAction("Index", "Task", new { clientId = model.ClientProfileId });

            }
            catch
            {
                return View(model);
            }
        }
    }
}
