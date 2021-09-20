using _4thyearProject.Enum;
using FabricProject.Dto.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speculum.ViewModel.Machine
{
    public class MachineViewModel
    {
        public IEnumerable<MachineDto> GetMachines { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Number { get; set; }

        public string machineType { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime EditeAt { get; set; }
        public ActiveViewModel Active { get; set; }


    }
}
