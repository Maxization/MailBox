
namespace MailBox.Database
{
    public class GroupUser
    {
        public int GroupID { get; set; }
        public Group Group { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
