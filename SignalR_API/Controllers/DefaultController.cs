using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR_API.Hubs;
using SignalR_API.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly ElectricService _electricService;

        public DefaultController(ElectricService electricService)
        {
            _electricService = electricService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveElectric(Electric electric)
        {
            await _electricService.SaveElectric(electric);
            IQueryable<Electric> electricList = _electricService.GetList();
            return Ok(electricList);
        }

        [HttpGet]
        public IActionResult SendData()
        {
            Random rnd = new Random();

            //5 farklı şehirde 10 farklı tarih için rastgele tüketim değerleri
            Enumerable.Range(1, 10).ToList().ForEach(x =>
            {
                foreach (ECity item in Enum.GetValues(typeof(ECity)))
                {
                    var newElectric = new Electric
                    {
                        City = item,
                        Consumption = rnd.Next(100, 1000),
                        ElectricDate = DateTime.Now.AddDays(x)//x ile 10 gün için geçerli verileri aldık
                    };

                    _electricService.SaveElectric(newElectric).GetAwaiter().GetResult();
                    System.Threading.Thread.Sleep(1000);//İşlemlerin her birinin 1er saniye ara ile gerçekleşmesi için
                }
            });

            return Ok("Elektrik tüketim verileri veri tabanına kaydedildi");
        }
    }
}
