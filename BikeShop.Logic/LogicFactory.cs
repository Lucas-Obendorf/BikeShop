using BikeShop.Logic.Interfaces;

namespace BikeShop.Logic
{
    public static class LogicFactory
    {
        public static IBikeRequests GetBikeRequests()
        {
            return new BikeRequests();
        }
    }
}
