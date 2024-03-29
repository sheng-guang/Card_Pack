﻿#define MACRO_CHINAR
using System.Collections.Generic;
using UnityEngine;





    /// <summary>
    /// Chinar可视控制台
    /// </summary>
    class ChinarViewConsole : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod]
        static void CreatOne()
        {
            Debug.Log("[Load]" + "GUI Log");
            var log = new GameObject();
            log.name = "[LogGUI]";
            var ins= log.AddComponent<ChinarViewConsole>();
            DontDestroyOnLoad(log);
            //ins.OnEnable();
        }
#if MACRO_CHINAR
        struct Log
        {
            public string Message;
            public string StackTrace;
            public LogType LogType;
        }


        #region Inspector 面板属性

        [Tooltip("快捷键-开/关控制台")] public KeyCode ShortcutKey = KeyCode.Q;
        public KeyCode ShortcutKey2 = KeyCode.W;
        public KeyCode ShortcutKey3 = KeyCode.E;
        public KeyCode ShortcutKey4 = KeyCode.R;
        [Tooltip("摇动开启控制台？")] public bool ShakeToOpen = true;
        [Tooltip("窗口打开加速度")] public float shakeAcceleration = 3f;
        [Tooltip("是否保持一定数量的日志")] public bool restrictLogCount = false;
        [Tooltip("最大日志数")] public int maxLogs = 1000;

        #endregion

        private readonly List<Log> logs = new List<Log>();
        private Log log;
        private Vector2 scrollPosition;
        private bool visible;
        public bool collapse;

        static readonly Dictionary<LogType, Color> logTypeColors = new Dictionary<LogType, Color>
        {
            {LogType.Assert, Color.white},
            {LogType.Error, Color.red},
            {LogType.Exception, Color.red},
            {LogType.Log, Color.white},
            {LogType.Warning, Color.yellow},
        };

        private const string ChinarWindowTitle = "Chinar-控制台";
        private const int Edge = 20;
        readonly GUIContent clearLabel = new GUIContent("清空", "清空控制台内容");
        readonly GUIContent hiddenLabel = new GUIContent("合并信息", "隐藏重复信息");

        readonly Rect titleBarRect = new Rect(0, 0, 10000, 20);



        void OnEnable()
        {
#if UNITY_4
            Application.RegisterLogCallback(HandleLog);
#else
            //Application.logMessageReceived -= HandleLog;
            Application.logMessageReceived += HandleLog;
#endif
        }


        void OnDisable()
        {
#if UNITY_4
            Application.RegisterLogCallback(null);
#else
            Application.logMessageReceived -= HandleLog;
#endif
        }


        void Update()
        {
            if (
                Input.GetKey(ShortcutKey)
                && Input.GetKey(ShortcutKey2)
                && Input.GetKey(ShortcutKey3)
                && Input.GetKeyDown(ShortcutKey4)
                ) visible = !visible;
            if (ShakeToOpen && Input.acceleration.sqrMagnitude > shakeAcceleration) visible = true;
        }


        void OnGUI()
        {
            if (!visible) return;
            Rect windowRect = new Rect(Edge, Edge, Screen.width - (Edge * 2), Screen.height - (Edge * 2));
            windowRect = GUILayout.Window(666, windowRect, DrawConsoleWindow, ChinarWindowTitle);

        }


        void DrawConsoleWindow(int windowid)
        {
            DrawLogsList();
            DrawToolbar();
            GUI.DragWindow(titleBarRect);
        }


        GUIStyle s = new GUIStyle();
        void DrawLogsList()
        {
            s.fontSize = 30;
            s.fixedHeight = s.fontSize * 1f;
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            for (var i = 0; i < logs.Count; i++)
            {
                if (collapse && i > 0) if (logs[i].Message == logs[i - 1].Message) continue;
                //GUI.contentColor = logTypeColors[logs[i].LogType];
                s.normal.textColor = logTypeColors[logs[i].LogType];

                GUILayout.Label("---------------------------------------------------------------------------\\", s);
                GUILayout.Label(logs[i].Message, s);
                
                if (logs[i].LogType == LogType.Error || logs[i].LogType == LogType.Exception)
                {

                    var ss = logs[i].StackTrace.Split('\n');
                    for (int j = 0; j < ss.Length; j++)
                    {
                        GUILayout.Label(ss[j], s);
                    }
                  
                }

                GUILayout.Label("---------------------------------------------------------------------------/", s);

                GUILayout.Label(" ", s);
                GUILayout.Label(" ", s);
            }
            GUILayout.EndScrollView();
            //GUI.contentColor = Color.white;
        }


        void DrawToolbar()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(clearLabel))
            {
                logs.Clear();
            }

            collapse = GUILayout.Toggle(collapse, hiddenLabel, GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();
        }


        void HandleLog(string message, string stackTrace, LogType type)
        {
            logs.Add(new Log
            {
                Message = message,
                StackTrace = stackTrace,
                LogType = type,
            });
            DeleteExcessLogs();
        }


        void DeleteExcessLogs()
        {
            if (!restrictLogCount) return;
            var amountToRemove = Mathf.Max(logs.Count - maxLogs, 0);
            print(amountToRemove);
            if (amountToRemove == 0)
            {
                return;
            }

            logs.RemoveRange(0, amountToRemove);
        }
#endif
    }
