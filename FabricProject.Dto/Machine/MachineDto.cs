using _4thyearProject.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FabricProject.Dto.Machine
{
  public  class MachineDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string MachineType { get; set; }

        public int Number { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime EditedAt { get; set; }
        public bool IsAdd { get; set; } = false;
    }
}
