using FabricProject.Dto.Color;
using FabricProject.Dto.Customer;
using FabricProject.Dto.CustomerOrder;
using FabricProject.Dto.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speculum.ViewModel.Statistics
{
    public class ChartViewModel
    {
        public IEnumerable<ColorDto> Colors { get; set; }
        public IEnumerable<MaterialDto> Materials { get; set; }
        public IEnumerable<CustomerOrderGetDto> CustomersOrders { get; set; }
        public IEnumerable<CustomerOrderDto> Customers { get; set; }
        public int[] CountOrderOfMonth { get; set; }
        public ActiveViewModel Active { get; set; }


    }
}
