using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShootingEditor2D
{
    public class Trigger2DCheck : MonoBehaviour
    {
        public int EnterCount = 0;
        /// <summary>
        /// 使用LayerMask 可以在Inspector配置想要触发碰撞的Layer
        /// </summary>
        public LayerMask TargetLayers;

        public bool Triggered
        {
            get { return EnterCount > 0; }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsInLayerMask(other.gameObject, TargetLayers))
            {
                EnterCount++;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (IsInLayerMask(other.gameObject, TargetLayers))
            {
                EnterCount--;
            }
        }
        /// <summary>
        /// LayerMask 中是否包含我想要的Layer
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="layerMask"></param>
        /// <returns></returns>
        private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
        {
            // 根据Layer数值进行移位获得用于运算的Mask值
            var objLayerMask = 1 << obj.layer;
            return (layerMask.value & objLayerMask) > 0;
        }
    }
}