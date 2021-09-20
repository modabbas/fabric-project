using FabrciProject.IData.Interfaces;
using FabricProject.DContext;
using FabricProject.Dto.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FabricProject.Data.Repositories
{
   public  class MaterialRepository:IMaterialRepository
    {
        private FabricProjectDbContext Context { get; }

        public MaterialRepository(FabricProjectDbContext fabricProjectDbContext)
        {
            Context = fabricProjectDbContext;
        }

        public IEnumerable<MaterialDto> GetMaterials(string query = null)
        {
            var results = Context.Materials
                .Where(cloth => (string.IsNullOrEmpty(query) || cloth.Name.Contains(query)) && cloth.DeletedAt == null)
                .Select(material => new MaterialDto()
                {

                    Id = material.Id,
                    Name = material.Name,
                    Amount=material.Amount,
                    CreatedAt = material.CreatedAt,
                    EditedAt = material.EditedAt

                }).ToList();
            return results;
        }
        public bool SetMaterial(MaterialDto materialDto)
        {
            try
            {
                var materialEntity = Context.Materials
                    .SingleOrDefault(material => material.Id == materialDto.Id);
                if (materialEntity == null)
                {
                    //add case
                    materialEntity = new Models.Models.Materail();
                    Context.Attach(materialEntity);
                }
                materialEntity.Name = materialDto.Name;
                materialEntity.Amount = materialDto.Amount;
                materialEntity.Id = materialDto.Id;
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool RemoveMaterial(int id)
        {

            try
            {
                var materialEntity = Context.Materials
                    .SingleOrDefault(cat => cat.Id == id && cat.DeletedAt == null);
                if (materialEntity == null)
                {

                    return false;
                }
                materialEntity.DeletedAt = DateTime.Now;
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
