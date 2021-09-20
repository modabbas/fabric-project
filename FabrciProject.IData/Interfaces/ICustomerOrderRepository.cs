using FabricProject.Dto.Customer;
using FabricProject.Dto.CustomerOrder;
using System;
using System.Collections.Generic;
using System.Text;

namespace FabrciProject.IData.Interfaces
{
    public interface ICustomerOrderRepository
    {
        IEnumerable<CustomerOrderGetDto> GetCustomerOrders(string query = null);
        CustomerOrderDto SetCustomerOrder(CustomerOrderDto customeOrderDto);
        bool RemoveCustomerOrder(int id);
        IEnumerable<CustomerOrderGetDto> CustomersOrder();
        IEnumerable<CustomerOrderDto> CustomerName();

        int[] CountOrderOfMonth();

    }
}
