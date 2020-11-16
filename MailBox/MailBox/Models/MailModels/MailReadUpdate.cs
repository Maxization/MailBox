
namespace MailBox.Models.MailModels
{
    public class MailReadUpdate
    {
        public int MailID { get; set; }
        public bool Read { get; set; }

        public MailReadUpdate(int mailId, bool read)
        {
            this.MailID = mailId;
            this.Read = read;
        }
    }
}
