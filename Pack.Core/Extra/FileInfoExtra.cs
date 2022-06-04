using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using UnityEngine;
namespace Pack
{
    public static class FileInfoExtra
    {
      public  static string ToStreamingAssetFullPath(this string BaseStr)
        {

            if (BaseStr.Contains("StreamingAssets") == false)
            {
                //Debug.Log("extend                " + BaseStr);
                BaseStr = Application.streamingAssetsPath + "/" + BaseStr;

            }
            return BaseStr;
        }
        public static string ToFullFilePath(this string BaseStr,string fileType=".txt")
        {
            var re = BaseStr.ToStreamingAssetFullPath();
            if (re.EndsWith(fileType) == false) re += fileType;
            return re;
        }

        public static DirectoryInfo GetDire(this string BaseStr) { return new DirectoryInfo(BaseStr.ToStreamingAssetFullPath()); }
        public static FileInfo ToFileInfo(this string BaseStr) { return new FileInfo(BaseStr.ToFullFilePath()); }
        //public functions
        static DirectoryInfo[] ZeroDIre = new DirectoryInfo[0];
        public static DirectoryInfo[] ToDirectoryInfos(this string name, SearchOption option = SearchOption.TopDirectoryOnly, string Forsearch = "*")
        {
            var dir = name.GetDire();
            if (dir.Exists == false) return ZeroDIre;
            var re = dir.GetDirectories(Forsearch, option);
            return re;
        }

        static FileInfo[] zero = new FileInfo[0];
        //获取文件夹下所有文件
        public static FileInfo[] ToFileinfos(this string name, string fosearch = "*.txt", SearchOption option = SearchOption.TopDirectoryOnly)
        {
            var dir = name.GetDire();
            if (dir.Exists == false) return zero;
            var re = dir.GetFiles(fosearch, option);
            return re;
        }
    }

}
