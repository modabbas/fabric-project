using System;
using System.Collections.Generic;
using System.Text;

namespace FabricProject.Dto.CustomerOrderDetailsMachine
{
    public class CustomerOrderDetailsMachineDto
    {
        public int Id { get; set; }
        
        public int MachineId { get; set; }

        public int CustomerOrderDetailsId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime EditedAt { get; set; }

        public string AmountWater { get; set; }

        public CustomerOrderDetailsMachineDto()
        {
            CreatedAt = DateTime.Now;
            EditedAt = DateTime.Now;
        }

    }
}
