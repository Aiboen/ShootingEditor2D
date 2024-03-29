using UnityEngine;

namespace ShootingEditor2D
{
    public class Enemy : MonoBehaviour
    {
        private Trigger2DCheck mWallCheck;
        private Trigger2DCheck mFallCheck;
        private Trigger2DCheck mGroundCheck;

        private Rigidbody2D mRigidbody2D;

        private float mMoveSpeed = 10f;

        private void Awake()
        {
            mRigidbody2D = GetComponent<Rigidbody2D>();

            mWallCheck = transform.Find("WallCheck").GetComponent<Trigger2DCheck>();
            mFallCheck = transform.Find("FallCheck").GetComponent<Trigger2DCheck>();
            mGroundCheck = transform.Find("GroundCheck").GetComponent<Trigger2DCheck>();
        }

        private void FixedUpdate()
        {
            var scaleX = transform.localScale.x;

            if (mGroundCheck.Triggered && mFallCheck.Triggered && !mWallCheck.Triggered)
            {
                mRigidbody2D.velocity = new Vector2(scaleX * mMoveSpeed, mRigidbody2D.velocity.y);
            }
            else
            {
                //·´×ª
                var localScale = transform.localScale;
                localScale.x = -scaleX;
                transform.localScale = localScale;
            }
        }
    }
}