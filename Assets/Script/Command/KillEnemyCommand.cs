using FrameworkDesign;
using UnityEngine;

namespace ShootingEditor2D
{
    public class KillEnemyCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetSystem<IStatSystem>().KillCount.Value++;

            //打中敌人随机概率获得随机个数子弹
            var randomIndex = Random.Range(0, 100);
            if (randomIndex < 80)
            {
                this.GetSystem<IGunSystem>().CurrentGun.BulletCountInGun.Value += Random.Range(1, 4);
            }
        }
    }
}