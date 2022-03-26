using System.Runtime.Serialization;

[Serializable]
public class ValueNotAllowedException : Exception
{
    public static string errorMessage;
    public ValueNotAllowedException()
    {
    }

    public ValueNotAllowedException(string? message) : base(message)
    {
        errorMessage = message;
    }

    public ValueNotAllowedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected ValueNotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}