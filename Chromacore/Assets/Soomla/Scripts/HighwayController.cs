using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Soomla
{
	public class HighwayController
	{
#if UNITY_IOS && !UNITY_EDITOR
		[DllImport ("__Internal")]
		private static extern int highwayController_initialize(string masterKey);
#endif

		public static void Initialize(string masterKey) {
			StoreController.SetupSoomSec();
			
#if UNITY_ANDROID && !UNITY_EDITOR
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaClass jniHighway = new AndroidJavaClass("com.soomla.unity.HighwayController")) {
				jniHighway.CallStatic("initialize", masterKey);
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS && !UNITY_EDITOR
			highwayController_initialize(masterKey);
#endif
		}
	}
}
