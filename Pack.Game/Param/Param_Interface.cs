using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public interface IParam<T> : IGet<T>
    {
        void Listen(Action act);
        IParam<T> SetValue(T value);

        new T Value { get; set; }
        IParam<T> TrySetDefault(T value);

    }

    public interface IBackToBaseData
    {
        void BackToBase();
    }



public interface ISetParamBuffable
{
    void SetValueBuffed(object o);
    void SetValueBase(object o);
}