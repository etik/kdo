using ITI.KDO.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Services
{
    public class NotificationServices
    {
        readonly NotificationGateway _notificationGateway;
        readonly UserGateway _userGateway;

        public NotificationServices(NotificationGateway notificationGateway, UserGateway userGateway)
        {
            _notificationGateway = notificationGateway;
            _userGateway = userGateway;
        }

        public Result<IEnumerable<Notification>> GetAllByRecipientsId(int recipientsId)
        {
            User user = _userGateway.FindById(recipientsId);
            return Result.Success(Status.Ok, _notificationGateway.GetAllByRecipientsEmail(user.Email));
        }

        public Result CreateNotification(string recipientsEmail, string senderEmail, string descriptions, bool inviteAccept)
        {
            if (!IsNameValid(recipientsEmail)) return Result.Failure(Status.BadRequest, "The recipients's email is not valid.");
            if (!IsNameValid(senderEmail)) return Result.Failure(Status.BadRequest, "The sender's email is not valid.");
            if (string.IsNullOrEmpty(descriptions)) return Result.Failure(Status.BadRequest, "Descriptions must not null.");

            _notificationGateway.Create(recipientsEmail, senderEmail, descriptions, inviteAccept);
            return Result.Success(Status.Ok);
        }

        public Result<Notification> GetById(int notificationId)
        {
            Notification notification;
            if (_notificationGateway.FindByNotificationId(notificationId) == null) return Result.Failure<Notification>(Status.NotFound, "Notification not found.");
            notification = _notificationGateway.FindByNotificationId(notificationId);
            return Result.Success(Status.Ok, notification);
        }

        public Result<int> Delete(int notificationId)
        {
            if(_notificationGateway.FindByNotificationId(notificationId) == null) return Result.Failure<int>(Status.NotFound, "Notification not found.");
            _notificationGateway.Delete(notificationId);
            return Result.Success(Status.Ok, notificationId);
        }

        public Result<Notification> UpdateNotification(int notificationId, string recipientsEmail, string senderEmail, string descriptions, bool inviteAccept)
        {
            if (!IsNameValid(recipientsEmail)) return Result.Failure<Notification>(Status.BadRequest, "The recipients's email is not valid.");
            if (!IsNameValid(senderEmail)) return Result.Failure<Notification>(Status.BadRequest, "The sender's email is not valid.");
            if (string.IsNullOrEmpty(descriptions)) return Result.Failure<Notification>(Status.BadRequest, "Descriptions must not null.");

            Notification notification;
            if(_notificationGateway.FindByNotificationId(notificationId) == null)
            {
                return Result.Failure<Notification>(Status.NotFound, "Notification not found.");
            }

            _notificationGateway.Update(notificationId, recipientsEmail, senderEmail, descriptions, inviteAccept);
            notification = _notificationGateway.FindByNotificationId(notificationId);
            return Result.Success(Status.Ok, notification);
        }

        bool IsNameValid(string name) => !string.IsNullOrWhiteSpace(name);
    }
}
