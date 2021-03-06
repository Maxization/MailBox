
using MailBox.Models.MailModels;
using System.Threading.Tasks;

namespace MailBox.Services.Interfaces
{
    public enum SortingEnum { ByDateFromNewest = 0, ByDateFromOldest = 1, BySenderAZ = 2, BySenderZA = 3, ByTopicAZ = 4, ByTopicZA = 5 };
    public enum FilterEnum { NoFilter = 0, FilterTopic = 1, FilterSenderName = 2, FilterSenderSurname = 3 };
    public interface IMailService
    {
        PagingMailInboxView GetUserMails(int userID, int page, SortingEnum sorting, FilterEnum filter, string filterPhrase);
        void UpdateMailRead(int userID, MailReadUpdate mail);
        MailDetailsView GetMail(int userID, int mailID);
        Task AddMail(int userID, NewMail mail);
        Task<byte[]> DownloadAttachment(string filename);
    }
}
