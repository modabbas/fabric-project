using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FabricProject.Models.Models
{
    public class CustomerOrder : BaseModel
    {

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        //ExposeId
        public int CustomerId { get; set; }

        public int Weight { get; set; }

        [ForeignKey("ClothId")]
        public Cloth Cloth { get; set; }
        //ExposeId
        public int ClothId { get; set; }


        public int PercentCotton { get; set; }

        public int PercentPolister { get; set; }

        //Not sure About It 
        public string OldColor { get; set; }


        public ICollection<CustomerOrderDetail> OrderDetailCustomers { get; set; }

        public CustomerOrder()
        {

        }
    }
}
