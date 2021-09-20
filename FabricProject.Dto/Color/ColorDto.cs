using System;
using System.Collections.Generic;
using System.Text;

namespace FabricProject.Dto.Color
{
    public class ColorDto
    {


        public int Id { get; set; }

        public string Name { get; set; }

        public string Amount { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsAdd { get; set; } = false;

        public DateTime EditedAt { get; set; }


        public ColorDto()
        {
            CreatedAt = DateTime.Now;
            EditedAt = DateTime.Now;
        }

    }
}
