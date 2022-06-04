using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pack
{
    public static partial class ResExtra
    {
        public static string FullName(this IRes r)
        {
            return r.PackName + "'" + r.KindName;
        }
        public static T LastItem<T>(this T[] list)
        {
            return list[list.Length - 1];
        }

        //public static string[] ToPossArgs(this Vector3 poss)
        //{
        //    return new string[] { "PossX:"+poss.x,"PossY:"+poss.y,"PossZ:"+poss.z };
        //}
    }
    public static partial class ResExtra
    {
 
        //��ȡԤ����·��
        public static string GetAssetFolder(IRes res) { return "Assets/" + res.DirectoryName + "/" + res.FullName(); }
        public static string GetAssetPath(this IRes res)
        { return GetAssetFolder(res) + "/" + res.FullName() + ".prefab"; }
        public static string GetAsyncAssetPath(this AsyncResTool n, IRes res)
        { return GetAssetFolder(res) + "/" + n.AssetName + ".prefab"; }
        public static string GetABDir(this IRes res)
        {
            return res.DirectoryName + "/" + res.FullName() + "/";
        }
        //��ȡ����·��
        public static string GetABPath(this IRes res)
        {
            return res.DirectoryName + "/" + res.FullName()+"/" + res.FullName();
        }
        //������async ���Լ��»��� ʹ���첽����ab�� �����ļ��������  ����˲����ص�ab��
        public static string GetABAsycPath(this IRes res)
        {
            return res.DirectoryName + "/" + res.FullName() + "/" + res.FullName() + "_";
        }
        ////���ð���
        //public static void SetNameforABbuild(this AssetBundleBuild b, IRes res)
        //{
        //    b.assetBundleName = res.FullName();
        //}
        //public static void SetNameForAbBuildAsync(this AssetBundleBuild b, IRes res)
        //{
        //    b.assetBundleName = res.FullName();
        //    b.assetBundleVariant = "Async";
        //}
    }
}
