#define RUN_IN_BACKGROUND // required for LiveUpdate

using UnityEngine;
using System.Collections;
using FMOD.Studio;
using System.IO;
using System;

public class FMOD_Listener : MonoBehaviour 
{
	public string[] pluginPaths;
	
	static FMOD_Listener sListener = null;
	
	void OnEnable()
	{		
		Initialize();
	}
	
	void loadBank(string fileName)
	{
		string bankPath = getStreamingAsset(fileName);
		
		FMOD.Studio.Bank bank = null;
		ERRCHECK(FMOD_StudioSystem.instance.System.loadBankFile(bankPath, out bank));
		
		Debug.Log("bank load: " + (bank != null ? "suceeded" : "failed!!"));
	}
	
	string getStreamingAsset(string fileName)
	{		
#if UNITY_IPHONE && !UNITY_EDITOR
		string bankPath = Application.dataPath + "/Raw";
#elif UNITY_STANDALONE_OSX && !UNITY_EDITOR
		string bankPath = Application.dataPath + "/Data/StreamingAssets";
#elif UNITY_ANDROID && !UNITY_EDITOR
		string bankPath = "jar:file://" + Application.dataPath + "!/assets";
#else
	    string bankPath = Application.dataPath + "/StreamingAssets";
#endif
		
		string assetPath = bankPath + "/" + fileName;
		
#if UNITY_ANDROID && !UNITY_EDITOR
		// Unpack the compressed JAR file
		string unpackedJarPath = Application.persistentDataPath + "/" + fileName;
		
		//Debug.Log("Unpacking bank from JAR file into:" + unpackedJarPath);
		
		if (File.Exists(unpackedJarPath))
		{
			Debug.Log("File already unpacked!!");
			File.Delete(unpackedJarPath);
			
			if (File.Exists(unpackedJarPath))
			{
				Debug.Log("Could NOT delete!!");				
			}
		}
		
		WWW dataStream = new WWW(assetPath);
		
		while(!dataStream.isDone) {} // FIXME: not safe
		
		
		if (!String.IsNullOrEmpty(dataStream.error))
		{
	        Debug.Log("### WWW ERROR IN DATA STREAM:" + dataStream.error);
		}
		
		//Debug.Log("Android unpacked jar path: " + unpackedJarPath);
		
		File.WriteAllBytes(unpackedJarPath, dataStream.bytes);
		
		//FileInfo fi = new FileInfo(unpackedJarPath);
		//Debug.Log("Unpacked bank size = " + fi.Length);
		
		assetPath = unpackedJarPath;
#endif

		return assetPath;
	}
	
	void Initialize()
	{
		//Debug.Log("Initialize Listener: BEGIN");

		if (sListener != null)
		{
			Debug.LogError("Too many listeners");
		}
		
		sListener = this;
		
		LoadPlugins();
		
		string bankListPath = getStreamingAsset("FMOD_bank_list.txt");		
		
		var bankList = System.IO.File.ReadAllLines(bankListPath);
		foreach (var bankName in bankList)
		{
			loadBank(bankName);
		}
	}
	
	void Start()
	{
#if UNITY_EDITOR && RUN_IN_BACKGROUND
		Application.runInBackground = true; // Prevent execution pausing when editor loses focus
#endif
	}
	
	void Update()
	{
		var attributes = UnityUtil.to3DAttributes(gameObject);
		
		FMOD_StudioSystem.instance.System.setListenerAttributes(attributes);
	}
	
	void ERRCHECK(FMOD.RESULT result)
	{
		FMOD_StudioSystem.ERRCHECK(result);
	}
	
	void LoadPlugins()
	{
		FMOD.System sys = null;
		FMOD_StudioSystem.ERRCHECK(FMOD_StudioSystem.instance.System.getLowLevelSystem(out sys));
		
		var dir = pluginPath;
		foreach (var name in pluginPaths)
		{
			var path = dir + "/" + GetPluginFileName(name);
			
			//Debug.Log("Loading plugin: " + path);
			if (!System.IO.File.Exists(path))
            {
                Debug.LogWarning("plugin not found: " + path);
            }
			
			uint handle = 0;
			FMOD_StudioSystem.ERRCHECK(sys.loadPlugin(path, ref handle));
		}
	}	
	
	string pluginPath
	{
		get
		{
			if (Application.platform == RuntimePlatform.WindowsEditor)
			{
				return Application.dataPath + "/Plugins/x86";
			}
			else if (Application.platform == RuntimePlatform.OSXEditor)
			{
				return Application.dataPath + "/Plugins";
			}
			else if (Application.platform == RuntimePlatform.WindowsPlayer)
			{
				return Application.dataPath + "/Plugins";
			}
			else if (Application.platform == RuntimePlatform.Android)
			{
				var dirInfo = new System.IO.DirectoryInfo(Application.persistentDataPath);
				string packageName = dirInfo.Parent.Name;
				return "/data/data/" + packageName + "/lib";
			}
			
			Debug.LogError("Unknown platform!");
			return "";
		}
	}
	
	string GetPluginFileName(string rawName)
	{
		if (Application.platform == RuntimePlatform.WindowsEditor)
		{
			return rawName + ".dll";
		}
		else if (Application.platform == RuntimePlatform.OSXEditor)
		{
			return rawName + ".dylib";
		}
		else if (Application.platform == RuntimePlatform.WindowsPlayer)
		{
			return rawName + ".dll";
		}
		else if (Application.platform == RuntimePlatform.Android)
		{
			return "lib" + rawName + ".so";
		}
		
		Debug.LogError("Unknown platform!");
		return "";		
	}
}
