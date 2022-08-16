using System.Net;

namespace ImportantDocuments.API.Exceptions;

[Serializable]
public class ApiException : Exception
{
    public HttpStatusCode HttpCode { get; set; }

    // Human-readable description of httpCode (e.g. 400 -> "Bad Request", 401 -> "Unauthorized", etc) 
    public string HttpMessage { get; set; }

    // Either one of the predefined values specified in ApiErrorCodes or any other value that can be parsed 
    // by the calling code to determine the specific kind of error that occurred
    public int ErrorCode { get; set; }

    // Human-readable description of the error. More detailed than httpMessage
    public string ErrorMessage { get; set; }

    // Unique identifier so that it can be correlated in logs
    public Guid ErrorId { get; protected set; }

    public ApiException(HttpStatusCode httpCode, string httpMessage, int errorCode, string errorMessage, Exception innerException) : base(errorMessage, innerException)
    {
        this.ErrorId = Guid.NewGuid();
        this.HttpCode = httpCode;
        this.HttpMessage = httpMessage;
        this.ErrorCode = errorCode;
        this.ErrorMessage = errorMessage;
    }
    public ApiException(HttpStatusCode httpCode, string httpMessage, int errorCode, string errorMessage)
        : this(httpCode, httpMessage, errorCode, errorMessage, null)
    {
    }

    public override string ToString()
    {
        return $"ErrorId:{ErrorId}. {base.ToString()}";
    }
}

public enum ApiErrorCode
{
    ResourceNotFound = 10001,
    InvalidArgument = 10002,
    Forbidden = 10003,
    InvalidUsernameOrPassword = 10004,
    MissingArgument = 10005,
    ResourceNotAvailable = 10006,
    FileMaxSizeExceeded = 10007,
    UnknownError = 10008
}
