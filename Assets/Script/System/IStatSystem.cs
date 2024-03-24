using FrameworkDesign;

namespace ShootingEditor2D
{
    public interface IStatSystem : ISystem
    {
        BindableProperty<int> KillCount { get; }
    }

    public class StatSystem : AbstractSystem, IStatSystem
    {
        public override void OnInit()
        {

        }

        public BindableProperty<int> KillCount { get; } = new BindableProperty<int>()
        {
            Value = 0,
        };
    }
}