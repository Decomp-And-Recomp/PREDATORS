using System;
using System.Collections.Generic;
using UnityEngine;

public class FlurryManager : MonoBehaviour
{
	private static FlurryManager _instance;

	private AndroidJavaClass unityBridge;

	private string key = "JKF6WQXKTCF4HB76Y3ZV";

	private bool initialized;

	public static FlurryManager Instance
	{
		get
		{
			return _instance;
		}
	}

	private void Awake()
	{
		if (!initialized)
		{
			//_instance = this;
			//UnityEngine.Object.DontDestroyOnLoad(this);
			//init();
		}
	}

	private void init()
	{
		initialized = true;
		//unityBridge = new AndroidJavaClass("com.angrymobgames.flurryplugin.UnityBridge");
		//unityBridge.CallStatic("init", key);
	}

	public void Close()
	{
		//unityBridge.CallStatic("close");
		//initialized = false;
	}

	public void LogEvent(string eventId)
	{
		//unityBridge.CallStatic("logevent", eventId);
	}

	public void LogEvent(string eventId, bool timed)
	{
		//unityBridge.CallStatic("logevent", eventId, timed);
	}

	public void LogEvent(string eventId, Dictionary<string, string> param)
	{
		/*using (AndroidJavaObject androidJavaObject = new AndroidJavaObject("android.os.Bundle"))
		{
			IntPtr methodID = AndroidJNI.GetMethodID(androidJavaObject.GetRawClass(), "putString", "(Ljava/lang/String;Ljava/lang/String;)V");
			object[] array = new object[2];
			if (param != null)
			{
				foreach (KeyValuePair<string, string> item in param)
				{
					array[0] = new AndroidJavaObject("java.lang.String", item.Key);
					array[1] = new AndroidJavaObject("java.lang.String", item.Value);
					AndroidJNI.CallVoidMethod(androidJavaObject.GetRawObject(), methodID, AndroidJNIHelper.CreateJNIArgArray(array));
				}
			}
			unityBridge.CallStatic("logevent", eventId, androidJavaObject);
		}*/
	}

	public void LogEvent(string eventId, Dictionary<string, string> param, bool timed)
	{
		/*using (AndroidJavaObject androidJavaObject = new AndroidJavaObject("android.os.Bundle"))
		{
			IntPtr methodID = AndroidJNI.GetMethodID(androidJavaObject.GetRawClass(), "putString", "(Ljava/lang/String;Ljava/lang/String;)V");
			object[] array = new object[2];
			if (param != null)
			{
				foreach (KeyValuePair<string, string> item in param)
				{
					array[0] = new AndroidJavaObject("java.lang.String", item.Key);
					array[1] = new AndroidJavaObject("java.lang.String", item.Value);
					AndroidJNI.CallVoidMethod(androidJavaObject.GetRawObject(), methodID, AndroidJNIHelper.CreateJNIArgArray(array));
				}
			}
			unityBridge.CallStatic("logevent", eventId, androidJavaObject, timed);
		}*/
	}

	public void EndTimedEvent(string eventId)
	{
		//unityBridge.CallStatic("endevent", eventId);
	}
}
