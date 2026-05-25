using System;

public static class StringExtensions
{
	public static int MaxCharsPerLine(this string aString)
	{
		char[] separator = new char[1] { '\n' };
		string[] array = aString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
		int num = 0;
		string[] array2 = array;
		foreach (string text in array2)
		{
			if (text.Length > num)
			{
				num = text.Length;
			}
		}
		return num;
	}
}
