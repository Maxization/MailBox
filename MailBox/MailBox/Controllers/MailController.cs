
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MailBox.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MailBox.Controllers
{

    [Authorize]
    [Authorize(Policy = "AssignToUser")]
    public class MailController : Controller
    {
        private readonly IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        public IActionResult Inbox()
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            return View();
        }

        public IActionResult Details(int id)
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var mail = _mailService.GetMail(userID, id);
            if (mail == null)
                return NotFound();
            return View(mail);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
