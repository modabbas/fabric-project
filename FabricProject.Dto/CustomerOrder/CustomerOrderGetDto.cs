using System;
using System.Collections.Generic;
using System.Text;

namespace FabricProject.Dto.CustomerOrder
{
  public  class CustomerOrderGetDto
    {
        public int Id { get; set; } 
        public int OrderCount { get; set; }
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
    }
}
