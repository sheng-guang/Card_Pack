using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;




    public class UnitData
    {
        static string Kind => nameof(UnitData);
        public static IEnumerable<UnitData> GetAllData()
        {
            EnsureLoaded();
            return DataList;
        }
        public static UnitData GetData(string fullName)
        {
            EnsureLoaded();
            if (loaded.TryGetValue(fullName, out var re)) return re;
            return Default;
        }
        public static UnitData Default { get; private set; } = new UnitData();
        public static void EnsureData(string FullName)
        {
            EnsureLoaded();
            if (loaded.ContainsKey(FullName)) return;
            CreatNewData(FullName);
        }
        public static void SaveAll()
        {
            EnsureLoaded();
            foreach (var item in Datas)
            {
                 var fileName = Kind + "/" + item.Key;
                fileName.WriteEachLine(item.Value.Values,fileType:".csv",titleInfo:getExample);
            }
        }
        static Dictionary<string, UnitData> loaded = new Dictionary<string, UnitData>();
        static Dictionary<string, Dictionary<string,UnitData>> Datas = new Dictionary<string, Dictionary<string,UnitData>>();
        static List<UnitData> DataList = new List<UnitData>();
        static bool Loaded = false;
        public static void ClearLoaded()
        {
            Debug.Log("ClearLoaded()");
            Loaded = false;
        }
        static void EnsureLoaded()
        {
            if (Loaded) { return; }
            Loaded = true;
            loaded.Clear();
            Datas.Clear();
            DataList.Clear();
            Kind.ToFileinfos("*.csv",option: SearchOption.AllDirectories).ForEachLines(CreatNewData);
            Debug.Log("load unit datas  count: " + DataList.Count);
        }
        static void CreatNewData(string s)
        {
            if (s.StartsWith("//")) return;
            //Debug.Log("load" + s);
            var ne = Creat(s);
            if (loaded.ContainsKey(ne.FullName)) {/* Debug.Log("already load " + ne.FullName + " data");*/ return; }
            loaded.Add(ne.FullName,ne);
            if (Datas.ContainsKey(ne.PackName)==false) Datas[ne.PackName] = new Dictionary<string, UnitData>();
            Datas[ne.PackName][ne.KindName] = ne;
            DataList.Add(ne);
        }
       
  
        public string OriginString;
        
        public string FullName;
        public string PackName;
        public string KindName;
        public int? Mana = null;
        public bool IsHero = false;

        public int? MaxHp=null;
        public int? atk = null;

        public float UIHeight = 0.2f;
        public float WideR = 0.3f;
        
        public int? speed = null;

        public override string ToString()
        {
            return 
                FullName + ","
                + (Mana.HasValue ? "" + Mana.Value : "") + ","
                + (IsHero ? "isHero" : "") + ","
                
                + (MaxHp.HasValue ? "" + MaxHp.Value : "") + ","
                +(atk.HasValue?""+atk.Value:"")+","
                
                +(UIHeight+"") + ","
                + (WideR+"") + ","

                + (speed.HasValue?""+speed.Value:"")+",";
        
        }

        public static string getExample =
            "//[0]full name" +
            ",[1]Mana" +
            ",[2]ishero" +

            ",[3]MaxHp" +
            ",[4] atk" +
            
            ",[5] UIHeight" +
            ",[6] WideR" +
            
            ",[7] speed" +
            ",[8]";

        static UnitData Creat(string s)
        {
            var re = new UnitData();
            re.OriginString = s;
            var ss = s.Split(new char[1] { ',' });
            try
            {

                re.FullName = ss[0];
                var names = ss[0].Split('\'');
                re.PackName = names[0];
                re.KindName = names[1];
                if(int.TryParse(ss[1],out var mana))
                {
                    re.Mana = mana;
                    //Debug.Log(   re.FullName+"  has mana"+re.Mana);
                }
                else 
                {
                    //Debug.Log(re.FullName + "  no mana" + re.Mana);
                }

                re.IsHero = string.IsNullOrWhiteSpace(ss[2])==false;

                if( int.TryParse(ss[3], out var hp))re.MaxHp=hp;
                if (int.TryParse(ss[4], out var atk)) re.atk = atk;

                if (float.TryParse(ss[5], out var UIHeight)) re.UIHeight = UIHeight;
                if (float.TryParse(ss[6], out var Wide)) re.WideR = Wide;

                if (int.TryParse(ss[7], out var speed)) re.speed = speed;

            }
            catch (Exception E)
            {
                Debug.Log(E.Message);
            }

            return re;

        }

    }




    public static class UnitDataExtra
    {
        public static string RemovePackName(this string full)
        {

            return full.Remove(0, full.IndexOf("'") + 1);
        }
        public static UnitData ToData(this string full)
        {
            return UnitData.GetData(full);
        }
    }


