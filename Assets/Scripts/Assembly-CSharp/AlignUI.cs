using System.Collections;
using UnityEngine;

public class AlignUI : MonoBehaviour
{
	public enum AlignPosition
	{
		Custom = 0,
		Left = 1,
		Right = 2
	}

	public AlignPosition alignPosition;

	public Camera mainCamera;

	public Vector2 viewportCoords = new Vector2(50f, 50f);

	private static int checksForResolutionChange;

	private float zDepth = 0.1f;

	private int screenWidth = 10;

	private int screenHeight = 10;

	private void Awake()
	{
		if (!mainCamera)
		{
			mainCamera = Camera.main;
		}
		zDepth = mainCamera.transform.InverseTransformPoint(base.transform.position).z;
		AlignBasedOnCamera();
	}

	private IEnumerator WaitForRealSeconds(float amount)
	{
		float realDeltaTime;
		for (float timer = 0f; timer < amount; timer += realDeltaTime)
		{
			float lastFrameRealTime = Time.realtimeSinceStartup;
			yield return null;
			float newFrameRealTime = Time.realtimeSinceStartup;
			realDeltaTime = newFrameRealTime - lastFrameRealTime;
		}
	}

	private IEnumerator CheckScreenSize()
	{
		checksForResolutionChange++;
		while (true)
		{
			if (Screen.width != screenWidth || Screen.height != screenHeight)
			{
				screenWidth = Screen.width;
				screenHeight = Screen.height;
				AlignBasedOnCamera();
			}
			for (int i = 0; i < 100; i++)
			{
				yield return null;
			}
		}
	}

	private void AlignBasedOnCamera()
	{
		switch (alignPosition)
		{
		case AlignPosition.Custom:
			base.transform.position = mainCamera.ViewportToWorldPoint(new Vector3(viewportCoords.x / 100f, viewportCoords.y / 100f, zDepth));
			break;
		case AlignPosition.Left:
		{
			Vector3 position2 = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0.5f, zDepth));
			base.transform.localPosition = new Vector3(mainCamera.transform.InverseTransformPoint(position2).x, base.transform.localPosition.y, base.transform.localPosition.z);
			break;
		}
		case AlignPosition.Right:
		{
			Vector3 position = mainCamera.ViewportToWorldPoint(new Vector3(1f, 0.5f, zDepth));
			base.transform.localPosition = new Vector3(mainCamera.transform.InverseTransformPoint(position).x, base.transform.localPosition.y, base.transform.localPosition.z);
			break;
		}
		}
	}
}
