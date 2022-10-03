using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

    public interface IDataCollection<T>
    {
        void LoadAll();
        void FixWindowIndex(ref int start, ref int end);
        T GetData(int index);
        T EnsureData(string FullName);
        T EnsureData_Save(string fullName);
        void ClearAllData_Save();

    }
    public class DataCollection<T>:IDataCollection<T> where T : IDBdata<T>, new()
    {

        public DataCollection()
        {
            def = new T();
            kind = def.DataKind;
        }
        T def;
        public string kind;
        public void LoadAll()
        {
            kind.ToFileinfos("*.csv", SearchOption.AllDirectories).ForEachLines(NewData);
        }

        T NewData_(string s)
        {
            var ne = def.CreatNew(s);
            if (ne.Null_Or_EqualNull()) return default;
            dic[ne.FullName] = ne;
            DataList.Add(ne);
            return ne;
        }
        Dictionary<string, T> dic = new Dictionary<string, T>();
        List<T> DataList = new List<T>();

        //增
        void NewData(string s) { NewData_(s); }
        public T EnsureData(string fullName)
        {
            if (dic.ContainsKey(fullName)) return dic[fullName];
            return NewData_(fullName);
        }
        //查
        public  T GetData(int index)
        {
            if (DataList.HaveIndex(index) == false) return default;
            return DataList[index];
        }
        public void FixWindowIndex(ref int start, ref int end)
        {
            //todo
            if (start < 0)
            {
                int ToMoveNext = 0 - start;
                start += ToMoveNext;
                end += ToMoveNext;
            }
            if (DataList.IndexOverMax(end))
            {
                int ToMoveback = end - DataList.MaxIndex();
                start -= ToMoveback;
                end -= ToMoveback;
            }
            if (start < 0) start = 0;
            //if (DataList.Count > index == false) return DataList.Count - 1;
            //return index;
        }

        public void ClearAllData_Save()
        {
            dic.Clear();
            DataList.Clear();
            SaveAll();
        }



        public T EnsureData_Save(string fullName)
        {
            var re = EnsureData(fullName);
            SaveAll();
            return re;
        }
        public void SaveAll()
        {
            var fileName = kind + "/" + "-";
            fileName.WriteEachLine(DataList, fileType: ".csv");

        }


    }

