namespace LocadoraDVD.Domain.Exceptions;
public class LocadoraDVDException : Exception
{
    public LocadoraDVDException(string message) : base(message) { }
    public static void When(bool hasError, string error)
    {
        if (hasError)
        {
            throw new LocadoraDVDException(error);
        }
    }
}