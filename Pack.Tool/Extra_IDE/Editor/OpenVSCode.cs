//using System.Diagnostics;
//using System.IO;
//using UnityEditor;
//using UnityEditor.Callbacks;
//using UnityEngine;

//[InitializeOnLoad]
//class OpenVS
//{
//	private const string VsCode = @"G:\Microsoft VS Code\Code.exe";
//	static OpenVS()
//	{
//		Call();
		
//	}
//	static void Call()
//    {
//		if (SessionState.GetBool(VsCode, false)) return;
//		SessionState.SetBool(VsCode, true);
//		UnityEngine.Debug.Log("Open: "+VsCode);
//        Process.Start(VsCode, Application.dataPath + "/Assets");

//    }
//}