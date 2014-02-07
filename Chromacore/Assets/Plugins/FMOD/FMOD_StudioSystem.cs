//#define FMOD_CONNECT_LIVEUPDATE

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FMOD
{
	namespace Studio
	{
		public static class UnityUtil
		{	
			static public VECTOR toFMODVector(this Vector3 vec)
			{
				VECTOR temp;
				temp.x = vec.x;
				temp.y = vec.y;
				temp.z = vec.z;
				
				return temp;
			}
			
			static public _3D_ATTRIBUTES to3DAttributes(this Vector3 pos)
			{
				FMOD.Studio._3D_ATTRIBUTES attributes = new FMOD.Studio._3D_ATTRIBUTES();
				attributes.forward = toFMODVector(Vector3.forward);
				attributes.up = toFMODVector(Vector3.up);
				attributes.position = toFMODVector(pos);
				
				return attributes;
			}
			
			static public _3D_ATTRIBUTES to3DAttributes(this GameObject go)
			{
				FMOD.Studio._3D_ATTRIBUTES attributes = new FMOD.Studio._3D_ATTRIBUTES();
				attributes.forward = toFMODVector(go.transform.forward);
				attributes.up = toFMODVector(go.transform.up);
				attributes.position = toFMODVector(go.transform.position);
		
				if (go.rigidbody)
					attributes.velocity = toFMODVector(go.rigidbody.velocity);
				
				return attributes;
			}
		}
	}
}

public class FMOD_StudioSystem : MonoBehaviour 
{
	FMOD.Studio.System system;
	Dictionary<string, FMOD.Studio.EventDescription> eventDescriptions = new Dictionary<string, FMOD.Studio.EventDescription>();
	bool isInitialized = false;
	
	static FMOD_StudioSystem sInstance;
	public static FMOD_StudioSystem instance
	{
		get
		{
			if (sInstance == null)
			{
				var go = new GameObject("FMOD_StudioSystem");
				sInstance = go.AddComponent<FMOD_StudioSystem>();
				
				sInstance.loadLowLevelBinary(); // do these hacks before calling ANY fmod functions!
				sInstance.Init();
			}
			return sInstance;
		}
	}
	
	public void noop() {}
	
	public FMOD.Studio.EventInstance getEvent(string path)
	{
		FMOD.Studio.EventInstance instance = null;
		
		if (string.IsNullOrEmpty(path))
		{
			Debug.LogError("Empty event path!");
			return null;
		}
		
		if (eventDescriptions.ContainsKey(path))
		{
			ERRCHECK(eventDescriptions[path].createInstance(out instance));
		}
		else
		{
			FMOD.GUID id = new FMOD.GUID();
			
			if (path.StartsWith("{"))
			{
				ERRCHECK(FMOD.Studio.Util.ParseID(path, out id));
			}
			else if (path.StartsWith("/"))
			{
				ERRCHECK(system.lookupEventID(path, out id));
			}
			else
			{
				Debug.LogError("Expected event path to start with '/'");
			}
			
			FMOD.Studio.EventDescription desc = null;
			ERRCHECK(system.getEvent(id, FMOD.Studio.LOADING_MODE.BEGIN_NOW, out desc));
			
			eventDescriptions.Add(path, desc);
			ERRCHECK(desc.createInstance(out instance));
		}
		
//		Debug.Log("get event: " + (instance != null ? "suceeded!!" : "failed!!")); //PAS
		
		return instance;
	}
	
	
	public void PlayOneShot(string path, Vector3 position)
	{
		PlayOneShot(path, position, 1.0f);
	}
	
	public void PlayOneShot(string path, Vector3 position, float volume)
	{
		var instance = getEvent(path);
		
		var attributes = FMOD.Studio.UnityUtil.to3DAttributes(position);
		ERRCHECK( instance.set3DAttributes(attributes) );
		//TODO ERRCHECK( instance.setVolume(volume) );
		ERRCHECK( instance.start() );
		ERRCHECK( instance.release() );
	}
	
	public FMOD.Studio.System System
	{
		get { return system; }
	}
	
	void loadLowLevelBinary()
	{
		// This is a hack that forces Android to load the .so libraries in the correct order
#if UNITY_ANDROID && !UNITY_EDITOR
		Debug.Log("loading binaries: " + FMOD.Studio.STUDIO_VERSION.dll + " and " + FMOD.VERSION.dll);
		AndroidJavaClass jSystem = new AndroidJavaClass("java.lang.System");
		jSystem.CallStatic("loadLibrary", FMOD.VERSION.dll);
		jSystem.CallStatic("loadLibrary", FMOD.Studio.STUDIO_VERSION.dll);
#endif
		
		// This is a hack that forces Unity to load the lowlevel dylib (required for mac)
		int temp1 = 0, temp2 = 0;
#if !UNITY_IPHONE || UNITY_EDITOR
		//Debug.Log("calling memory getStats");
		FMOD.Memory.GetStats(ref temp1, ref temp2);
#endif
	}
	
	void Init() 
	{
		Debug.Log("FMOD_StudioSystem: Initialize");
		
		if (isInitialized)
		{
			return;
		}
		
		Debug.Log("FMOD_StudioSystem: System_Create");
        ERRCHECK(FMOD.Studio.Factory.System_Create(out system));
		
		FMOD.Studio.INITFLAGS flags = FMOD.Studio.INITFLAGS.NORMAL;
		
#if FMOD_CONNECT_LIVEUPDATE
		flags |= FMOD.Studio.INITFLAGS.LIVEUPDATE;
#endif
		
		Debug.Log("FMOD_StudioSystem: system.init");
		FMOD.RESULT result = FMOD.RESULT.OK;
        result = system.init(1024, flags, FMOD.INITFLAGS.NORMAL, (System.IntPtr)null);
		
#if FMOD_CONNECT_LIVEUPDATE
		if (result == FMOD.RESULT.ERR_NET_SOCKET_ERROR)
		{
			Debug.LogWarning("LiveUpdate disabled: socket in already in use");
			flags &= ~FMOD.Studio.INITFLAGS.LIVEUPDATE;
        	result = system.init(64, flags, FMOD.INITFLAGS.NORMAL, (System.IntPtr)null);			
		}
#endif		
		ERRCHECK(result);
		
		isInitialized = true;
	}
	
	void OnApplicationPause(bool pauseStatus) 
	{
		// TODO: pause master channelgroup
    }
	
	void Update() 
	{
		if (isInitialized)
			ERRCHECK(system.update());
	}
	
	void OnDisable()
	{
		if (isInitialized)
			ERRCHECK(system.release());
	}
	
	public static void ERRCHECK(FMOD.RESULT result)
	{
		if (result != FMOD.RESULT.OK)
		{
			Debug.LogError("FMOD Error (" + result.ToString() + "): " + FMOD.Error.String(result));
		}
	}
}
