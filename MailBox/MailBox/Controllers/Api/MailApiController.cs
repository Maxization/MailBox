
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MailBox.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using MailBox.Models.MailModels;
using MailBox.Contracts.Responses;
using System.Threading.Tasks;

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

        /// <summary>
        /// Create new mail
        /// </summary>
        /// <param name="mail"></param>
        /// <returns>Error list if any</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NewMail mail)
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            ErrorResponse errorResponse = new ErrorResponse();
            try
            {
                await _mailService.AddMail(userID, mail);
            }
            catch (Exception ex)
            {
                errorResponse.Errors.Add(new ErrorModel { FieldName = ex.Message, Message = ex.InnerException.Message });
                Response.StatusCode = 400;
            }

            return new JsonResult(errorResponse);
        }

        /// <summary>
        /// Gets mails of logged user
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sorting"></param>
        /// <param name="filter"></param>
        /// <param name="filterPhrase"></param>
        /// <returns>List of mails in JSON</returns>
        [HttpGet]
        public IActionResult GetMails(int page, SortingEnum sorting, FilterEnum filter, string filterPhrase)
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            return new JsonResult(_mailService.GetUserMails(userID, page, sorting, filter, filterPhrase));
        }

        /// <summary>
        /// Update mail "Read" status (read/unread)
        /// </summary>
        /// <param name="mail"></param>
        /// <returns>OK</returns>
        [HttpPut]
        public IActionResult UpdateRead([FromBody] MailReadUpdate mail)
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            _mailService.UpdateMailRead(userID, mail);
            return Ok();
        }
    }
}
