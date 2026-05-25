using System.Collections;
using UnityEngine;

public class PlayAnimationOnEnable : MonoBehaviour
{
	public bool isPredator;

	public float deactivateTime = 10f;

	public bool autoPlayAnimation = true;

	public bool fadeOutDeactivate;

	public string animationToPlay = "die_fall_front";

	public Transform particleParent;

	public float bloodSprayStartDelay;

	public float bloodSprayDuration = 2f;

	public GameObject boneStructure;

	public GameObject enemyTypeCuchillo;

	public GameObject enemyTypeHanzo;

	public GameObject enemyTypeIsabelle;

	public GameObject enemyTypeMombasa;

	public GameObject enemyTypeNikolai;

	public GameObject enemyTypeNoland;

	public GameObject enemyTypeRoyce;

	public GameObject enemyTypeSniper;

	public GameObject enemyTypeSoldierMachineGun;

	public GameObject enemyTypeSoldierMachete;

	public GameObject enemyTypeStans;

	public GameObject enemyTypeFalconer;

	public GameObject enemyTypeTracker;

	public GameObject enemyTypeMrBlack;

	public GameObject enemyTypeDog;

	private Animation anim;

	private void Awake()
	{
		anim = base.GetComponent<Animation>();
	}

	private void OnEnable()
	{
		if (autoPlayAnimation)
		{
			anim[animationToPlay].time = 0f;
			anim.Play();
		}
		StartCoroutine(Deactivate());
	}

	public void PlayAnim(AnimationInfo animationInfo)
	{
		anim[animationInfo.AnimationName].time = 0f;
		anim.Play(animationInfo.AnimationName);
		if (AManager.BloodOn && (bool)particleParent)
		{
			StartCoroutine(SprayBlood());
		}
	}

	public void PlayAnimOnEnemy(AnimationInfo animationInfo)
	{
		switch (animationInfo.Enemy_Type)
		{
		case EnemyType.None:
			base.gameObject.SetActiveRecursively(true);
			break;
		case EnemyType.Cuchillo:
			if ((bool)boneStructure)
			{
				boneStructure.gameObject.SetActiveRecursively(true);
			}
			if ((bool)enemyTypeCuchillo)
			{
				enemyTypeCuchillo.gameObject.active = true;
			}
			break;
		case EnemyType.Dog:
			if ((bool)boneStructure)
			{
				boneStructure.gameObject.SetActiveRecursively(true);
			}
			if ((bool)enemyTypeDog)
			{
				enemyTypeDog.gameObject.active = true;
			}
			break;
		case EnemyType.Falconer:
			if ((bool)boneStructure)
			{
				boneStructure.gameObject.SetActiveRecursively(true);
			}
			if ((bool)enemyTypeFalconer)
			{
				enemyTypeFalconer.gameObject.active = true;
			}
			break;
		case EnemyType.Hanzo:
			if ((bool)boneStructure)
			{
				boneStructure.gameObject.SetActiveRecursively(true);
			}
			if ((bool)enemyTypeHanzo)
			{
				enemyTypeHanzo.gameObject.active = true;
			}
			break;
		case EnemyType.Isabelle:
			if ((bool)boneStructure)
			{
				boneStructure.gameObject.SetActiveRecursively(true);
			}
			if ((bool)enemyTypeIsabelle)
			{
				enemyTypeIsabelle.gameObject.active = true;
			}
			break;
		case EnemyType.Mombasa:
			if ((bool)boneStructure)
			{
				boneStructure.gameObject.SetActiveRecursively(true);
			}
			if ((bool)enemyTypeMombasa)
			{
				enemyTypeMombasa.gameObject.active = true;
			}
			break;
		case EnemyType.Nikolai:
			if ((bool)boneStructure)
			{
				boneStructure.gameObject.SetActiveRecursively(true);
			}
			if ((bool)enemyTypeNikolai)
			{
				enemyTypeNikolai.gameObject.active = true;
			}
			break;
		case EnemyType.Noland:
			if ((bool)boneStructure)
			{
				boneStructure.gameObject.SetActiveRecursively(true);
			}
			if ((bool)enemyTypeNoland)
			{
				enemyTypeNoland.gameObject.active = true;
			}
			break;
		case EnemyType.Royce:
			if ((bool)boneStructure)
			{
				boneStructure.gameObject.SetActiveRecursively(true);
			}
			if ((bool)enemyTypeRoyce)
			{
				enemyTypeRoyce.gameObject.active = true;
			}
			break;
		case EnemyType.Sniper:
			if ((bool)boneStructure)
			{
				boneStructure.gameObject.SetActiveRecursively(true);
			}
			if ((bool)enemyTypeSniper)
			{
				enemyTypeSniper.gameObject.active = true;
			}
			break;
		case EnemyType.SoldierMachete:
			if ((bool)boneStructure)
			{
				boneStructure.gameObject.SetActiveRecursively(true);
			}
			if ((bool)enemyTypeSoldierMachete)
			{
				enemyTypeSoldierMachete.gameObject.active = true;
			}
			break;
		case EnemyType.SoldierRifle:
			if ((bool)boneStructure)
			{
				boneStructure.gameObject.SetActiveRecursively(true);
			}
			if ((bool)enemyTypeSoldierMachineGun)
			{
				enemyTypeSoldierMachineGun.gameObject.active = true;
			}
			break;
		case EnemyType.Stans:
			if ((bool)boneStructure)
			{
				boneStructure.gameObject.SetActiveRecursively(true);
			}
			if ((bool)enemyTypeStans)
			{
				enemyTypeStans.gameObject.active = true;
			}
			break;
		case EnemyType.MrBlack:
			if ((bool)boneStructure)
			{
				boneStructure.gameObject.SetActiveRecursively(true);
			}
			if ((bool)enemyTypeMrBlack)
			{
				enemyTypeMrBlack.gameObject.active = true;
			}
			break;
		case EnemyType.Tracker:
			if ((bool)boneStructure)
			{
				boneStructure.gameObject.SetActiveRecursively(true);
			}
			if ((bool)enemyTypeTracker)
			{
				enemyTypeTracker.gameObject.active = true;
			}
			break;
		}
		anim[animationInfo.AnimationName].time = 0f;
		anim.Play(animationInfo.AnimationName);
		if (AManager.BloodOn && (bool)particleParent)
		{
			StartCoroutine(SprayBlood());
		}
	}

	private IEnumerator SprayBlood()
	{
		yield return new WaitForSeconds(bloodSprayStartDelay);
		Transform bloodEffect = ((!isPredator) ? AManager.instance.GetPoolObject(AManager.PoolObjectType.BloodSpraySmall) : AManager.instance.GetPoolObject(AManager.PoolObjectType.BloodSprayPredatorAnimated));
		if ((bool)bloodEffect)
		{
			bloodEffect.gameObject.active = true;
			ParticleEmitter bloodEmitter = bloodEffect.GetComponent<ParticleEmitter>();
			if ((bool)bloodEmitter)
			{
				bloodEffect.position = particleParent.position;
				bloodEffect.rotation = particleParent.rotation;
				bloodEffect.parent = particleParent;
				bloodEmitter.emit = true;
				yield return new WaitForSeconds(bloodSprayDuration);
				bloodEmitter.emit = false;
			}
		}
	}

	private IEnumerator Deactivate()
	{
		yield return new WaitForSeconds(deactivateTime);
		base.gameObject.SetActiveRecursively(false);
	}
}
