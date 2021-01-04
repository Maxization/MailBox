
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using MailBox.Services.Interfaces;
using MailBox.Models.MailModels;
using MailBox.Contracts.Responses;

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

        /// <summary>
        /// Create new mail
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="text"></param>
        /// <param name="ccRecipientsAddresses"></param>
        /// <param name="bccRecipientsAddresses"></param>
        /// <param name="files"></param>
        /// <returns>Error list if any</returns>
        [HttpPost]
        public async Task<IActionResult> Create(string topic, string text, List<string> ccRecipientsAddresses, List<string> bccRecipientsAddresses, List<IFormFile> files)
        {
            NewMail mail = new NewMail
            {
                Topic = topic,
                Text = text,
                CCRecipientsAddresses = ccRecipientsAddresses,
                BCCRecipientsAddresses = bccRecipientsAddresses,
                Files = files
            };
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
        /// <param name="filename"></param>
        /// <param name="id"></param>
        /// <returns>List of mails in JSON</returns>
        [HttpGet]
        public async Task<IActionResult> DownloadAttachment(string filename, Guid id)
        {
            byte[] array = await _mailService.DownloadAttachment(id.ToString() + filename);
            return File(array, "application/" + filename.Substring(filename.IndexOf('.') + 1), filename);
        }
    }
}
