using FabricProject.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FabricProject.Models.Models
{
    public class CustomerOrderDetail : BaseModel
    {
        [ForeignKey("CustomerOrderId")]
        public CustomerOrder CustomerOrder { get; set; }

        [ForeignKey("ColorId")]
        public Color Color { get; set; }

        public int ColorId { get; set; }

        public int CustomerOrderId { get; set; }


        public int ColorAmount { get; set; }

        public int OldLenght { get; set; }

        public bool IsOut { get; set; } = false;

        public bool IsDeliver { get; set; } = false;

        public int PartialWeghit { get; set; }

        public ICollection<Deliver> Deliver { get; set; }

        public ICollection<MachineOrder> MachineOrders { get; set; }

        public ICollection<MaterialOrderDetailCustomer> MaterailOrderDetailCustoemrs { get; set; }

        public ICollection<CustomerOrderDetailsMachine> CustomerOrderDetailsMachine { get; set; }
        public CustomerOrderDetail()
        {
            Deliver = new List<Deliver>();
            MaterailOrderDetailCustoemrs = new List<MaterialOrderDetailCustomer>();
            CustomerOrderDetailsMachine = new List<CustomerOrderDetailsMachine>();
        }
    }
}
