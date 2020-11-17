
namespace MailBox.Models.GroupModels
{
    public class NewGroup
    {
        public int OwnerID { get; set; }
        public string Name { get; set; }

        public NewGroup(int ownerID, string name)
        {
            this.OwnerID = ownerID;
            this.Name = name;
        }
    }
}
