using FrameworkDesign;

namespace ShootingEditor2D
{
    /// <summary>
    /// 开枪命令
    /// </summary>
    public class ShootCommand : AbstractCommand
    {
        public static readonly ShootCommand Single = new ShootCommand();

        protected override void OnExecute()
        {
            //子弹数量
            var gunSystem = this.GetSystem<IGunSystem>();
            gunSystem.CurrentGun.BulletCountInGun.Value--;
            gunSystem.CurrentGun.GunState.Value = GunState.Shooting;

            var gunConfigItem = this.GetModel<IGunConfigModel>().GetItemByName(gunSystem.CurrentGun.Name.Value);
            //开枪间隔
            var timeSystem = this.GetSystem<ITimeSystem>();
            timeSystem.AddDelayTask(1 / gunConfigItem.Frequency, () =>
            {
                gunSystem.CurrentGun.GunState.Value = GunState.Idle;

                //自动换弹
                if (gunSystem.CurrentGun.BulletCountInGun.Value <= 0)
                {
                    this.SendCommand<ReloadCommand>();
                }
            });
        }
    }
}