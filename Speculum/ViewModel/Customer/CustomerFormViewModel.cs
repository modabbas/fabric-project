using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speculum.ViewModel.Customer
{
    public class CustomerFormViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int CustomerId { get; set; }
        public ActiveViewModel Active { get; set; }

    }
}
