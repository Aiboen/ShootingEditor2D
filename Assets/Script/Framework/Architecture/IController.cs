namespace FrameworkDesign
{
    public interface IController : IBelongToArchitecture, ICanGetModle, ICanGetSystem, ICanSendCommand, ICanRegisterEvent
    {

    }

    public abstract class AbstractController : IController
    {
        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return null;
        }
    }
}
