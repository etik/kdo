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
    }
}
