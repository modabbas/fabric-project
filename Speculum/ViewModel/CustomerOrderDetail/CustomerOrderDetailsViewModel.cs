using FabricProject.Dto.Color;
using FabricProject.Dto.CustomerOrder;
using FabricProject.Dto.CustomerOrderDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speculum.ViewModel.CustomerOrderDetail
{
    public class CustomerOrderDetailsViewModel
    {

        public IEnumerable<CustomerOrderDetailsGetDto> CustomerOrderDetails { get; set; }

        public IEnumerable<CustomerOrderDetailsGetDto> OrdersInMachineDetails { get; set; }

        public IEnumerable<CustomerOrderDetailsGetDto> DoneOrders { get; set; }


        public IEnumerable<ColorDto> Colors { get; set; }

        public IEnumerable<CustomerOrderGetDto> CustomerOrders { get; set; }

        public int Id { get; set; }

        public int ColorId { get; set; }

        public int CustomerOrderId { get; set; }

        public string CustomerName { get; set; }

        public string ClothName { get; set; }

        public int ColorAmount { get; set; }

        public int OldLenght { get; set; }

        public bool IsOut { get; set; } = false;

        public bool IsDeliver { get; set; } = false;

        public int PartialWeghit { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime EditedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
        public ActiveViewModel Active { get; set; }

    }
}
