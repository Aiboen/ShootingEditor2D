using FrameworkDesign;

namespace ShootingEditor2D
{
    public interface IPlayerModel : IModel
    {
        BindableProperty<int> HP { get; }
    }


    public class PlayerModel : AbstractModel, IPlayerModel
    {
        public BindableProperty<int> HP { get; } = new BindableProperty<int>()
        {
            Value = 3,
        };

        public override void OnInit()
        {

        }
    }
}
