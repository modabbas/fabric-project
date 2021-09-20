using FabricProject.Dto.Cloth;
using System;
using System.Collections.Generic;
using System.Text;

namespace FabrciProject.IData.Interfaces
{
    public interface IClothRespository
    {
        IEnumerable<ClothDto> GetCloths(string query = null);
        ClothDto SetCloth(ClothDto clothDto);
        bool RemoveCloth(int id);
        ClothDto GetById(int id);

    }
}
