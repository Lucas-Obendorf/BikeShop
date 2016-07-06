using System.Collections;
using BikeShop.Infrastructure;
using System.Collections.Generic;

namespace BikeShop.Data.Interfaces
{
    public interface IBikeData
    {
        Result<IEnumerable<BikeInfo>> GetAllBikes();
        Result<BikeInfo> GetBike(int id);
        Result<int> CreateNewBike(BikeInfo bike);
        IEnumerable<Error> UpdateBike(BikeInfo bike);
        IEnumerable<Error> DeleteBike(int id);
    }
}
