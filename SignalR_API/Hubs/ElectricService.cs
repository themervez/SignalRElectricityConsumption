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
            await _hubContext.Clients.All.SendAsync("ReceiveElectricList", GetElectricChartList());
        }
        public List<ElectricChart> GetElectricChartList()
        {
            List<ElectricChart> electricCharts = new List<ElectricChart>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select Tarih,[1],[2],[3],[4],[5] from (select[City],[Consumption],cast([ElectricDate] as date) as Tarih from Electrics) as electricT pivot (Sum(Consumption) for City in ([1],[2],[3],[4],[5])) as ptable order by Tarih asc";

                command.CommandType = System.Data.CommandType.Text;//Text veya StoredProcedure olabilirdi sql'de kullandığımız yapıya göre
                _context.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ElectricChart electricChart = new ElectricChart();
                        electricChart.ElectricDate = reader.GetDateTime(0).ToShortDateString();//Tarih alındı

                        Enumerable.Range(1, 5).ToList().ForEach(x =>
                        {
                            if (System.DBNull.Value.Equals(reader[x]))//reader'dan gelen değerin boş olma durumu için
                            {
                                electricChart.Consumptions.Add(0);//Grafiğe yazdırabilmek için
                            }
                            else
                            {
                                electricChart.Consumptions.Add(reader.GetInt32(x));
                            }
                        });

                        electricCharts.Add(electricChart);
                    }
                }

                _context.Database.CloseConnection();
                return electricCharts;
            }
        }

    }
}
