using FabricProject.Dto.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speculum.ViewModel.Customer
{
    public class CustomerViewModel
    {
        public IEnumerable<CustomerDto> GetCustomers { get; set; }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime EditedAt { get; set; }

        public int CustomerId
        {
            get; set;

        }

    }
}