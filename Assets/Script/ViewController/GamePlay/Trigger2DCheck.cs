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
        /// ʹ��LayerMask ������Inspector������Ҫ������ײ��Layer
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
        /// LayerMask ���Ƿ��������Ҫ��Layer
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="layerMask"></param>
        /// <returns></returns>
        private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
        {
            // ����Layer��ֵ������λ������������Maskֵ
            var objLayerMask = 1 << obj.layer;
            return (layerMask.value & objLayerMask) > 0;
        }
    }
}