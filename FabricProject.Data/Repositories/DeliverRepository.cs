using FabrciProject.IData.Interfaces;
using FabricProject.DContext;
using FabricProject.Dto.Deliver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FabricProject.Data.Repositories
{
    public class DeliverRepository : IDeliverRepository
    {
        private FabricProjectDbContext Context { get; }





        public DeliverRepository(FabricProjectDbContext fabricProjectDbContext)
        {
            Context = fabricProjectDbContext;
        }

        public IEnumerable<DeliverDto> GetDelivered(string query = null)
        {
            var results = Context.Delivers
                .Where(deliver =>   deliver.DeletedAt == null )
                .Select(deliver => new DeliverDto()
                {

                    Id = deliver.Id,
                    NewLength = deliver.NewLength,
                    OldLength = deliver.OrderDetail.OldLenght,
                    IsDeliver=deliver.IsDeliver,
                    OrderDetailCustomerId = deliver.OrderDetailCustomerId,
                    customerName   =deliver.OrderDetail.CustomerOrder.Customer.Name,
                    Phone = deliver.OrderDetail.CustomerOrder.Customer.Phone,
                    CreatedAt = deliver.CreatedAt,
                    EditedAt = deliver.EditedAt

                }).ToList();
            return results;
        }

        public IEnumerable<DeliverDto> GetGiven(string query = null)
        {
            var results = Context.Delivers
                .Where(deliver => deliver.DeletedAt == null && deliver.IsDeliver)
                .Select(deliver => new DeliverDto()
                {

                    Id = deliver.Id,
                    NewLength = deliver.NewLength,
                    OldLength = deliver.OrderDetail.OldLenght,
                    IsDeliver = deliver.IsDeliver,
                    OrderDetailCustomerId = deliver.OrderDetailCustomerId,
                    customerName = deliver.OrderDetail.CustomerOrder.Customer.Name,
                    Phone = deliver.OrderDetail.CustomerOrder.Customer.Phone,
                    CreatedAt = deliver.CreatedAt,
                    EditedAt = deliver.EditedAt

                }).ToList();
            return results;
        }
        public bool EnterTheDeliver(DeliverDto deliverDto)
        {
            try
            {
                var deliverEntity = Context.Delivers
                    .SingleOrDefault(deliver => deliver.Id == deliverDto.Id);
                var OrderEntity = Context.OrderDetailCustomers
                   .SingleOrDefault(deliver => deliver.Id == deliverDto.OrderDetailCustomerId);
                
                if (deliverEntity == null)
                {
                    //add case
                    deliverEntity = new Models.Models.Deliver();
                    Context.Attach(deliverEntity);
                }
                deliverEntity.CreatedAt = deliverDto.CreatedAt;
                deliverEntity.EditedAt = deliverDto.EditedAt;
                deliverEntity.OrderDetailCustomerId = deliverDto.OrderDetailCustomerId;
                deliverEntity.NewLength = deliverDto.NewLength;
                deliverEntity.IsDeliver = false;
                deliverEntity.Id = deliverDto.Id;
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
      public  bool LeaveTheDeliver(int id)
        {

            try
            {
                var deliverEntity = Context.Delivers
                    .SingleOrDefault(cat => cat.Id == id && cat.DeletedAt == null);
                if (deliverEntity == null)
                {

                    return false;
                }
                deliverEntity.IsDeliver = true ;
                deliverEntity.EditedAt = DateTime.Now;
                Context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool RemoveDeliver(int id)
        {

            try
            {
                var deliverEntity = Context.Cloths
                    .SingleOrDefault(cat => cat.Id == id && cat.DeletedAt == null);
                if (deliverEntity == null)
                {

                    return false;
                }
                deliverEntity.DeletedAt = DateTime.Now;
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
