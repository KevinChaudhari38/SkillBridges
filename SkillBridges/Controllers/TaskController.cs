using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkillBridges.Models;
using SkillBridges.ViewModels;

namespace SkillBridges.Controllers
{
    public class TaskController : Controller
    {
        private readonly IUnitOfWork2 _unitOfWork;
        private readonly IMapper _mapper;
        public TaskController(IUnitOfWork2 unitOfWork, IMapper mapper)
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
