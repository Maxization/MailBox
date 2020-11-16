
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MailBox.Models.MailModels;
using MailBox.Services.ServicesInterfaces;

namespace MailBox.Controllers
{
    public class MailController : Controller
    {
        private readonly ILogger<MailController> _logger;
        private readonly IGroupService _groupService;

        public MailController(ILogger<MailController> logger, IGroupService groupService)
        {
            _logger = logger;
            _groupService = groupService;
        }

        public IActionResult Index()
        {
            ViewData["groups"] = _groupService.GetUserGroupsList(1);
            return View();
        }

        public IActionResult NewMail()
        {
            ViewData["groups"] = _groupService.GetUserGroupsList(1);
            return View();
        }

        [HttpGet]
        public IActionResult UserMails(int ID)
        {
            List<MailInboxView> models = new List<MailInboxView>();
            return Json(models);
        }
    }
}
