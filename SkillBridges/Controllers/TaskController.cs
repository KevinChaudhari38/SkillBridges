using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkillBridges.Models;
using SkillBridges.Repositories;
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
        
        
        public IActionResult IndexByCategory(string professionalProfileId,string SelectedCategoryId,TaskType? Type)
        {
            var tasks= _unitOfWork.Tasks.GetAll();
            var categories = _unitOfWork.Categories.GetAll();
            if (Type.HasValue)
            {
                categories = _unitOfWork.Categories.GetByType(Type.Value);
                if (!string.IsNullOrEmpty(SelectedCategoryId))
                {
                    tasks = _unitOfWork.Tasks.GetByCategoryId(SelectedCategoryId);
                    Console.WriteLine("All tasks by category and type");
                }
                else
                {
                    tasks = _unitOfWork.Tasks.GetByType(Type.Value);
                    Console.WriteLine("All tasks by type");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(SelectedCategoryId))
                {
                    tasks = _unitOfWork.Tasks.GetByCategoryId(SelectedCategoryId);
                    Console.WriteLine("All tasks by category");
                }
                else
                {
                    Console.WriteLine("All tasks");
                }
            }
                Console.WriteLine("SelectedCategory :- " + SelectedCategoryId);
           
           
            var result = _mapper.Map<List<TaskViewModel>>(tasks);

           
            
            var taskApplications=_unitOfWork.TaskApplications.GetByProfessionalId(professionalProfileId);
            
            var vm = new ProfessionalTasksViewModel
            {
                ProfessionalProfileId = professionalProfileId,
                Tasks = result,
                Categories=categories,
                SelectedCategoryId= SelectedCategoryId,
                TaskApplications=taskApplications,
                Types=_unitOfWork.Categories.GetTypes().ToList(),
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
        
        public IActionResult Create(string clientId,TaskType? Type)
        {
            Console.WriteLine("Type " + Type.ToString());
            var typeToUse = Type ?? TaskType.Local;
            
            var categories = _unitOfWork.Categories.GetByType(typeToUse)
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId,
                    Text = c.Name
                }).ToList();

            var vm = new TaskCreateViewModel
            {
                ClientProfileId = clientId,
                Categories = categories,
                Types = _unitOfWork.Categories.GetTypes(),
                Type = typeToUse,
                Cities = _unitOfWork.Tasks.GetCities(),
            };
            Console.WriteLine("Client Id :- " + vm.ClientProfileId);
            Console.WriteLine("Type Selected :- " + vm.Type);
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
                vm.Types = _unitOfWork.Categories.GetTypes();
                vm.Cities = _unitOfWork.Tasks.GetCities();
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
