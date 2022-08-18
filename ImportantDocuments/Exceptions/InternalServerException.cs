using System.Net;

namespace ImportantDocuments.API.Exceptions;

/// <summary>
/// Use mainly for wrapping unhandled exceptions when the server can not accomplish its task
/// </summary>
public class InternalServerException : ApiException
{
    public const string GenericMessage = "An unknown error occurred.";

    /// <summary>
    /// Constructor with innerException
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public InternalServerException(string message = GenericMessage, Exception innerException = null)
        : base(HttpStatusCode.InternalServerError, "Internal Server Error", (int) ApiErrorCode.ResourceNotAvailable,
            message, innerException)
    {
    }
}