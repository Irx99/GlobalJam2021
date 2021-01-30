﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
    /// <summary>
    /// This struct lets you declare observable properties.
    /// For example, let's say you have a class called Character, and you declare its speed like so : 
    /// 
    /// public MMObservable<float> Speed;
    /// 
    /// then, in any other class, you can register to OnValueChanged events on that property (usually in OnEnable) :
    /// 
    /// protected virtual void OnEnable()
    /// {
    ///     _myCharacter.Speed.OnValueChanged += OnSpeedChange;
    /// }
    /// 
    /// and unsubscribe like so :
    /// 
    /// protected virtual void OnDisable()
    /// {
    ///     _myCharacter.Speed.OnValueChanged -= OnSpeedChange;
    /// }
    /// 
    /// and then all you need is a method to handle that speed change :
    /// 
    /// protected virtual void OnSpeedChange()
    /// {
    ///     Debug.Log(_myCharacter.Speed.Value);
    /// }
    /// 
    /// You can look at the MMObservableTest demo scene for an example of how it's used.
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct MMObservable<T>
    {
        public Action OnValueChanged;

        private T _value;
        
        public T Value
        {
            get { return _value;  }
            set
            {
                if (!value.Equals(_value))
                {
                    _value = value;
                    OnValueChanged?.Invoke();
                }
            }
        }
    }
}
