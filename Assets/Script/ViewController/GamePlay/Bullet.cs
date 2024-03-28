using FrameworkDesign;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShootingEditor2D
{
    public class Bullet : MonoBehaviour, IController
    {
        private Rigidbody2D mRigidbody;

        private void Awake()
        {
            mRigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            mRigidbody.velocity = new Vector2(10, mRigidbody.velocity.y);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {

            if (other.gameObject.CompareTag("Enemy"))
            {
                this.SendCommand<KillEnemyCommand>();
                Destroy(other.gameObject);
            }
        }



        public IArchitecture GetArchitecture()
        {
            return ShootingEditor2D.Interface;
        }
    }
}