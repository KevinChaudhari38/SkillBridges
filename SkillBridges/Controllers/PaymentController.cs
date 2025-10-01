using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkillBridges.Models;
using SkillBridges.Services;
using SkillBridges.ViewModels;

namespace SkillBridges.Controllers
{
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

            var orderId = _razorpayService.CreateOrder(task.TaskId, task.Budjet);
            task.RazorpayOrderId = orderId;
            _unitOfWork.Tasks.Update(task);
            _unitOfWork.Save();

            ViewBag.Key = _razorpayService.Key;
            ViewBag.Amount =(int)( task.Budjet * 100);
            ViewBag.ToDisplay = task.Budjet;
            ViewBag.OrderId = orderId;
            ViewBag.TaskId = task.TaskId;

            return View();
        }

        [HttpPost]
        public IActionResult VerifyPayment(string razorpay_payment_id,
                                           string razorpay_order_id,
                                           string razorpay_signature,
                                           string taskId)
        {
            var task = _unitOfWork.Tasks.GetById(taskId);
            if (task == null) return NotFound();

            bool isValid = _razorpayService.VerifyPayment(
                                razorpay_payment_id,
                                razorpay_order_id,
                                razorpay_signature);

            var paymentRecord = new Payment
            {
                TaskId = task.TaskId,
                ClientProfileId = task.ClientProfileId,
                ProfessionalProfileId = task.ProfessionalProfileId,
                Amount = task.Budjet,
                RazorpayOrderId = razorpay_order_id,
                RazorpayPaymentId = razorpay_payment_id,
                PaymentStatus = isValid ? "Success" : "Failed"
            };

            _unitOfWork.Payments.insert( paymentRecord );

            if (isValid)
            {
                task.PaymentStatus = "Paid";
                _unitOfWork.Tasks.Update(task);
                //_unitOfWork.Save();
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
        public IActionResult Index(string clientId)
        {
            if (string.IsNullOrEmpty(clientId)) return BadRequest();
            var payments = _unitOfWork.Payments.GetByClientId(clientId);
            var vm = _mapper.Map<List<PaymentViewModel>>(payments);
            return View(vm);
        }

        [HttpGet]
        public IActionResult ProfessionalHistory(string professionalId) {
            if (string.IsNullOrEmpty(professionalId)) return BadRequest();
            var payments = _unitOfWork.Payments.GetByProfProfileId(professionalId);
            var vm = _mapper.Map<List<PaymentViewModel>>(payments);
            return View("Index", vm);
        }


    }
}
