using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FabricProject.Models.Models
{
    public class Deliver : BaseModel
    {

        public int Length { get; set; }


        [ForeignKey("OrderDetailCustomerId")]
        public CustomerOrderDetail OrderDetail { get; set; }

        //ExsposeId
        public int OrderDetailCustomerId { get; set; }

        public bool IsDeliver { get; set; } = false;

        public int NewLength { get; set; }
        public Deliver()
        {

        }
    }
}
