using System;

namespace SuperNiceProject.Exceptions
{
    public class PersistenceUnavailableException : Exception
    {
        public PersistenceUnavailableException() : base()
        { }

        public PersistenceUnavailableException(string message) : base(message)
        { }

        public PersistenceUnavailableException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
