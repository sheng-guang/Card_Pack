using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using UnityEngine;

    public static class StreamingAssetsTXTHub
    {
        //dele
        public static bool DeleTXT(this string UserDefinePath)
        {
            var f = UserDefinePath.ToFileInfo();
            if (f.Exists == false) return false;
            f.Delete();
            return true;
        }
        //add
        public static FileInfo EnsureTXT(this string UserDefinePath)
        {
            FileInfo to = UserDefinePath.ToFileInfo();
            //int i = 0;
            //while (to.Exists)
            //{
            //    to = (UserDefinePath + i).ToFileInfo();
            //    i++;
            //}
            using (var w = File.Create(to.FullName))
            {
            }
            return to;
        }

        //foreach
        //file info
        public static void ForeachFileInfos(this FileInfo[] infos, Action<FileInfo> ForEach)
        {
            foreach (var item in infos) ForEach(item);
        }
        //
        public static void ForEachLines(this FileInfo info, Action<string> Act)
        {
            try
            {
                using (StreamReader sr = new StreamReader(info.FullName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null) Act(line);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.StackTrace);
            }
        }
        public static void ForEachLines(this FileInfo[] infos, Action<string> Act)
        {
            foreach (var item in infos)
            {
                ForEachLines(item, Act);
            }
         
        }
        
        public static void WriteEachLine<T>(this string path, IEnumerable<T> list, Func<T, string> func=null , string fileType = ".txt", string titleInfo="")
        {
            if (func == null) func = x => x.ToString();
            path = path.ToFullFilePath(fileType);

            using (StreamWriter sw = new StreamWriter(path, false))
            {

               if(string.IsNullOrWhiteSpace( titleInfo)==false) sw.WriteLine(titleInfo);
                foreach (var item in list)
                {
                    sw.WriteLine(func(item));
                }
            }
        }

 





    }







    public static class StringHelpre
    {
        static char[] sp = new char[1];
        public static string[] Split_(this string str, StringSplitOptions option, params char[] Separator)
        {
            var re = str.Split(Separator, option);
            return re;
        }
        public static string[] Split_(this string str, params char[] Separator)
        {
            var re = str.Split(Separator, StringSplitOptions.None);
            return re;
        }
        public static string[] Split_(this string str)
        {
            var re = str.Split(',');
            return re;
        }
    }

