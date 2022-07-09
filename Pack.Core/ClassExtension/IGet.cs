using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//interface-----------------------------------------------------------------------------------------------------------------
public interface IGet<T>
{
    T Value { get; }
}

//public interface IGetSeat<T> : IGet<T>
//{
//    IGet<T> IGet { get; set; }
//     IGetSeat<T> SetIGet(object o);
//     IGetSeat<T> TrySetDefault(object o);
//}

//class----------------------------------------------------------------------------------------------------------------
public class IGetSeat<T> : IGet<T> /*IGetSeat<T>*/, ISetObj, ISet<T>
{
    public IGet<T> IGet { get; set; }
    public T Value { get { return IGet.NotNull_and_NotEqualNull() ? IGet.Value : default; } }
    T ISet<T>.Value { set => SetIGet(value); }


    bool CanSetDefault = true;
    public IGetSeat<T> TrySetDefault(object o)
    {
        if (CanSetDefault == false) return this;
        IGet = o.ToIGet<T>();
        return this;
    }

    public void setValue(object value)
    {
        SetIGet(value);
    }
    public IGetSeat<T> SetIGet(object o)
    {
        CanSetDefault = false;
        //这里无法使用方法重载
        //需要在外部重载
        IGet = o.ToIGet<T>();
        return this;
    }
    public static implicit operator T(IGetSeat<T> d)
    {
        return d.Value;
    }
    //public static explicit operator Digit(double d)
    //{
    //    return new Digit(d);
    //}

    public override string ToString()
    {
        return "" + GetType().Name + "  (  " + (IGet.NotNull_and_NotEqualNull() ? IGet.ToString() : " null ") + " ) ";

    }
}

public abstract class Get<T> : IGet<T>
{
    public abstract T Value { get; set; }


    public static implicit operator Get<T>(Func<T> get)
    {
        return new IGetFunc<T>() { f = get };
    }
}



public class IGet_Value<T> : Get<T>
{
    public IGet_Value() { }
    public IGet_Value(T value)
    {
        Value = value;
    }

    public override T Value { get; set; } = default;
    public override string ToString()
    {
        return "" + GetType().Name + " ( " + Value + " ) ";
    }
}
public class IGetFunc<T> : Get<T>
{
    public IGetFunc() { }
    public IGetFunc(Func<T>ff) { f = ff; }
    public Func<T> f = null;
    public override T Value
    {
        get { return f == null ? default : f(); }
        set { }
    }
}

