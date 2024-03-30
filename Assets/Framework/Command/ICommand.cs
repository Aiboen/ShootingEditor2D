namespace FrameworkDesign
{
    public interface ICommand : IBelongToArchitecture, ICanSetArchitecture, ICanGetModle, ICanGetSystem, ICanGetUtility, ICanSendEvent, ICanSendCommand, ICanSendQuery
    {
        void Execute();
    }

    public abstract class AbstractCommand : ICommand
    {
        private IArchitecture mArchitecture;

        public IArchitecture GetArchitecture()
        {
            return mArchitecture;
        }

        public void SetArchitecture(IArchitecture architecture)
        {
            mArchitecture = architecture;
        }

        protected abstract void OnExecute();

        void ICommand.Execute()
        {
            OnExecute();
        }
    }
}