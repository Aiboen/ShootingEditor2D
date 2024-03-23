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
        /// ע�� System
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        void RegisterSystem<T>(T instance) where T : ISystem;

        /// <summary>
        /// ע��Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        void RegisterModel<T>(T instance) where T : IModel;

        /// <summary>
        /// ע�� Utility
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        void RegisterUtility<T>(T instance) where T : IUtility;

        /// <summary>
        /// ��� Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetModel<T>() where T : class, IModel;

        /// <summary>
        /// ���System
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetSystem<T>() where T : class, ISystem;

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetUtiliy<T>() where T : class, IUtility;
        /// <summary>
        /// ��������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void SendCommand<T>() where T : ICommand, new();
        /// <summary>
        /// ��������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        void SendCommand<T>(T command) where T : ICommand;

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void SendEvent<T>() where T : new();

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        void SendEvent<T>(T e);

        /// <summary>
        /// ע���¼�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="onEvent"></param>
        /// <returns></returns>
        IUnRegister RegisetrEvent<T>(Action<T> onEvent);

        /// <summary>
        /// ע���¼�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="onEvent"></param>
        void UnRegisterEvent<T>(Action<T> onEvent);
    }

    /// <summary>
    /// �ܹ�
    /// </summary>
    public abstract class Architecture<T> : IArchitecture where T : Architecture<T>, new()
    {
        /// <summary>
        /// �Ƿ��Ѿ���ʼ�����
        /// </summary>
        private bool mInited = false;
        /// <summary>
        /// ���ڳ�ʼ��System�Ļ���
        /// </summary>
        private List<ISystem> mSystem = new List<ISystem>();
        /// <summary>
        /// ���ڳ�ʼ��Model�Ļ���
        /// </summary>
        private List<IModel> mModels = new List<IModel>();

        private IOCContainer mContainer = new IOCContainer();

        protected abstract void Init();



        #region ���Ƶ���ģʽ ���ǽ����ڲ�����
        /// <summary>
        /// ����ע��
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

        //ȷ�� Container ����ʵ����
        static void MakeSureArchitecture()
        {
            if (mArchitecture == null)
            {
                mArchitecture = new T();
                mArchitecture.Init();

                OnRegisterPatch?.Invoke(mArchitecture);

                //��ʼ��model
                foreach (var architectureModel in mArchitecture.mModels)
                {
                    architectureModel.Init();
                }
                //���Model
                mArchitecture.mModels.Clear();

                foreach (var architectureSystem in mArchitecture.mSystem)
                {
                    architectureSystem.Init();
                }
                //���System
                mArchitecture.mSystem.Clear();

                mArchitecture.mInited = true;
            }
        }
        #endregion

        #region ע��
        /// <summary>
        /// ע��System
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public void RegisterSystem<T>(T instance) where T : ISystem
        {
            //��ֵ
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
        /// ע��Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public void RegisterModel<T>(T instance) where T : IModel
        {
            //��Ҫ��model��ֵһ��
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
        /// ע��Utility
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public void RegisterUtility<T>(T instance) where T : IUtility
        {
            mContainer.Register<T>(instance);
        }

        #endregion

        #region ��ȡ
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

        #region ����

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

        #region �¼�
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
