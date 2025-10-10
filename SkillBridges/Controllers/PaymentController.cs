using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillBridges.Models;
using SkillBridges.Repositories;
using SkillBridges.Services;
using SkillBridges.ViewModels;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SkillBridges.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RazorpayService _razorpayService;
        private readonly IMapper _mapper;

        public PaymentController(IUnitOfWork unitOfWork, RazorpayService razorpayService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _razorpayService = razorpayService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Pay(string taskId)
        {
            var task = _unitOfWork.Tasks.GetById(taskId);
            if (task == null) return NotFound();

            var vm = new PaymentCreateViewModel
            {
                TaskId = task.TaskId,
                Amount = task.Budjet
            };

            return View("PaymentForm",vm);
        }
        [HttpPost]
        public IActionResult Pay(PaymentCreateViewModel vm)
        {
            Console.WriteLine("Payment :- " + vm.Amount);
            var task = _unitOfWork.Tasks.GetById(vm.TaskId);
            if (task == null) return NotFound();

            if (vm.Amount <= 0)
                return BadRequest();

            var orderId= _razorpayService.CreateOrder(task.TaskId, vm.Amount);
            task.RazorpayOrderId = orderId;
            _unitOfWork.Tasks.Update(task);
            _unitOfWork.Save();

            vm.RazorpayKey = _razorpayService.Key;
            vm.RazorpayOrderId = orderId;

            return View("Pay", vm);
        }

        [HttpPost]
        public IActionResult VerifyPayment(PaymentCreateViewModel vm)
        {
            var task = _unitOfWork.Tasks.GetById(vm.TaskId);
            if (task == null) return NotFound();

            bool isValid = _razorpayService.VerifyPayment(
                        vm.RazorpayPaymentId,
                        vm.RazorpayOrderId,
                        vm.RazorpaySignature);
            Console.WriteLine("Payment :- " + vm.Amount);
            var paymentRecord = new Payment
            {
                TaskId = task.TaskId,
                ClientProfileId = task.ClientProfileId,
                ProfessionalProfileId = task.ProfessionalProfileId,
                Amount = vm.Amount,
                RazorpayOrderId = vm.RazorpayOrderId,
                RazorpayPaymentId = vm.RazorpayPaymentId,
                PaymentStatus = isValid ? "Success" : "Failed"
               
            };

            _unitOfWork.Payments.insert( paymentRecord );

            if (isValid)
            {
                task.PaymentStatus = "Paid";
                _unitOfWork.Tasks.Update(task);
                TempData["Message"] = "Payment Successful!";
            }
            else
            {
                task.PaymentStatus = "Payment Failed";
                _unitOfWork.Tasks.Update(task);
                TempData["Message"] = "Payment Failed!";
            }

            _unitOfWork.Save();

            return RedirectToAction("Index", "Task", new {clientId=task.ClientProfileId});
        }

        [HttpGet]
        [Authorize(Roles ="Client,Admin")]
        public IActionResult Index(string TaskId)
        {
            if (string.IsNullOrEmpty(TaskId)) return BadRequest();
            var payments = _unitOfWork.Payments.GetByTaskId(TaskId);
            var vm = _mapper.Map<List<PaymentViewModel>>(payments);
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles ="Admin,Professional")]
        public IActionResult ProfessionalHistory(string professionalId) {
            professionalId ??= User.FindFirstValue("ProfessionalProfileId");
            if (string.IsNullOrEmpty(professionalId)) return BadRequest();
            var payments = _unitOfWork.Payments.GetByProfProfileId(professionalId);
            var vm = _mapper.Map<List<PaymentViewModel>>(payments);
            return View("Index", vm);
        }
        [HttpGet]
        [Authorize(Roles = "Client,Admin")]
        public IActionResult ClientHistory(string ClientId){
            ClientId ??=User.FindFirstValue("ClientProfileId");
            if (string.IsNullOrEmpty(ClientId)) return BadRequest();
            var payments = _unitOfWork.Payments.GetByClientId(ClientId);
            var vm = _mapper.Map<List<PaymentViewModel>>(payments);
            return View("Index",vm);
         }

    }
}
