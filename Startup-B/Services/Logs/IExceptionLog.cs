namespace Startup_B.Services.Exception
{
    public interface IExceptionLog
    {
        Task LogException(string method, string errorMsg, DateTime logTime);

        Task LogErrors(string method, string errorMsg);
    }
}
