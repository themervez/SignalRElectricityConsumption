using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalR_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR_API.Hubs
{
    public class ElectricService
    {
        private readonly Context _context;

        private readonly IHubContext<ElectricHub> _hubContext;

        public ElectricService(Context context, IHubContext<ElectricHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }
        public IQueryable<Electric> GetList()
        {
            return _context.Electrics.AsQueryable();//Electric verilerini Queryable olarak aldık
        }
        public async Task SaveElectric(Electric electric)
        {
            await _context.Electrics.AddAsync(electric);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveElectricList","data");
        }

    }
}
