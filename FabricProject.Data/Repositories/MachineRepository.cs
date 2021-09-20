using FabrciProject.IData.Interfaces;
using FabricProject.DContext;
using FabricProject.Dto.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FabricProject.Data.Repositories
{
    public class MachineRepository : IMachineRepository

    {
        private readonly FabricProjectDbContext context;

        public MachineRepository(FabricProjectDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<MachineDto> Get(string query = null)
        {
            var results = context.Machines
                    .Where(oo => (string.IsNullOrEmpty(query) || oo.Name.Contains(query)) && oo.DeletedAt == null)
                    .Select(oo => new MachineDto()
                    {
                        Id = oo.Id,
                        MachineType = oo.MachineType,
                        Name = oo.Name,
                        Number = oo.Number.Value

                    }).ToList();
            return results;
        }

        public bool Remove(int id)
        {
            var result = context.Machines
                .SingleOrDefault(M => M.Id == id);
            if (result != null)
            {
                result.DeletedAt = DateTime.Now;
                context.SaveChanges();
                return true;
            }
            return false;

        }

        public MachineDto Set(MachineDto NewMachine)
        {
            try
            {
                var result = context.Machines
                    .SingleOrDefault(M => M.Id == NewMachine.Id);
                if (result == null)
                {
                    var isExist = context.Machines
                      .Where(machine => !machine.DeletedAt.HasValue &&
                      machine.Name.Contains(NewMachine.Name))
                      .Count();
                    if (isExist != 0)
                    {
                        return null;
                    }
                    NewMachine.IsAdd = true;
                    result = new Models.Models.Machine();
                    context.Attach(result);
                    result.CreatedAt = NewMachine.CreatedAt;
                }
                result.Name = NewMachine.Name;
                result.MachineType = NewMachine.MachineType;
                result.EditedAt = NewMachine.EditedAt;
                context.SaveChanges();
                NewMachine.Id = result.Id;
                return NewMachine;
            }
            catch (Exception) { return null; }
        }
    }
}
