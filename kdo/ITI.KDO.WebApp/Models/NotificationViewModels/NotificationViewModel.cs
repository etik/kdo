using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Models.NotificationViewModels
{
    public class NotificationViewModel
    {
        public int NotificationId { get; set; }

        public int UserId { get; set; }

        public string RecipientsEmail { get; set; }

        public string SenderEmail { get; set; }

        public string Descriptions { get; set; }

        public bool InviteAccept { get; set; }
    }
}
