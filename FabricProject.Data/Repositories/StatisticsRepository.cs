using FabrciProject.IData.Interfaces;
using FabricProject.DContext;
using FabricProject.Dto.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FabricProject.Data.Repositories
{
  public  class StatisticsRepository:IStatisticsRepository
    {


        private FabricProjectDbContext Context { get; }





        public StatisticsRepository(FabricProjectDbContext fabricProjectDbContext)
        {
            Context = fabricProjectDbContext;
        }



        public int GetCustomerOrderDetails()
        {
            var results = Context.OrderDetailCustomers
                .Where(customerOrderDetail => (  customerOrderDetail.DeletedAt == null && customerOrderDetail.IsOut == false && customerOrderDetail.IsDeliver == false))
                .Count();
            return results;
        }

        public int GetOrdersInMachineDetails()
        {
            var results = Context.OrderDetailCustomers
                .Where(customerOrderDetail => (  customerOrderDetail.DeletedAt == null && customerOrderDetail.IsOut == false && customerOrderDetail.IsDeliver == true))
                .Count();
            return results;
        }

        public int GetDoneOrders()
        {
            var results = Context.OrderDetailCustomers
                .Where(customerOrderDetail => (  customerOrderDetail.DeletedAt == null && customerOrderDetail.IsOut == true && customerOrderDetail.IsDeliver == true))
                .Count();
            return results;
        }

        public int GetDoneOrdersOfDeliver()
        {
            var results = Context.OrderDetailCustomers
                .Where(customerOrderDetail => (  customerOrderDetail.DeletedAt == null && customerOrderDetail.IsOut == true && customerOrderDetail.IsDeliver == true) && customerOrderDetail.Deliver.Count == 0)
                .Count();
            return results;
        }

        public int GetDelivered()
        {
            var results = Context.Delivers
                .Where(deliver => deliver.DeletedAt == null && deliver.IsDeliver==false)
               .Count();
            return results;
        }

        public int FirstWeight() {
            var results = Context.OrderDetailCustomers
                .Where(customerOrderDetail => (customerOrderDetail.DeletedAt == null && customerOrderDetail.IsOut == false && customerOrderDetail.IsDeliver == false))
                .Sum(s => s.PartialWeghit );
               
            return results;

        }

    public    int MachinesWeight()
        {
            var results = Context.OrderDetailCustomers
               .Where(customerOrderDetail => (customerOrderDetail.DeletedAt == null && customerOrderDetail.IsOut == false && customerOrderDetail.IsDeliver == true))
                 .Sum(s => s.PartialWeghit);
            return results;
        }


   public     int DoneWeight()
        {
            var results = Context.OrderDetailCustomers
               .Where(customerOrderDetail => (customerOrderDetail.DeletedAt == null && customerOrderDetail.IsOut == true && customerOrderDetail.IsDeliver == true) && customerOrderDetail.Deliver.Count == 0)
              .Sum(s => s.PartialWeghit);
            return results;
        }

    public    int DeliveredWeight()
        {
            var results = Context.Delivers
               .Where(deliver => deliver.DeletedAt == null && deliver.IsDeliver == false)
              .Sum(s => s.OrderDetail.PartialWeghit);
            return results;
        }

    }
}
