using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MailBox.Models;
using MailBox.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MailBox.Controllers
{
    [Authorize]
    public class MailController : Controller
    {
        IUserService _userService;

        public MailController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            ViewData["Mails"] = _userService.GetUserMails(userID);
            return View();
        }

        public IActionResult Details(int id)
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var mail = _userService.GetMail(userID, id);
            if (mail == null) return NotFound();
            return View(mail);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewMail mail)
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            _userService.CreateMail(userID, mail);
            return View();
        }

    }
}
