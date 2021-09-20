using FabricProject.Dto.CustomerOrderDetailsMachine;
using System;
using System.Collections.Generic;
using System.Text;

namespace FabrciProject.IData.Interfaces
{
    public interface ICustomerOrderDetailsMachineRepository
    {
        IEnumerable<CustomerOrderDetailsMachineDto> GetMachineOrders(string query = null);
        bool RemoveOrderMachine(int id);
        bool SetOrderMachine(CustomerOrderDetailsMachineDto customerOrderDetailsMachine);



    }
}
