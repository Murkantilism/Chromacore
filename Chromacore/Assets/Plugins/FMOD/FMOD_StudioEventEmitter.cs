using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using FMOD.Studio;

public class FMOD_StudioEventEmitter : MonoBehaviour 
{
	public FMODAsset asset;
	public string path = "";
	public bool startEventOnAwake = true;

	FMOD.Studio.EventInstance evt;
	private bool hasStarted = false;

	[System.Serializable]
	public class Parameter
	{
		public string name;
		public float value;
	}
	
	public void Play()
	{
		if (evt != null)
		{
			ERRCHECK(evt.start());
		}
		else
		{
			Debug.Log("Tried to play event without a valid instance: " + path);
			return;			
		}
	}
	
	public void Stop()
	{
		if (evt != null)
		{
			ERRCHECK(evt.stop());
		}		
	}	
	
	public FMOD.Studio.ParameterInstance getParameter(string name)
	{
		FMOD.Studio.ParameterInstance param = null;
		ERRCHECK(evt.getParameter(name, out param));
			
		return param;
	}

	public FMOD.Studio.PLAYBACK_STATE getPlaybackState()
	{
		if (evt == null || !evt.isValid())
			return FMOD.Studio.PLAYBACK_STATE.STOPPED;
		
		FMOD.Studio.PLAYBACK_STATE state = PLAYBACK_STATE.IDLE;
		
		if (ERRCHECK (evt.getPlaybackState(out state)) == FMOD.RESULT.OK)
			return state;
		
		return FMOD.Studio.PLAYBACK_STATE.STOPPED;
	}

	void Start() 
	{
		CacheEventInstance();
		
		if (startEventOnAwake)
			StartEvent();
	}
	
	void CacheEventInstance()
	{
		if (asset != null)
		{
			evt = FMOD_StudioSystem.instance.getEvent(asset.id);				
		}
		else if (!String.IsNullOrEmpty(path))
		{
			evt = FMOD_StudioSystem.instance.getEvent(path);
		}
		else
		{
			Debug.LogError("No asset or path specified for Event Emitter");
		}
	}

	static bool isShuttingDown = false;

	void OnApplicationQuit() 
	{
		isShuttingDown = true;
	}

	void OnDestroy() 
	{
		if (isShuttingDown)
			return;

//		Debug.Log ("Destroy called");
		if (evt != null && evt.isValid()) 
		{
			if (getPlaybackState () != FMOD.Studio.PLAYBACK_STATE.STOPPED)
			{
				Debug.Log ("Release evt: " + path);
				ERRCHECK (evt.stop(FMOD.Studio.STOP_MODE.IMMEDIATE));
			}
			
			ERRCHECK(evt.release ());
			evt = null;
		}

	}

	public void StartEvent()
	{		
		if (evt == null || !evt.isValid())
		{
			CacheEventInstance();
		}
		
		// Attempt to release as oneshot
		if (evt != null && evt.isValid())
		{
			ERRCHECK(evt.start());
			//if (evt.release() == FMOD.RESULT.OK) 
			{
				//evt = null;
			}
		}
		else
		{
			Debug.Log("Event retrival failed for event: " + path);
		}

		hasStarted = true;
	}

	public bool HasFinished()
	{
		if (!hasStarted)
			return false;
		if (evt == null || !evt.isValid())
			return true;
		
		return getPlaybackState () == FMOD.Studio.PLAYBACK_STATE.STOPPED;
	}

	void Update() 
	{
		if (evt != null && evt.isValid ()) 
		{
			var attributes = UnityUtil.to3DAttributes (gameObject);			
			ERRCHECK (evt.set3DAttributes(attributes));			
		} 
		else 
		{
			evt = null;
		}
	}
	
	FMOD.RESULT ERRCHECK(FMOD.RESULT result)
	{
		FMOD_StudioSystem.ERRCHECK(result);
		return result;
	}
}
