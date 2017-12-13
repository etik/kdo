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
        readonly ContactServices _contactServices;

        public NotificationController(NotificationServices notificationServices, ContactServices contactServices)
        {
            _notificationServices = notificationServices;
            _contactServices = contactServices;
        }

        [HttpGet("{userId}/getAll")]
        public IActionResult GetAllNotificationByUserId(int userId)
        {
            Result<IEnumerable<Notification>> result = _notificationServices.GetAllByUserId(userId);
            return this.CreateResult<IEnumerable<Notification>, IEnumerable<NotificationViewModel>>(result, o =>
            {
                o.ToViewModel = x => x.Select(s => s.ToNotificationViewModel());
            });
        }
    }
}