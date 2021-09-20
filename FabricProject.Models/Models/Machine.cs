using _4thyearProject.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FabricProject.Models.Models
{
    public class Machine : BaseModel
    {
        public string Name { get; set; }

        public string MachineType { get; set; }

        public int? Number { get; set; }

        public ICollection<MachineOrder> MachineOrders { get; set; }
        public ICollection<CustomerOrderDetailsMachine> CustomerOrderDetailsMachine { get; set; }
        public Machine()
        {
            MachineOrders = new List<MachineOrder>();
            CustomerOrderDetailsMachine = new List<CustomerOrderDetailsMachine>();
        }
    }
}
