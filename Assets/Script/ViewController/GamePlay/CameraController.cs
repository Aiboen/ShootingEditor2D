using UnityEngine;

namespace ShootingEditor2D
{
    public class CameraController : MonoBehaviour
    {
        private Transform mPlayerTrans;

        private void Update()
        {
            if (!mPlayerTrans)
            {
                var playerObj = GameObject.FindWithTag("Player");
                if (playerObj)
                {
                    mPlayerTrans = playerObj.transform;
                }
                else
                {
                    return;
                }
            }

            var cameraPos = transform.position;

            var playerPos = mPlayerTrans.position;

            cameraPos.x = playerPos.x + 3;
            cameraPos.y = playerPos.y + 2;

            transform.position = cameraPos;
        }
    }
}