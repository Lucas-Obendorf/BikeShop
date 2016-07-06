using System.Collections;
using System.Collections.Generic;
using BikeShop.Infrastructure;

namespace BikeShop.Logic.Interfaces
{
    public interface IBikeRequests
    {
        Result<IEnumerable<BikeInfo>> GetInfoForAllBikes();
        Result<BikeInfo> GetBikeInfo(int id);
        Result<bool> SaveBike(BikeInfo bike);
        IEnumerable<Error> DeleteBike(int id);
    }
}
