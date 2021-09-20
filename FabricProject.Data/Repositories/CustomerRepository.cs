using FabricProject.DContext;
using FabrciProject.IData.Interfaces;
using FabricProject.Dto.Customer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace FabricProject.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {

        private FabricProjectDbContext Context { get; }
       


        public CustomerRepository(FabricProjectDbContext fabricProjectDbContext)
        {
            Context = fabricProjectDbContext;
        }

        public IEnumerable<CustomerDto> GetCustomers(string query = null)
        {
            var results = Context.Customers
                .Where(customer =>( string.IsNullOrEmpty(query) || customer.Name.Contains(query)) && customer.DeletedAt ==null)
                .Select(customer => new CustomerDto()
                {
                    CustomerId = customer.CustomerId,
                    Id = customer.CustomerId,
                    Name = customer.Name,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    numOrders=customer.CustomerOrders.Count()
                }).ToList();
            return results;
        }
        public CustomerDto SetCustomer(CustomerDto customerDto)
        {
            try
            {
                var customerEntity = Context.Customers
                    .SingleOrDefault(store => store.CustomerId == customerDto.CustomerId);
                if (customerEntity == null)
                {
                    customerDto.IsAdd = true;
                    //add case
                    customerEntity = new Models.Models.Customer();
                    Context.Attach(customerEntity);
                }
                 
                customerEntity.Name = customerDto.Name;
                customerEntity.EditedAt = customerDto.EditedAt;
                customerEntity.CreatedAt = customerDto.CreatedAt;
                customerEntity.Email = customerDto.Email;
                customerEntity.Phone = customerDto.Phone;
                Context.SaveChanges();
                customerDto.CustomerId = customerEntity.CustomerId;
                return customerDto;
            }
            catch (Exception)
            {
                return customerDto;
            }

        }

        public bool RemoveCustomer(int id)
        {
            try
            {
                var categoryEntity = Context.Customers
                    .SingleOrDefault(cat => cat.CustomerId == id && cat.DeletedAt == null);
                if (categoryEntity == null)
                {
                    return false;
                }
                categoryEntity.DeletedAt = DateTime.Now;
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
