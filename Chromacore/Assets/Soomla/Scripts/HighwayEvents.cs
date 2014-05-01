using System;
using UnityEngine;

namespace Soomla
{
	public class HighwayEvents : MonoBehaviour 
	{
        private const string TAG = "SOOMLA HighwayEvents";
        private static HighwayEvents instance = null;
        
        void Awake(){
            if(instance == null){     //making sure we only initialize one instance.
                instance = this;
                GameObject.DontDestroyOnLoad(this.gameObject);
            } else {                    //Destroying unused instances.
                GameObject.Destroy(this.gameObject);
            }
        }
        
        public void onHWMetadataChanged(string message) {
            StoreUtils.LogDebug(TAG, "SOOMLA/UNITY onHWMetadataChanged");

            HighwayEvents.OnHWMetadataChanged();
        }
        
        public void onHWBalancesUpdated(string message) {
            StoreUtils.LogDebug(TAG, "SOOMLA/UNITY onHWBalancesUpdated");

            HighwayEvents.OnHWBalancesUpdated();
        }

        public delegate void Action();

		public static Action OnHWMetadataChanged = delegate {};

		public static Action OnHWBalancesUpdated = delegate {};

	}
}
