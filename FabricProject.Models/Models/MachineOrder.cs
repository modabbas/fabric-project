using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FabricProject.Models.Models
{
    public class MachineOrder : BaseModel
    {

        [ForeignKey("OrderDetailId")]
        public CustomerOrderDetail OrderDetailCustomer { get; set; }

        public int OrderDetailId { get; set; }
        [ForeignKey("MachineId")]
        public Machine Machine { get; set; }
      
        public int MachineId { get; set; }

        public int WaterAmount { get; set; }
        public MachineOrder()
        {

        }

    }
}
