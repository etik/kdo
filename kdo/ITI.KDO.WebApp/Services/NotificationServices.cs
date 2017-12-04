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

        public NotificationServices(NotificationGateway notificationGateway)
        {
            _notificationGateway = notificationGateway;
        }

        public Result<IEnumerable<Notification>> GetAllByUserId(int userId)
        {
            return Result.Success(Status.Ok, _notificationGateway.GetAllByUserId(userId));
        }

        public Result<int> CreateNotification(int userId, string recipientsEmail, string senderEmail, string descriptions, bool inviteAccept)
        {
            if (!IsNameValid(recipientsEmail)) return Result.Failure<int>(Status.BadRequest, "The recipients's email is not valid.");
            if (!IsNameValid(senderEmail)) return Result.Failure<int>(Status.BadRequest, "The sender's email is not valid.");
            if (string.IsNullOrEmpty(descriptions)) return Result.Failure<int>(Status.BadRequest, "Descriptions must not null.");

            _notificationGateway.Create(userId, recipientsEmail, senderEmail, descriptions, inviteAccept);
            return Result.Success(Status.Ok, userId);
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

        public Result<Notification> UpdateNotification(int notificationId, int userId, string recipientsEmail, string senderEmail, string descriptions, bool inviteAccept)
        {
            if (!IsNameValid(recipientsEmail)) return Result.Failure<Notification>(Status.BadRequest, "The recipients's email is not valid.");
            if (!IsNameValid(senderEmail)) return Result.Failure<Notification>(Status.BadRequest, "The sender's email is not valid.");
            if (string.IsNullOrEmpty(descriptions)) return Result.Failure<Notification>(Status.BadRequest, "Descriptions must not null.");

            Notification notification;
            if(_notificationGateway.FindByNotificationId(notificationId) == null)
            {
                return Result.Failure<Notification>(Status.NotFound, "Notification not found.");
            }

            _notificationGateway.Update(notificationId, userId, recipientsEmail, senderEmail, descriptions, inviteAccept);
            notification = _notificationGateway.FindByNotificationId(notificationId);
            return Result.Success(Status.Ok, notification);
        }

        bool IsNameValid(string name) => !string.IsNullOrWhiteSpace(name);
    }
}
