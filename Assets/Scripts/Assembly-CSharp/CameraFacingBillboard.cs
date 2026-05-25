using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour
{
	public Transform MainCamera;

	private Transform xform;

	private void Start()
	{
		if (!MainCamera)
		{
			MainCamera = Camera.main.transform;
		}
		xform = MainCamera.transform;
	}

	private void Update()
	{
		base.transform.LookAt(xform);
	}

	private void OnBecameVisible()
	{
		base.enabled = true;
	}

	private void OnBecameInvisible()
	{
		base.enabled = false;
	}
}
