using FrameworkDesign;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShootingEditor2D
{
    public class Bullet : MonoBehaviour, IController
    {
        private Rigidbody2D mRigidbody;
        private float moveSpeed = 10f;

        private void Awake()
        {
            mRigidbody = GetComponent<Rigidbody2D>();

            Destroy(gameObject, 5);
        }

        private void Start()
        {
            var isRight = Mathf.Sign(transform.localScale.x);

            mRigidbody.velocity = Vector2.right * moveSpeed * isRight;

        }

        private void OnCollisionEnter2D(Collision2D other)
        {

            if (other.gameObject.CompareTag("Enemy"))
            {
                this.SendCommand<KillEnemyCommand>();
                Destroy(other.gameObject);

                Destroy(gameObject);
            }
        }



        public IArchitecture GetArchitecture()
        {
            return ShootingEditor2D.Interface;
        }
    }
}