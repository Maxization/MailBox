using Microsoft.AspNetCore.Authentication.AzureADB2C.UI.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Contracts.Responses
{
    public class ErrorResponse 
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
