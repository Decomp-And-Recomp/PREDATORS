using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{
	public Transform newGrenade;

	public float reloadTime = 1f;

	public float elevationForce = 5f;

	public float throwForce = 30f;

	private float lastShot = -10f;

	private void Update()
	{
		if (Input.GetButtonDown("Fire3") && Time.time > reloadTime + lastShot)
		{
			Transform transform = (Transform)Object.Instantiate(newGrenade, base.transform.position + Vector3.up, Quaternion.identity);
			transform.GetComponent<Rigidbody>().AddForce(base.transform.TransformDirection(new Vector3(0f, elevationForce, throwForce)) + base.transform.forward);
			Physics.IgnoreCollision(transform.GetComponent<Rigidbody>().GetComponent<Collider>(), base.transform.root.GetComponent<Collider>());
			lastShot = Time.time;
		}
	}
}
