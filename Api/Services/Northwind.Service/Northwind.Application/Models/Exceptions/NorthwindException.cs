using Exceptions;

namespace Northwind.Application.Models.Exceptions
{
    public class NorthwindException : BaseException
    {
        public NorthwindException(string message) : base(message)
        {
        }
    }
}
