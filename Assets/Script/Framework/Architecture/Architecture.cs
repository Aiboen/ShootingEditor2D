using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace FrameworkDesign
{

    public interface IArchitecture
    {

        /// <summary>
        /// 注册 System
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        void RegisterSystem<T>(T instance) where T : ISystem;

        /// <summary>
        /// 注册Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        void RegisterModel<T>(T instance) where T : IModel;

        /// <summary>
        /// 注册 Utility
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        void RegisterUtility<T>(T instance) where T : IUtility;

        /// <summary>
        /// 获得 Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetModel<T>() where T : class, IModel;

        /// <summary>
        /// 获得System
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetSystem<T>() where T : class, ISystem;

        /// <summary>
        /// 获取工具
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetUtiliy<T>() where T : class, IUtility;
        /// <summary>
        /// 发送命令
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void SendCommand<T>() where T : ICommand, new();
        /// <summary>
        /// 发送命令
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        void SendCommand<T>(T command) where T : ICommand;

        /// <summary>
        /// 发送事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void SendEvent<T>() where T : new();

        /// <summary>
        /// 发送事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        void SendEvent<T>(T e);

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="onEvent"></param>
        /// <returns></returns>
        IUnRegister RegisetrEvent<T>(Action<T> onEvent);

        /// <summary>
        /// 注销事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="onEvent"></param>
        void UnRegisterEvent<T>(Action<T> onEvent);
    }

    /// <summary>
    /// 架构
    /// </summary>
    public abstract class Architecture<T> : IArchitecture where T : Architecture<T>, new()
    {
        /// <summary>
        /// 是否已经初始化完成
        /// </summary>
        private bool mInited = false;
        /// <summary>
        /// 用于初始化System的缓存
        /// </summary>
        private List<ISystem> mSystem = new List<ISystem>();
        /// <summary>
        /// 用于初始化Model的缓存
        /// </summary>
        private List<IModel> mModels = new List<IModel>();

        private IOCContainer mContainer = new IOCContainer();

        protected abstract void Init();



        #region 类似单例模式 但是仅在内部访问
        /// <summary>
        /// 增加注册
        /// </summary>
        public static Action<T> OnRegisterPatch = architecture => { };

        private static T mArchitecture = null;

        public static IArchitecture Interface
        {
            get
            {
                if (mArchitecture == null)
                {
                    MakeSureArchitecture();
                }
                return mArchitecture;
            }
        }

        //确保 Container 是有实例的
        static void MakeSureArchitecture()
        {
            if (mArchitecture == null)
            {
                mArchitecture = new T();
                mArchitecture.Init();

                OnRegisterPatch?.Invoke(mArchitecture);

                //初始化model
                foreach (var architectureModel in mArchitecture.mModels)
                {
                    architectureModel.Init();
                }
                //清空Model
                mArchitecture.mModels.Clear();

                foreach (var architectureSystem in mArchitecture.mSystem)
                {
                    architectureSystem.Init();
                }
                //清空System
                mArchitecture.mSystem.Clear();

                mArchitecture.mInited = true;
            }
        }
        #endregion

        #region 注册
        /// <summary>
        /// 注册System
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public void RegisterSystem<T>(T instance) where T : ISystem
        {
            //赋值
            instance.SetArchitecture(this);

            mContainer.Register<T>(instance);

            if (mInited)
            {
                instance.Init();
            }
            else
            {
                mSystem.Add(instance);
            }
        }
        /// <summary>
        /// 注册Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public void RegisterModel<T>(T instance) where T : IModel
        {
            //需要给model赋值一下
            instance.SetArchitecture(this);

            mContainer.Register<T>(instance);

            if (mInited)
            {
                instance.Init();
            }
            else
            {
                mModels.Add(instance);
            }
        }
        /// <summary>
        /// 注册Utility
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public void RegisterUtility<T>(T instance) where T : IUtility
        {
            mContainer.Register<T>(instance);
        }

        #endregion

        #region 获取
        public T GetModel<T>() where T : class, IModel
        {
            return mContainer.Get<T>();
        }

        public T GetSystem<T>() where T : class, ISystem
        {
            return mContainer.Get<T>();
        }

        public T GetUtiliy<T>() where T : class, IUtility
        {
            return mContainer.Get<T>();
        }

        #endregion

        #region 发送

        public void SendCommand<T>() where T : ICommand, new()
        {
            var command = new T();
            command.SetArchitecture(this);
            command.Execute();
        }

        public void SendCommand<T>(T command) where T : ICommand
        {
            command.SetArchitecture(this);
            command.Execute();
        }

        #endregion

        #region 事件
        private ITypeEventSystem mTypeEventSystem = new TypeEventSystem();
        public void SendEvent<T>() where T : new()
        {
            mTypeEventSystem.Send<T>();
        }

        public void SendEvent<T>(T e)
        {
            mTypeEventSystem.Send<T>(e);
        }

        public IUnRegister RegisetrEvent<T>(Action<T> onEvent)
        {
            return mTypeEventSystem.Register<T>(onEvent);
        }

        public void UnRegisterEvent<T>(Action<T> onEvent)
        {
            mTypeEventSystem.UnRegister<T>(onEvent);
        }
        #endregion
    }
}
