using FabricProject.Dto.Material;
using System;
using System.Collections.Generic;
using System.Text;

namespace FabrciProject.IData.Interfaces
{
    public interface IMaterialRepository
    {
        IEnumerable<MaterialDto> GetMaterials(string query = null);
        bool  SetMaterial(MaterialDto materialDto);
        bool RemoveMaterial(int id);
    }
}
