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

        public void Shoot()
        {
            if (mGunInfo.BulletCount.Value > 0)
            {
                var bullet = Instantiate(mBullet, mBullet.transform.position, mBullet.transform.rotation);
                bullet.transform.localScale = mBullet.transform.lossyScale;
                bullet.SetActive(true);

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