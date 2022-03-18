using System.Runtime.Serialization;

[Serializable]
public class ValueNotAllowedException : Exception
{
    public ValueNotAllowedException()
    {
    }

    public ValueNotAllowedException(string? message) : base(message)
    {
    }

    public ValueNotAllowedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected ValueNotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}