using FrameworkDesign;
using UnityEngine;

namespace ShootingEditor2D
{
    public class GunPickItem : MonoBehaviour, IController
    {
        public string gunName;
        public int bulletCountInGun;
        public int bulletCountOutGun;

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return ShootingEditor2D.Interface;
        }

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