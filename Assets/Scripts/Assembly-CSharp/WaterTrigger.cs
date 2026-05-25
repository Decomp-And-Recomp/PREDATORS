using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
	public Collider playerCollider;

	private void Start()
	{
		if (!playerCollider)
		{
			playerCollider = GameObject.FindWithTag("Player").GetComponent<Collider>();
		}
	}

	public void OnTriggerEnter(Collider col)
	{
		if (col == playerCollider)
		{
			playerCollider.SendMessage("WaterEnter", SendMessageOptions.DontRequireReceiver);
		}
	}

	public void OnTriggerExit(Collider col)
	{
		if (col == playerCollider)
		{
			playerCollider.SendMessage("WaterExit", SendMessageOptions.DontRequireReceiver);
		}
	}
}
