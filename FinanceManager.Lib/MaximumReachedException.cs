using System.Runtime.Serialization;

namespace PersonalFinanceManager
{
    [Serializable]
    internal class MaximumReachedException : Exception
    {
        public static string errorMessage;
        public MaximumReachedException()
        {
        }

        public MaximumReachedException(string? message) : base(message)
        {
            errorMessage = message;
        }

        public MaximumReachedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected MaximumReachedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}