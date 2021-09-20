using FabricProject.Dto.ClothMaterial;
using System;
using System.Collections.Generic;
using System.Text;

namespace FabrciProject.IData.Interfaces
{
   
        public interface IClothMaterialRepository
        {
            IEnumerable<ClothMaterialDto> GetClothMaterials(string query = null);
            bool SetClothMaterial(ClothMaterialDto clothMaterialDto);
            bool RemoveClothMaterial(int id);
  }       
    
}
