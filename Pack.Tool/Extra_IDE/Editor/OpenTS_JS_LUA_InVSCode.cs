using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class OpenShaderInNotepad
{
	private const string VsCode = @"G:\Microsoft VS Code\Code.exe";
	static Process process;
	[OnOpenAsset(0)]
	public static bool OpenInNotepad(int instanceID, int line, int column)
	{
		string p = Path.GetFullPath(AssetDatabase.GetAssetPath(instanceID));
		bool UseVSCode = false;
		bool isJS_txt = false;
		if (p.EndsWith(".ts")) UseVSCode = true;
		if (p.EndsWith(".ts.txt")) { UseVSCode = true; }
		if (p.EndsWith(".js")) UseVSCode = true;
		if (p.EndsWith(".js.txt")) { UseVSCode = true; isJS_txt = true; }
		if (p.EndsWith(".lua")) UseVSCode = true;
		if (p.EndsWith(".lua.txt")) UseVSCode = true;
		if (p.EndsWith(".csv")) UseVSCode = true;
		if (UseVSCode == false) return false;

		if (process == null)
		{
			UnityEngine.Debug.Log("VSC null");
			process = Process.Start(VsCode);
			Thread.Sleep(1000);
			Process.Start(VsCode, Application.dataPath + "/Assets");
			Thread.Sleep(1000);

		}

		if (process == null || process.HasExited)
		{
			UnityEngine.Debug.Log("VSC closed open new");
			process = Process.Start(VsCode);
			Thread.Sleep(1000);
			Process.Start(VsCode, Application.dataPath + "/Assets");
			Thread.Sleep(1000);

		}
		Process.Start(VsCode, p);
		if (isJS_txt)
		{
			string to2 = p.Replace(".js.txt", ".ts");
			if (File.Exists(to2)) Process.Start(VsCode, to2);
		}
		return true;
	}

}