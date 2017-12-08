using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Models.ContactViewModels
{
    public class ContactViewModel
    {
        public int UserId { get; set; }

        public int FriendId { get; set; }

        public bool Invitation { get; set; }
    }
}
