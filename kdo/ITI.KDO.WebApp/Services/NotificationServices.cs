using ITI.KDO.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Services
{
    public class NotificationServices
    {
        readonly UserGateway _userGateway;
        readonly ContactGateway _contactGateway;

        public NotificationServices(UserGateway userGateway, ContactGateway contactGateway)
        {
            _userGateway = userGateway;
            _contactGateway = contactGateway;
        }

        public Result<IEnumerable<Notification>> GetAllByUserId(int userId)
        {
            IEnumerable<Notification> listNotification = GetNotificationList(_contactGateway.FindAllNotificationByUserId(userId));
            return Result.Success(Status.Ok, listNotification);
        }

        public IEnumerable<Notification> GetNotificationList(IEnumerable<ContactData> listContactData)
        {
            List<Notification> listNotification = new List<Notification>();
            foreach(ContactData contactData in listContactData)
            {
                Notification notification = new Notification();
                notification.ContactId = contactData.ContactId;
                notification.SenderEmail = _userGateway.FindById(contactData.UserId).Email;
                notification.RecipientsEmail = _userGateway.FindById(contactData.FriendId).Email;
                listNotification.Add(notification);
            }
            return listNotification;
        }
    }
}
