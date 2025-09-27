using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillBridges.Models;
using SkillBridges.ViewModels;

namespace SkillBridges.Controllers
{
    [Authorize(Roles ="Client,Admin")]
    public class ClientController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClientController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(string userId)
        {
            ClientProfile vm = new ClientProfile
            {
                UserId = userId
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(ClientProfile client)
        {
            _unitOfWork.Clients.insert(client);
            _unitOfWork.Save(); 
            return RedirectToAction("Details", "Home", new { id = client.UserId });
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var client = _unitOfWork.Clients.GetByUserId(id);
            if (client == null)
            {
                return RedirectToAction("Create", new { userId = id });
            }
            var model = _mapper.Map<ClientEditViewModel>(client);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ClientEditViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var existing = _unitOfWork.Clients.GetById(vm.ClientProfileId);
            _mapper.Map(vm, existing);
            _unitOfWork.Clients.update(existing);
            _unitOfWork.Save(); 

            return RedirectToAction("Details", "Home", new { id = existing.UserId });
        }
    }
}
