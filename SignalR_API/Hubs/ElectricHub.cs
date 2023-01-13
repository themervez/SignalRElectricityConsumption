using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalR_API.Hubs
{
    public class ElectricHub : Hub
    {
        private readonly ElectricService _service;

        public ElectricHub(ElectricService service)
        {
            _service = service;
        }
        public async Task GetElectricList()
        {
            await Clients.All.SendAsync("ReceiveElectricList", "Elektrik verilerini al");//connection.on ile bağlanılacak:ReceiveElectricList invoke:GetElectricList
        }
    }
}
