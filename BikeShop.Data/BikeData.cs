using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BikeShop.Data.Interfaces;
using BikeShop.Infrastructure;
using Dapper;
using Npgsql;

namespace BikeShop.Data
{
    public class BikeData : IBikeData
    {
        private IDbConnection GetConnection()
        {
            var connection = new NpgsqlConnection("User ID=bikeshopwebsite;Password=password;Host=localhost;Port=5432;Database=BikeShop;");

            connection.Open();

            return connection;
        }

        public Result<IEnumerable<BikeInfo>> GetAllBikes()
        {
            var result = new Result<IEnumerable<BikeInfo>>();

            try
            {
                using (var conn = GetConnection())
                {
                    var bikes = conn.Query<BikeInfo>("SELECT Id, Description, Price, Rating, Quantity, Type FROM BikeInfo");

                    result.Value = bikes;
                }
            }
            catch
            {
                result.Errors.Add(new Error("Could not retrieve current inventory, please try again later."));
            }

            return result;
        }

        public Result<BikeInfo> GetBike(int id)
        {
            var result = new Result<BikeInfo>();

            try
            {
                using (var conn = GetConnection())
                {
                    var bike = conn.Query<BikeInfo>("SELECT Id, Description, Price, Rating, Quantity, Type FROM BikeInfo WHERE Id = @BikeId", new { BikeId = id }).FirstOrDefault();

                    result.Value = bike;
                }
            }
            catch
            {
                result.Errors.Add(new Error("Could not retrieve information for bike, please try again later."));
            }

            return result;
        }

        public Result<int> CreateNewBike(BikeInfo bike)
        {
            var result = new Result<int>();

            try
            {
                using (var conn = GetConnection())
                {
                    var bikeId = conn.Execute("INSERT INTO BikeInfo (Description, Rating, Price, Quantity, Type) VALUES (@Description, @Rating, @Price, @Quantity, @Type);", new { Description = bike.Description, Rating = bike.Rating, Price = bike.Price, Quantity = bike.Quantity, Type = bike.Type });

                    result.Value = bikeId;
                }
            }
            catch
            {
                result.Errors.Add(new Error("Could not create bike, please try again later."));
            }

            return result;
        }

        public IEnumerable<Error> UpdateBike(BikeInfo bike)
        {
            var errors = new List<Error>();

            try
            {
                using (var conn = GetConnection())
                {
                    conn.Execute("UPDATE BikeInfo SET Description = @Description, Rating = @Rating, Price = @Price, Quantity = @Quantity, Type=@Type WHERE Id = @BikeId;", new { BikeId = bike.Id, Description = bike.Description, Rating = bike.Rating, Price = bike.Price, Quantity = bike.Quantity, Type = bike.Type });
                }
            }
            catch
            {
                errors.Add(new Error("Could not update bike, please try again later."));
            }

            return errors;
        }

        public IEnumerable<Error> DeleteBike(int id)
        {
            var errors = new List<Error>();

            try
            {
                using (var conn = GetConnection())
                {
                    conn.Execute("DELETE FROM BikeInfo WHERE Id = @BikeId;", new { BikeId = id });
                }
            }
            catch
            {
                errors.Add(new Error("Could not delete bike, please try again later."));
            }

            return errors;
        }
    }
}
