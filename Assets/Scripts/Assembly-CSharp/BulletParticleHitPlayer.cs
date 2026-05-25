using UnityEngine;

public class BulletParticleHitPlayer : MonoBehaviour
{
	public Collider playerCollider;

	public float hitPoints = 30f;

	private void Start()
	{
		if (!playerCollider)
		{
			playerCollider = GameObject.FindWithTag("Player").GetComponent<Collider>();
		}
	}

	private void OnParticleCollision(GameObject objectHit)
	{
		if (objectHit.GetComponent<Collider>() == playerCollider)
		{
			objectHit.transform.SendMessage("ApplyDamage", new AttackInfo(hitPoints), SendMessageOptions.DontRequireReceiver);
		}
	}
}
