using FrameworkDesign;
using System;
using UnityEngine;

namespace ShootingEditor2D
{
    public class UIController : MonoBehaviour, IController
    {
        private IStatSystem mStatSystem;
        private IPlayerModel mPlayerModel;
        private IGunSystem mGunSystem;

        private void Awake()
        {
            mStatSystem = this.GetSystem<IStatSystem>();
            mPlayerModel = this.GetModel<IPlayerModel>();
            mGunSystem = this.GetSystem<IGunSystem>();
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
            GUI.Label(new Rect(10, 60, 300, 100), $"子弹:{mGunSystem.CurrentGun.BulletCount.Value}", mLabelStyle.Value);
            GUI.Label(new Rect(Screen.width - 10 - 300, 10, 300, 100), $"击杀数量:{mStatSystem.KillCount.Value}", mLabelStyle.Value);
        }

        private void OnDestroy()
        {
            mStatSystem = null;
            mPlayerModel = null;
            mGunSystem = null;
        }

        public IArchitecture GetArchitecture()
        {
            return ShootingEditor2D.Interface;
        }
    }
}