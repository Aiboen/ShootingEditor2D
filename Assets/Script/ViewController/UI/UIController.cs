using FrameworkDesign;
using System;
using UnityEngine;

namespace ShootingEditor2D
{
    public class UIController : ShootingEditor2DController
    {
        private IStatSystem mStatSystem;
        private IPlayerModel mPlayerModel;
        private IGunSystem mGunSystem;

        private int mMaxBulletCount;

        private void Awake()
        {
            mStatSystem = this.GetSystem<IStatSystem>();
            mPlayerModel = this.GetModel<IPlayerModel>();
            mGunSystem = this.GetSystem<IGunSystem>();

            //查询代码
            mMaxBulletCount = this.SendQuery<int>(new MaxBulletCountQuery(mGunSystem.CurrentGun.Name.Value));
            //注册事件
            this.RegisterEvent<OnCurrentGunChanged>(e =>
            {
                mMaxBulletCount = this.SendQuery(new MaxBulletCountQuery(e.Name));
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        /// <summary>
        /// 自定义字体大小
        /// </summary>
        private readonly Lazy<GUIStyle> mLabelStyle = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.label)
        {
            fontSize = 40
        });

        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 300, 100), $"生命:{mPlayerModel.HP.Value}/3", mLabelStyle.Value);
            GUI.Label(new Rect(10, 60, 300, 100), $"枪内子弹:{mGunSystem.CurrentGun.BulletCountInGun.Value}/{mMaxBulletCount}", mLabelStyle.Value);
            GUI.Label(new Rect(10, 110, 300, 100), $"枪外子弹:{mGunSystem.CurrentGun.BulletCountOutGun.Value}", mLabelStyle.Value);
            GUI.Label(new Rect(10, 160, 300, 100), $"枪械名字:{mGunSystem.CurrentGun.Name.Value}", mLabelStyle.Value);
            GUI.Label(new Rect(10, 210, 300, 100), $"枪械状态:{mGunSystem.CurrentGun.GunState.Value}", mLabelStyle.Value);
            GUI.Label(new Rect(Screen.width - 10 - 300, 10, 300, 100), $"击杀数量:{mStatSystem.KillCount.Value}", mLabelStyle.Value);
        }

        private void OnDestroy()
        {
            mStatSystem = null;
            mPlayerModel = null;
            mGunSystem = null;
        }
    }
}