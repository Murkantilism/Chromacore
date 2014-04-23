using UnityEngine;
using System.Collections;
using UnityEditor.Callbacks;
using UnityEditor;
using System.Diagnostics;

public class PostProcessScriptStarter : MonoBehaviour {
	[PostProcessBuild]
	public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {
#if UNITY_IOS
		string buildToolsDir = Application.dataPath + @"/Soomla/Editor/build-tools";
		
		Process proc = new System.Diagnostics.Process();
		proc.StartInfo.UseShellExecute = false;
		proc.StartInfo.CreateNoWindow = true;
		//		proc.StartInfo.RedirectStandardOutput = true;
		proc.StartInfo.RedirectStandardError = true;
		proc.EnableRaisingEvents=false; 
		proc.StartInfo.FileName = "chmod";
		proc.StartInfo.Arguments = "755 \"" + buildToolsDir + "/PostprocessBuildPlayerScriptForSoomla\"";
		proc.Start();
		//		string output = proc.StandardOutput.ReadToEnd();
		string err = proc.StandardError.ReadToEnd();
		proc.WaitForExit();

		proc = new System.Diagnostics.Process();
		proc.StartInfo.UseShellExecute = false;
		proc.StartInfo.CreateNoWindow = true;
//		proc.StartInfo.RedirectStandardOutput = true;
		proc.StartInfo.RedirectStandardError = true;
		proc.EnableRaisingEvents=false; 
		proc.StartInfo.FileName = buildToolsDir + "/PostprocessBuildPlayerScriptForSoomla";
		proc.StartInfo.Arguments = Application.dataPath.Replace(" ", "_SOOM@#") + " " + pathToBuiltProject.Replace(" ", "_SOOM@#");
		proc.Start();
//		string output = proc.StandardOutput.ReadToEnd();
		err = proc.StandardError.ReadToEnd();
		proc.WaitForExit();
//		UnityEngine.Debug.Log("out: " + output);
		if (proc.ExitCode != 0) {
			UnityEngine.Debug.Log("error: " + err + "   code: " + proc.ExitCode);
		}
#endif
    }
}
