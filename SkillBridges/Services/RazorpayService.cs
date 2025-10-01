using Microsoft.Extensions.Configuration;
using Razorpay.Api;
using System.Collections.Generic;

namespace SkillBridges.Services
{
    public class RazorpayService
    {
        private readonly string _key;
        private readonly string _secret;

        public RazorpayService(IConfiguration config)
        {
            _key = config["Razorpay:Key"];
            _secret = config["Razorpay:Secret"];
        }

        public string CreateOrder(string receiptId, decimal amount)
        {
            var client = new RazorpayClient(_key, _secret);
            var options = new Dictionary<string, object>
            {
                { "amount", (int)(amount * 100) },
                { "currency", "INR" },
                { "receipt", receiptId },
                { "payment_capture", 1 }
            };

            Order order = client.Order.Create(options);
            return order["id"].ToString();
        }


        public bool VerifyPayment(string razorpayPaymentId, string razorpayOrderId, string razorpaySignature)
        {
            try
            {
                Dictionary<string, string> attributes = new Dictionary<string, string>();
                attributes.Add("razorpay_payment_id", razorpayPaymentId);
                attributes.Add("razorpay_order_id", razorpayOrderId);
                attributes.Add("razorpay_signature", razorpaySignature);

                Utils.verifyPaymentSignature(attributes);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public string Key => _key;
    }
}