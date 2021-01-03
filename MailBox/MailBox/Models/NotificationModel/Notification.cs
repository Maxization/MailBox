
namespace MailBox.Models.NotificationModel
{
    public class Notification
    {
        public string Content { get; set; }
        public string ContentType { get; set; } = "string";
        public string[] RecipientsList { get; set; }
        public bool WithAttachments { get; set; }
    }
}
