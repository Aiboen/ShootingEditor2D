using System;

namespace FrameworkDesign
{
    public class BindableProperty<T>
    {
        private T mValue;

        public T Value
        {
            get => mValue;

            set
            {
                if (!mValue.Equals(value))
                {
                    mValue = value;
                    mOnValueChanged?.Invoke(value);
                }
            }
        }

        private Action<T> mOnValueChanged = (v) => { };

        public IUnRegister RegisterOnValueChanged(Action<T> onValueChanged)
        {
            mOnValueChanged += onValueChanged;
            return new BindablePropertyUnRegister<T>()
            {
                BindableProperty = this,
                OnValueChanged = onValueChanged
            };
        }

        public void UnRegisterOnValueChanged(Action<T> onValueChanged)
        {
            mOnValueChanged -= onValueChanged;
        }
    }

    public class BindablePropertyUnRegister<T> : IUnRegister
    {
        public BindableProperty<T> BindableProperty { get; set; }

        public Action<T> OnValueChanged { get; set; }

        public void UnRegister()
        {
            BindableProperty.UnRegisterOnValueChanged(OnValueChanged);

            BindableProperty = null;
            OnValueChanged = null;
        }
    }
}