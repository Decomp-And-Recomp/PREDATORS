using System.Collections;
using UnityEngine;

public class AlignUIMuffin : MonoBehaviour
{
	public enum AlignPosition
	{
		Custom = 0,
		Left = 1,
		Right = 2,
		None = 3
	}

	public enum AlignScale
	{
		None = 0,
		FitWidthProportional = 1,
		FitWidthAndHeight = 2,
		ScaleBasedOnContentScaleFactor = 3
	}

	public AlignPosition alignPosition;

	public AlignScale alignScale;

	public float ScreenWidthSetup = 480f;

	public float ScreenHeightSetup = 320f;

	public Camera mainCamera;

	public float viewportXCoordinate = 0.5f;

	public bool alignDelayed;

	private float zDepth = 0.1f;

	private Vector3 initScale;

	private static float contentScaleFactor;

	public static float ContentScaleFactor
	{
		get
		{
			if (PlatformDependent.tablet)
			{
				float num = (float)Screen.width / (float)Screen.height;
				contentScaleFactor = num / 1.5f;
				contentScaleFactor = Mathf.Min(1f, contentScaleFactor);
			}
			else
			{
				contentScaleFactor = 1f;
			}
			return contentScaleFactor;
		}
	}

	public void Awake()
	{
		if (!mainCamera)
		{
			mainCamera = Camera.main;
		}
		initScale = base.transform.localScale;
		if (alignPosition != AlignPosition.None)
		{
			zDepth = mainCamera.transform.InverseTransformPoint(base.transform.position).z;
		}
		if (alignDelayed)
		{
			StartCoroutine(AlignDelayed());
		}
		else
		{
			AlignBasedOnCamera();
		}
	}

	private IEnumerator AlignDelayed()
	{
		yield return new WaitForSeconds(0.1f);
		AlignBasedOnCamera();
	}

	private void OnEnable()
	{
	}

	private void AlignBasedOnCamera()
	{
		AlignScaleBasedOnCamera();
		if (alignPosition != AlignPosition.None && viewportXCoordinate != -1f)
		{
			AlignPositionBasedOnCamera();
		}
	}

	public static void UpdateAllUIs()
	{
		AlignUIMuffin[] array = (AlignUIMuffin[])Object.FindObjectsOfType(typeof(AlignUIMuffin));
		AlignUIMuffin[] array2 = array;
		foreach (AlignUIMuffin alignUIMuffin in array2)
		{
			alignUIMuffin.AlignBasedOnCamera();
		}
	}

	private void AlignScaleBasedOnCamera()
	{
		switch (alignScale)
		{
		case AlignScale.FitWidthProportional:
		{
			float num = (float)Screen.width / ScreenWidthSetup;
			base.transform.localScale = initScale * num;
			break;
		}
		case AlignScale.FitWidthAndHeight:
			base.transform.localScale = new Vector3(initScale.x * mainCamera.aspect / (ScreenWidthSetup / ScreenHeightSetup), initScale.y, initScale.z);
			break;
		case AlignScale.ScaleBasedOnContentScaleFactor:
			base.transform.localScale = initScale * ContentScaleFactor;
			break;
		}
	}

	private void AlignPositionBasedOnCamera()
	{
		switch (alignPosition)
		{
		case AlignPosition.Custom:
		{
			Vector3 position3 = mainCamera.ViewportToWorldPoint(new Vector3(viewportXCoordinate, 0.5f, zDepth));
			if ((bool)base.transform.parent)
			{
				base.transform.localPosition = new Vector3(base.transform.parent.InverseTransformPoint(position3).x, base.transform.localPosition.y, base.transform.localPosition.z);
			}
			else
			{
				base.transform.localPosition = new Vector3(mainCamera.transform.InverseTransformPoint(position3).x, base.transform.localPosition.y, base.transform.localPosition.z);
			}
			break;
		}
		case AlignPosition.Left:
		{
			Vector3 position2 = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0.5f, zDepth));
			if ((bool)base.transform.parent)
			{
				base.transform.localPosition = new Vector3(base.transform.parent.InverseTransformPoint(position2).x, base.transform.localPosition.y, base.transform.localPosition.z);
			}
			else
			{
				base.transform.localPosition = new Vector3(mainCamera.transform.InverseTransformPoint(position2).x, base.transform.localPosition.y, base.transform.localPosition.z);
			}
			break;
		}
		case AlignPosition.Right:
		{
			Vector3 position = mainCamera.ViewportToWorldPoint(new Vector3(1f, 0.5f, zDepth));
			if ((bool)base.transform.parent)
			{
				base.transform.localPosition = new Vector3(base.transform.parent.InverseTransformPoint(position).x, base.transform.localPosition.y, base.transform.localPosition.z);
			}
			else
			{
				base.transform.localPosition = new Vector3(mainCamera.transform.InverseTransformPoint(position).x, base.transform.localPosition.y, base.transform.localPosition.z);
			}
			break;
		}
		}
	}
}
