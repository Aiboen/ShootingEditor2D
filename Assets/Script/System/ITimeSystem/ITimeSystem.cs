using FrameworkDesign;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingEditor2D
{
    public interface ITimeSystem : ISystem
    {
        float CurrentSeconds { get; }

        void AddDelayTask(float seconds, Action onFinish);
    }

    public class TimeSystem : AbstractSystem, ITimeSystem
    {
        public class TiemSystemUpdateBehaviour : MonoBehaviour
        {
            public event Action OnUpdate;

            private void Update()
            {
                OnUpdate?.Invoke();
            }
        }

        public override void OnInit()
        {
            var updateBehaviourGameObj = new GameObject(nameof(TiemSystemUpdateBehaviour));
            UnityEngine.Object.DontDestroyOnLoad(updateBehaviourGameObj);

            var updateBehaviour = updateBehaviourGameObj.AddComponent<TiemSystemUpdateBehaviour>();

            updateBehaviour.OnUpdate += OnUpdate;
        }

        private void OnUpdate()
        {
            CurrentSeconds += Time.deltaTime;

            if (mDelayTask.Count > 0)
            {
                var currentNote = mDelayTask.First;

                while (currentNote != null)
                {
                    var delayTask = currentNote.Value;
                    var nextNote = currentNote.Next;

                    if (delayTask.State == DelayTaskState.NotStart)
                    {
                        delayTask.State = DelayTaskState.Started;

                        delayTask.StartSeconds = CurrentSeconds;
                        delayTask.FinishSeconds = CurrentSeconds + delayTask.Seconds;
                    }
                    else if (delayTask.State == DelayTaskState.Started)
                    {
                        if (CurrentSeconds > delayTask.FinishSeconds)
                        {
                            delayTask.State = DelayTaskState.Finish;
                            delayTask.OnFinish?.Invoke();
                            delayTask.OnFinish = null;
                            mDelayTask.Remove(currentNote);
                        }

                        currentNote = nextNote;
                    }
                }
            }
        }

        public float CurrentSeconds { get; private set; } = 0.0f;

        private LinkedList<DelayTask> mDelayTask = new LinkedList<DelayTask>();

        public void AddDelayTask(float seconds, Action onFinish)
        {
            var delayTask = new DelayTask
            {
                Seconds = seconds,
                OnFinish = onFinish,
                State = DelayTaskState.NotStart,
            };

            mDelayTask.AddLast(new LinkedListNode<DelayTask>(delayTask));
        }
    }
}