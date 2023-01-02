namespace Startup_B.Services.Exception
{
    public class ExceptionLog : IExceptionLog
    {
        public Task LogErrors(string method, string errorMsg)
        {
            throw new NotImplementedException();
        }

        public async Task LogException(string method, string errorMsg, DateTime logTime)
        {
            throw new NotImplementedException();
        }
    }
}
