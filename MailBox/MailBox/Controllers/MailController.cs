using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MailBox.Models;

namespace MailBox.Controllers
{
    [Route("[controller]/[action]")]
    public class MailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UserMails(int id)
        {
            List<Mail> models = new List<Mail>();
            models.Add(new Mail(false,"someone1","Hi Take care!",new DateTime(2020,2,14)));
            models.Add(new Mail(false,"someone2","Hi Take care!",new DateTime(2020,2,12)));
            models.Add(new Mail(false,"someone3","Hi Take care!",new DateTime(2020,2,12)));
            models.Add(new Mail(false,"someone4","Hi Take care!",new DateTime(2020,2,11)));
            models.Add(new Mail(true,"someone5","Hi Take care!",new DateTime(2020,2,10)));
            models.Add(new Mail(true,"someone6","Hi Take care!",new DateTime(2020,2,5)));
            models.Add(new Mail(true,"someone7","Hi Take care!",new DateTime(2020,2,4)));
            models.Add(new Mail(true,"someone8","Hi Take care!",new DateTime(2020,2,3)));
            models.Add(new Mail(true,"someone9","Hi Take care!",new DateTime(2020,2,1)));
            return Json(models);
        }
    }
}
