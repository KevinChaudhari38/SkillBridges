using Microsoft.AspNetCore.Mvc;
using SkillBridges.Models;
using SkillBridges.Services;

namespace SkillBridges.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RazorpayService _razorpayService;

        public PaymentController(IUnitOfWork unitOfWork, RazorpayService razorpayService)
        {
            _unitOfWork = unitOfWork;
            _razorpayService = razorpayService;
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
            ViewBag.Amount = task.Budjet * 100;
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

            if (isValid)
            {
                task.PaymentStatus = "Paid";
                _unitOfWork.Tasks.Update(task);
                _unitOfWork.Save();
                TempData["Message"] = "Payment Successful!";
            }
            else
            {
                TempData["Message"] = "Payment Failed!";
            }

            return RedirectToAction("Index", "Task", new {clientId=task.ClientProfileId});
        }
    }
}
