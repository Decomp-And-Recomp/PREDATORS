using System;
using System.IO;
using UnityEngine;

public class GooglePlayDownloader
{
	private const string Environment_MEDIA_MOUNTED = "mounted";

	private static AndroidJavaClass detectAndroidJNI;

	private static AndroidJavaClass Environment;

	private static string obb_package;

	private static int obb_version;

	static GooglePlayDownloader()
	{
		if (!RunningOnAndroid())
		{
			return;
		}
		try
		{
			Environment = new AndroidJavaClass("android.os.Environment");
		}
		catch (Exception ex)
		{
			Environment = null;
		}
	}

	public static bool RunningOnAndroid()
	{
		if (detectAndroidJNI == null)
		{
			try
			{
				detectAndroidJNI = new AndroidJavaClass("android.os.Build");
			}
			catch
			{
				return false;
			}
		}
		return detectAndroidJNI != null && detectAndroidJNI.GetRawClass() != IntPtr.Zero;
	}

	public static string GetExpansionFilePath()
	{
		if (Environment == null)
		{
			return null;
		}
		try
		{
			populateOBBData();
			if (Environment.CallStatic<string>("getExternalStorageState", new object[0]) != "mounted")
			{
				return null;
			}
			using (AndroidJavaObject androidJavaObject = Environment.CallStatic<AndroidJavaObject>("getExternalStorageDirectory", new object[0]))
			{
				string arg = androidJavaObject.Call<string>("getPath", new object[0]);
				return string.Format("{0}/{1}/{2}", arg, "Android/obb", obb_package);
			}
		}
		catch (Exception ex)
		{
			return null;
		}
	}

	public static string GetMainOBBPath(string expansionFilePath)
	{
		if (expansionFilePath == null)
		{
			return null;
		}
		try
		{
			populateOBBData();
			string text = string.Format("{0}/main.{1}.{2}.obb", expansionFilePath, obb_version, obb_package);
			if (!File.Exists(text))
			{
				return null;
			}
			return text;
		}
		catch
		{
			return null;
		}
	}

	public static string GetPatchOBBPath(string expansionFilePath)
	{
		if (expansionFilePath == null)
		{
			return null;
		}
		try
		{
			populateOBBData();
			string text = string.Format("{0}/patch.{1}.{2}.obb", expansionFilePath, obb_version, obb_package);
			if (!File.Exists(text))
			{
				return null;
			}
			return text;
		}
		catch
		{
			return null;
		}
	}

	public static void FetchOBB()
	{
	}

	private static void populateOBBData()
	{
		if (obb_version != 0)
		{
			return;
		}
		try
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
				obb_package = @static.Call<string>("getPackageName", new object[0]);
				AndroidJavaObject androidJavaObject = @static.Call<AndroidJavaObject>("getPackageManager", new object[0]).Call<AndroidJavaObject>("getPackageInfo", new object[2] { obb_package, 0 });
				obb_version = androidJavaObject.Get<int>("versionCode");
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("[GooglePlayDownloader] populateOBBData failed: " + ex.Message);
		}
	}
}
