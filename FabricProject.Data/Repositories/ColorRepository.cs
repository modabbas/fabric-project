using FabrciProject.IData.Interfaces;
using FabricProject.DContext;
using FabricProject.Dto.Color;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FabricProject.Dto.Customer;
using Exceptions;
using FabricProject.Models.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FabricProject.SharedKernal.Data;

namespace FabricProject.Data.Repositories
{
    public class ColorRepository : IColorRepository
    {
        private FabricProjectDbContext Context { get; }


        public ColorRepository(FabricProjectDbContext dbContext)
        {
            Context = dbContext;
        }
        public OperationResult<int> RemoveColor(int id)
        {
            var operationResult = new OperationResult<int>()
            {
                ResultData = id
            };
            try
            {
                var categoryEntity = Context
                            .Colors
                            .SingleOrDefault(color => color.Id == id && !color.DeletedAt.HasValue);
                if (categoryEntity == null)
                {
                    operationResult.IsSuccess = true;
                    return operationResult;
                }
                categoryEntity.DeletedAt = DateTime.Now;
                Context.SaveChanges();
                operationResult.IsSuccess = true;
                return operationResult;
            }
            catch (Exception ex)
            {
                operationResult.Exception = ex;
            }
            return operationResult;

        }


        public IEnumerable<ColorDto> GetColors(string query = null)
        {
            var results = Context.Colors
                .Where(color => (string.IsNullOrEmpty(query) || color.Name.Contains(query)) && color.DeletedAt == null)
                .Select(color => new ColorDto()
                {
                    Id = color.Id,
                    Name = color.Name,
                    Amount = color.Amount
                }).ToList();
            return results;

        }

        public ColorDto GetById(int id)
        {
            return Context.Colors
                 .Where(cc => !cc.DeletedAt.HasValue && cc.Id == id)
                 .Select(cc => new ColorDto()
                 {
                     Id = id,
                     Name = cc.Name,
                     Amount = cc.Amount
                 }).Single();
        }


        public ColorDto SetColor(ColorDto colorDto)
        {
            try
            {
                var colorEntity = Context.Colors
                    .SingleOrDefault(color => color.Id == colorDto.Id);
                if (colorEntity == null)
                {
                    var isExist = Context.Colors
                        .Where(color => !color.DeletedAt.HasValue &&
                        color.Name.Contains(colorDto.Name))
                        .Count();

                    if (isExist != 0)
                    {
                        return null;
                    }
                    colorDto.IsAdd = true;
                    //add case
                    colorEntity = new Models.Models.Color();
                    Context.Attach(colorEntity);
                }
                colorEntity.Name = colorDto.Name;
                colorEntity.EditedAt = colorDto.EditedAt;
                colorEntity.CreatedAt = colorDto.CreatedAt;
                colorEntity.Amount = colorDto.Amount;
                Context.SaveChanges();
                colorDto.Id = colorEntity.Id;
                return colorDto;
            }
            catch (ColorException)
            {
                return colorDto;
            }

        }


        public bool IsColorExit(string colorName)
        {
            return Context.Colors
                .Any(cat => cat.Name.ToUpper() == colorName.ToUpper());
        }


    }
}

