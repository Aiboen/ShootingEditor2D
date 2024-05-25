using FrameworkDesign;
using UnityEngine;

namespace ShootingEditor2D
{
    public class SupplyStatioin : ShootingEditor2DController
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                this.SendCommand<FullBulletCommand>();

                Destroy(this.gameObject);
            }
        }
    }
}