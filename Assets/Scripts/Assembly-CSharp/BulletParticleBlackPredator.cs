using UnityEngine;

public class BulletParticleBlackPredator : MonoBehaviour
{
	public Transform attackerParent;

	public float hitPoints = 30f;

	private Transform explosionEffect;

	public bool plasmaParticle;

	private AttackInfo attackInfo;

	private void Awake()
	{
		attackInfo = new AttackInfo(hitPoints, Vector3.zero, 1);
	}

	private void OnParticleCollision(GameObject objectHit)
	{
		if ((bool)attackerParent)
		{
			attackInfo.AttackerPosition = attackerParent.position;
		}
		if (plasmaParticle)
		{
			explosionEffect = AManager.instance.GetPoolObject(AManager.PoolObjectType.ExplosionPlasmaGunBlue);
			if ((bool)explosionEffect)
			{
				explosionEffect.position = objectHit.transform.position + Vector3.up;
				explosionEffect.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
			}
		}
		objectHit.transform.SendMessage("ApplyDamageBlackPredatorPlasma", attackInfo, SendMessageOptions.DontRequireReceiver);
	}
}
