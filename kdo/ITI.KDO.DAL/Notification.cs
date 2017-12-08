using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.KDO.DAL
{
    public class Notification
    {
        public int NotificationId { get; set; }

        public string RecipientsEmail { get; set; }

        public string SenderEmail { get; set; }

        public string Descriptions { get; set; }

        public bool InviteAccept { get; set; }
    }
}
