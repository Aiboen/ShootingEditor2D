using FrameworkDesign;
using Unity.VisualScripting;
using UnityEngine;

namespace ShootingEditor2D
{
    public class Player : MonoBehaviour, IController
    {
        private Rigidbody2D mRigidbody2D;
        private Trigger2DCheck mGroundCheck;

        /// <summary>
        /// 枪械
        /// </summary>
        private Gun mGun;

        /// <summary>
        /// 移动速度
        /// </summary>
        private const int moveSpeed = 5;

        /// <summary>
        /// 人物跳跃检查
        /// </summary>
        private bool mJumpPressed;

        /// <summary>
        /// 跳跃速度
        /// </summary>
        private readonly float mJumpSpeed = 5f;

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
            //空格跳跃
            if (Input.GetKeyDown(KeyCode.Space))
                mJumpPressed = true;
            //鼠标左键开枪
            if (Input.GetMouseButtonDown(0))
                mGun.Shoot();
            //R键换弹
            if (Input.GetKeyDown(KeyCode.R))
                mGun.Reload();
            //Q键换枪
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("换枪键按下");
                this.SendCommand(new ShiftGunCommand());
            }
        }

        private void FixedUpdate()
        {
            var horizontalMovement = Input.GetAxis("Horizontal");

            //转向
            if (horizontalMovement * transform.localScale.x < 0)
            {
                var localScale = transform.localScale;
                localScale.x = -localScale.x;
                transform.localScale = localScale;
            }

            mRigidbody2D.velocity = new Vector2(horizontalMovement * moveSpeed, mRigidbody2D.velocity.y);

            //跳跃
            var grounded = mGroundCheck.Triggered;
            if (mJumpPressed && grounded)
            {
                mRigidbody2D.velocity = new Vector2(mRigidbody2D.velocity.x, mJumpSpeed);
            }
            mJumpPressed = false;
        }

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return ShootingEditor2D.Interface;
        }
    }
}