using Exceptions;

namespace Person.Application.Models.Exceptions
{
    internal class PersonApplicationException: BaseException
    {
        public PersonApplicationException(string message):base(message)
        {

        }
    }
}
