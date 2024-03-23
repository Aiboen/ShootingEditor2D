using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShootingEditor2D
{
    public class Bullet : MonoBehaviour
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
    }
}