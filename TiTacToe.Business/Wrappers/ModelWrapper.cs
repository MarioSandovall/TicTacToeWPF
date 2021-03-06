﻿using Prism.Mvvm;
using System.Runtime.CompilerServices;

namespace TiTacToe.Business.Wrappers
{
    public class ModelWrapper<T> : BindableBase
    {
        public ModelWrapper(T model)
        {
            Model = model;
        }

        public T Model { get; }

        protected virtual void SetValue<TValue>(TValue value,
            [CallerMemberName] string propertyName = null)
        {
            typeof(T).GetProperty(propertyName).SetValue(Model, value);
            OnPropertyChanged(propertyName);
        }

        protected virtual TValue GetValue<TValue>([CallerMemberName] string propertyName = null)
        {
            return (TValue) typeof(T).GetProperty(propertyName)?.GetValue(Model);
        }
    }
}
