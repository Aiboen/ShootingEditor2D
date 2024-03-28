using FrameworkDesign;

namespace ShootingEditor2D
{
    public interface IGunSystem : ISystem
    {
        GunInfo CurrentGun { get; }
    }

    public class GunSystem : AbstractSystem, IGunSystem
    {
        /// <summary>
        /// 枪械信息
        /// </summary>
        public GunInfo CurrentGun { get; } = new GunInfo()
        {
            BulletCount = new BindableProperty<int>()
            {
                Value = 3,
            }
        };

        public override void OnInit()
        {

        }
    }
}
