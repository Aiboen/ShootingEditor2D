using FrameworkDesign;

namespace ShootingEditor2D
{
    public class ShootingEditor2D : Architecture<ShootingEditor2D>
    {
        protected override void Init()
        {
            //注册Model
            this.RegisterModel<IGunConfigModel>(new GunConfigModel());
            this.RegisterModel<IPlayerModel>(new PlayerModel());

            //注册System
            this.RegisterSystem<IGunSystem>(new GunSystem());
            this.RegisterSystem<IStatSystem>(new StatSystem());
            this.RegisterSystem<ITimeSystem>(new TimeSystem());
        }
    }
}