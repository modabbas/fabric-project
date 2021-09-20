using FabrciProject.IData.Interfaces;
using FabricProject.DContext;
using FabricProject.Dto.Cloth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FabricProject.Data.Repositories
{
    public class ClothRepository : IClothRespository
    {
        private FabricProjectDbContext Context { get; }

        public ClothRepository(FabricProjectDbContext fabricProjectDbContext)
        {
            Context = fabricProjectDbContext;
        }

        public IEnumerable<ClothDto> GetCloths(string query = null)
        {
            var results = Context.Cloths
                .Where(cloth => (string.IsNullOrEmpty(query) || cloth.Name.Contains(query)) && cloth.DeletedAt == null)
                .Select(cloth => new ClothDto()
                {

                    Id = cloth.Id,
                    Name = cloth.Name,
                    CreatedAt = cloth.CreatedAt,
                    EditedAt = cloth.EditedAt

                }).ToList();
            return results;
        }
        public ClothDto SetCloth(ClothDto clothDto)
        {
            try
            {
                var clothEntity = Context.Cloths
                    .SingleOrDefault(store => store.Id == clothDto.Id);
                if (clothEntity == null)
                {
                    //add case
                    var isExist = Context.Cloths
                       .Where(cloth => !cloth.DeletedAt.HasValue &&
                       cloth.Name.Contains(clothDto.Name))
                       .Count();
                    if (isExist != 0)
                    {
                        return null;
                    }
                    clothDto.isAdd = true;
                    clothEntity = new Models.Models.Cloth();
                    Context.Attach(clothEntity);
                }
                clothEntity.Name = clothDto.Name;
                clothEntity.Id = clothDto.Id;
                Context.SaveChanges();
                clothDto.Id = clothEntity.Id;
                return clothDto;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public ClothDto GetById(int id)
        {
            return
                Context.Cloths
                 .Where(cc => !cc.DeletedAt.HasValue && cc.Id == id)
                 .Select(cc => new ClothDto()
                 {
                     Id = id,
                     Name = cc.Name,
                 }).Single();
        }


        public bool RemoveCloth(int id)
        {

            try
            {
                var clothEntity = Context.Cloths
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
