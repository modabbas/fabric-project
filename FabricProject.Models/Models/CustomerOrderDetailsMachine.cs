using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FabricProject.Models.Models
{
    public class CustomerOrderDetailsMachine : BaseModel
    {
        [ForeignKey("CustomerOrderDetailId")]

        public CustomerOrderDetail CustomerOrderDetail { get; set; }
        public int? CustomerOrderDetailId { get; set; }


        [ForeignKey("MachineId")]
        public Machine Machine { get; set; }

        public int? MachineId { get; set; }

        public string AmountWater { get; set; }



    }
}
