using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


    public interface IResData
    {
        string DataKind { get; }
        string FullName { get; }
    }

    public class DefData : IDBdata<DefData>
    {
        public string DataKind => "-";

        public string FullName { get; set; }
        public string Line { get; set; }
        public DefData CreatNew(string s)
        {
            return Creat(s);
        }
        public static DefData Creat(string s)
        {
            if (s.StartsWith("//")) return null;
            if (string.IsNullOrWhiteSpace(s)) return null;
            var re = new DefData();
            re.Line = s;
            var ss = s.Split_();
            try
            {
                re.FullName = ss[0];
            }
            catch (Exception) { }
            return re;
        }
        public override string ToString()
        {
            return Line;
        }
    }

    public static class DB
    {
        static Dictionary<string, IDataCollection<DefData>> datas = new Dictionary<string, IDataCollection<DefData>>();
        public static IDataCollection<DefData> Datas(string kind)
        {
            if (datas.ContainsKey(kind)) return datas[kind];
            var ne = new DataCollection<DefData>();
            ne.kind = kind;
            datas[kind] = ne;
            return ne;
        }
    }
