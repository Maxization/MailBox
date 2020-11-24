
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MailBox.Models;
using MailBox.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using MailBox.Models.MailModels;
using System.Diagnostics.CodeAnalysis;
using FluentValidation.Validators;

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
            ViewData["Mails"] = _mailService.GetUserMails(1);
            return View();
        }

        public IActionResult Details(int id)
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var mail = _mailService.GetMail(userID, id);
            if (mail == null) return NotFound();
            return View(mail);
        }

        public IActionResult SortByDate()
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var mails = _mailService.GetUserMails(1);
            Comparison<MailInboxView> dateComparison = new Comparison<MailInboxView>((MailInboxView a,MailInboxView b) => 
            {
                if(a.Date > b.Date) return -1;
                if (a.Date < b.Date) return 1;
                return 0;
            });
            mails.Sort(dateComparison);
            ViewData["Mails"] = mails;
            return View("Index");
        }

        public IActionResult SortByTopic()
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var mails = _mailService.GetUserMails(1);
            Comparison<MailInboxView> dateComparison = new Comparison<MailInboxView>((MailInboxView a, MailInboxView b) =>
            {
                return a.Topic.CompareTo(b.Topic);
            });
            mails.Sort(dateComparison);
            ViewData["Mails"] = mails;
            return View("Index");
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
            int userID = 1;// int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            _mailService.UpdateMailRead(userID, mail);

            return View();
        }

    }
}
