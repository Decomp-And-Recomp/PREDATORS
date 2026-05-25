using UnityEngine;

public class TimedExplosion : MonoBehaviour
{
	public GameObject explosionPrefab;

	public float explodeSecs = 10f;

	public float explosionRadius = 5f;

	public float removeExplosionSecs = 0.3f;

	public float explosionDamage = 50f;

	private float creationTime;

	private void Start()
	{
		creationTime = Time.time;
	}

	private void Update()
	{
		if (Time.time > creationTime + explodeSecs)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(explosionPrefab, base.transform.position, base.transform.rotation);
			Collider[] array = Physics.OverlapSphere(base.transform.position, explosionRadius);
			Collider[] array2 = array;
			foreach (Collider collider in array2)
			{
				Vector3 a = collider.ClosestPointOnBounds(base.transform.position);
				float num = Vector3.Distance(a, base.transform.position);
				float num2 = 1f - Mathf.Clamp01(num / explosionRadius);
				num2 *= explosionDamage;
				collider.SendMessageUpwards("ApplyDamage", num2, SendMessageOptions.DontRequireReceiver);
			}
			Object.Destroy(base.gameObject);
			Object.Destroy(gameObject.gameObject, removeExplosionSecs);
		}
	}
}
