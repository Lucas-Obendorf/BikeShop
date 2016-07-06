using BikeShop.Infrastructure;
using BikeShop.Logic.Interfaces;
using FluentValidation;

namespace BikeShop.Logic
{
    public static class ValidatorFactory
    {
        public static IValidator<BikeInfo> GetBikeValidator()
        {
            return new BikeValidator();
        }
    }
}
