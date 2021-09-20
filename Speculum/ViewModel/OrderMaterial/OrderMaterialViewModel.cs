using FabricProject.Dto.CustomerOrderDetails;
using FabricProject.Dto.OrderMaterials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speculum.ViewModel.OrderMaterial
{
    public class OrderMaterialViewModel
    {

        public IEnumerable<OrderMaterialsDto> orderMaterials { get; set; }
        public IEnumerable<OrderMaterialsDto> Materials { get; set; }
        public IEnumerable<CustomerOrderDetailsGetDto> Orders { get; set; }
        public int MaterialId { get; set; }
        public int ClothMaterialId { get; set; }
    
        public int Id { get; set; }

        public int Amount { get; set; }

        public string Material { get; set; }

        public int weight { get; set; }

        public int OrderDetailCustomerId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime EditedAt { get; set; }
        public ActiveViewModel Active { get; set; }

    }
}
