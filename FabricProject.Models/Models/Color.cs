using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FabricProject.Models.Models
{
    public class Color : BaseModel
    {

        public string Name { get; set; }

        public string Amount { get; set; }


        public virtual ICollection<Lab> Labs { get; set; }


        public ICollection<CustomerOrderDetail> OrderDetailCustomers { get; set; }
        public Color()
        {
            Labs = new List<Lab>();

        }
    }
}
