using System;

namespace DebtDiary.DataProvider
{
    public class NoInternetConnectionException : Exception
    {
        public NoInternetConnectionException()
        {
        }

        public NoInternetConnectionException(string message)
            : base(message)
        {
        }

        public NoInternetConnectionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
