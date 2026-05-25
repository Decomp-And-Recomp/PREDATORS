using System.Collections.Generic;
using UnityEngine;

public class InputGUI
{
	private const float INCREASE_AMOUNT = 0.03f;

	private const float DECREASE_AMOUNT = 0.029126167f;

	private static Collider colliderToHiglightNew = null;

	private static Collider colliderToHiglightOld = null;

	private static List<TextMesh> cachedTextMeshes = new List<TextMesh>();

	private static List<TextMesh> auxList = new List<TextMesh>();

	private static Color lightRed = new Color(1f, 19f / 85f, 0f);

	private static List<TouchGUI> touchList = new List<TouchGUI>();

	private static int lastFrameTouches = -1;

	public static int touchCount
	{
		get
		{
			return PlatformDependent.InputTouchCount();
		}
	}

	public static List<TouchGUI> touches
	{
		get
		{
			if (lastFrameTouches != Time.frameCount)
			{
				touchList.Clear();
				int num = PlatformDependent.InputTouchCount();
				for (int i = 0; i < num; i++)
				{
					touchList.Add(PlatformDependent.GetTouch(i));
				}
				lastFrameTouches = Time.frameCount;
			}
			return touchList;
		}
	}

	public static TouchGUI GetTouch(int aID)
	{
		return touches[aID];
	}

	public static Collider GetHitCollider(Vector2 screenPosition, Camera currentCamera)
	{
		Ray ray = currentCamera.ScreenPointToRay(screenPosition);
		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo, 10000f))
		{
			return hitInfo.collider;
		}
		return null;
	}

	public static void UpdateMouseCursorGUITexture(GUITexture aMouseCursor, Camera aCamera)
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.x = Mathf.Clamp(mousePosition.x, 0f, Screen.width);
		mousePosition.y = Mathf.Clamp(mousePosition.y, 0f, Screen.height);
		aMouseCursor.pixelInset = new Rect(mousePosition.x - aMouseCursor.pixelInset.height / 3f, mousePosition.y - aMouseCursor.pixelInset.height, aMouseCursor.pixelInset.width, aMouseCursor.pixelInset.height);
		colliderToHiglightNew = GetHitCollider(mousePosition, aCamera);
		if (colliderToHiglightNew == null)
		{
			if (colliderToHiglightOld != null)
			{
				TurnBackColor();
			}
		}
		else if (colliderToHiglightOld != null)
		{
			if (colliderToHiglightNew != colliderToHiglightOld)
			{
				TurnBackColor();
				ColorNewHitCollider();
			}
		}
		else
		{
			ColorNewHitCollider();
		}
		colliderToHiglightOld = colliderToHiglightNew;
	}

	public static void UpdateMouseCursorGUITextureTrophyRoom(GUITexture aMouseCursor, Camera aCamera)
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.x = Mathf.Clamp(mousePosition.x, 0f, Screen.width);
		mousePosition.y = Mathf.Clamp(mousePosition.y, 0f, Screen.height);
		aMouseCursor.pixelInset = new Rect(mousePosition.x - aMouseCursor.pixelInset.height / 3f, mousePosition.y - aMouseCursor.pixelInset.height, aMouseCursor.pixelInset.width, aMouseCursor.pixelInset.height);
		colliderToHiglightNew = GetHitCollider(mousePosition, aCamera);
		if (colliderToHiglightNew == null)
		{
			if (colliderToHiglightOld != null)
			{
				SetScaleSelected(false, colliderToHiglightOld.transform);
			}
		}
		else if (colliderToHiglightOld != null)
		{
			if (colliderToHiglightNew != colliderToHiglightOld)
			{
				SetScaleSelected(false, colliderToHiglightOld.transform);
				SetScaleSelected(true, colliderToHiglightNew.transform);
			}
		}
		else
		{
			SetScaleSelected(true, colliderToHiglightNew.transform);
		}
		colliderToHiglightOld = colliderToHiglightNew;
	}

	private static TextMesh[] GetTextMeshesInFirstLevelOfChildren(Transform aTransform)
	{
		auxList.Clear();
		TextMesh component = aTransform.gameObject.GetComponent<TextMesh>();
		if (component != null)
		{
			auxList.Add(component);
		}
		int childCount = aTransform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			component = aTransform.GetChild(i).gameObject.GetComponent<TextMesh>();
			if (component != null)
			{
				auxList.Add(component);
			}
		}
		return auxList.ToArray();
	}

	private static void ColorNewHitCollider()
	{
		TextMesh[] textMeshesInFirstLevelOfChildren = GetTextMeshesInFirstLevelOfChildren(colliderToHiglightNew.transform);
		TextMesh[] array = textMeshesInFirstLevelOfChildren;
		foreach (TextMesh textMesh in array)
		{
			textMesh.GetComponent<Renderer>().material.color = lightRed;
			SetScaleSelected(true, textMesh.transform);
			if (!cachedTextMeshes.Contains(textMesh))
			{
				cachedTextMeshes.Add(textMesh);
			}
		}
	}

	private static void TurnBackColor()
	{
		foreach (TextMesh cachedTextMesh in cachedTextMeshes)
		{
			if (cachedTextMesh != null)
			{
				cachedTextMesh.GetComponent<Renderer>().material.color = Color.red;
				SetScaleSelected(false, cachedTextMesh.transform);
			}
		}
		cachedTextMeshes.Clear();
	}

	private static void SetScaleSelected(bool aValue, Transform aTextMesh)
	{
		if (aTextMesh.gameObject.layer == 19)
		{
			return;
		}
		if (aTextMesh.GetComponent<Renderer>() != null && aTextMesh.GetComponent<Renderer>().material.HasProperty("_Color"))
		{
			Color color = aTextMesh.GetComponent<Renderer>().material.color;
			if (aValue)
			{
				color.r += 0.1f;
			}
			else
			{
				color.r -= 0.1f;
			}
			aTextMesh.GetComponent<Renderer>().material.color = color;
		}
		Vector3 localScale = aTextMesh.localScale;
		if (aValue)
		{
			if (localScale.x < 0f || localScale.y < 0f || localScale.z < 0f)
			{
				localScale -= Mathf.Abs(localScale.x) * Vector3.one * 0.03f;
			}
			else
			{
				localScale += Mathf.Abs(localScale.x) * Vector3.one * 0.03f;
			}
		}
		else if (localScale.x < 0f || localScale.y < 0f || localScale.z < 0f)
		{
			localScale += Mathf.Abs(localScale.x) * Vector3.one * 0.029126167f;
		}
		else
		{
			localScale -= Mathf.Abs(localScale.x) * Vector3.one * 0.029126167f;
		}
		aTextMesh.transform.localScale = localScale;
	}
}
