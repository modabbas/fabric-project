using FabrciProject.IData.Interfaces;
using FabricProject.DContext;
using FabricProject.Dto.CustomerOrderDetails;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FabricProject.Data.Repositories
{
    public class CustomerOrderDetailsRepository : ICustomerOrderDetailsRepository
    {
        private FabricProjectDbContext Context { get; }

        public CustomerOrderDetailsRepository(FabricProjectDbContext fabricProjectDbContext)
        {
            Context = fabricProjectDbContext;
        }

        public IEnumerable<CustomerOrderDetailsGetDto> GetCustomerOrderDetails(string query = null)
        {
            var results = Context.OrderDetailCustomers
                .Where(customerOrderDetail => (string.IsNullOrEmpty(query) && customerOrderDetail.DeletedAt == null && customerOrderDetail.IsOut == false && customerOrderDetail.IsDeliver == false))
                .Select(customerOrderDetail => new CustomerOrderDetailsGetDto()
                {
                    CustomerName = customerOrderDetail.CustomerOrder.Customer.Name,
                    ClothName = customerOrderDetail.CustomerOrder.Cloth.Name,
                    ColorName = customerOrderDetail.Color.Name,
                    PartialWeghit = customerOrderDetail.PartialWeghit,
                    OldLenght = customerOrderDetail.OldLenght,
                    CreatedAt = customerOrderDetail.CreatedAt,
                    ColorAmount = customerOrderDetail.ColorAmount,
                    ColorId = customerOrderDetail.ColorId,
                    CustomerOrderId = customerOrderDetail.CustomerOrderId,
                    Id = customerOrderDetail.Id

                }).ToList();
            return results;
        }

        public IEnumerable<CustomerOrderDetailsGetDto> GetOrdersInMachineDetails(string query = null)
        {
            var results = Context.OrderDetailCustomers
                .Where(customerOrderDetail => (string.IsNullOrEmpty(query) && customerOrderDetail.DeletedAt == null && customerOrderDetail.IsOut == false && customerOrderDetail.IsDeliver == true))
                .Select(customerOrderDetail => new CustomerOrderDetailsGetDto()
                {
                    CustomerName = customerOrderDetail.CustomerOrder.Customer.Name,
                    ClothName = customerOrderDetail.CustomerOrder.Cloth.Name,
                    ColorName = customerOrderDetail.Color.Name,
                    PartialWeghit = customerOrderDetail.PartialWeghit,
                    OldLenght = customerOrderDetail.OldLenght,
                    CreatedAt = customerOrderDetail.CreatedAt,
                    ColorAmount = customerOrderDetail.ColorAmount,
                    ColorId = customerOrderDetail.ColorId,
                    CustomerOrderId = customerOrderDetail.CustomerOrderId,
                    Id = customerOrderDetail.Id

                }).ToList();
            return results;
        }

        public IEnumerable<CustomerOrderDetailsGetDto> GetDoneOrders(string query = null)
        {
            var results = Context.OrderDetailCustomers
                .Where(customerOrderDetail => (string.IsNullOrEmpty(query) && customerOrderDetail.DeletedAt == null && customerOrderDetail.IsOut == true && customerOrderDetail.IsDeliver == true))
                .Select(customerOrderDetail => new CustomerOrderDetailsGetDto()
                {
                    CustomerName = customerOrderDetail.CustomerOrder.Customer.Name,
                    ClothName = customerOrderDetail.CustomerOrder.Cloth.Name,
                    ColorName = customerOrderDetail.Color.Name,
                    PartialWeghit = customerOrderDetail.PartialWeghit,
                    OldLenght = customerOrderDetail.OldLenght,
                    CreatedAt = customerOrderDetail.CreatedAt,
                    ColorAmount = customerOrderDetail.ColorAmount,
                    ColorId = customerOrderDetail.ColorId,
                    CustomerOrderId = customerOrderDetail.CustomerOrderId,
                    Id = customerOrderDetail.Id,
                    GoneToDeliver = customerOrderDetail.Deliver.Count()

                }).ToList();
            return results;
        }

        public IEnumerable<CustomerOrderDetailsGetDto> GetDoneOrdersOfDeliver(string query = null)
        {
            var results = Context.OrderDetailCustomers
                .Where(customerOrderDetail => (string.IsNullOrEmpty(query) && customerOrderDetail.DeletedAt == null && customerOrderDetail.IsOut == true && customerOrderDetail.IsDeliver == true) && customerOrderDetail.Deliver.Count == 0)
                .Select(customerOrderDetail => new CustomerOrderDetailsGetDto()
                {
                    CustomerName = customerOrderDetail.CustomerOrder.Customer.Name,
                    ClothName = customerOrderDetail.CustomerOrder.Cloth.Name,
                    ColorName = customerOrderDetail.Color.Name,
                    PartialWeghit = customerOrderDetail.PartialWeghit,
                    OldLenght = customerOrderDetail.OldLenght,
                    CreatedAt = customerOrderDetail.CreatedAt,
                    ColorAmount = customerOrderDetail.ColorAmount,
                    ColorId = customerOrderDetail.ColorId,
                    CustomerOrderId = customerOrderDetail.CustomerOrderId,
                    Id = customerOrderDetail.Id,
                    GoneToDeliver = customerOrderDetail.Deliver.Count()

                }).ToList();
            return results;
        }

        public CustomerOrderDetailsDto SetCustomerOrderDetails(CustomerOrderDetailsDto customerOrderDetailsDto)
        {
            try
            {
                var customerOrderEntity = Context.OrderDetailCustomers
                    .SingleOrDefault(order => order.Id == customerOrderDetailsDto.Id);
                var ColorEntity = Context.Colors
                    .SingleOrDefault(color => color.Id == customerOrderDetailsDto.ColorId);
                var BigOrderEntity = Context.CustomerOrders
                    .Where(order => order.Id == customerOrderDetailsDto.CustomerOrderId)
                    .SingleOrDefault();
                var OrderWieght = Convert.ToInt32(BigOrderEntity.Weight) - customerOrderDetailsDto.PartialWeghit;
                BigOrderEntity.Weight = OrderWieght;
                var Color = Convert.ToInt32(ColorEntity.Amount) - customerOrderDetailsDto.ColorAmount;
                ColorEntity.Amount = Convert.ToString(Color);
                if (customerOrderEntity == null)
                {
                    //add case
                    customerOrderEntity = new Models.Models.CustomerOrderDetail();
                    Context.Attach(customerOrderEntity);
                }
                customerOrderEntity.ColorId = customerOrderDetailsDto.ColorId;
                customerOrderEntity.ColorAmount = customerOrderDetailsDto.ColorAmount;
                customerOrderEntity.CustomerOrderId = customerOrderDetailsDto.CustomerOrderId;
                customerOrderEntity.EditedAt = customerOrderDetailsDto.EditedAt;
                customerOrderEntity.CreatedAt = customerOrderDetailsDto.CreatedAt;
                customerOrderEntity.OldLenght = customerOrderDetailsDto.OldLenght;
                customerOrderEntity.PartialWeghit = customerOrderDetailsDto.PartialWeghit;
                customerOrderEntity.IsDeliver = false;
                customerOrderEntity.IsOut = false;
                Context.SaveChanges();
                customerOrderDetailsDto.Id = customerOrderEntity.Id;

                customerOrderDetailsDto.ClothName = Context.CustomerOrders
                    .Where(co => co.Id == customerOrderDetailsDto.CustomerOrderId)
                    .Select(co => co.Cloth.Name)
                    .SingleOrDefault();
                customerOrderDetailsDto.CustomerName = Context.CustomerOrders
                    .Where(co => co.Id == customerOrderDetailsDto.CustomerOrderId)
                    .Select(co => co.Customer.Name)
                    .SingleOrDefault();
                customerOrderDetailsDto.ColorName = Context.Colors
                    .Where(color => color.Id == customerOrderDetailsDto.ColorId)
                    .Select(color => color.Name)
                    .SingleOrDefault();
                return customerOrderDetailsDto;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public RemoveSubOrderDto RemoveCustomerOrderDetails(int id)
        {
            try
            {
                RemoveSubOrderDto removeSubOrderDto = new RemoveSubOrderDto();
                var CustomerOrderEntity = Context.OrderDetailCustomers
                    .SingleOrDefault(cat => cat.Id == id && cat.DeletedAt == null);
                CustomerOrderEntity.DeletedAt = DateTime.Now;
                var BigOrderEntity = Context.CustomerOrders
                    .Where(co => co.Id == CustomerOrderEntity.CustomerOrderId)
                    .SingleOrDefault();
                var ColorEntity = Context.Colors
                    .Where(color => color.Id == CustomerOrderEntity.ColorId)
                    .SingleOrDefault();
                BigOrderEntity.Weight += CustomerOrderEntity.PartialWeghit;
                ColorEntity.Amount = (Convert.ToInt32(ColorEntity.Amount) +
                    CustomerOrderEntity.ColorAmount)
                    .ToString();
                Context.SaveChanges();
                removeSubOrderDto.WeightReturn = BigOrderEntity.Weight;
                removeSubOrderDto.AmountReturn = ColorEntity.Amount;
                removeSubOrderDto.ColorId = CustomerOrderEntity.ColorId;
                removeSubOrderDto.OrderId = CustomerOrderEntity.CustomerOrderId;
                return removeSubOrderDto;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool RemoveOrderFromDeliver(int id)
        {

            try
            {
                var CustomerOrderEntity = Context.OrderDetailCustomers
                    .SingleOrDefault(cat => cat.Id == id && cat.DeletedAt == null);
                if (CustomerOrderEntity == null)
                {
                    return false;
                }
                CustomerOrderEntity.IsDeliver = true;
                Context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveOrderFromMachine(int id)
        {
            try
            {
                var CustomerOrderEntity = Context.OrderDetailCustomers
                    .SingleOrDefault(cat => cat.Id == id && cat.DeletedAt == null);
                if (CustomerOrderEntity == null)
                {

                    return false;
                }
                CustomerOrderEntity.IsOut = true;
                Context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public CustomerOrderDetailsDto EditCustomerOrderDetails(CustomerOrderDetailsDto customeOrderDetailsDto)
        {
            var SubOrderEntity = Context.OrderDetailCustomers
                .Where(sub => sub.Id == customeOrderDetailsDto.Id)
                .SingleOrDefault();
                var entityState = EntityState.Modified;
            var BigOrderEntity = Context.CustomerOrders
                .Where(big => big.Id == customeOrderDetailsDto.CustomerOrderId)
                .SingleOrDefault();
            var ColorEntity = Context.Colors
                .Where(color => color.Id == customeOrderDetailsDto.ColorId)
                .SingleOrDefault();
            SubOrderEntity.OldLenght = customeOrderDetailsDto.OldLenght;
            SubOrderEntity.ColorId = customeOrderDetailsDto.ColorId;
            if (SubOrderEntity.PartialWeghit > customeOrderDetailsDto.PartialWeghit)
            {
                BigOrderEntity.Weight += SubOrderEntity.PartialWeghit - customeOrderDetailsDto.PartialWeghit;
            }
            else if (SubOrderEntity.PartialWeghit < customeOrderDetailsDto.PartialWeghit)
            {
                BigOrderEntity.Weight -= customeOrderDetailsDto.PartialWeghit - SubOrderEntity.PartialWeghit;
            }
            if (SubOrderEntity.ColorAmount > customeOrderDetailsDto.ColorAmount)
            {
                int ColorAmount = Convert.ToInt32(ColorEntity.Amount);
                ColorAmount += SubOrderEntity.ColorAmount - customeOrderDetailsDto.ColorAmount;
                ColorEntity.Amount = ColorAmount.ToString();
            }
            else if (SubOrderEntity.ColorAmount < customeOrderDetailsDto.ColorAmount)
            {
                int ColorAmount = Convert.ToInt32(ColorEntity.Amount);
                ColorAmount -= customeOrderDetailsDto.ColorAmount - SubOrderEntity.ColorAmount;
                ColorEntity.Amount = ColorAmount.ToString();
            }
            SubOrderEntity.PartialWeghit = customeOrderDetailsDto.PartialWeghit;
            SubOrderEntity.ColorAmount = customeOrderDetailsDto.ColorAmount;
            Context.Entry(SubOrderEntity).State = entityState;
            Context.SaveChanges();
            return customeOrderDetailsDto;
        }
    }
}
