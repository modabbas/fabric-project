using FabrciProject.IData.Interfaces;
using FabricProject.DContext;
using FabricProject.Dto.Lab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace FabricProject.Data.Repositories
{
    public class LabRepository : ILabRepository
    {

        private FabricProjectDbContext Context { get; }


        public LabRepository(FabricProjectDbContext fabricProjectDbContext)
        {
            Context = fabricProjectDbContext;

        }
        public IEnumerable<LabDto> GetSamples(string query = null)
        {
            var results = Context.Labs
                   .Where(oo => (string.IsNullOrEmpty(query) || oo.Cloth.Name.Contains(query)) && oo.DeletedAt == null)
                   .Select(oo => new LabDto()
                   {
                       ClothId = oo.ClothId,
                       NewColorId = oo.ColorId,
                       ClothName = oo.Cloth.Name,
                       ColorName = oo.NewColor.Name,
                       SuccuessPercent = oo.SuccuessPercent,
                       Id = oo.Id,
                   }).ToList();

            return results;
        }

        public bool RemoveSample(int id)
        {
            try
            {
                var LabEntity = Context.Labs
                    .SingleOrDefault(lab => lab.Id == id && lab.DeletedAt == null);
                if (LabEntity == null) return false;
                LabEntity.DeletedAt = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        public LabDto SetSample(LabDto labDto)
        {
            try
            {
                var labEntity =
                    Context
                    .Labs
                    .SingleOrDefault(lab => lab.Id == labDto.Id);
                if (labEntity == null)
                {
                    //add case
                    labEntity = new Models.Models.Lab();
                    Context.Attach(labEntity);
                }
                labEntity.ClothId = labDto.ClothId;
                labEntity.ColorId = labDto.NewColorId;
                labEntity.SuccuessPercent = labDto.SuccuessPercent;
                labEntity.EditedAt = labDto.EditedAt;
                labEntity.CreatedAt = labDto.CreatedAt;
                Context.SaveChanges();

                labDto.ClothName = Context.Cloths
                    .Where(cloth => cloth.Id == labEntity.ClothId)
                    .Select(cloth => cloth.Name)
                    .SingleOrDefault();
                labDto.ColorName = Context.Colors
                    .Where(color => color.Id == labEntity.ColorId)
                  .Select(color => color.Name)
                 .SingleOrDefault();
                labDto.Id = labEntity.Id;
                return labDto;
            }
            catch (Exception)
            {
                return labDto;
            }

        }


    }
}
