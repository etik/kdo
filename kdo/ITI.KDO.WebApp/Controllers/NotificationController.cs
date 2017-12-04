using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ITI.KDO.WebApp.Authentification;
using ITI.KDO.WebApp.Services;
using ITI.KDO.DAL;
using ITI.KDO.WebApp.Models.NotificationViewModels;

namespace ITI.KDO.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(ActiveAuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class NotificationController : Controller
    {
        readonly NotificationServices _notificationServices;

        public NotificationController(NotificationServices notificationServices)
        {
            _notificationServices = notificationServices;
        }

        [HttpGet("{userId}/getNotificationByUserId")]
        public IActionResult GetNotificationList(int userId)
        {
            Result<IEnumerable<Notification>> result = _notificationServices.GetAllByUserId(userId);
            return this.CreateResult<IEnumerable<Notification>, IEnumerable<NotificationViewModel>>(result, o =>
            {
                o.ToViewModel = x => x.Select(s => s.ToNotificationViewModel());
            });
        }

        [HttpGet("{notificationId}", Name = "GetNotification")]
        public IActionResult GetNotificationById(int notificationId)
        {
            Result<Notification> result = _notificationServices.GetById(notificationId);
            return this.CreateResult<Notification, NotificationViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToNotificationViewModel();
            });
        }

        [HttpPost]
        public IActionResult CreateNotification([FromBody] NotificationViewModel model)
        {
            Result<int> result = _notificationServices.CreateNotification(model.UserId, model.RecipientsEmail, model.SenderEmail, model.Descriptions, model.InviteAccept);
            return this.CreateResult(result);
        }

        [HttpPut("{notificationId}")]
        public IActionResult UpdateStudent(int notificationId, [FromBody] NotificationViewModel model)
        {
            Result<Notification> result = _notificationServices.UpdateNotification(model.NotificationId, model.UserId, model.RecipientsEmail, model.SenderEmail, model.Descriptions, model.InviteAccept);
            return this.CreateResult<Notification, NotificationViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToNotificationViewModel();
            });
        }

        [HttpDelete("{notificationId}")]
        public IActionResult DeleteNotification(int notificationId)
        {
            Result<int> result = _notificationServices.Delete(notificationId);
            return this.CreateResult(result);
        }
    }
}