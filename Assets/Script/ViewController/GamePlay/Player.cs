using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace ShootingEditor2D
{
    public class Player : MonoBehaviour
    {
        private Rigidbody2D mRigidbody2D;
        private Trigger2DCheck mGroundCheck;
        private Gun mGun;


        const int moveSpeed = 5;

        private bool mJumpPressed;
        private void Awake()
        {
            mRigidbody2D = GetComponent<Rigidbody2D>();
            mGroundCheck = transform.Find("GroundCheck").GetComponent<Trigger2DCheck>();
            if (mGroundCheck == null)
            {
                mGroundCheck = transform.Find("GroundCheck").AddComponent<Trigger2DCheck>();
                mGroundCheck.TargetLayers = LayerMask.NameToLayer("Default");
            }
            mGun = transform.Find("Gun").GetComponent<Gun>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                mJumpPressed = true;
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                mGun.Shoot();
            }
        }

        private void FixedUpdate()
        {
            var horizontalMovement = Input.GetAxis("Horizontal");

            //×ªÏò
            if (horizontalMovement * transform.localScale.x < 0)
            {
                var localScale = transform.localScale;
                localScale.x = -localScale.x;
                transform.localScale = localScale;
            }

            mRigidbody2D.velocity = new Vector2(horizontalMovement * moveSpeed, mRigidbody2D.velocity.y);

            var grounded = mGroundCheck.Triggered;
            if (mJumpPressed && grounded)
            {
                mRigidbody2D.velocity = new Vector2(mRigidbody2D.velocity.x, 5);
            }
            mJumpPressed = false;
        }
    }
}
