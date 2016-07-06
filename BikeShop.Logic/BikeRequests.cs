using System.Collections.Generic;
using System.Linq;
using BikeShop.Data;
using BikeShop.Data.Interfaces;
using BikeShop.Infrastructure;
using BikeShop.Logic.Interfaces;
using FluentValidation;

namespace BikeShop.Logic
{
    public class BikeRequests : IBikeRequests
    {
        private IBikeData _bikeData;
        public IBikeData BikeData
        {
            get { return _bikeData ?? (_bikeData = DataFactory.GetBikeData()); }
            set { _bikeData = value; }
        }

        private IValidator<BikeInfo> _bikeValidator;
        public IValidator<BikeInfo> BikeValidator
        {
            get { return _bikeValidator ?? (_bikeValidator = ValidatorFactory.GetBikeValidator()); }
            set { _bikeValidator = value; }
        }  
       
        public Result<IEnumerable<BikeInfo>> GetInfoForAllBikes()
        {
            var result = new Result<IEnumerable<BikeInfo>>();

            var bikeResults = BikeData.GetAllBikes();

            result.CombineErrors(bikeResults);

            if (!result.Errors.Any())
            {
                result.Value = bikeResults.Value;
            }

            return result;
        }

        public Result<BikeInfo> GetBikeInfo(int id)
        {
            return BikeData.GetBike(id);
        }

        public Result<bool> SaveBike(BikeInfo bike)
        {
            var result = new Result<bool>
            {
                Value = false
            };

            if (bike == null)
            {
                result.Errors.Add(new Error("Cannot save an empty bike."));
            }
            else
            {
                var validationResult = BikeValidator.Validate(bike);

                result.CombineErrors(validationResult);

                if (validationResult.IsValid)
                {
                    if (!result.Errors.Any())
                    {
                        if (bike.Id == 0)
                        {
                            var newIdResult = BikeData.CreateNewBike(bike);

                            if (newIdResult.Errors.Any())
                            {
                                result.CombineErrors(newIdResult);
                            }
                            else
                            {
                                bike.Id = newIdResult.Value;
                                result.Value = true;
                            }
                        }
                        else
                        {
                            var updateErrors = BikeData.UpdateBike(bike);

                            result.CombineErrors(updateErrors);

                            if (!result.Errors.Any())
                            {
                                result.Value = true;
                            }
                        }
                    }
                }
            }

            return result;
        }

        public IEnumerable<Error> DeleteBike(int id)
        {
            var result = new List<Error>();

            if (id <= 0)
            {
                result.Add(new Error("Cannot delete: Invalid bike id"));
            }
            else
            {
                var deleteErrors = BikeData.DeleteBike(id);

                result.AddRange(deleteErrors);
            }

            return result;
        }
    }
}
