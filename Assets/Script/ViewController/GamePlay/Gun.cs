using UnityEngine;
namespace ShootingEditor2D
{
    public class Gun : MonoBehaviour
    {
        private GameObject mBullet;

        private void Awake()
        {
            mBullet = transform.Find("Bullet").gameObject;
        }

        public void Shoot()
        {
            var bullet = Instantiate(mBullet, mBullet.transform.position, mBullet.transform.rotation);
            bullet.transform.localScale = mBullet.transform.lossyScale;
            bullet.SetActive(true);
        }
    }
}