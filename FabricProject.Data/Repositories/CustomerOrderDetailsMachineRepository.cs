using FabrciProject.IData.Interfaces;
using FabricProject.DContext;
using FabricProject.Dto.CustomerOrderDetailsMachine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FabricProject.Data.Repositories
{
    public class CustomerOrderDetailsMachineRepository : ICustomerOrderDetailsMachineRepository
    {
        private FabricProjectDbContext Context { get; }


        public CustomerOrderDetailsMachineRepository(FabricProjectDbContext fabricProjectDbContext)
        {
            Context = fabricProjectDbContext;

        }
        public IEnumerable<CustomerOrderDetailsMachineDto> GetMachineOrders(string query = null)
        {
            var results = Context.CustomerOrderDetailsMachine
                   .Where(oo => string.IsNullOrEmpty(query) && oo.DeletedAt == null)
                   .Select(oo => new CustomerOrderDetailsMachineDto()
                   {
                       Id = oo.Id,
                       CustomerOrderDetailsId = oo.CustomerOrderDetailId.Value,
                       MachineId = oo.MachineId.Value,
                       AmountWater = oo.AmountWater,
                   })
                   .ToList();

            return results;
        }

        public bool RemoveOrderMachine(int id)
        {
            try
            {
                var OrderMachineEntity = Context.CustomerOrderDetailsMachine
                    .SingleOrDefault(lab => lab.Id == id && lab.DeletedAt == null);
                if (OrderMachineEntity == null) return false;
                OrderMachineEntity.DeletedAt = DateTime.Now;
                Context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        public bool SetOrderMachine(CustomerOrderDetailsMachineDto customerOrderDetailsMachineDto)
        {
            try
            {
                var BigOrderMachineEntity = Context.CustomerOrderDetailsMachine
                    .SingleOrDefault(ll => ll.Id == customerOrderDetailsMachineDto.Id);
                if (BigOrderMachineEntity == null)
                {
                    BigOrderMachineEntity = new Models.Models.CustomerOrderDetailsMachine();
                    Context.Attach(BigOrderMachineEntity);
                }
                BigOrderMachineEntity.CustomerOrderDetailId = customerOrderDetailsMachineDto.CustomerOrderDetailsId;
                BigOrderMachineEntity.MachineId = customerOrderDetailsMachineDto.MachineId;
                BigOrderMachineEntity.AmountWater = customerOrderDetailsMachineDto.AmountWater;
                BigOrderMachineEntity.EditedAt = DateTime.Now;
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
