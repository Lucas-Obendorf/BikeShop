using System;
using FluentValidation.Results;

namespace BikeShop.Infrastructure
{
    public class Error
    {
        public string Message { get; }

        public Error(string message)
        {
            Message = message;
        }

        public Error(Exception e)
        {
            Message = e.Message;
        }
    }
}
