namespace FrameworkDesign
{
    public interface ICanGetModle : IBelongToArchitecture
    {

    }

    public static class CanGetModelExtension
    {
        public static T GetModel<T>(this ICanGetModle self) where T : class, IModel
        {
            return self.GetArchitecture().GetModel<T>();
        }
    }
}