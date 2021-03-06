using System;

namespace Gimme.Core.Exceptions
{
    public class DependencyUnavailableException : Exception
    {
        public DependencyUnavailableException() : base()
        { }

        public DependencyUnavailableException(string message) : base(message)
        { }

        public DependencyUnavailableException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
