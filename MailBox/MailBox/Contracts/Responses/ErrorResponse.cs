
using System.Collections.Generic;

namespace MailBox.Contracts.Responses
{
    public class ErrorResponse 
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
