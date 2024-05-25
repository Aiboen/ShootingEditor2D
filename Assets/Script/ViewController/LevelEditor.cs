using System;
using UnityEngine;

namespace ShootingEditor2D
{
    public class LevelEditor : MonoBehaviour
    {
        public SpriteRenderer EmptyHighlight;

        /// <summary>
        /// 当前高亮块坐标
        /// </summary>
        private float mCurrentHighlihtPosX;

        private float mCurrentHighlihtPosY;

        /// <summary>
        /// 是否可绘制
        /// </summary>
        private bool mCanDraw;

        /// <summary>
        /// 当前鼠标下的 GameObject
        /// </summary>
        private GameObject mCurrentObjectMouseOn;

        private OperateMode mCurrentOperateMode = OperateMode.Draw;

        private readonly Lazy<GUIStyle> mModeLabelStyle = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.label)
        {
            fontSize = 30,
            alignment = TextAnchor.MiddleCenter
        });

        private readonly Lazy<GUIStyle> mButtonStyle = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.button)
        {
            fontSize = 30,
        });

        private void OnGUI()
        {
            var modeLabelRect = RectHelper.RectForAnchorCenter(Screen.width * 0.5f, 35, 200, 50);
            // 显示当前模式
            GUI.Label(modeLabelRect, mCurrentOperateMode.ToString(), mModeLabelStyle.Value);

            var drawButtonRect = new Rect(10, 10, 150, 40);
            if (GUI.Button(drawButtonRect, "绘制", mButtonStyle.Value))
            {
                mCurrentOperateMode = OperateMode.Draw;
            }

            var eraseButtonRect = new Rect(10, 60, 150, 40);
            if (GUI.Button(eraseButtonRect, "橡皮", mButtonStyle.Value))
            {
                mCurrentOperateMode = OperateMode.Erase;
            }
        }

        private void Update()
        {
            var mousePosition = Input.mousePosition;
            var worldMousePos = Camera.main.ScreenToWorldPoint(mousePosition);

            //限制鼠标坐标为整数
            worldMousePos.x = Mathf.Floor(worldMousePos.x + 0.5f);
            worldMousePos.y = Mathf.Floor(worldMousePos.y + 0.5f);
            worldMousePos.z = 0;

            if (GUIUtility.hotControl == 0)
            {
                EmptyHighlight.gameObject.SetActive(true);
            }
            else
            {
                EmptyHighlight.gameObject.SetActive(false);
            }

            if (Math.Abs(EmptyHighlight.transform.position.x - worldMousePos.x) < 0.1f
                && Math.Abs(EmptyHighlight.transform.position.y - worldMousePos.y) < 0.1f)
            {
            }
            else
            {
                //设置高亮块的位置
                var emptyHighlightPos = worldMousePos;
                emptyHighlightPos.z = -1;
                EmptyHighlight.transform.position = emptyHighlightPos;

                Ray ray = Camera.main.ScreenPointToRay(mousePosition);

                var hit = Physics2D.Raycast(ray.origin, Vector2.zero, Mathf.Infinity);

                //碰撞说明有地块
                if (hit.collider)
                {
                    if (mCurrentOperateMode == OperateMode.Draw)
                    {
                        EmptyHighlight.color = new Color(1, 0, 0, 0.5f); // 红色代表不能绘制
                    }
                    else
                    {
                        EmptyHighlight.color = new Color(1, 0.5f, 0, 0.5f); // 橙色代表可擦除
                    }

                    mCanDraw = false;
                    mCurrentObjectMouseOn = hit.collider.gameObject;
                }
                else
                {
                    if (mCurrentOperateMode == OperateMode.Draw)
                    {
                        EmptyHighlight.color = new Color(1, 1, 1, 0.5f); // 白色代表可以绘制
                    }
                    else
                    {
                        EmptyHighlight.color = new Color(0, 0, 1, 0.5f); // 蓝色代表橡皮状态
                    }

                    mCanDraw = true;
                    mCurrentObjectMouseOn = null;
                }

                if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) && GUIUtility.hotControl == 0)
                {
                    if (mCanDraw && mCurrentOperateMode == OperateMode.Draw)
                    {
                        var groundPrefab = Resources.Load<GameObject>("Ground");
                        var groundGameObj = Instantiate(groundPrefab, transform);

                        groundGameObj.transform.position = worldMousePos;
                        groundGameObj.name = "Ground";
                    }
                    else if (mCurrentObjectMouseOn && mCurrentOperateMode == OperateMode.Erase)
                    {
                        Destroy(mCurrentObjectMouseOn);
                    }
                }
            }
        }
    }
}