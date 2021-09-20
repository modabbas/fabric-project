using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FabricProject.Models.Models
{
    public class Lab : BaseModel
    {


        public int? ColorId { get; set; }
        public int? ClothId { get; set; }

        [ForeignKey("ColorId")]

        public Color NewColor { get; set; }

        [ForeignKey("ClothId")]

        public Cloth Cloth { get; set; }


        public int SuccuessPercent { get; set; }

        public Lab()
        {

        }
    }
}
