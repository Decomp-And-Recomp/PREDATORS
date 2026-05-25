using UnityEngine;

public class ArrowRespawnPoint : MonoBehaviour
{
	public Vector3 targetPoint = Vector3.zero;

	private void Update()
	{
		Quaternion to = Quaternion.LookRotation(targetPoint + new Vector3(0f, 5.5f, 0f) - base.transform.position);
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, to, Time.deltaTime);
	}
}
