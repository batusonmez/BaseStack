using Exceptions;

namespace Index.Application.Models { 
    public class IndexException:BaseException
    {
        public new static void ThrowIf(bool argument, string message)
        {
            if (argument)
            {
                throwException(message);
            }
        }


        public new static void ThrowIfNull(object? argument, string message)
        {
            if (argument == null)
            {
                throwException(message);
            }
        }

        private static void throwException(string message)
        {
            throw new(message);
        }

        public IndexException(string message):base(message)
        {

        }
    }
}
