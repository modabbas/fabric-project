using _4thyearProject;
using FabricProject.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FabricProject.Models.Models
{
    public class ClothMaterail : BaseModel
    {
        [ForeignKey("ClothId")]
        public Cloth Cloth { get; set; }
        public int ClothId { get; set; }

        [ForeignKey("MaterailId")]
        public Materail Material { get; set; }
        public int MaterailId { get; set; }

        public ICollection<MaterialOrderDetailCustomer> MaterialOrderDetailCustomers { get; set; }

        public ClothMaterail()
        {
            MaterialOrderDetailCustomers = new List<MaterialOrderDetailCustomer>();
        }

    }
}
