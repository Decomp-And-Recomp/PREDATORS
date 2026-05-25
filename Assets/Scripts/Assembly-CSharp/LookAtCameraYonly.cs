using UnityEngine;

public class LookAtCameraYonly : MonoBehaviour
{
	public Camera cameraToLookAt;

	private void Update()
	{
		Vector3 vector = cameraToLookAt.transform.position - base.transform.position;
		vector.x = (vector.z = 0f);
		base.transform.LookAt(cameraToLookAt.transform.position - vector);
	}
}
