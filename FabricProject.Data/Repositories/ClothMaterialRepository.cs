using FabrciProject.IData.Interfaces;
using FabricProject.DContext;
using FabricProject.Dto.ClothMaterial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FabricProject.Data.Repositories
{
  public  class ClothMaterialRepository:IClothMaterialRepository
    {
        private FabricProjectDbContext Context { get; }

        public ClothMaterialRepository(FabricProjectDbContext fabricProjectDbContext)
        {
            Context = fabricProjectDbContext;
        }

        public IEnumerable<ClothMaterialDto> GetClothMaterials(string query = null)
        {
            var results = Context.ClothMaterails
                .Where(cloth => (string.IsNullOrEmpty(query)   && cloth.DeletedAt == null))
                .Select(cloth => new ClothMaterialDto()
                {

                    Id = cloth.Id,
                    Material = cloth.Material.Name,
                    Cloth=cloth.Cloth.Name

                }).ToList();
            return results;
        }
        public bool SetClothMaterial(ClothMaterialDto clothMaterialDto)
        {
            try
            {
                var clothMaterialEntity = Context.ClothMaterails
                    .SingleOrDefault(store => store.Id == clothMaterialDto.Id);
                if (clothMaterialEntity == null)
                {
                    //add case
                    clothMaterialEntity = new Models.Models.ClothMaterail();
                    Context.Attach(clothMaterialEntity);
                }
                clothMaterialEntity.MaterailId = clothMaterialDto.MaterailId;
                clothMaterialEntity.ClothId = clothMaterialDto.ClothId;

                clothMaterialEntity.Id = clothMaterialDto.Id;
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool RemoveClothMaterial(int id)
        {

            try
            {
                var clothEntity = Context.ClothMaterails
                    .SingleOrDefault(cat => cat.Id == id && cat.DeletedAt == null);
                if (clothEntity == null)
                {

                    return false;
                }
                clothEntity.DeletedAt = DateTime.Now;
                Context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
