using FabricProject.Dto.CustomerOrderDetails;
using FabricProject.Dto.CustomerOrderDetailsMachine;
using FabricProject.Dto.Machine;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speculum.ViewModel.CustomerOrderDetailMachine
{
    public class CustomerOrderDetailMachineViewModel
    {
        public int Id { get; set; }

        public int? CustomerOrderDetailId { get; set; }
        public int? MachineId { get; set; }

        public IEnumerable<CustomerOrderDetailsGetDto> GetCustomerOrderDetailsDto { get; set; }
        public IEnumerable<CustomerOrderDetailsMachineDto> GetCustomerOrderDetailsMachinesDto { get; set; }


        public IEnumerable<MachineDto> GetMachineDto { get; set; }

        public string AmountWater { get; set; }

        public IEnumerable<SelectListItem> Machines { get; set; }

        public IEnumerable<SelectListItem> CustomerOrderDetail { get; set; }



        public CustomerOrderDetailMachineViewModel()
        {
            GetCustomerOrderDetailsDto = new List<CustomerOrderDetailsGetDto>();
            GetCustomerOrderDetailsMachinesDto = new List<CustomerOrderDetailsMachineDto>();
            GetMachineDto = new List<MachineDto>();
        }
        public ActiveViewModel Active { get; set; }

    }
}
