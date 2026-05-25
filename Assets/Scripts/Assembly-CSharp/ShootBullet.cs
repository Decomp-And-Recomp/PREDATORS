using UnityEngine;

public class ShootBullet : MonoBehaviour
{
	public Transform newBullet;

	public float reloadTime = 0.1f;

	private float lastShot = -10f;

	public void FireBullets()
	{
		if (Time.time > reloadTime + lastShot)
		{
			Transform transform = (Transform)Object.Instantiate(newBullet, base.transform.position + Vector3.up, base.transform.rotation);
			Transform transform2 = (Transform)Object.Instantiate(newBullet, base.transform.position + Vector3.up, base.transform.rotation);
			Transform transform3 = (Transform)Object.Instantiate(newBullet, base.transform.position + Vector3.up, base.transform.rotation);
			transform2.Rotate(0f, -20f, 0f);
			transform3.Rotate(0f, 20f, 0f);
			Physics.IgnoreCollision(transform.GetComponent<Collider>(), base.transform.root.GetComponent<Collider>());
			Physics.IgnoreCollision(transform2.GetComponent<Collider>(), base.transform.root.GetComponent<Collider>());
			Physics.IgnoreCollision(transform3.GetComponent<Collider>(), base.transform.root.GetComponent<Collider>());
			Physics.IgnoreCollision(transform.GetComponent<Collider>(), transform2.GetComponent<Collider>());
			Physics.IgnoreCollision(transform2.GetComponent<Collider>(), transform3.GetComponent<Collider>());
			Physics.IgnoreCollision(transform3.GetComponent<Collider>(), transform.GetComponent<Collider>());
			lastShot = Time.time;
		}
	}
}
