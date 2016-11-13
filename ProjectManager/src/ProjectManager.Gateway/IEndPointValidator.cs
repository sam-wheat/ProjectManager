using ProjectManager.Core;

namespace ProjectManager.Gateway
{
    public interface IEndPointValidator
    {
        bool IsInterfaceAlive(IEndPointConfiguration endPoint);
    }
}