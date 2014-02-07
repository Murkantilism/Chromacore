using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FMOD_StudioEventEmitter))]
public class FMODEventEmitterInspector : Editor
{
    FMOD_StudioEventEmitter emitter;
	
	void Awake()
	{
        emitter = (FMOD_StudioEventEmitter)target;
	}
	
	public override void OnInspectorGUI()
	{	
		if (emitter.asset != null)
		{
			emitter.path = emitter.asset.id; // Note: set path to guid just in case the asset gets deleted

			emitter.asset = (FMODAsset)EditorGUILayout.ObjectField(emitter.asset, typeof(FMODAsset), false);
			
			GUILayout.BeginHorizontal();
			{
				GUILayout.Label("Path:");
				EditorGUILayout.SelectableLabel(emitter.asset.path, GUILayout.Height(14));
			}
			GUILayout.EndHorizontal();	
			GUILayout.BeginHorizontal();	
			{
				GUILayout.Label("GUID:");
				EditorGUILayout.SelectableLabel(emitter.asset.id, GUILayout.Height(14));
			}
			GUILayout.EndHorizontal();
			emitter.startEventOnAwake = GUILayout.Toggle(emitter.startEventOnAwake, "Start Event on Awake");
		}
		else
		{
			DrawDefaultInspector();
		}
	}
}
