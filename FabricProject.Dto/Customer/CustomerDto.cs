using System;

namespace FabricProject.Dto.Customer
{
    public class CustomerDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int numOrders { get; set; }

        public int CustomerId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime EditedAt { get; set; }

        public bool IsAdd { get; set; } = false;
    }
}
