
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
using MailBox.Helpers;
using MailBox.Validators.MailValidators;

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
        /// <returns>List of errors if any</returns>
        [HttpPost]
        public async Task<IActionResult> Create(string topic, string text, List<string> ccRecipientsAddresses, List<string> bccRecipientsAddresses, List<IFormFile> files)
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            ErrorResponse errorResponse = new ErrorResponse();

            NewMail newMail = new NewMail
            {
                Topic = topic,
                Text = text,
                CCRecipientsAddresses = ccRecipientsAddresses,
                BCCRecipientsAddresses = bccRecipientsAddresses,
                Files = files
            };
            var newMailValidator = new NewMailValidator().Validate(newMail);
            if (!newMailValidator.IsValid)
            {
                foreach (var error in newMailValidator.Errors)
                    errorResponse.Errors.Add(new ErrorModel { FieldName = error.PropertyName, Message = error.ErrorMessage });
                Response.StatusCode = 400;
                return new JsonResult(errorResponse);
            }

            try
            {
                await _mailService.AddMail(userID, newMail);
            }
            catch (Exception ex)
            {
                errorResponse.Errors.Add(new ErrorModel { FieldName = ex.Message, Message = ex.InnerException.Message });
                Response.StatusCode = 400;
            }

            return new JsonResult(errorResponse);
        }

        /// <summary>
        /// Downloads selected attachment
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="id"></param>
        /// <returns>File or list of errors</returns>
        [HttpGet]
        public async Task<IActionResult> DownloadAttachment(string filename, string id)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            byte[] array;
            try
            {
                array = await _mailService.DownloadAttachment(id + filename);
            }
            catch (Exception ex)
            {
                errorResponse.Errors.Add(new ErrorModel { FieldName = ex.Message, Message = ex.InnerException.Message });
                Response.StatusCode = 400;
                return new JsonResult(errorResponse);
            }

            string extension;
            try
            {
                extension = filename.Substring(filename.LastIndexOf('.'));
            }
            catch (Exception)
            {
                extension = "";
            }
            return new JsonResult(File(array, MIMEAssistant.GetMIMEType(extension), filename));
        }
    }
}
