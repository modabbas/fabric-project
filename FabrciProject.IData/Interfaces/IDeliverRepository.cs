using FabricProject.Dto.Deliver;
using System;
using System.Collections.Generic;
using System.Text;

namespace FabrciProject.IData.Interfaces
{
    public interface IDeliverRepository
    {
        IEnumerable<DeliverDto> GetDelivered(string query = null);

        IEnumerable<DeliverDto> GetGiven(string query = null);

        bool EnterTheDeliver(DeliverDto deliverDto);

        bool LeaveTheDeliver(int id); 

        bool RemoveDeliver(int id);
    }
}
