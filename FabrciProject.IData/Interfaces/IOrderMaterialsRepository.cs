using FabricProject.Dto.OrderMaterials;
using System;
using System.Collections.Generic;
using System.Text;

namespace FabrciProject.IData.Interfaces
{
    public interface IOrderMaterialsRepository
    {
       
            IEnumerable<OrderMaterialsDto> GetOrderMaterials(string query = null);
            bool SetOrderMaterial(OrderMaterialsDto orderMaterialsDto);
            bool RemoveOrderMaterial(int id);

        IEnumerable<OrderMaterialsDto> GetMaterials(string query = null);



    }
}
