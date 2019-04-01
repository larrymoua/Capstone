using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

using Twilio.TwiML;
using Twilio.AspNet.Mvc;

namespace SoloCapstone.Controllers
{
    public class SmsController : TwilioController
    {
        public ActionResult SendSms(string InventoryStockedName)
        {
            var accountSid = "AC2a73e8f06d582d58b14ce38938725590";
            string authToken = "7a7061e6ad35a73b65e5756024172516";
            TwilioClient.Init(accountSid, authToken);

            var to = new PhoneNumber("14143914140");
            var from = new PhoneNumber("+12026842116");

            var message = MessageResource.Create(
                to: to, 
                from: from,
                body: $"{InventoryStockedName} has now been stocked!");
            return RedirectToAction("Index", "Order");
        }
        public ActionResult LowStock(string InventoryStockedName)
        {
            var accountSid = "AC2a73e8f06d582d58b14ce38938725590";
            string authToken = "7a7061e6ad35a73b65e5756024172516";
            TwilioClient.Init(accountSid, authToken);

            var to = new PhoneNumber("14143914140");
            var from = new PhoneNumber("+12026842116");

            var message = MessageResource.Create(
                to: to,
                from: from,
                body: $"{InventoryStockedName} is low in inventory!");
            return RedirectToAction("Index", "Order");
        }


    }
}