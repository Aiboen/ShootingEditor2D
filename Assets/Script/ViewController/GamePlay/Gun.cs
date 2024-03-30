using FrameworkDesign;
using UnityEngine;

namespace ShootingEditor2D
{
    public class Gun : MonoBehaviour, IController
    {
        private GameObject mBullet;

        private GunInfo mGunInfo;

        private void Awake()
        {
            mBullet = transform.Find("Bullet").gameObject;
            mGunInfo = this.GetSystem<IGunSystem>().CurrentGun;
        }

        /// <summary>
        /// 开枪射击
        /// </summary>
        public void Shoot()
        {
            if (mGunInfo.BulletCountInGun.Value > 0 && mGunInfo.GunState.Value == GunState.Idle)
            {
                var bullet = Instantiate(mBullet, mBullet.transform.position, mBullet.transform.rotation);
                bullet.transform.localScale = mBullet.transform.lossyScale;
                bullet.SetActive(true);
                //发送开枪命令
                this.SendCommand(ShootCommand.Single);
            }
        }

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return ShootingEditor2D.Interface;
        }

        private void OnDestroy()
        {
            mGunInfo = null;
        }
    }
}