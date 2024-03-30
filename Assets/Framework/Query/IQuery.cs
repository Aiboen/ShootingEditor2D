namespace FrameworkDesign
{
    public interface IQuery<T> : IBelongToArchitecture, ICanSetArchitecture, ICanGetModle, ICanGetSystem, ICanGetUtility, ICanSendQuery
    {
        T Do();
    }

    public abstract class AbstractQuery<T> : IQuery<T>
    {
        private IArchitecture mArchitecture;

        public T Do()
        {
            return OnDo();
        }

        protected abstract T OnDo();

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return mArchitecture;
        }

        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            mArchitecture = architecture;
        }
    }
}