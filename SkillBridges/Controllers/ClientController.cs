using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using SkillBridges.Models;
using SkillBridges.ViewModels;
using System.Runtime.CompilerServices;

namespace SkillBridges.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private IMapper _mapper;
        public ClientController(IClientRepository clientRepository, IMapper mapper)
        {
               
            _clientRepository = clientRepository;
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
            
            _clientRepository.insert(client);
            return RedirectToAction("Details", "Home", new { id = client.UserId });
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var client=_clientRepository.GetByUserId(id);
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

            var existing = _clientRepository.GetById(vm.ClientProfileId);
            _mapper.Map(vm, existing);
            _clientRepository.update(existing);

            return RedirectToAction("Details", "Home", new { id = existing.UserId });
        }
    }
}
