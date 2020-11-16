
namespace MailBox.Models.UserModels
{
    public class UserEnableUpdate
    {
        public string Address { get; set; }
        public bool Enable { get; set; }

        public UserEnableUpdate(string address, bool enable)
        {
            this.Address = address;
            this.Enable = enable;
        }
    }
}
