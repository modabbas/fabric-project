using FabrciProject.IData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Speculum.ViewModel;
using Speculum.ViewModel.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speculum.Controllers
{
    [Authorize(Roles ="Admin")]
    public class StatisticsController : Controller
    {
        private IStatisticsRepository StatisticsRepository { get; }
        public IColorRepository ColorRepository { get; set; }
        public IMaterialRepository MaterialRepository { get; set; }
        public ICustomerOrderRepository CustomerOrderRepository { get; set; }

        public StatisticsController(IStatisticsRepository statisticsRepository,
            IColorRepository colorRepoistory, IMaterialRepository materialRepository,
            ICustomerOrderRepository customerRepository)
          {

            this.StatisticsRepository = statisticsRepository;
            ColorRepository = colorRepoistory;
            MaterialRepository = materialRepository;
            CustomerOrderRepository = customerRepository;
          }

        [HttpPost]
        public IActionResult Chart()
        {
            var Result = new ChartViewModel()
            {
                Colors = ColorRepository.GetColors(),
                Materials = MaterialRepository.GetMaterials(),
                CustomersOrders = CustomerOrderRepository.CustomersOrder(),
                Customers = CustomerOrderRepository.CustomerName(),
                CountOrderOfMonth = CustomerOrderRepository.CountOrderOfMonth()
            };
            return Json(Result);
        }

        public IActionResult Index()
        {
            var vm = new StatsViewModel()
            {
               OrderFirstLevel =StatisticsRepository.GetCustomerOrderDetails(),
               MachineLevel=StatisticsRepository.GetOrdersInMachineDetails(),
               BeforeDeliverLevel =StatisticsRepository.GetDoneOrdersOfDeliver(),
               EnterDeliverLevel=StatisticsRepository.GetDelivered(),
               OrderFirstLevelWeight =StatisticsRepository.FirstWeight(),
               MachineLevelWeight=StatisticsRepository.MachinesWeight(),
               BeforeDeliverLevelWeight =StatisticsRepository.DoneWeight(),
               EnterDeliverLevelWeight=StatisticsRepository.DeliveredWeight(),
            };
            return View(vm);
        }
     
    }
}
