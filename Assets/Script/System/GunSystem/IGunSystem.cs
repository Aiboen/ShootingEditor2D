using FrameworkDesign;
using System.Collections.Generic;
using System.Linq;

namespace ShootingEditor2D
{
    public interface IGunSystem : ISystem
    {
        /// <summary>
        /// 当前手持枪支
        /// </summary>
        GunInfo CurrentGun { get; }

        /// <summary>
        /// 枪支队列
        /// </summary>
        Queue<GunInfo> GunInfos { get; }

        /// <summary>
        /// 捡枪
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bulletCountInGun"></param>
        /// <param name="bulletCountOutGun"></param>
        void PickGun(string name, int bulletCountInGun, int bulletCountOutGun);

        /// <summary>
        /// 切换枪械
        /// </summary>
        void ShiftGun();
    }

    public class GunSystem : AbstractSystem, IGunSystem
    {
        /// <summary>
        /// 拥有枪支队列
        /// </summary>
        private Queue<GunInfo> mGunInfos = new Queue<GunInfo>();

        public Queue<GunInfo> GunInfos { get => mGunInfos; }

        public override void OnInit()
        {
        }

        /// <summary>
        /// 枪械信息
        /// </summary>
        public GunInfo CurrentGun { get; } = new GunInfo()
        {
            BulletCountInGun = new BindableProperty<int>()
            {
                Value = 3
            },
            BulletCountOutGun = new BindableProperty<int>()
            {
                Value = 1
            },
            Name = new BindableProperty<string>()
            {
                Value = "手枪"
            },

            GunState = new BindableProperty<GunState>()
            {
                Value = GunState.Idle
            }
        };

        public void PickGun(string name, int bulletCountInGun, int bulletCountOutGun)
        {
            //捡到的枪和当前枪是同一种
            if (CurrentGun.Name.Value == name)
            {
                CurrentGun.BulletCountInGun.Value += bulletCountInGun;
                CurrentGun.BulletCountOutGun.Value += bulletCountOutGun;
            }
            //已经拥有这把抢了
            else if (mGunInfos.Any(info => info.Name.Value == name))
            {
                var gunInfo = mGunInfos.First(info => info.Name.Value == name);

                gunInfo.BulletCountOutGun.Value += bulletCountOutGun;
                gunInfo.BulletCountInGun.Value += bulletCountInGun;
            }
            else
            {
                EnqueueCurrentGun(name, bulletCountInGun, bulletCountOutGun);
            }
        }

        public void ShiftGun()
        {
            UnityEngine.Debug.Log(mGunInfos.Count);
            if (mGunInfos.Count > 0)
            {
                var nextGunInfo = mGunInfos.Dequeue();
                EnqueueCurrentGun(nextGunInfo.Name.Value, nextGunInfo.BulletCountInGun.Value, nextGunInfo.BulletCountOutGun.Value);
            }
        }

        private void EnqueueCurrentGun(string nextGunName, int nextGunBulletCountInGun, int nextGunBulletCountOutGun)
        {
            //复制当前枪械信息
            var currentGunInfo = new GunInfo()
            {
                Name = new BindableProperty<string> { Value = CurrentGun.Name.Value },
                BulletCountInGun = new BindableProperty<int> { Value = CurrentGun.BulletCountInGun.Value },
                BulletCountOutGun = new BindableProperty<int> { Value = CurrentGun.BulletCountOutGun.Value },
                GunState = new BindableProperty<GunState> { Value = CurrentGun.GunState.Value },
            };

            //缓存
            mGunInfos.Enqueue(currentGunInfo);

            //新枪设置为当前枪
            CurrentGun.Name = new BindableProperty<string> { Value = nextGunName };
            CurrentGun.GunState = new BindableProperty<GunState> { Value = GunState.Idle };
            CurrentGun.BulletCountInGun = new BindableProperty<int> { Value = nextGunBulletCountInGun };
            CurrentGun.BulletCountOutGun = new BindableProperty<int> { Value = nextGunBulletCountOutGun };

            //发送换枪事件
            this.SendEvent(new OnCurrentGunChanged { Name = nextGunName });
        }
    }
}