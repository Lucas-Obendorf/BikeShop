using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BikeShop.Infrastructure
{
    public class BikeInfo
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public float Rating { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; }
    }
}
