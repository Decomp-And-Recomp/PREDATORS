using System.Collections.Generic;
using UnityEngine;

public static class TextMeshExtensions
{
	private static List<TextMesh> textMeshesThatAreDisplayingMessages = new List<TextMesh>();

	public static void DeactivateChildRenderer(this Component aComp)
	{
		bool active = aComp.gameObject.active;
		aComp.gameObject.SetActiveRecursively(true);
		aComp.GetComponentInChildren<TextMesh>().GetComponent<Renderer>().enabled = false;
		aComp.gameObject.SetActiveRecursively(active);
	}

	public static void SetTextInChildren(this Component aComp, string aText)
	{
		bool active = aComp.gameObject.active;
		aComp.gameObject.SetActiveRecursively(true);
		aComp.GetComponentInChildren<TextMesh>().text = aText;
		aComp.gameObject.SetActiveRecursively(active);
	}

	public static int MaxCharsPerLine(this TextMesh aTextMesh)
	{
		char[] separator = new char[2] { '(', ')' };
		string[] array = aTextMesh.name.Split(separator);
		string[] array2 = array;
		foreach (string s in array2)
		{
			int result = -1;
			if (int.TryParse(s, out result))
			{
				return result;
			}
		}
		return -1;
	}

	public static TextMesh GetShadowText(this TextMesh aTextMesh)
	{
		if (aTextMesh.transform.GetChildCount() > 0)
		{
			return aTextMesh.transform.GetChild(0).GetComponent<TextMesh>();
		}
		return null;
	}

	public static string CleanName(this TextMesh aTextMesh)
	{
		char[] separator = new char[2] { '(', ')' };
		return aTextMesh.name.Split(separator)[0];
	}

	public static string WithMaxCharsPerLine(this string aString, int charsPerLine, bool niceLayout = false)
	{
		char[] separator = new char[1] { ' ' };
		string[] array = aString.Split(separator);
		string text = string.Empty;
		string text2 = string.Empty;
		for (int i = 0; i < array.Length - 1; i++)
		{
			text2 += array[i];
			if ((text2 + array[i + 1]).Length >= charsPerLine)
			{
				if (niceLayout)
				{
					int num = charsPerLine - text2.Length;
					while (num > 0)
					{
						for (int j = 0; j < text2.Length; j++)
						{
							if (num <= 0)
							{
								break;
							}
							if (text2[j] == ' ')
							{
								text2 = text2.Insert(j, " ");
								j++;
								num--;
							}
						}
					}
				}
				text = text + text2 + "\n";
				text2 = string.Empty;
			}
			else
			{
				text2 += " ";
			}
		}
		return text + text2 + array[array.Length - 1];
	}

	public static string TimeFormat(this float time)
	{
		int num = (int)time / 60;
		int num2 = (int)time % 60;
		string text = string.Empty;
		if (num < 10)
		{
			text += "0";
		}
		text = text + num + ":";
		if (num2 < 10)
		{
			text += "0";
		}
		return text + num2 + string.Empty;
	}
}
