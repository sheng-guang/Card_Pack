using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Pack
{
    public class QucikAccessWindow : EditorWindow
    {

        [MenuItem("Tool window/Quick Access")]
        public static void Init()
        {
            Debug.Log("open QucikAccessWindow");
            QucikAccessWindow window = (QucikAccessWindow)GetWindow(typeof(QucikAccessWindow));
            window.Show();
        }
        private void OnEnable()
        {
            ins = ObjectList.EnsureObjectList();
            count = ins.ShowCount;

            Debug.Log("quick obj access enable :"+ins);

        }
        ObjectList ins;
        List<Object> list => ins.objs;
        private void OnGUI()
        {
            Time.timeScale = EditorGUILayout.Slider(Time.timeScale, 0, 1);
            bool save = false;
            count = EditorGUILayout.IntField(count);
            if (ins&&count != ins.ShowCount)
            {
                save = true;
                ins.ShowCount = count;
            }
            while (list.Count >= count == false)
            {
                list.Add(null);
                save = true;
            }
            for (int i = 0; i < count; i++)
            {
                var old = list[i];
                list[i] = EditorGUILayout.ObjectField(list[i], typeof(Object));
                if (old != list[i]) save = true;
            }
            if (save) Save();

        }
        int count = 15;
        [SerializeField]

        public string test = "2";
        public void Save()
        {
            Debug.Log("save ObjectList");
            ins.SaveSelf();
        }
    }






}
