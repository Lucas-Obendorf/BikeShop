using BikeShop.Data.Interfaces;

namespace BikeShop.Data
{
    public static class DataFactory
    {
        public static IBikeData GetBikeData()
        {
            return new BikeData();
        }
    }
}
