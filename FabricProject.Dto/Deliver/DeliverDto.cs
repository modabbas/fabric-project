using System;
using System.Collections.Generic;
using System.Text;

namespace FabricProject.Dto.Deliver
{
  public   class DeliverDto
    {
       
        //post
        public int Id { get; set; }

        public int OrderDetailCustomerId { get; set; }

        public bool IsDeliver { get; set; } = false;

        public int NewLength { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime EditedAt { get; set; }

        //these ones are for get !
        public int OldLength { get; set; }
        public int MainOrderId { get; set; }
        public string customerName { get; set; }

        public string Phone { get; set; }
    }
}
