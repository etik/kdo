using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.KDO.DAL
{
    public class ItemQuantity
    {
        public int QuantityId { get; set; }

        public int Quantity { get; set; }

        public int RecipientId { get; set; }

        public int NominatorId { get; set; }

        public int EventId { get; set; }

        public int PresentId { get; set; }
    }
}
