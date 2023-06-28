namespace MiniHubApi.Application.Interfaces
{
    public interface ILogCustomServices
    {
        void LogError(string log, bool middleware, string comment, string errorCode, string errorMessage);
        void LogInfo(string log, bool middleware, string comment = null);
    }
}
