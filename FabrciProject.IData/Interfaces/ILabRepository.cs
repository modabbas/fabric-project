using FabricProject.Dto.Lab;
using System;
using System.Collections.Generic;
using System.Text;

namespace FabrciProject.IData.Interfaces
{
    public interface ILabRepository
    {
        IEnumerable<LabDto> GetSamples(string query = null);
        public LabDto SetSample(LabDto labDto);
        bool RemoveSample(int id);
    }
}
