
namespace MailBox.Models.GroupModels
{
    public class GroupNameUpdate
    {
        public int GroupID { get; set; }
        public string Name { get; set; }

        public GroupNameUpdate(int groupId, string newName)
        {
            this.GroupID = groupId;
            this.Name = newName;
        }
    }
}
