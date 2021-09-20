using System;
using System.Collections.Generic;
using System.Text;

namespace FabricProject.Models.Models
{
    public class BaseModel
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime EditedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public BaseModel()
        {
            CreatedAt = DateTime.Now;
            EditedAt = DateTime.Now;

        }
    }
}
