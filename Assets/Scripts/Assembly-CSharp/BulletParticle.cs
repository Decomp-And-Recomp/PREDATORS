using UnityEngine;

public class BulletParticle : MonoBehaviour
{
	public Transform attackerParent;

	public float hitPoints = 30f;

	private Transform explosionEffect;

	public bool playerShootsTheParticle;

	public bool plasmaParticle;

	public int plasmaParticleLevel = 1;

	private AttackInfo attackInfo;

	private void Awake()
	{
		attackInfo = new AttackInfo(hitPoints, Vector3.zero, 1);
		if (playerShootsTheParticle)
		{
			attackInfo.PredatorAttack = true;
		}
	}

	private void OnParticleCollision(GameObject objectHit)
	{
		if ((bool)attackerParent)
		{
			attackInfo.AttackerPosition = attackerParent.position;
		}
		if (plasmaParticle)
		{
			switch (plasmaParticleLevel)
			{
			case 1:
				explosionEffect = AManager.instance.GetPoolObject(AManager.PoolObjectType.ExplosionPlasmaGunBlue);
				break;
			case 2:
				explosionEffect = AManager.instance.GetPoolObject(AManager.PoolObjectType.ExplosionPlasmaGunYellow);
				break;
			case 3:
				explosionEffect = AManager.instance.GetPoolObject(AManager.PoolObjectType.ExplosionPlasmaGunRed);
				break;
			}
			if ((bool)explosionEffect)
			{
				explosionEffect.position = objectHit.transform.position + Vector3.up;
				explosionEffect.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
			}
			objectHit.transform.SendMessage("ApplyDamage", attackInfo, SendMessageOptions.DontRequireReceiver);
		}
		else
		{
			objectHit.transform.SendMessage("ApplyDamage", attackInfo, SendMessageOptions.DontRequireReceiver);
		}
	}
}
