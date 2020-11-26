
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MailBox.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using MailBox.Models.MailModels;
using System;
using MailBox.Contracts.Responses;

namespace MailBox.Controllers
{

    [Authorize]
    public class MailController : Controller
    {
        IMailService _mailService;
        public MailController(IMailService userService)
        {
            _mailService = userService;
        }
        public IActionResult Index()
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            ViewData["Mails"] = _mailService.GetUserMails(userID);
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

        [HttpPost]
        public IActionResult Create(NewMail mail)
        {
            
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            _mailService.CreateMail(userID, mail);
            return View();
        }

        [HttpPut]
        public IActionResult UpdateRead(MailReadUpdate mail)
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            _mailService.UpdateMailRead(userID, mail);

            return View();
        }

        public IActionResult GetMails()
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            return  Json(_mailService.GetUserMails(userID));
        }

    }
}
