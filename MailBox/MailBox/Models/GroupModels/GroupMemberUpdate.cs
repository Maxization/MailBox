
namespace MailBox.Models.GroupModels
{
    public class GroupMemberUpdate
    {
        public int GroupID { get; set; }
        public string GroupMemberAddress { get; set; }

        public GroupMemberUpdate(int groupID, string groupMemberAddress)
        {
            this.GroupID = groupID;
            this.GroupMemberAddress = groupMemberAddress;
        }
    }
}
