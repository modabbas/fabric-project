using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FabricProject.Models.Models
{
    public class Customer :BaseModel
    {

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        [Key]
        ///This Will Be PrimaryKey
        public int CustomerId { get; set; }
        public ICollection<CustomerOrder> CustomerOrders { get; set; }

        public Customer()
        {

        }
    }
}
