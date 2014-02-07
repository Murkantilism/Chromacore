//#define DISABLE_LOW_LEVEL

/* ========================================================================================== */
/* FMOD System - C# Wrapper . Copyright (c), Firelight Technologies Pty, Ltd. 2004-2012.       */
/*                                                                                            */
/*                                                                                            */
/* ========================================================================================== */

using System;
using System.Text;
using System.Runtime.InteropServices;

namespace FMOD
{
namespace Studio
{		
    public class STUDIO_VERSION
    {
#if UNITY_IPHONE && !UNITY_EDITOR
        public const string dll    = "__Internal";
#else
        public const string dll    = "fmodstudio";
#endif
    }
		
	public enum LOADING_MODE
	{
	    BEGIN_NOW,
	    PROHIBITED
	}
		
	public enum STOP_MODE
	{
	    ALLOWFADEOUT,
	    IMMEDIATE
	}

    public enum LOADING_STATE
    {
        UNLOADING,
        UNLOADED,
        LOADING,
        LOADED
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct _3D_ATTRIBUTES
    {
        public VECTOR position;
        public VECTOR velocity;
        public VECTOR forward;
        public VECTOR up;
    }
    
    [StructLayout(LayoutKind.Sequential)]
    public struct PROGRAMMER_SOUND_PROPERTIES
    {
        public IntPtr namePtr;                    
        public IntPtr eventInstance;
        public IntPtr sound;

        public string name { get { return Marshal.PtrToStringAnsi(namePtr); } }
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PARAMETER_DESCRIPTION
    {
        public IntPtr namePtr;
        public float minimum;
        public float maximum;

        public string name { get { return Marshal.PtrToStringAnsi(namePtr); } }
    };

	 
	[Flags]
	public enum INITFLAGS
	{
		NORMAL,
		LIVEUPDATE,
		ALLOW_MISSING_PLUGINS
	}

	public enum PLAYBACK_STATE
	{
		PLAYING,
		IDLE,
		SUSTAINING,
		STOPPED
	}

    public enum EVENT_CALLBACK_TYPE
    {
        STARTED,                     // Called when an instance starts. Parameters = FMOD_STUDIO_EVENT_INSTANCE_HANDLE.
        STOPPED,                     // Called when an instance stops. Parameters = FMOD_STUDIO_EVENT_INSTANCE_HANDLE.
        STOLEN,                      // Called when an instance is stopped by "steal oldest" or "steal quietest" behaviour. Parameters = FMOD_STUDIO_EVENT_INSTANCE_HANDLE.
        CREATE_PROGRAMMER_SOUND,     // Called when a programmer sound needs to be created in order to play a programmer instrument. Parameters = FMOD_STUDIO_PROGRAMMER_SOUND_PROPERTIES.
        DESTROY_PROGRAMMER_SOUND     // Called when a programmer sound needs to be destroyed. Parameters = FMOD_STUDIO_PROGRAMMER_SOUND_PROPERTIES.
    }

	public enum LOAD_MEMORY_MODE
	{
	    MEMORY,                    			// When passed to Studio::System::loadBankMemory, FMOD duplicates the memory into its own buffers. Your buffer can be freed after Studio::System::loadBankMemory returns.
	    MEMORY_POINT               			// This differs from FMOD_STUDIO_LOAD_MEMORY in that FMOD uses the memory as is, without duplicating the memory into its own buffers. Cannot not be freed after load, only after calling Studio::Bank::unload.
	}
	
	public class Factory
    {
        public static RESULT System_Create(out System studiosystem)
        {
            RESULT result = RESULT.OK;
            IntPtr systemraw = new IntPtr();
			studiosystem = null;

            result = FMOD_Studio_System_Create(out systemraw, FMOD.VERSION.number);
            if (result != RESULT.OK)
            {
                return result;
            }

            studiosystem = new System();
            studiosystem.setRaw(systemraw);

            return result;
        }

        #region importfunctions

        [DllImport (STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_System_Create                      (out IntPtr studiosystem, uint headerVersion);
			
        #endregion
	}
	
	public class Util
	{
		public static RESULT ParseID(string idString, out GUID id)
		{
            return FMOD_Studio_ParseID(Encoding.UTF8.GetBytes(idString + Char.MinValue), out id);
		}

        #region importfunctions

        [DllImport (STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_ParseID                      (byte[] idString, out GUID id);

        #endregion
	}
		
	public class HandleBase
	{
		public bool isValid()
		{
			return rawPtr != IntPtr.Zero;
		}

		public void setRaw(IntPtr raw)
		{
			onSetRaw(raw);
			rawPtr = raw;
		}

		protected void invalidateHandle()
		{
			setRaw(IntPtr.Zero);
		}
			
		protected IntPtr rawPtr;
		protected virtual void onSetRaw(IntPtr newRaw) {}
	}
	
    public class System : HandleBase
    {
        // Initialization / system functions.
        public RESULT init                      (int maxchannels, INITFLAGS studioFlags, FMOD.INITFLAGS flags, IntPtr extradriverdata)
        {
            return FMOD_Studio_System_Initialize(rawPtr, maxchannels, studioFlags, flags, extradriverdata);
        }
        public RESULT release                   ()
        {
			IntPtr oldRaw = rawPtr;
			invalidateHandle();
			return FMOD_Studio_System_Release(oldRaw);
        }
        public RESULT update                    ()
        {
            return FMOD_Studio_System_Update(rawPtr);
        }
#if !DISABLE_LOW_LEVEL
        public RESULT getLowLevelSystem(out FMOD.System system)
        {
            RESULT result = RESULT.OK;
            IntPtr systemraw = new IntPtr();
			system = null;

            try
            {
                result = FMOD_Studio_System_GetLowLevelSystem(rawPtr, out systemraw);
            }
            catch
            {
                result = RESULT.ERR_INVALID_PARAM;
            }
            if (result != RESULT.OK)
            {
                return result;
            }

            system = new FMOD.System();
            system.setRaw(systemraw);

            return result;
        }
#endif
        public RESULT getEvent            (GUID guid, LOADING_MODE mode, out EventDescription _event)
        {
            RESULT result   = RESULT.OK;
            IntPtr eventraw = new IntPtr();
            _event = null;

            try
            {
                result = FMOD_Studio_System_GetEvent(rawPtr, ref guid, mode, out eventraw);
            }
            catch
            {
                result = RESULT.ERR_INVALID_PARAM;
            }
            if (result != RESULT.OK)
            {
                return result;
            }

            _event = new EventDescription();
            _event.setRaw(eventraw);

            return result;
        }
        public RESULT getMixerStrip       (GUID guid, LOADING_MODE mode, out MixerStrip strip)
        {
            RESULT result   = RESULT.OK;
            IntPtr mixerstripraw = new IntPtr();
            strip = null;

            try
            {
                result = FMOD_Studio_System_GetMixerStrip(rawPtr, ref guid, mode, out mixerstripraw);
            }
            catch
            {
                result = RESULT.ERR_INVALID_PARAM;
            }
            if (result != RESULT.OK)
            {
                return result;
            }

            strip = new MixerStrip();
            strip.setRaw(mixerstripraw);

            return result;
        }
        public RESULT lookupEventID(string path, out GUID guid)
        {
            return FMOD_Studio_System_LookupEventID(rawPtr, Encoding.UTF8.GetBytes(path + Char.MinValue), out guid);
        }
        public RESULT lookupBusID(string path, out GUID guid)
        {
            return FMOD_Studio_System_LookupBusID(rawPtr, Encoding.UTF8.GetBytes(path + Char.MinValue), out guid);
        }
        public RESULT setListenerAttributes(_3D_ATTRIBUTES attributes)
        {
            return FMOD_Studio_System_SetListenerAttributes(rawPtr, ref attributes);
        }
        public RESULT loadBankFile(string name, out Bank bank)
        {
            RESULT result = RESULT.OK;
            IntPtr bankraw = new IntPtr();
            bank = null;

            try
            {
                result = FMOD_Studio_System_LoadBankFile(rawPtr, Encoding.UTF8.GetBytes(name + Char.MinValue), out bankraw);
            }
            catch
            {
                result = RESULT.ERR_INVALID_PARAM;
            }
            if (result != RESULT.OK)
            {
                return result;
            }

            bank = new Bank();
            bank.setRaw(bankraw);

            return result;
        }
        public RESULT loadBankMemory(IntPtr buffer, int length, LOAD_MEMORY_MODE mode, out Bank bank)
        {
            RESULT result = RESULT.OK;
            IntPtr bankraw = new IntPtr();
            bank = null;

            try
            {
                result = FMOD_Studio_System_LoadBankMemory(rawPtr, buffer, length, mode, out bankraw);
            }
            catch
            {
                result = RESULT.ERR_INVALID_PARAM;
            }
            if (result != RESULT.OK)
            {
                return result;
            }

            bank = new Bank();
            bank.setRaw(bankraw);

            return result;
        }

        #region importfunctions

        [DllImport (STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_System_Initialize              (IntPtr studiosystem, int maxchannels, INITFLAGS studioFlags, FMOD.INITFLAGS flags, IntPtr extradriverdata);
        [DllImport (STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_System_Release                 (IntPtr studiosystem);
        [DllImport (STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_System_Update                  (IntPtr studiosystem);
        [DllImport (STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_System_GetLowLevelSystem       (IntPtr studiosystem, out IntPtr system);
        [DllImport (STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_System_GetEvent                (IntPtr studiosystem, ref GUID guid, LOADING_MODE mode, out IntPtr description);
        [DllImport (STUDIO_VERSION.dll)]
		private static extern RESULT FMOD_Studio_System_GetMixerStrip			(IntPtr studiosystem, ref GUID guid, LOADING_MODE mode, out IntPtr mixerStrip);
        [DllImport (STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_System_LookupEventID           (IntPtr studiosystem, byte[] path, out GUID guid);
        [DllImport (STUDIO_VERSION.dll)]
		private static extern RESULT FMOD_Studio_System_LookupBusID				(IntPtr studiosystem, byte[] path, out GUID guid);
		[DllImport (STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_System_SetListenerAttributes   (IntPtr studiosystem, ref _3D_ATTRIBUTES attributes);
        [DllImport (STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_System_LoadBankFile            (IntPtr studiosystem, byte[] filename, out IntPtr bank);
        [DllImport (STUDIO_VERSION.dll)]
		private static extern RESULT FMOD_Studio_System_LoadBankMemory			(IntPtr system, IntPtr buffer, int length, LOAD_MEMORY_MODE mode, out IntPtr bank);
        [DllImport (STUDIO_VERSION.dll)]
		private static extern RESULT FMOD_Studio_System_UnloadAll               (IntPtr studiosystem);

		#endregion
	}
	
	public class EventDescription : HandleBase
    {
        public RESULT getParameterCount(out int count)
        {
            return FMOD_Studio_EventDescription_GetParameterCount(rawPtr, out count);
        }

        public RESULT getParameterByIndex(int index, out PARAMETER_DESCRIPTION parameter)
        {
			parameter = new FMOD.Studio.PARAMETER_DESCRIPTION();
            return FMOD_Studio_EventDescription_GetParameterByIndex(rawPtr, index, out parameter);
        }

        public RESULT getParameter(string name, out PARAMETER_DESCRIPTION parameter)
        {
			parameter = new FMOD.Studio.PARAMETER_DESCRIPTION();
            return FMOD_Studio_EventDescription_GetParameter(rawPtr, name, out parameter);
        }

        public RESULT createInstance           (out EventInstance instance)
        {
            RESULT      result        = RESULT.OK;
            IntPtr      eventinstanceraw = new IntPtr();
            instance = null;

            try
            {
                result = FMOD_Studio_EventDescription_CreateInstance(rawPtr, out eventinstanceraw);
            }
            catch
            {
                result = RESULT.ERR_INVALID_PARAM;
            }
            if (result != RESULT.OK)
            {
                return result;
            }
            instance = new EventInstance();
            instance.setRaw(eventinstanceraw);

            return result;
        }
        
        #region importfunctions
			
		[DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventDescription_GetID(IntPtr eventdescription, out GUID id);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventDescription_GetParameterCount(IntPtr eventdescription, out int count);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventDescription_GetParameterByIndex(IntPtr eventdescription, int index, out PARAMETER_DESCRIPTION parameter);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventDescription_GetParameter(IntPtr eventdescription, string name, out PARAMETER_DESCRIPTION parameter);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventDescription_GetUserPropertyCount(IntPtr eventdescription, out int count);
        //[DllImport(STUDIO_VERSION.dll)]
        //private static extern RESULT FMOD_Studio_EventDescription_GetUserPropertyByIndex(IntPtr eventdescription, int index, out USER_PROPERTY property);
        //[DllImport(STUDIO_VERSION.dll)]
        //private static extern RESULT FMOD_Studio_EventDescription_GetUserProperty(IntPtr eventdescription, string name, out USER_PROPERTY property);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventDescription_GetLength(IntPtr eventdescription, out int length);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventDescription_GetMinimumDistance(IntPtr eventdescription, out float distance);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventDescription_GetMaximumDistance(IntPtr eventdescription, out float distance);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventDescription_IsOneshot(IntPtr eventdescription, out int oneshot);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventDescription_IsStream(IntPtr eventdescription, out int isStream);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventDescription_Is3D(IntPtr eventdescription, out int is3D);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventDescription_ReleaseAllInstances(IntPtr eventdescription);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventDescription_CreateInstance(IntPtr eventdescription, out IntPtr eventinstance);
        //[DllImport(STUDIO_VERSION.dll)]
        //private static extern RESULT FMOD_Studio_EventDescription_SetCallback(IntPtr eventdescription, EVENT_CALLBACK callback);

        #endregion
	}

    public class EventInstance : HandleBase
    {
        public RESULT getVolume(out float volume)
        {
            return FMOD_Studio_EventInstance_GetVolume(rawPtr, out volume);
        }
        public RESULT setVolume(float volume)
        {
            return FMOD_Studio_EventInstance_SetVolume(rawPtr, volume);
        }
        public RESULT getPitch(out float pitch)
        {
            return FMOD_Studio_EventInstance_GetPitch(rawPtr, out pitch);
        }
        public RESULT setPitch(float pitch)
        {
            return FMOD_Studio_EventInstance_SetPitch(rawPtr, pitch);
        }
        public RESULT set3DAttributes               (_3D_ATTRIBUTES attributes)
        {
            return FMOD_Studio_EventInstance_Set3DAttributes(rawPtr, ref attributes);
        }
        public RESULT getPaused(out bool paused)
        {
            int p = 0;
            RESULT result = FMOD_Studio_EventInstance_GetPaused(rawPtr, out p);
            paused = (p != 0);
            return result;
        }
        public RESULT setPaused(bool paused)
        {
            return FMOD_Studio_EventInstance_SetPaused(rawPtr, (paused ? 1 : 0));
        }
        public RESULT start()
        {
            return FMOD_Studio_EventInstance_Start(rawPtr);
        }
        public RESULT stop()
        {
            return stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
        public RESULT stop(STOP_MODE mode)
        {
            return FMOD_Studio_EventInstance_Stop(rawPtr, mode);
        }
        public RESULT getTimelinePosition(out int position)
        {
            return FMOD_Studio_EventInstance_GetTimelinePosition(rawPtr, out position);
        }
        public RESULT setTimelinePosition(int position)
        {
            return FMOD_Studio_EventInstance_SetTimelinePosition(rawPtr, position);
        }
        public RESULT getPlaybackState(out PLAYBACK_STATE state)
        {
            return FMOD_Studio_EventInstance_GetPlaybackState(rawPtr, out state);
        }
#if false && !DISABLE_LOW_LEVEL
        public RESULT getChannelGroup(out FMOD.ChannelGroup group)
        {
            RESULT result = RESULT.OK;
            IntPtr groupraw = new IntPtr();
            group = null;

            try
            {
                result = FMOD_Studio_EventInstance_GetChannelGroup(rawPtr, out groupraw);
            }
            catch
            {
                result = RESULT.ERR_INVALID_PARAM;
            }
            if (result != RESULT.OK)
            {
                return result;
            }

            group = new FMOD.ChannelGroup();
            group.setRaw(groupraw);

            return result;
        }
#endif
        public RESULT release()
        {
			IntPtr oldRaw = rawPtr;
			invalidateHandle(); // clear callback before releasing

			FMOD.RESULT result = FMOD_Studio_EventInstance_Release(oldRaw);

			if (result == FMOD.RESULT.ERR_EVENT_WONT_STOP)
			{
				setRaw(oldRaw);
			}
				
			return result;
        }
        public RESULT getParameter(string name, out ParameterInstance instance)
        {
            RESULT result = RESULT.OK;
            IntPtr parameterinstanceraw = new IntPtr();
            instance = null;

            try
            {
                result = FMOD_Studio_EventInstance_GetParameter(rawPtr, Encoding.UTF8.GetBytes(name + Char.MinValue), out parameterinstanceraw);
            }
            catch
            {
                result = RESULT.ERR_INVALID_PARAM;
            }
            if (result != RESULT.OK)
            {
                return result;
            }
            instance = new ParameterInstance();
            instance.setRaw(parameterinstanceraw);

            return result;
        }
        public RESULT getParameterByIndex(int index, out ParameterInstance instance)
        {
            RESULT result = RESULT.OK;
            IntPtr parameterinstanceraw = new IntPtr();
            instance = null;

            try
            {
                result = FMOD_Studio_EventInstance_GetParameterByIndex(rawPtr, index, out parameterinstanceraw);
            }
            catch
            {
                result = RESULT.ERR_INVALID_PARAM;
            }
            if (result != RESULT.OK)
            {
                return result;
            }
            instance = new ParameterInstance();
            instance.setRaw(parameterinstanceraw);

            return result;
        }
        public RESULT getCue(string name, out CueInstance instance)
        {
            RESULT result = RESULT.OK;
            IntPtr cueinstanceraw = new IntPtr();
            instance = null;

            try
            {
                result = FMOD_Studio_EventInstance_GetCue(rawPtr, Encoding.UTF8.GetBytes(name + Char.MinValue), out cueinstanceraw);
            }
            catch
            {
                result = RESULT.ERR_INVALID_PARAM;
            }
            if (result != RESULT.OK)
            {
                return result;
            }
            instance = new CueInstance();
            instance.setRaw(cueinstanceraw);

            return result;
        }
        public RESULT createSubEvent(string name, out EventInstance instance)
        {
            RESULT result = RESULT.OK;
            IntPtr subeventraw = new IntPtr();
			instance = null;

            try
            {
                result = FMOD_Studio_EventInstance_CreateSubEvent(rawPtr, Encoding.UTF8.GetBytes(name + Char.MinValue), out subeventraw);
            }
            catch
            {
                result = RESULT.ERR_INVALID_PARAM;
            }
            if (result != RESULT.OK)
            {
                return result;
            }
            instance = new EventInstance();
            instance.setRaw(subeventraw);

            return result;
        }
        public RESULT getLoadingState(out LOADING_STATE state)
        {
            return FMOD_Studio_EventInstance_GetLoadingState(rawPtr, out state);
        }

#if false // WIP - callbacks coming soon
    	public delegate void OnPlaybackStateChange(EVENT_CALLBACK_TYPE type, EventInstance instance);
		public OnPlaybackStateChange onPlaybackStateChange;
#endif

        #region wrapperinternal

        private delegate RESULT EVENT_CALLBACK(EVENT_CALLBACK_TYPE type, IntPtr parameters);

#if false // WIP - callbacks coming soon
		private EVENT_CALLBACK cachedCallback; // cached to prevent gc free-ing the callback delegate
		
		public EventInstance()
		{
			cachedCallback = wrapperCallback;
		}		
		
		override protected void onSetRaw(IntPtr newRaw)
		{
#if !UNITY_IPHONE //TODO: test callbacks on iOS
			// clear the old callback
			if (rawPtr != IntPtr.Zero)
				FMOD_Studio_EventInstance_SetCallback(rawPtr, null);
			
			// set the new callback
			if (newRaw != IntPtr.Zero)
				FMOD_Studio_EventInstance_SetCallback(newRaw, cachedCallback);
#endif
		}
		
		private RESULT wrapperCallback(EVENT_CALLBACK_TYPE type, IntPtr parameters)
		{
			if (onPlaybackStateChange != null)
				onPlaybackStateChange(type, this);
				
			return RESULT.OK;
		}

		~EventInstance()
		{
			setRaw(IntPtr.Zero); // clear callback
		}
#endif
			
        #endregion

        #region importfunctions

        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventInstance_GetVolume            (IntPtr _event, out float volume);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventInstance_SetVolume            (IntPtr _event, float volume);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventInstance_GetPitch             (IntPtr _event, out float pitch);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventInstance_SetPitch             (IntPtr _event, float pitch);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventInstance_Set3DAttributes      (IntPtr _event, ref _3D_ATTRIBUTES attributes);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventInstance_SetPaused            (IntPtr _event, int paused);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventInstance_GetPaused            (IntPtr _event, out int paused);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventInstance_Start                (IntPtr _event);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventInstance_Stop                 (IntPtr _event, STOP_MODE mode);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventInstance_GetTimelinePosition  (IntPtr _event, out int position);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventInstance_SetTimelinePosition  (IntPtr _event, int position);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventInstance_GetPlaybackState     (IntPtr _event, out PLAYBACK_STATE state);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventInstance_GetChannelGroup      (IntPtr _event, out IntPtr group);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventInstance_GetParameter         (IntPtr _event, byte[] name, out IntPtr parameter);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventInstance_GetParameterByIndex  (IntPtr _event, int index, out IntPtr parameter);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventInstance_GetCue               (IntPtr _event, byte[] name, out IntPtr cue);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventInstance_CreateSubEvent       (IntPtr _event, byte[] name, out IntPtr _instance);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventInstance_GetLoadingState      (IntPtr _event, out LOADING_STATE state);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventInstance_SetCallback          (IntPtr _event, EVENT_CALLBACK callback);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_EventInstance_Release              (IntPtr _event);
        
        #endregion
	}

    public class CueInstance : HandleBase
    {
        public RESULT setValue(float value)
        {
            return FMOD_Studio_CueInstance_Trigger(rawPtr);
        }

        #region importfunctions

        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_CueInstance_Trigger(IntPtr cue);

        #endregion
    }

    public class ParameterInstance : HandleBase
    {
        public RESULT setValue(float value)
        {
            return FMOD_Studio_ParameterInstance_SetValue(rawPtr, value);
        }

        #region importfunctions

        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_ParameterInstance_SetValue(IntPtr parameter, float value);

        #endregion
    }

    public class MixerStrip : HandleBase
    {
        public RESULT getFaderLevel(out float volume)
        {
            return FMOD_Studio_MixerStrip_GetFaderLevel(rawPtr, out volume);
        }
        public RESULT setFaderLevel(float volume)
        {
            return FMOD_Studio_MixerStrip_SetFaderLevel(rawPtr, volume);
        }
        public RESULT getPaused(out bool paused)
        {
            RESULT result;
            int p = 0;

            result = FMOD_Studio_MixerStrip_GetPaused(rawPtr, out p);

            paused = (p != 0);

            return result;
        }
        public RESULT setPaused(bool paused)
        {
            return FMOD_Studio_MixerStrip_SetPaused(rawPtr, (paused ? 1 : 0));
        }
        public RESULT stopAllEvents(STOP_MODE mode)
        {
            return FMOD_Studio_MixerStrip_StopAllEvents(rawPtr, mode);
        }
        public RESULT getLoadingState(out LOADING_STATE state)
        {
            return FMOD_Studio_MixerStrip_GetLoadingState(rawPtr, out state);
        }
        public RESULT release()
        {
            return FMOD_Studio_MixerStrip_Release(rawPtr);
        }

        #region importfunctions

        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_MixerStrip_GetFaderLevel   (IntPtr strip, out float value);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_MixerStrip_SetFaderLevel   (IntPtr strip, float value);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_MixerStrip_GetPaused       (IntPtr strip, out int paused);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_MixerStrip_SetPaused       (IntPtr strip, int paused);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_MixerStrip_StopAllEvents   (IntPtr strip, STOP_MODE mode);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_MixerStrip_GetLoadingState (IntPtr strip, out LOADING_STATE state);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_MixerStrip_Release         (IntPtr strip);

        #endregion
    }

    public class Bank : HandleBase
    {
		public RESULT loadSampleData()
		{
			return FMOD_Studio_Bank_LoadSampleData(rawPtr);
		}
		public RESULT unloadSampleData()
		{
			return FMOD_Studio_Bank_UnloadSampleData(rawPtr);
		}			
        public RESULT unload()
        {
            RESULT result = FMOD_Studio_Bank_Unload(rawPtr);

            if (result != RESULT.OK)
            {
                return result;
            }

            rawPtr = IntPtr.Zero;
				
			return RESULT.OK;
        }

        #region importfunctions

        [DllImport(STUDIO_VERSION.dll)]
		private static extern RESULT FMOD_Studio_Bank_LoadSampleData(IntPtr bank);
        [DllImport(STUDIO_VERSION.dll)]
		private static extern RESULT FMOD_Studio_Bank_UnloadSampleData(IntPtr bank);
        [DllImport(STUDIO_VERSION.dll)]
        private static extern RESULT FMOD_Studio_Bank_Unload(IntPtr bank);

        #endregion
    }
} // System
		
} // FMOD
