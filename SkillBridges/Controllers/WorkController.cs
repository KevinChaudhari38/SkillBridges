using Microsoft.AspNetCore.Mvc;
using SkillBridges.Models;

namespace SkillBridges.Controllers
{
    public class WorkController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public WorkController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(string TaskId)
        {
            var vm = _unitOfWork.WorkSubmissions.GetByTaskId(TaskId);
            return View(vm);
        }
        public IActionResult Details(string id)
        {
            var vm=_unitOfWork.WorkSubmissions.GetById(id);
            return View(vm);
        }
        public IActionResult Create(string TaskId)
        {
            var task=_unitOfWork.Tasks.GetById(TaskId);
            Console.WriteLine("Prfessional Id :- " + task.ProfessionalProfileId);
            var vm = new WorkSubmission
            {
                TaskId = TaskId,
                ProfessionalProfileId = task.ProfessionalProfileId,
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Create(WorkSubmission workSubmission,IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("FilePath", "Please upload a PDF file.");
                return View(workSubmission);
            }

            
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine("wwwroot/uploads/works", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            workSubmission.FilePath = "/uploads/works/" + fileName;
            
            _unitOfWork.WorkSubmissions.insert(workSubmission);
            _unitOfWork.Save();
            return RedirectToAction("Index", "TaskApplication", new {professionalProfileId=workSubmission.ProfessionalProfileId});
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var vm=_unitOfWork.WorkSubmissions.GetById(id);
            if (vm == null) return NotFound();
            return View(vm);
        }
        [HttpPost]
        public IActionResult Edit(WorkSubmission workSubmission,IFormFile file)
        {
            var vm=_unitOfWork.WorkSubmissions.GetById(workSubmission.WorkSubmissionId);
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("FilePath", "Please upload a PDF file.");
                return View(workSubmission);
            }
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine("wwwroot/uploads/works", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            workSubmission.FilePath = "/uploads/works/" + fileName;
            vm.Notes = workSubmission.Notes;
            vm.FilePath = workSubmission.FilePath;
            vm.SubmittedAt= DateTime.Now; 
            _unitOfWork.WorkSubmissions.update(vm);
            _unitOfWork.Save();
            return RedirectToAction("Index", new {TaskId=workSubmission.TaskId});
        }
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var vm = _unitOfWork.WorkSubmissions.GetById(id);
            if (vm == null) return NotFound();
            return View(vm);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(string id)
        {
            var vm=_unitOfWork.WorkSubmissions.GetById(id);
            if (vm == null) return NotFound();
            _unitOfWork.WorkSubmissions.delete(vm);
            _unitOfWork.Save();
            return RedirectToAction("Index",new {TaskId=vm.TaskId});
        }
    }
}
