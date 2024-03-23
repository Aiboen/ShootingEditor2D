using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace FrameworkDesign
{
    public class Singleton<T> where T : class
    {
        private static T mInstance;
        public static T Instance
        {
            get
            {
                if (mInstance == null)
                {
                    //ͨ�������ȡ����
                    var ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);

                    //��ȡ�޲η�public�Ĺ���
                    var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);
                    if (ctor == null)
                    {
                        throw new Exception("Non-public Constructor() not found in" + typeof(T));
                    }
                    mInstance = ctor.Invoke(null) as T;
                }
                return mInstance;
            }
        }
    }
}