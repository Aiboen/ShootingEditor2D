using FrameworkDesign;

namespace ShootingEditor2D
{
    /// <summary>
    /// 查询子弹最大值
    /// </summary>
    public class MaxBulletCountQuery : AbstractQuery<int>
    {
        private readonly string mGunName;

        public MaxBulletCountQuery(string gunName)
        {
            mGunName = gunName;
        }

        protected override int OnDo()
        {
            var gunConfigModel = this.GetModel<IGunConfigModel>();
            var gunConfigItem = gunConfigModel.GetItemByName(mGunName);
            return gunConfigItem.BulletMaxCount;
        }
    }
}