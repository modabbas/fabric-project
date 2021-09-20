using System;
using System.Collections.Generic;
using System.Text;

namespace FabricProject.Dto.CustomerOrderDetails
{
  public  class CustomerOrderDetailsGetDto
    {
        public int Id { get; set; }

        public int ColorId { get; set; }

        public int CustomerOrderId { get; set; }

        public string CustomerName { get; set; }

        public string ClothName { get; set; }

        public string ColorName { get; set; }

        public int ColorAmount { get; set; }

        public int OldLenght { get; set; }

        public bool IsOut { get; set; } = false;

        public bool IsDeliver { get; set; } = false;

        public int PartialWeghit { get; set; }

        public int GoneToDeliver { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime EditedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
