using FrameworkDesign;

namespace ShootingEditor2D
{
    public class AddBulletCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var gunSystem = this.GetSystem<IGunSystem>();
            var gunConfigModel = this.GetModel<IGunConfigModel>();

            AddBullet(gunSystem.CurrentGun, gunConfigModel);

            foreach (var gunInfo in gunSystem.GunInfos)
            {
                AddBullet(gunInfo, gunConfigModel);
            }
        }

        private void AddBullet(GunInfo gunInfo, IGunConfigModel gunConfigModel)
        {
            var gunConfigItem = gunConfigModel.GetItemByName(gunInfo.Name.Value);

            if (!gunConfigItem.NeedBullet)
            {
                //直接填满即可
                gunInfo.BulletCountInGun.Value = gunConfigItem.BulletMaxCount;
            }
            else
            {
                gunInfo.BulletCountOutGun.Value += gunConfigItem.BulletMaxCount;
            }
        }
    }
}