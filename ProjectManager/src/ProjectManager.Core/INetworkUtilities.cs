namespace ProjectManager.Core
{
    public interface INetworkUtilities
    {
        bool IsConnectionStringValid(string connectionString);
        bool IsNetworkAvailable();
        bool VerifyDBServerConnectivity(string WindowsDomainName, string connectionString);
    }
}