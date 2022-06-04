using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
namespace Pack
{

    [CreateAssetMenu(fileName = "ObjectList", menuName = "Creat ObjectList Asset")]
    public class ObjectList : ScriptableObject
    {
        [SerializeField]
        public int ShowCount = 15;
        [SerializeField]
        public List<Object> objs = new List<Object>();
        public static ObjectList EnsureObjectList()
        {
            string p = "Assets/Resources_/ObjectList.asset";
            var list = AssetDatabase.LoadAssetAtPath(p, typeof(ObjectList));
            if (list == null)
            {
                list = CreateInstance<ObjectList>();
                if (AssetDatabase.IsValidFolder("Assets/Resources_") == false) AssetDatabase.CreateFolder("Assets", "Resources_");
                AssetDatabase.CreateAsset(list, p);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                list = AssetDatabase.LoadAssetAtPath(p, typeof(ScriptableObject));
            }
            return list as ObjectList;
        }
        public void SaveSelf()
        {
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
#endif
        }

    }
}