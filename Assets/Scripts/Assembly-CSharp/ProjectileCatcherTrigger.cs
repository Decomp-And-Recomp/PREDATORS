using UnityEngine;

public class ProjectileCatcherTrigger : MonoBehaviour
{
	private Transform collisionChild;

	public void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("ProjectileWeapon") && (bool)col.GetComponent<Rigidbody>())
		{
			col.isTrigger = true;
			collisionChild = col.transform.GetChild(0);
			if ((bool)collisionChild)
			{
				collisionChild.gameObject.active = false;
			}
		}
	}
}
