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
    }
}