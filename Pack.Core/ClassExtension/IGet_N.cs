using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pack
{
    //public interface IGetSeat_N<T> :IGet<N<T>>, IGet<T> where T : struct, IEquatable<T>
    //{
    //    IGet<N<T>> IGet { get; set; }
    //    IGetSeat_N<T> SetIGet(object o);
    //    IGetSeat_N<T> TrySetDefault(object o);
    //}

    //class----------------------------------------------------------------------------------------------------------------
    public class IGetSeat_N<T> :IGet<N<T>>,IGet<T>, ISetObj/*,IGetSeat_N<T> */  
        where T : struct, IEquatable<T>
    {

        public IGet<N<T>> IGet { get; set; }
        
        public T Value { get { return IGet.NotNull_and_NotEqualNull() ? IGet.Value : default; } }
        N<T> IGet<N<T>>.Value => IGet.Value;
        
        //default------------------------------------------------------------------------------
         bool CanSetDefault = true;
        public IGetSeat_N<T> TrySetDefault(object o)
        {
            if (CanSetDefault == false) return this;
            IGet = o.ToIGet<N<T>>();
            return this;
        }



        //set-----------------------------------------------------------------------------------------
        void ISetObj.setValue(object value)
        {
            SetIGet(value);
        }
        public IGetSeat_N<T> SetIGet(object o)
        {
            CanSetDefault = false;
            IGet = o.ToIGet<N< T>>();
            return this;
        }


        public override string ToString()
        {
            return GetType() + ":   " + IGet.ToString();
        }
    }

}