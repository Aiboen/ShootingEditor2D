using FrameworkDesign;

namespace ShootingEditor2D
{
    /// <summary>
    /// 填满所有弹药
    /// </summary>
    public class FullBulletCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var gunSystem = this.GetSystem<IGunSystem>();
            var gunConfigModel = this.GetModel<IGunConfigModel>();

            gunSystem.CurrentGun.BulletCountInGun.Value = gunConfigModel.GetItemByName(gunSystem.CurrentGun.Name.Value).BulletMaxCount;

            foreach (var gunSystemGunInfo in gunSystem.GunInfos)
            {
                gunSystemGunInfo.BulletCountInGun.Value = gunConfigModel.GetItemByName(gunSystemGunInfo.Name.Value).BulletMaxCount;
            }
        }
    }
}