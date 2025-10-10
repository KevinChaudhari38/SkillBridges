using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillBridges.Models;
using SkillBridges.Repositories;
using SkillBridges.ViewModels;
using System.Data;
using System.Security.Claims;
using System.Threading.Tasks;

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
        [Authorize(Roles ="Admin,Professional")]
        public IActionResult Index(string professionalProfileId)
        {
            professionalProfileId ??= User.FindFirstValue("ProfessionalProfileId");
            var model=_unitOfWork.TaskApplications.GetByProfessionalId(professionalProfileId);
            var vm=_mapper.Map<List<TaskApplicationViewModel>>(model);
            return View(vm);
        }
        [Authorize(Roles ="Client,Admin")]
        public IActionResult ClientRequests(string taskId)
        {
            var model = _unitOfWork.TaskApplications.GetByTaskId(taskId);
            var vm = _mapper.Map<List<TaskApplicationViewModel>>(model);
            return View(vm);
        }
        [Authorize(Roles ="Professional")]
        public IActionResult Create(string taskId,string clientId,string professionalId)
        {
            professionalId ??=User.FindFirstValue("ProfessionalProfileId");
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

            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("FilePath", "Please upload a PDF,PPT,.MP4 or Zip, go back and try again");
                if (vm != null) return View(vm);
                var um = new TaskApplicationCreateViewModel
                {
                    TaskId = null,
                    ProfessionalProfileId = null,
                    ClientProfileId=null,
                };
                return View(um);
            }

            var model = _mapper.Map<TaskApplication>(vm);

            if (file != null && file.Length > 0)
            {
                var allowedExtensions = new[] { ".pdf", ".ppt", ".pptx", ".mp4"};
                var extension = Path.GetExtension(file.FileName).ToLower();
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("File", "Only PDF,PPT and MP4 files are allowed.");
                    return View(vm);
                }
                var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                var uploadsPath = Path.Combine(wwwRootPath, "uploads", "proofs");

                if (!Directory.Exists(uploadsPath))
                    Directory.CreateDirectory(uploadsPath);
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
        [Authorize(Roles ="Client")]
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
        [Authorize(Roles = "Client")]
        public IActionResult Reject(string id)
        {
            var model = _unitOfWork.TaskApplications.GetById(id);
            if (model == null) return NotFound();
            model.Status = ApplicationStatus.Rejected;
            _unitOfWork.Save();
            return RedirectToAction("ClientRequests", new { taskId = model.TaskId });
        }
        [Authorize(Roles = "Professional")]
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
                if (!string.IsNullOrEmpty(vm.File))
                {

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", vm.File.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                _unitOfWork.TaskApplications.Delete(vm);
                _unitOfWork.Save();
                return RedirectToAction("IndexForProfessional", "Task", new { professionalProfileId = model.ProfessionalProfileId });
                
            }
            catch
            {
                return View(model);
            }
        }
        [Authorize(Roles = "Client")]
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
                if (!string.IsNullOrEmpty(vm.File))
                {

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", vm.File.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
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
