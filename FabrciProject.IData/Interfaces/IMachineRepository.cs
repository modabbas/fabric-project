using FabricProject.Dto.Machine;
using System;
using System.Collections.Generic;
using System.Text;

namespace FabrciProject.IData.Interfaces
{
    public interface IMachineRepository
    {
        IEnumerable<MachineDto> Get(string query = null);
        bool Remove(int id);
        MachineDto Set(MachineDto NewMachine);


    }
}
