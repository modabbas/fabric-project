using _4thyearProject;
using FabricProject.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FabricProject.Models.Models
{
    public class MaterialOrderDetailCustomer : BaseModel
    {

        [ForeignKey("ClothMaterialId")]

        public ClothMaterail ClothMaterial { get; set; }

        public int? ClothMaterialId { get; set; }

        [ForeignKey("OrderDetailCustomerId")]

        public CustomerOrderDetail OrderDetailCustomer { get; set; }


        public int? OrderDetailCustomerId { get; set; }


        public int Amount { get; set; }

        public MaterialOrderDetailCustomer()
        {
        }

    }
}
