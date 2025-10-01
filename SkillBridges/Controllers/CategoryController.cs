using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillBridges.Models;
using SkillBridges.ViewModels;

namespace SkillBridges.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var vm = _unitOfWork.Categories.GetAll();
            var model = _mapper.Map<List<CategoryViewModel>>(vm);
            return View(model);
        }
        public IActionResult Create()
        {
            var vm = new CategoryCreateViewModel{
                Types = _unitOfWork.Categories.GetTypes()
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Create(CategoryCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is invalid"); ;
                foreach (var entry in ModelState)
                {
                    foreach (var error in entry.Value.Errors)
                    {
                        Console.WriteLine($"Validation error in field '{entry.Key}': {error.ErrorMessage}");
                    }
                }
                return View(vm);
            }
            Console.WriteLine("Model state is valid");
            Console.WriteLine("Typem:- " + vm.type.ToString());
            var model = _mapper.Map<Category>(vm);
            _unitOfWork.Categories.Insert(model);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(string id)
        {
            var vm = _unitOfWork.Categories.GetById(id);
            if (vm == null) return NotFound();
            var model = _mapper.Map<CategoryViewModel>(vm);
            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(string id)
        {
            var model = _unitOfWork.Categories.GetById(id);
            if (model == null) return NotFound();
            _unitOfWork.Categories.Delete(model);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
