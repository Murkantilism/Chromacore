using UnityEngine;
using UnityEditor;
using System.Collections;

public class BuildStreamedSceneAssetBundle : MonoBehaviour {

	public static bool pcP = false;
	public static bool androidP = false;
	public static bool iphoneP = false;

	[MenuItem("Build/BuildWebplayerStreamedLevels")]
	public static void MyBuild(){
		#if UNITY_STANDALONE
		pcP = true;
		androidP = false;
		iphoneP = false;
		#endif
		
		#if UNITY_IPHONE
		pcP = false;
		androidP = false;
		iphoneP = true;
		#endif
		
		#if UNITY_ANDROID
		pcP = false;
		androidP = true;
		iphoneP = false;
		#endif

		string[] levels  = {"Assets/Scenes/sceneLoader.unity", "Assets/Scenes/MainMenu.unity", "Assets/Scenes/LevelSelect.unity", "Assets/Scenes/Level1.unity",
			"Assets/Scenes/Level2.unity", "Assets/Scenes/Level3.unity", "Assets/Scenes/Level4.unity",
			"Assets/Scenes/Level5.unity", "Assets/Scenes/Level6.unity", "Assets/Scenes/Level7.unity",
			"Assets/Scenes/Level8.unity", "Assets/Scenes/Level9.unity", "Assets/Scenes/Level10.unity"};
		if (androidP == true){
		BuildPipeline.BuildStreamedSceneAssetBundle( levels,
		                                            "Levels-AssetBundle.unity3d", BuildTarget.Android);
		}

		if (iphoneP == true){
			BuildPipeline.BuildStreamedSceneAssetBundle( levels,
			                                            "Levels-AssetBundle.unity3d", BuildTarget.iPhone);
		}

		if (pcP == true){
			BuildPipeline.BuildStreamedSceneAssetBundle( levels,
			                                            "Levels-AssetBundle.unity3d", BuildTarget.WebPlayer);
		}
	}
}