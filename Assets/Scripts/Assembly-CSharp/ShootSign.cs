using System.Collections;
using UnityEngine;

public class ShootSign : MonoBehaviour
{
	public Transform objectToActivate;

	public Transform parentDestructable;

	public float destroySignCheckTime = 0.2f;

	public Collider playerCollider;

	private bool triggerUsed;

	private void Start()
	{
		if (!playerCollider)
		{
			playerCollider = GameObject.FindWithTag("Player").GetComponent<Collider>();
		}
		if (!objectToActivate)
		{
			objectToActivate = base.transform.GetChild(0);
		}
	}

	public void OnTriggerEnter(Collider col)
	{
		if (!triggerUsed && col == playerCollider)
		{
			triggerUsed = true;
			if ((bool)objectToActivate)
			{
				objectToActivate.gameObject.active = true;
				StartCoroutine(CheckAndDestroy());
			}
		}
	}

	private IEnumerator CheckAndDestroy()
	{
		while ((bool)parentDestructable)
		{
			yield return new WaitForSeconds(destroySignCheckTime);
		}
		if ((bool)objectToActivate)
		{
			Object.Destroy(objectToActivate.gameObject);
		}
		Object.Destroy(base.gameObject);
	}
}
