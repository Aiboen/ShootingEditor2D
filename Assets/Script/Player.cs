using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShootingEditor2D
{
    public class Player : MonoBehaviour
    {
        private Rigidbody2D mRigidbody2D;

        const int moveSpeed = 5;

        private void Awake()
        {
            mRigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            var horizontalMovement = Input.GetAxis("Horizontal");

            mRigidbody2D.velocity = new Vector2(horizontalMovement * moveSpeed, mRigidbody2D.velocity.y);
        }
    }
}
