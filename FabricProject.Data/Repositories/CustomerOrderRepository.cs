using FabrciProject.IData.Interfaces;
using FabricProject.DContext;
using FabricProject.Dto.Customer;
using FabricProject.Dto.CustomerOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FabricProject.Data.Repositories
{
  public  class CustomerOrderRepository:ICustomerOrderRepository
    {
        private FabricProjectDbContext Context { get; }

        public CustomerOrderRepository(FabricProjectDbContext fabricProjectDbContext)
        {
            Context = fabricProjectDbContext;
        }

        public IEnumerable<CustomerOrderGetDto> GetCustomerOrders(string query = null)
        {
            var results = Context.CustomerOrders
                .Where(customer => (string.IsNullOrEmpty(query)   && customer.DeletedAt == null))
                .Select(customer => new CustomerOrderGetDto()
                { 
                    CustomerName=customer.Customer.Name,
                    ClothName=customer.Cloth.Name,
                    CustomerId = customer.CustomerId,
                    ClothId = customer.ClothId,
                    PercentPolister=customer.PercentPolister,
                    PercentCotton=customer.PercentCotton,
                    CreatedAt=customer.CreatedAt,
                    Weight=customer.Weight,
                    Id=customer.Id
                    
                }).ToList();
            return results;
        }
        public CustomerOrderDto SetCustomerOrder(CustomerOrderDto customerOrderDto)
        {
            try
            {
                var customerOrderEntity = Context.CustomerOrders
                    .SingleOrDefault(order => order.Id == customerOrderDto.Id);
                if (customerOrderEntity == null)
                {
                    //add case
                    customerOrderEntity = new Models.Models.CustomerOrder();
                    Context.Attach(customerOrderEntity);
                }
                customerOrderEntity.ClothId = customerOrderDto.ClothId;
                customerOrderEntity.CustomerId = customerOrderDto.CustomerId;
                customerOrderEntity.EditedAt = customerOrderDto.EditedAt;
                customerOrderEntity.CreatedAt = customerOrderDto.CreatedAt;
                customerOrderEntity.Weight = customerOrderDto.Weight;
                customerOrderEntity.PercentCotton = customerOrderDto.PercentCotton;
                customerOrderEntity.PercentPolister = customerOrderDto.PercentPolister;
                Context.SaveChanges();
                customerOrderDto.CustomerName = Context.Customers
                    .Where(customer => customer.CustomerId == customerOrderEntity.CustomerId)
                    .Select(customer => customer.Name)
                    .SingleOrDefault();
                customerOrderDto.ClothName = Context.Cloths
                    .Where(cloth => cloth.Id == customerOrderEntity.ClothId)
                    .Select(cloth => cloth.Name)
                    .SingleOrDefault();
                customerOrderDto.Id = customerOrderEntity.Id;
                return customerOrderDto;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public bool RemoveCustomerOrder(int id)
        {
            try
            {
                var ExistSmallOrder = Context.OrderDetailCustomers
                    .Where(odc => !odc.DeletedAt.HasValue &&
                    odc.CustomerOrderId == id)
                    .Count();
                if (ExistSmallOrder != 0)
                {
                    return false;
                }
                var CustomerOrderEntity = Context.CustomerOrders
                    .SingleOrDefault(cat => cat.Id == id && cat.DeletedAt == null);
                if (CustomerOrderEntity == null)
                {
                    return false;
                }
                CustomerOrderEntity.DeletedAt = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<CustomerOrderGetDto> CustomersOrder()
        {
            var result = Context.CustomerOrders
                .Where(order => !order.DeletedAt.HasValue)
                .OrderBy(order=>order.CustomerId)
                .GroupBy(order=>order.CustomerId)
                .Select(order => new CustomerOrderGetDto
                {
                    OrderCount = order.Count(),
                    Id = order.Key,
                    
                }).ToList();
            return result;
        }

        public IEnumerable<CustomerOrderDto> CustomerName()
        {
            var result = Context.CustomerOrders
                .Where(co => !co.DeletedAt.HasValue)
                .Select(customer => new CustomerOrderDto
                {
                    CustomerId = customer.CustomerId,
                    CustomerName = customer.Customer.Name
                })
                .Distinct()
                .ToList()
                .OrderBy(customer => customer.CustomerId);
            return result;
        }

        public int[] CountOrderOfMonth()
        {
            int[] count = new int[12];
            var dataLast = Context.CustomerOrders
                .Where(order =>!order.DeletedAt.HasValue && order.CreatedAt.Year == DateTime.Now.Year)
                .ToList();
            for (int j = 0; j < 12; j++)
            {
                count[j] = (dataLast
                     .Count(order => order.CreatedAt.Month == j + 1));
            }
            return count;
        }
    }
}

