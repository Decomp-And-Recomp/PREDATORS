using UnityEngine;

public class ConstantVelocity : MonoBehaviour
{
	public Vector3 direction = Vector3.forward;

	private void Start()
	{
	}

	private void Update()
	{
		base.GetComponent<Rigidbody>().velocity = base.transform.rotation * direction;
	}
}
