using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.KDO.DAL
{
    public class Contact
    {
        public int PersonOneId { get; set; }

        public int PersonTwoId { get; set; }

        public bool PendingInvite { get; set; }
    }
}
