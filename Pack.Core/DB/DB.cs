using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

    public interface IDBdata<T>
    {
        string DataKind { get; }
        string FullName { get; set; }
        string Line { get; set; }
        T CreatNew(string s);
    }


    public static class DB<T> where T : IDBdata<T>, new()
    {
        static IDataCollection<T> collection = new DataCollection<T>();
        static DB()
        {
            collection.LoadAll();
        }
        public static IDataCollection<T> Datas()
        {
            return collection;
        }
    }







   
