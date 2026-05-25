using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Language
{
	public const string LANGUAGE_PREF = "Language";

	private static Language instance;

	public Dictionary<string, string> textTable = new Dictionary<string, string>();

	private List<SystemLanguage> supportedLanguages = new List<SystemLanguage>
	{
		SystemLanguage.English,
		SystemLanguage.French,
		SystemLanguage.Italian,
		SystemLanguage.German,
		SystemLanguage.Spanish
	};

	private SystemLanguage currentLanguage = SystemLanguage.Unknown;

	public static SystemLanguage CurrentLang
	{
		get
		{
			return Instance.CurrentLanguage;
		}
	}

	public static Language Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new Language();
				SystemLanguage aLanguageName = DefaultLanguage();
				instance.LoadLanguage(aLanguageName);
			}
			return instance;
		}
	}

	public SystemLanguage CurrentLanguage
	{
		get
		{
			return currentLanguage;
		}
	}

	public static string FormatString(string s, int maxCharsPerLine)
	{
		int num = 0;
		int num2 = 0;
		for (int i = 1; i < s.Length; i++)
		{
			num2++;
			if (s[i] == ' ')
			{
				num = i;
			}
			if (s[i] == '\n')
			{
				num2 = 0;
			}
			else if (num2 % maxCharsPerLine == 0)
			{
				if (s[i] == ' ')
				{
					s = s.Remove(i, 1);
					s = s.Insert(i, "\n");
					num2 = 0;
				}
				else
				{
					s = s.Remove(num, 1);
					s = s.Insert(num, "\n");
					num2 = i - num;
				}
			}
		}
		return s;
	}

	public static string GetTxt(string aKey, int charsPerLine = -1)
	{
		return Instance.GetText(aKey, charsPerLine);
	}

	public static void LoadLang(SystemLanguage aLang)
	{
		Instance.LoadLanguage(aLang);
	}

	public static SystemLanguage DefaultLanguage()
	{
		return (SystemLanguage)EncryptedPlayerPrefs.GetInt("Language", 22);
	}

	private void LoadLanguage(SystemLanguage aLanguageName)
	{
		if (aLanguageName == currentLanguage)
		{
			return;
		}
		textTable.Clear();
		TextAsset textAsset = (TextAsset)Resources.Load("Languages", typeof(TextAsset));
		using (StringReader stringReader = new StringReader(textAsset.text))
		{
			string text = stringReader.ReadLine();
			int num = 0;
			char[] separator = new char[1] { '|' };
			string[] array = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
			int num2 = -1;
			int num3 = array.Length;
			for (int i = 0; i < num3; i++)
			{
				if (array[i].Equals(aLanguageName.ToString()))
				{
					num2 = i;
					break;
				}
			}
			if (num2 == -1 || !supportedLanguages.Contains(aLanguageName))
			{
				num2 = 1;
				currentLanguage = SystemLanguage.English;
			}
			else
			{
				currentLanguage = aLanguageName;
			}
			EncryptedPlayerPrefs.SetInt("Language", (int)currentLanguage);
			while ((text = stringReader.ReadLine()) != null)
			{
				array = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
				string text2 = array[num2].Replace("\\n", "\n").Replace("\\t", "\t");
				text2 = text2.Replace("\\ N", "\n");
				text2 = text2.Replace("\\ n", "\n");
				if (textTable.ContainsKey(array[0].Trim()))
				{
					Debug.LogError("key " + array[0].Trim() + " already present");
				}
				else
				{
					textTable.Add(array[0].Trim(), text2);
				}
				num++;
			}
			stringReader.Close();
		}
	}

	private string GetText(string key, int charsPerLine = -1)
	{
		if (!textTable.ContainsKey(key))
		{
			textTable.Add(key, key);
			Debug.LogWarning("key " + key + " does not exist in the textTable dictionary");
		}
		if (charsPerLine > 0)
		{
			return FormatString(textTable[key], charsPerLine);
		}
		return textTable[key];
	}
}
