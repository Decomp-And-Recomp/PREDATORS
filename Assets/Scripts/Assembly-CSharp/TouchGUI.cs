using UnityEngine;

public struct TouchGUI
{
	private Vector2 iPosition;

	private Vector2 iDeltaPosition;

	private TouchPhase iTouchPhase;

	private int iFingerID;

	public Vector2 deltaPosition
	{
		get
		{
			return iDeltaPosition;
		}
		set
		{
			iDeltaPosition = value;
		}
	}

	public Vector2 position
	{
		get
		{
			return iPosition;
		}
		set
		{
			iPosition = value;
		}
	}

	public TouchPhase phase
	{
		get
		{
			return iTouchPhase;
		}
		set
		{
			iTouchPhase = value;
		}
	}

	public int fingerId
	{
		get
		{
			return iFingerID;
		}
		set
		{
			iFingerID = value;
		}
	}

	public override string ToString()
	{
		string text = "TOUCH = ";
		string text2 = text;
		text = string.Concat(text2, "position ", position, " ");
		text2 = text;
		text = string.Concat(text2, "phase ", phase, " ");
		text2 = text;
		return text2 + "phase " + fingerId + " ";
	}
}
