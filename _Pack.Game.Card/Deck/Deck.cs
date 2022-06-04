using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

namespace Pack
{

    public class Deck 
    {
        Deck(FileInfo info)
        {
            var nn = info.Name.Replace(".txt","").Split('.');

            if (nn.Length > 0) Name = nn[0];
            if(nn.Length>1)HeroFullName = nn[1];
            this.info = info;
        }
        public FileInfo info { get; private set; }

        static string DirBeging => nameof(Deck);
        static List<Deck> datas = new List<Deck>();

        public static List<Deck> GetDeckList()
        {
            datas.Clear();
            DirBeging.ToFileinfos().ForeachFileInfos(x=> { datas.Add(new Deck(x)); });
            return datas;
        }
        public string Name { get; private set; }
        string HeroFullName=null;
        public string GetHeroName() { return HeroFullName; }



        List<OneKindUnit> units = new List<OneKindUnit>();
        Dictionary<string, OneKindUnit> dic = new Dictionary<string, OneKindUnit>();
        public void SetHeroName(string value) { HeroFullName = value; }
        public List<OneKindUnit> GetUnits() { EnsureLoad(); return units; }
 
    
        //load
        bool loaded = false;
        public void EnsureLoad()
        {
            if (loaded) return;
            loaded = true;
            if (info == null) return;
            //Debug.Log("loadDeck  " + info.FullName);
            info.ForEachLines(OnReadLine);
        }
        void OnReadLine(string line)
        {
            if (line.StartsWith("hero:")) { HeroFullName = line.Replace("hero:", "");return; }
            foreach (var item in line.Split_(StringSplitOptions.RemoveEmptyEntries, ',')) { }
        }
        //add
        public void AddUnit(string full)
        {
            if (dic.TryGetValue(full, out var to) == false)
            {
                to = new OneKindUnit() { fullName = full };
                units.Add(to); dic.Add(full, to);
            }
            to.count++;
        }
        //remove
        public void RemoveUnit(string full)
        {
            if (dic.TryGetValue(full, out var to) == false) return;
            to.count--;
            if (to.count <= 0) { dic.Remove(full); units.Remove(to); }
        }


        public void SaveData()
        {
            var fullname = (DirBeging + "/" + Name + "." + HeroFullName).ToFullFilePath();
        }
        void SaveData(StreamWriter r)
        {
            if (HeroFullName != null) r.WriteLine("hero:" + HeroFullName);
            foreach (var item in units)
            { for (int i = 0; i < item.count; i++) { r.Write(item.fullName + ","); } }
        }

        public struct OneKindUnit { public int count; public string fullName; }

    }

}
