using FabricProject.Dto.CustomerOrderDetails;
using FabricProject.Dto.Deliver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speculum.ViewModel.Deliver
{
    public class DeliverViewModel
    {
        public IEnumerable<DeliverDto> DeliverDetails { get; set; } // مستودع لم تسلم بعد

        public IEnumerable<DeliverDto> GoneOrders { get; set; }// طلبيات المسلمة

        public IEnumerable<CustomerOrderDetailsGetDto> Orders { get; set; }

        public int Id { get; set; }

        public int NewLength { get; set; }


        public int CustomerOrderId { get; set; }

      
 
        public bool IsDeliver { get; set; } = false;
 
        public DateTime CreatedAt { get; set; }

        public DateTime EditedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
        public ActiveViewModel Active { get; set; }

    }
}
