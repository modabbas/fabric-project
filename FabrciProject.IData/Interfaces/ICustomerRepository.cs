using FabricProject.Dto.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace FabrciProject.IData.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<CustomerDto> GetCustomers(string query = null);
        CustomerDto SetCustomer(CustomerDto customerDto);
        bool RemoveCustomer(int id);
    }
}
