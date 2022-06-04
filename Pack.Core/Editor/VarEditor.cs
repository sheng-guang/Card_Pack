using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Pack
{

    public class Editor<T> : Editor where T : class
    {
        public T tar;
        public virtual void Awake()
        {
            tar = target as T;
            if (tar == null) tar = (target as Component).GetComponent<T>();
        }
    }
}
