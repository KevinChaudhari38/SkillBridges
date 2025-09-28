using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkillBridges.Models;
using SkillBridges.ViewModels;

namespace SkillBridges.Controllers
{
    public class TaskController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TaskController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index(string clientId)
        {
            var model = _unitOfWork.Tasks.GetByClientId(clientId);
            var vm = _mapper.Map<List<TaskViewModel>>(model);
            return View(vm);
        }
        
        
        public IActionResult IndexByCategory(string professionalProfileId,string SelectedCategoryId)
        {
            var tasks= _unitOfWork.Tasks.GetAll();
            Console.WriteLine("SelectedCategory :- " + SelectedCategoryId);
            if (!string.IsNullOrEmpty(SelectedCategoryId))
            {
                Console.WriteLine("Not Null Hit");
                tasks = _unitOfWork.Tasks.GetByCategoryId(SelectedCategoryId); ;
            }
           
            var result = _mapper.Map<List<TaskViewModel>>(tasks);

            var categories = _unitOfWork.Categories.GetAll();
            
            var taskApplications=_unitOfWork.TaskApplications.GetByProfessionalId(professionalProfileId);

            var vm = new ProfessionalTasksViewModel
            {
                ProfessionalProfileId = professionalProfileId,
                Tasks = result,
                Categories=categories,
                SelectedCategoryId= SelectedCategoryId,
                TaskApplications=taskApplications
            };
            return View(vm);
        }

        public IActionResult IndexForProfessional(string ProfessionalProfileId)
        {
            var tasks=_unitOfWork.Tasks.GetByProfessionalId(ProfessionalProfileId);
            var vm=_mapper.Map<List<TaskViewModel>>(tasks);
            return View(vm);
        }
        public IActionResult Details(string id)
        {
            var task = _unitOfWork.Tasks.GetById(id);
            if (task == null)
            {
                return NotFound();
            }
            var model=_mapper.Map<TaskViewModel>(task);
            return View(model);
        }
        public IActionResult InProgress(string TaskId)
        {
            var vm=_unitOfWork.Tasks.GetById(TaskId);
            vm.Status = Models.TaskStatus.InProgress;
            _unitOfWork.Save();
            return RedirectToAction("IndexForProfessional", new {vm.ProfessionalProfileId});
        }
        
        public IActionResult Create(string clientId)
        {
            var categories = _unitOfWork.Categories.GetAll().Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.Name
            });
            var vm = new TaskCreateViewModel
            {
                ClientProfileId = clientId,
                Categories = categories
            };
            Console.WriteLine("Client Id :- " + vm.ClientProfileId);
            return View(vm);
        }
        [HttpPost]
        public IActionResult Create(TaskCreateViewModel vm) {
            Console.WriteLine("Post Method Hit");
            if (!ModelState.IsValid)
            {
                var categories = _unitOfWork.Categories.GetAll().Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.Name
                });
                vm.Categories = categories;
                return View(vm);
            }
            var task=_mapper.Map<Models.Task>(vm);
            _unitOfWork.Tasks.Insert(task);
            _unitOfWork.Save();
            return RedirectToAction("Details",new {id=task.TaskId});
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var task=_unitOfWork.Tasks.GetById(id);
            if (task == null)
            {
                return NotFound();
            }
            var vm = _mapper.Map<TaskEditViewModel>(task);
            vm.Categories=_unitOfWork.Categories.GetAll().Select(c=>new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.Name
            });
            return View(vm);
        }
        [HttpPost]
        public IActionResult Edit(TaskEditViewModel vm) {
            Console.WriteLine("Edit POST method hit for TaskId: " + vm.TaskId);
           var user=_unitOfWork.Clients.GetById(vm.ClientProfileId);
            if (user == null) return NotFound();
            
            if (!ModelState.IsValid)
            {
                
                vm.Categories = _unitOfWork.Categories.GetAll().Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.Name
                });
                return View(vm);
            }
            var existingTask = _unitOfWork.Tasks.GetById(vm.TaskId);
            if (existingTask == null)
                return NotFound();
            _mapper.Map(vm, existingTask);
            _unitOfWork.Tasks.Update(existingTask);
            _unitOfWork.Save();
            return RedirectToAction("Details", new { id = existingTask.TaskId });
        }

        public IActionResult Delete(string id)
        {
            var task = _unitOfWork.Tasks.GetById(id);
            if (task == null)
            {
                return NotFound();
            }
            Console.WriteLine("Id before post " + task.TaskId);
            var vm = _mapper.Map<TaskViewModel>(task);
            Console.WriteLine("Id before post " + vm.TaskId);
            return View(vm);
        }
        [HttpPost]
        public IActionResult Delete(TaskViewModel vm)
        {
            var task=_unitOfWork.Tasks.GetById(vm.TaskId);
            Console.WriteLine("Id before "+vm.TaskId);
            if (task == null)
            {
                return NotFound();
            }
            Console.WriteLine("Id After " + vm.TaskId);
            _unitOfWork.Tasks.Delete(task);
            _unitOfWork.Save();

            return RedirectToAction("Index",new {clientId=vm.ClientProfileId});
        }
        
    }
}
