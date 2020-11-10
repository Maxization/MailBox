using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MailBox.Models;

namespace MailBox.Controllers
{
    public class MailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NewMail()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UserMails(int id)
        {
            List<InboxMail> models = new List<InboxMail>();
            return Json(models);
        }
    }
}
