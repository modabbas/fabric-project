using FabrciProject.IData.Interfaces;
using FabricProject.DContext;
using FabricProject.Dto.OrderMaterials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FabricProject.Data.Repositories
{
  public  class OrderMaterialRepository:IOrderMaterialsRepository
    {
        private FabricProjectDbContext Context { get; }

        public OrderMaterialRepository(FabricProjectDbContext fabricProjectDbContext)
        {
            Context = fabricProjectDbContext;
        }

        public IEnumerable<OrderMaterialsDto> GetOrderMaterials(string query = null)
        {
            var results = Context.MaterialOrderDetailCustomers
                .Where(cloth => (string.IsNullOrEmpty(query)) && cloth.DeletedAt == null)
                .Select(material => new OrderMaterialsDto()
                {

                    Id = material.Id,
                    Material = material.ClothMaterial.Material.Name,
                    OrderDetailCustomerId=material.OrderDetailCustomer.Id,
                    Amount=material.Amount,
                    weight=material.OrderDetailCustomer.PartialWeghit

                }).ToList();
            return results;
        }
        public bool SetOrderMaterial(OrderMaterialsDto orderMaterialsDto)
        {
            try
            {
                var materialEntity = Context.MaterialOrderDetailCustomers
                    .SingleOrDefault(material => material.Id == orderMaterialsDto.Id);
                var clothMaterialEntity = Context.ClothMaterails
                  .SingleOrDefault(material => material.Id == orderMaterialsDto.ClothMaterialId);
                var MaterialEntity = Context.Materials
                 .SingleOrDefault(material => material.Id == clothMaterialEntity.MaterailId);

                var Mat = Convert.ToInt32(MaterialEntity.Amount) - orderMaterialsDto.Amount;
                MaterialEntity.Amount = Mat.ToString();
                if (materialEntity == null)
                {
                    //add case
                    materialEntity = new Models.Models.MaterialOrderDetailCustomer();
                    Context.Attach(materialEntity);
                }
                materialEntity.ClothMaterialId = orderMaterialsDto.ClothMaterialId;
                materialEntity.OrderDetailCustomerId = orderMaterialsDto.OrderDetailCustomerId;
                materialEntity.Amount = orderMaterialsDto.Amount;
                materialEntity.Id = orderMaterialsDto.Id;
                materialEntity.CreatedAt = orderMaterialsDto.CreatedAt;
                materialEntity.EditedAt = orderMaterialsDto.EditedAt;
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool RemoveOrderMaterial(int id)
        {

            try
            {
                var orderMaterialEntity = Context.MaterialOrderDetailCustomers
                    .SingleOrDefault(cat => cat.Id == id && cat.DeletedAt == null);
                if (orderMaterialEntity == null)
                {
                    return false;
                }
                orderMaterialEntity.DeletedAt = DateTime.Now;
                Context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
     public   IEnumerable<OrderMaterialsDto> GetMaterials(string query = null)
        {
            var results = Context.ClothMaterails
               .Where(cloth => (string.IsNullOrEmpty(query)) && cloth.DeletedAt == null)
                .GroupBy(car => car.Material.Name)
                  .Select(g => new  OrderMaterialsDto(){
                      ClothMaterialId=g.First().Id,
                      Material=g.First().Material.Name,
                      MaterialId=g.First().MaterailId 
                  })
   
                   .ToList(); 
            return results;
        }
    }
}
