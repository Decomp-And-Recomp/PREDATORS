using UnityEngine;

public class FireBullet : MonoBehaviour
{
	public Transform newBullet;

	public float bulletSpeed = 2f;

	public float reloadTime = 0.5f;

	private float lastShot = -10f;

	public void Fire()
	{
		if (Time.time > reloadTime + lastShot)
		{
			Transform transform = (Transform)Object.Instantiate(newBullet, base.transform.position, base.transform.rotation);
			Physics.IgnoreCollision(transform.GetComponent<Collider>(), base.transform.GetComponent<Collider>());
			lastShot = Time.time;
		}
	}

	public void Fire(Vector3 shootPosition)
	{
		if (Time.time > reloadTime + lastShot)
		{
			Transform transform = (Transform)Object.Instantiate(newBullet, shootPosition, base.transform.rotation);
			Physics.IgnoreCollision(transform.GetComponent<Collider>(), base.transform.GetComponent<Collider>());
			lastShot = Time.time;
		}
	}
}
