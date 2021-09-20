using System;
using System.Collections.Generic;
using System.Text;

namespace FabrciProject.IData.Interfaces
{
   public interface IStatisticsRepository
    {
        int GetCustomerOrderDetails();

        int GetOrdersInMachineDetails();

        int GetDoneOrders();

        int GetDoneOrdersOfDeliver();

        int GetDelivered();

        int FirstWeight();

        int MachinesWeight();
 

        int DoneWeight();

        int DeliveredWeight();

    }
}
