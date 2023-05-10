namespace Exceptions
{
    public abstract class BaseException : Exception
    {
        public  static void ThrowIf(bool argument, string message)
        {
            if (argument)
            {
                throwException(message);
            }
        }


        public static void ThrowIfNull(object? argument, string message)
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

        public BaseException(string message) : base(message)
        {

        }
    }
}