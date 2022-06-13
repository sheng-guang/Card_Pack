using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Unity.CodeEditor;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class OpenShaderInNotepad
{
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

		OpenVsCode(Application.dataPath + "/Assets");
		OpenVsCode(p);
        if (isJS_txt)
        {
            string to2 = p.Replace(".js.txt", ".ts");
            if (File.Exists(to2)) OpenVsCode(to2);
		}
    
        return true;
	}

	private const string VsCode = @"G:\Microsoft VS Code\Code.exe";

	public static void OpenVsCode(string filePath)
	{
		Open(VsCode, filePath);
	}

	public static void Open(string app, string args)
	{
		using (Process myProcess = new Process())
		{
			myProcess.StartInfo.UseShellExecute = true;
			myProcess.StartInfo.FileName = app;
			myProcess.StartInfo.Arguments = args;
			myProcess.StartInfo.CreateNoWindow = true;
			myProcess.Start();
		}
	}



}