using System.Collections.Generic;

namespace SignalR_API.Models
{
    public class ElectricChart
    {
        public ElectricChart()
        {
            Consumptions = new List<int>();
        }
        public string ElectricDate { get; set; }
        public List<int> Consumptions { get; set; }
    }
}
