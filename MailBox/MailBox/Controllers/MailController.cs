<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MailBox.Models;
using MailBox.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
=======
﻿
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MailBox.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using MailBox.Models.MailModels;
>>>>>>> 1804e2ea130883836e578a8fc840507263ab688a

namespace MailBox.Controllers
{
    [Authorize]
    public class MailController : Controller
    {
        IMailService _mailService;
<<<<<<< HEAD

=======
>>>>>>> 1804e2ea130883836e578a8fc840507263ab688a
        public MailController(IMailService userService)
        {
            _mailService = userService;
        }

        public IActionResult Index()
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
<<<<<<< HEAD
            var test = _mailService.GetUserMails(userID);
=======
>>>>>>> 1804e2ea130883836e578a8fc840507263ab688a
            ViewData["Mails"] = _mailService.GetUserMails(userID);
            return View();
        }

        public IActionResult Details(int id)
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var mail = _mailService.GetMail(userID, id);
            if (mail == null) return NotFound();
            return View(mail);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
<<<<<<< HEAD
=======
        [ValidateAntiForgeryToken]
>>>>>>> 1804e2ea130883836e578a8fc840507263ab688a
        public IActionResult Create(NewMail mail)
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            _mailService.CreateMail(userID, mail);
<<<<<<< HEAD
            return RedirectToAction("Index");
=======
            return View();
>>>>>>> 1804e2ea130883836e578a8fc840507263ab688a
        }

    }
}
