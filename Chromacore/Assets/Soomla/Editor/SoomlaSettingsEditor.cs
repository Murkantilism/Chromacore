using UnityEngine;
using UnityEditor;
using UnityEditor.SoomlaEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[CustomEditor(typeof(SoomSettings))]
public class SoomlaSettingsEditor : Editor
{
	
    bool showAndroidSettings = (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android);
    bool showIOSSettings = (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iPhone);

	GUILayoutOption fieldHeight = GUILayout.Height(16);
	GUILayoutOption fieldWidth = GUILayout.Width(120);

	GUIContent customSecLabel = new GUIContent("Custom Secret [?]:", "The application encryption secret.");
	GUIContent soomSecLabel = new GUIContent("SoomSec [?]:", "A global secret which is used as a higher level protection.");

	GUIContent publicKeyLabel = new GUIContent("Play API Key [?]:", "The API key from Google Play dev console.");
	GUIContent packageNameLabel = new GUIContent("Package Name [?]", "Your package as defined in Unity.");

	GUIContent iosSsvLabel = new GUIContent("Receipt Validation [?]:", "Check if you want your purchases validated with SOOMLA Server Side Protection Service.");

	GUIContent frameworkVersion = new GUIContent("Framework Version [?]", "The SOOMLA Framework version. ");
    GUIContent buildVersion = new GUIContent("Framework Build [?]", "The SOOMLA Framework build.");

	public void OnEnable() {
		// Generating AndroidManifest.xml
		ManifestTools.GenerateManifest();
	}

    public override void OnInspectorGUI()
    {
			SoomlaGUI();
			AndroidGUI();
			IOSGUI();
			AboutGUI();
    }

    private void SoomlaGUI()
    {
		EditorGUILayout.BeginHorizontal();
		string url = "file://" + Application.dataPath + @"/Soomla/Resources/soom_logo.png";
		WWW www = new WWW(url);
		while(!www.isDone){}
		GUIContent logoImgLabel = new GUIContent (www.texture);
		EditorGUILayout.LabelField(logoImgLabel, GUILayout.MaxHeight(70), GUILayout.ExpandWidth(true));
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.HelpBox("Make sure you fill out all the information below", MessageType.None);

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(customSecLabel, fieldWidth, fieldHeight);
		SoomSettings.CustomSecret = EditorGUILayout.TextField(SoomSettings.CustomSecret, fieldHeight);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(soomSecLabel, fieldWidth, fieldHeight);
		SoomSettings.SoomSecret = EditorGUILayout.TextField(SoomSettings.SoomSecret, fieldHeight);
		EditorGUILayout.EndHorizontal();


        EditorGUILayout.Space();
    }

    private void IOSGUI()
    {
        showIOSSettings = EditorGUILayout.Foldout(showIOSSettings, "iOS Build Settings");
        if (showIOSSettings)
        {
			SoomSettings.IosSSV = EditorGUILayout.Toggle(iosSsvLabel, SoomSettings.IosSSV);
        }
        EditorGUILayout.Space();
    }

    private void AndroidGUI()
    {
        showAndroidSettings = EditorGUILayout.Foldout(showAndroidSettings, "Android Settings");
        if (showAndroidSettings)
        {
            SelectableLabelField(packageNameLabel, PlayerSettings.bundleIdentifier);

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(publicKeyLabel, fieldWidth, fieldHeight);
			SoomSettings.AndroidPublicKey = EditorGUILayout.TextField(SoomSettings.AndroidPublicKey, fieldHeight);
			EditorGUILayout.EndHorizontal();

			if (!SoomlaAndroidUtil.IsSetupProperly())
			{
				var msg = "You have errors in your Android setup. More info in the SOOMLA docs.";
				switch (SoomlaAndroidUtil.SetupError)
				{
				case SoomlaAndroidUtil.ERROR_NO_SDK:
					msg = "You need to install the Android SDK!  Set the location of Android SDK in: " + (Application.platform == RuntimePlatform.OSXEditor ? "Unity" : "Edit") + "->Preferences->External Tools";
					break;
				case SoomlaAndroidUtil.ERROR_NO_KEYSTORE:
					msg = "Your defined keystore doesn't exist! You'll need to create a debug keystore or point to your keystore in 'Publishing Settings' from 'File -> Build Settings -> Player Settings...'";
					break;
				}
				
				EditorGUILayout.HelpBox(msg, MessageType.Error);
			}
		}
		EditorGUILayout.Space();
    }

    private void AboutGUI()
    {
        EditorGUILayout.HelpBox("SOOMLA SDK Info", MessageType.None);
		SelectableLabelField(frameworkVersion, "1.3.0");
		SelectableLabelField(buildVersion, "1");
        EditorGUILayout.Space();
    }

    private void SelectableLabelField(GUIContent label, string value)
    {
        EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(label, GUILayout.Width(140), fieldHeight);
        EditorGUILayout.SelectableLabel(value, fieldHeight);
        EditorGUILayout.EndHorizontal();
    }

}
