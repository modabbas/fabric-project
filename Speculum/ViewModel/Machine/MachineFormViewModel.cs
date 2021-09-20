using _4thyearProject.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speculum.ViewModel.Machine
{
    public class MachineFormViewModel
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public int Number { get; set; }

        public MachineType machineType { get; set; }
        public ActiveViewModel Active { get; set; }

    }
}
