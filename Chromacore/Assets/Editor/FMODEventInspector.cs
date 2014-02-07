using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FMODAsset))]
public class FMODEventInspector : Editor
{
	FMODAsset currentAsset; //Make an easy shortcut to the Dialogue your editing
	bool isPlaying = false;
	struct Param
	{
		public FMOD.Studio.PARAMETER_DESCRIPTION desc;
		public float val;
	}
	
	Param[] parameters = new Param[0];
	
	void Awake()
	{
		currentAsset=(FMODAsset)target;
		FMODEditorExtension.StopEvent();
		isPlaying = false;
		
		// set up parameters
		FMOD.Studio.EventDescription desc = FMODEditorExtension.GetEventDescription(currentAsset.id);
		int count;
		desc.getParameterCount(out count);
		parameters = new Param[count];
		
		for (int i = 0; i < count; ++i)
		{
			desc.getParameterByIndex(i, out parameters[i].desc);			
			parameters[i].val = parameters[i].desc.minimum;			
		}
	}
	
	void OnDestroy()
	{
		FMODEditorExtension.StopEvent();		
	}
	
	public override void OnInspectorGUI()
	{
		//GUILayout.Label("Event: " + currentAsset.name);
		GUILayout.Label("Path: " + currentAsset.path);
		GUILayout.Label("GUID: " + currentAsset.id);
		
		GUILayout.BeginHorizontal();
		if (!isPlaying && GUILayout.Button("Play", new GUILayoutOption[0]))
		{
			FMODEditorExtension.AuditionEvent(currentAsset.id);
			isPlaying = true;
		}
		if (isPlaying && GUILayout.Button("Stop", new GUILayoutOption[0]))
		{
			FMODEditorExtension.StopEvent();
			isPlaying = false;
		}
		GUILayout.EndHorizontal();		
		
		for (int i = 0; i < parameters.Length; ++i)
		{			
			GUILayout.BeginHorizontal();	
			GUILayout.Label(parameters[i].desc.name);
			parameters[i].val = GUILayout.HorizontalSlider(parameters[i].val, parameters[i].desc.minimum, parameters[i].desc.maximum, new GUILayoutOption[0]);
			FMODEditorExtension.SetEventParameterValue(i, parameters[i].val);
			GUILayout.EndHorizontal();
		}
	}
}
