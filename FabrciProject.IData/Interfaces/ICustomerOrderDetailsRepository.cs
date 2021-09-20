using FabricProject.Dto.CustomerOrderDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace FabrciProject.IData.Interfaces
{
    public interface ICustomerOrderDetailsRepository
    {
        IEnumerable<CustomerOrderDetailsGetDto> GetCustomerOrderDetails(string query = null);

        IEnumerable<CustomerOrderDetailsGetDto> GetOrdersInMachineDetails(string query = null);

        IEnumerable<CustomerOrderDetailsGetDto> GetDoneOrders(string query = null);

        IEnumerable<CustomerOrderDetailsGetDto> GetDoneOrdersOfDeliver(string query = null);

        CustomerOrderDetailsDto SetCustomerOrderDetails(CustomerOrderDetailsDto customeOrderDetailsDto);

        CustomerOrderDetailsDto EditCustomerOrderDetails(CustomerOrderDetailsDto customeOrderDetailsDto);

        RemoveSubOrderDto RemoveCustomerOrderDetails(int id);

        bool RemoveOrderFromDeliver(int id);

        bool RemoveOrderFromMachine(int id);
    }
}
