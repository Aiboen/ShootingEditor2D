using FrameworkDesign;
using Debug = UnityEngine.Debug;

namespace ShootingEditor2D
{
    /// <summary>
    /// 换弹命令
    /// </summary>
    public class ReloadCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var currentGun = this.GetSystem<IGunSystem>().CurrentGun;

            var gunConfigItem = this.GetModel<IGunConfigModel>().GetItemByName(currentGun.Name.Value);

            //需要子弹的数量
            var needBulletCount = gunConfigItem.BulletMaxCount - currentGun.BulletCountInGun.Value;
            //需要换弹
            if (needBulletCount > 0)
            {
                //有额外子弹
                if (currentGun.BulletCountOutGun.Value > 0)
                {
                    //切换状态
                    currentGun.GunState.Value = GunState.Reload;
                    this.GetSystem<ITimeSystem>().AddDelayTask(gunConfigItem.ReloadSeconds, () =>
                    {
                        //子弹充足
                        if (currentGun.BulletCountOutGun.Value > needBulletCount)
                        {
                            currentGun.BulletCountOutGun.Value -= needBulletCount;
                            currentGun.BulletCountInGun.Value += needBulletCount;
                        }
                        else
                        {
                            currentGun.BulletCountInGun.Value += currentGun.BulletCountOutGun.Value;
                            currentGun.BulletCountOutGun.Value = 0;
                        }

                        currentGun.GunState.Value = GunState.Idle;
                    });
                }
                else
                {
                    Debug.Log(currentGun.Name.Value + "没有额外子弹");
                }
            }
            else
            {
                Debug.Log(currentGun.Name.Value + "子弹数量已经是最大值");
            }
        }
    }
}