using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class EncryptedPlayerPrefs
{
	private static string privateKey = "453h45HTfH556hedYfsd";

	public static string[] keys = new string[5] { "6th5HF1d", "6dFht65g", "4DGevr4f", "Sgje30n4", "htlj45hF" };

	public static string Md5(string strToEncrypt)
	{
		UTF8Encoding uTF8Encoding = new UTF8Encoding();
		byte[] bytes = uTF8Encoding.GetBytes(strToEncrypt);
		MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
		byte[] array = mD5CryptoServiceProvider.ComputeHash(bytes);
		string text = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			text += Convert.ToString(array[i], 16).PadLeft(2, '0');
		}
		return text.PadLeft(32, '0');
	}

	public static void SaveEncryption(string key, string type, string value)
	{
		int num = (int)Mathf.Floor(UnityEngine.Random.value * (float)keys.Length);
		string text = keys[num];
		string value2 = Md5(type + "_" + privateKey + "_" + text + "_" + value);
		PlayerPrefs.SetString(key + "_encryption_check", value2);
		PlayerPrefs.SetInt(key + "_used_key", num);
	}

	public static bool CheckEncryption(string key, string type, string value)
	{
		int @int = PlayerPrefs.GetInt(key + "_used_key");
		string text = keys[@int];
		string text2 = Md5(type + "_" + privateKey + "_" + text + "_" + value);
		if (!PlayerPrefs.HasKey(key + "_encryption_check"))
		{
			return false;
		}
		string @string = PlayerPrefs.GetString(key + "_encryption_check");
		return @string == text2;
	}

	public static void SetInt(string key, int value)
	{
		PlayerPrefs.SetInt(key, value);
		SaveEncryption(key, "int", value.ToString());
		PlayerPrefs.Save();
	}

	public static void SetFloat(string key, float value)
	{
		PlayerPrefs.SetFloat(key, value);
		SaveEncryption(key, "float", Mathf.Floor(value * 1000f).ToString());
		PlayerPrefs.Save();
	}

	public static void SetString(string key, string value)
	{
		PlayerPrefs.SetString(key, value);
		SaveEncryption(key, "string", value);
		PlayerPrefs.Save();
	}

	public static int GetInt(string key)
	{
		return GetInt(key, 0);
	}

	public static float GetFloat(string key)
	{
		return GetFloat(key, 0f);
	}

	public static string GetString(string key)
	{
		return GetString(key, string.Empty);
	}

	public static int GetInt(string key, int defaultValue)
	{
		int @int = PlayerPrefs.GetInt(key);
		if (!CheckEncryption(key, "int", @int.ToString()))
		{
			return defaultValue;
		}
		return @int;
	}

	public static float GetFloat(string key, float defaultValue)
	{
		float @float = PlayerPrefs.GetFloat(key);
		if (!CheckEncryption(key, "float", Mathf.Floor(@float * 1000f).ToString()))
		{
			return defaultValue;
		}
		return @float;
	}

	public static string GetString(string key, string defaultValue)
	{
		string @string = PlayerPrefs.GetString(key);
		if (!CheckEncryption(key, "string", @string))
		{
			return defaultValue;
		}
		return @string;
	}

	public static bool HasKey(string key)
	{
		return PlayerPrefs.HasKey(key);
	}

	public static void DeleteKey(string key)
	{
		PlayerPrefs.DeleteKey(key);
		PlayerPrefs.DeleteKey(key + "_encryption_check");
		PlayerPrefs.DeleteKey(key + "_used_key");
	}
}
