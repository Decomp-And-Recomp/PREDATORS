using UnityEngine;

public class BillboardFixed : MonoBehaviour
{
	public Transform MainCamera;

	private Transform xForm;

	private Transform cameraXform;

	private Vector3 cameraBackVector;

	private Vector3 cameraUpVector;

	private void Start()
	{
		if (!MainCamera)
		{
			MainCamera = Camera.main.transform;
		}
		cameraXform = MainCamera.transform;
		xForm = base.transform;
		cameraBackVector = cameraXform.rotation * Vector3.back;
		cameraUpVector = cameraXform.rotation * Vector3.up;
	}

	private void LateUpdate()
	{
		base.transform.LookAt(xForm.position + cameraBackVector, cameraUpVector);
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
