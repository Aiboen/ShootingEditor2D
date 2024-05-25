using FrameworkDesign;
using UnityEngine;

namespace ShootingEditor2D
{
    public class GunPickItem : ShootingEditor2DController
    {
        public string gunName;
        public int bulletCountInGun;
        public int bulletCountOutGun;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                this.SendCommand<PickCommand>(new PickCommand(gunName, bulletCountInGun, bulletCountOutGun));

                Destroy(this.gameObject);
            }
        }
    }
}