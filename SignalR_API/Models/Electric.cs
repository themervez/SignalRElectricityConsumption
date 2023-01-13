using System;

namespace SignalR_API.Models
{
    public enum ECity
    {
        mersin = 1,
        adana = 2,
        istanbul = 3,
        ankara = 4,
        izmir = 5
    }

    public class Electric
    {
        public int ID { get; set; }
        public ECity City { get; set; }
        public int Consumption { get; set; }//Elektrik Tüketim Miktarı
        public DateTime ElectricDate { get; set; }//Şehrin Elektrik Tüketim Tarihi
    }
}
