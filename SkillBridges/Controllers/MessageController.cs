using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillBridges.Models;
using SkillBridges.Repositories;
using SkillBridges.ViewModels;

namespace SkillBridges.Controllers
{
    [Authorize(Roles = "Client,Professional")]
    public class MessageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MessageController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper= mapper;    
        }
       
        public IActionResult Index(string TaskId,string ViewerId)
        {
            var task=_unitOfWork.Tasks.GetById(TaskId);
            var vm=_unitOfWork.Messages.GetByTaskId(TaskId);
            var model=_mapper.Map<List<TaskMessageViewModel>>(vm);

            foreach(var msg in model)
            {
                var original = _unitOfWork.Messages.GetById(msg.MessageId);

                if (original.SenderRole == Models.UserRole.Client)
                {
                    var client1 = _unitOfWork.Clients.GetById(original.SenderId);
                    msg.SenderName = client1.OrganizationName;
                }
                else
                {
                    var professional = _unitOfWork.Professionals.GetById(original.SenderId);
                    msg.SenderName = professional.User.Name;
                }
                if (original.SenderRole == Models.UserRole.Professional)
                {
                    var client1 = _unitOfWork.Clients.GetById(original.ReceiverId);
                    msg.ReceiverName = client1.OrganizationName;
                }
                else
                {
                    var professional = _unitOfWork.Professionals.GetById(original.ReceiverId);
                    msg.ReceiverName = professional.User.Name;
                }
                if (original.ReceiverId == ViewerId)
                {
                     original.IsRead = true;
                    _unitOfWork.Messages.update(original);  
                }    
            }
            
            _unitOfWork.Save();
            var client=_unitOfWork.Clients.GetById(ViewerId);
            string Role;
            if (client != null)
            {
                Role = "Client";
            }
            else
            {
                Role = "Professional";
            }
                var km = new TaskConversationViewModel
                {
                    TaskId = task.TaskId,
                    ClientProfileId = task.ClientProfileId,
                    ProfessionalProfileId = task.ProfessionalProfileId,
                    Role =Role,
                    Messages = model,
                };
            return View(km);
        }
        public IActionResult Create(string id,string senderId,string receiverId)
        {
            var professional=_unitOfWork.Professionals.GetById(senderId);
            TaskMessageCreateViewModel mes = new TaskMessageCreateViewModel
            {
                TaskId = id,
                SenderId = senderId,
                ReceiverId = receiverId,
            };
            if (professional == null)
            {
                mes.SenderRole = ViewModels.UserRole.Client; 
            }
            else
            {
                mes.SenderRole = ViewModels.UserRole.Professional;
            }

            return View(mes);
        }
        [HttpPost]
        public IActionResult Create(TaskMessageCreateViewModel me)
        {
            if(!ModelState.IsValid)
            {
                Console.WriteLine("Invalid State");
                foreach(var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach(var error in errors)
                    {
                        Console.WriteLine($"Model error on property {key}:{error}");
                    }
                }
                return View(me);
            }
            var vm=_mapper.Map<TaskMessage>(me);
            _unitOfWork.Messages.insert(vm);
            _unitOfWork.Save();
            return RedirectToAction("Index", new {TaskId=me.TaskId,ViewerId=me.SenderId});
        }
        [HttpGet]
        public IActionResult Edit(string id,string SenderId)
        {
            var msg = _unitOfWork.Messages.GetById(id);
            if (msg == null) return NotFound();
            if (msg.SenderId != SenderId)
            {
                return RedirectToAction("Index", new { TaskId = msg.TaskId, ViewerId = SenderId });
            }
            var vm = _mapper.Map<TaskMessageViewModel>(msg);
            return View(vm);
        }
        [HttpPost]
        public IActionResult Edit(TaskMessageViewModel vm)
        {
            
            var msg = _unitOfWork.Messages.GetById(vm.MessageId);
            if(msg == null) return NotFound();
            if (msg.SenderId != vm.SenderId)
            {
                return RedirectToAction("Index", new { TaskId = msg.TaskId, ViewerId = vm.SenderId });
            }
            msg.Message = vm.Message;
            _unitOfWork.Messages.update(msg);
            _unitOfWork.Save();
            return RedirectToAction("Index", new { TaskId = msg.TaskId, ViewerId = vm.SenderId });
        }
        [HttpGet]
        public IActionResult Delete(string id, string SenderId)
        {
            var msg = _unitOfWork.Messages.GetById(id);
            if (msg == null) return NotFound();
            if (msg.SenderId != SenderId)
            {
                return RedirectToAction("Index", new { TaskId = msg.TaskId, ViewerId = SenderId });
            }
            var vm = _mapper.Map<TaskMessageViewModel>(msg);
            return View(vm);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(TaskMessageViewModel vm)
        {
            var msg = _unitOfWork.Messages.GetById(vm.MessageId);
            if (msg == null) return NotFound();
            if (msg.SenderId != vm.SenderId)
            {
                return RedirectToAction("Index", new { TaskId = msg.TaskId, ViewerId = vm.SenderId });
            }
            _unitOfWork.Messages.delete(msg);
            _unitOfWork.Save();
            return RedirectToAction("Index", new { TaskId = msg.TaskId, ViewerId = vm.SenderId });
        }

    }
}
