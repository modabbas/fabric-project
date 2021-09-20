using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speculum.ViewModel.Statistics
{
    public class StatsViewModel
    {
     public   int OrderFirstLevel; //مستودع الاستلام
     public     int MachineLevel;
     public   int BeforeDeliverLevel; //قبل مستودع التسليم
     public    int EnterDeliverLevel; // مستودع التسليم

        public int OrderFirstLevelWeight; //مستودع الاستلام
        public int MachineLevelWeight;
        public int BeforeDeliverLevelWeight; //قبل مستودع التسليم
        public int EnterDeliverLevelWeight; // مستودع التسليم
        public ActiveViewModel Active { get; set; }

    }
}
