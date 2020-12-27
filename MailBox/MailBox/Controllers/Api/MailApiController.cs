
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MailBox.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using MailBox.Models.MailModels;
using System;
using MailBox.Contracts.Responses;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using MailBox.Models.NotificationModel;
using System.Text;
using Newtonsoft.Json;

namespace MailBox.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize(Policy = "AssignToUser")]
    public class MailApiController : ControllerBase
    {
        private readonly IMailService _mailService;

        public MailApiController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NewMail mail)
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            ErrorResponse errorResponse = new ErrorResponse();
            try
            {
                _mailService.AddMail(userID, mail);    
            }
            catch (Exception ex)
            {
                errorResponse.Errors.Add(new ErrorModel { FieldName = ex.Message, Message = ex.InnerException.Message });
                Response.StatusCode = 400;
            }

            return new JsonResult(errorResponse);
        }

        [HttpGet]
        public IActionResult GetMails()
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            return new JsonResult(_mailService.GetUserMails(userID));
        }

        [HttpPut]
        public IActionResult UpdateRead([FromBody] MailReadUpdate mail)
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            _mailService.UpdateMailRead(userID, mail);
            return Ok();
        }
    }
}
