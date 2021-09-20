using System;
using System.Collections.Generic;
using System.Text;

namespace FabricProject.Dto.CustomerOrderDetails
{
   public class RemoveSubOrderDto
    {
        public int ColorId { get; set; }
        public int OrderId { get; set; }
        public string AmountReturn { get; set; }
        public int WeightReturn { get; set; }
    }
}
