using UnityEngine;

namespace ShootingEditor2D
{
    public class CameraController : MonoBehaviour
    {
        private Transform mPlayerTrans;


        private Vector3 TargetPos;

        private float mMinX = -5;
        private float mMaxX = 5;
        private float mMinY = -5;
        private float mMaxY = 5;

        private void Update()
        {
            if (!mPlayerTrans)
            {
                var playerGameObj = GameObject.FindWithTag("Player");

                if (playerGameObj)
                {
                    mPlayerTrans = playerGameObj.transform;
                }
                else
                {
                    return;
                }
            }

            var isRight = Mathf.Sign(mPlayerTrans.localScale.x);

            var playerPos = mPlayerTrans.position;

            TargetPos.x = playerPos.x + 3 * isRight;
            TargetPos.y = playerPos.y + 2;
            TargetPos.z = -10;

            var smoothSpeed = 5;

            // 增加一个平滑处理
            var position = transform.position;
            position = Vector3.Lerp(position,
                new Vector3(TargetPos.x, TargetPos.y, position.z), smoothSpeed * Time.deltaTime);

            // 锁定在一个固定区域
            transform.position = new Vector3(Mathf.Clamp(position.x, mMinX, mMaxX),
                Mathf.Clamp(position.y, mMinY, mMaxY),
                position.z);
        }
    }
}