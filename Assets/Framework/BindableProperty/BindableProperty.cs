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
                //Equals函数的调用者不能为空，引用类型默认值为空会引发空指针异常，导致System无法被注册
                //解决方法使用值value调用 value.Equals(mValue)
                if (!value.Equals(mValue))
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