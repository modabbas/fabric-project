using FabricProject.Dto.Cloth;
using FabricProject.Dto.Customer;
using FabricProject.Dto.CustomerOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speculum.ViewModel.CustomerOrder
{
    public class CustomerOrderViewModel
    {
        public IEnumerable<CustomerOrderGetDto> CustomerOrders { get; set; }
        public IEnumerable<CustomerDto> Customers { get; set; }
        public IEnumerable<ClothDto> Cloths { get; set; }
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int Weight { get; set; }
        public int ClothId { get; set; }
        public string ClothName { get; set; }
        public int PercentCotton { get; set; }
        public int PercentPolister { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime EditedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public ActiveViewModel Active { get; set; }

    }
}
