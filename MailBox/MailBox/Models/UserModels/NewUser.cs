
namespace MailBox.Models.UserModels
{
    public class NewUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }

        public NewUser(string name, string surname, string address)
        {
            this.Name = name;
            this.Surname = surname;
            this.Address = address;
        }
    }
}
