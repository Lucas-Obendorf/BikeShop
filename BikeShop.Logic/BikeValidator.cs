using BikeShop.Infrastructure;
using FluentValidation;

namespace BikeShop.Logic
{
    public class BikeValidator : AbstractValidator<BikeInfo>
    {
        public BikeValidator()
        {
            RuleFor(bike => bike.Price).GreaterThan(1.00m);
            RuleFor(bike => bike.Rating).InclusiveBetween(0, 5);
            RuleFor(bike => bike.Type).NotNull().Length(1, 50);
            RuleFor(bike => bike.Description).NotNull().Length(1, 500);
        }
    }
}
