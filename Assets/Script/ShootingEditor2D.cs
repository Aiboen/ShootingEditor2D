using FrameworkDesign;

namespace ShootingEditor2D
{
    public class ShootingEditor2D : Architecture<ShootingEditor2D>
    {
        protected override void Init()
        {
            this.RegisterModel<IPlayerModel>(new PlayerModel());
            this.RegisterSystem<IStatSystem>(new StatSystem());
        }
    }
}