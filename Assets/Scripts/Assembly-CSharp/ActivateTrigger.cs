using UnityEngine;

public class ActivateTrigger : MonoBehaviour
{
	public GameObject objectToActivate;

	public Collider playerCollider;

	private bool triggerUsed;

	private void Start()
	{
		if (!playerCollider)
		{
			playerCollider = GameObject.FindWithTag("Player").GetComponent<Collider>();
		}
	}

	public void OnTriggerEnter(Collider col)
	{
		if (!triggerUsed && col == playerCollider)
		{
			triggerUsed = true;
			objectToActivate.active = true;
		}
	}
}
