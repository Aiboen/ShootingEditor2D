using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign
{
    public class IOCContainer
    {
        /// <summary>
        /// ÊµÀý
        /// </summary>
        public Dictionary<Type, object> mInstances = new Dictionary<Type, object>();
        /// <summary>
        /// ×¢²á
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public void Register<T>(T instance)
        {
            var key = typeof(T);

            if (mInstances.ContainsKey(key))
            {
                mInstances[key] = instance;
            }
            else
            {
                mInstances.Add(key, instance);
            }
        }
        /// <summary>
        /// »ñÈ¡
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>() where T : class
        {
            var key = typeof(T);
            object retObj;

            if (mInstances.TryGetValue(key, out retObj))
            {
                return retObj as T;
            }

            return null;
        }
    }
}
