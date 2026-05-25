using System.Collections;
using UnityEngine;

public class Nikolai : BaseEnemy
{
	private const float animGrabbedStartLength = 1.2f;

	private const float animShotGunMeleeStartIdle = 1f / 6f;

	private const float animShotGunMeleeEndMove = 0.5f;

	private const float animGetHitBlockingMoveStart = 7f / 30f;

	private const float animGetHitBlockingIdleEnd = 1f / 3f;

	private const float animKnifeAttackLStartIdle = 0.2f;

	private const float animKnifeAttackLMove = 2f / 15f;

	private const float animKnifeAttackLEndIdle = 7f / 30f;

	private const float animKnifeAttackRStartIdle = 0.2f;

	private const float animKnifeAttackRMove = 2f / 15f;

	private const float animKnifeAttackREndIdle = 7f / 30f;

	private const float animKnifeAttackHeavyMove = 1f / 3f;

	private const float animKnifeAttackHeavyEndIdle = 0.1f;

	private const float animGrabbedHitHigh = 0.6f;

	private const float animGrabbedHitLow = 0.6f;

	private const float animGrabbedHeadOffStart = 1.5f;

	public float blockModeTime = 4f;

	public float meleeRadius = 2f;

	public MeshRenderer muzzleFlash;

	public ParticleEmitter particleBulletsEmitter;

	public AudioClip soundShoot;

	public AudioClip soundMinigunWindUp;

	public int shotsToFire = 4;

	public float shootRate = 0.1f;

	private float shootTimerMoveForwardInterval = 7f;

	private int s;

	private IEnumerator PerformAnimation(EnemyAnimation attackAnimationType)
	{
		switch (attackAnimationType)
		{
		case EnemyAnimation.MeleeShotgunAttack:
		{
			anim["shotgun_melee"].time = 0f;
			anim.CrossFade("shotgun_melee", 0.1f);
			Vector3 attackDirection = (currentTarget.position - xForm.position).normalized;
			float timer2 = 1f / 6f;
			base.targetRotation = Quaternion.LookRotation(attackDirection);
			while (timer2 > 0f)
			{
				timer2 -= Time.deltaTime;
				xForm.rotation = Quaternion.Slerp(xForm.rotation, base.targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			Collider[] colliders = Physics.OverlapSphere(xForm.TransformPoint(meleeOffset), meleeRadiusDamage);
			Collider[] array = colliders;
			foreach (Collider hit in array)
			{
				if (hit == currentTarget.GetComponent<Collider>())
				{
					attackInfoEnemy.Damage = meleeDamage;
					attackInfoEnemy.AttackerPosition = xForm.position;
					attackInfoEnemy.AnimationNr = 1;
					currentTarget.SendMessage("ApplyDamage", attackInfoEnemy, SendMessageOptions.DontRequireReceiver);
					break;
				}
			}
			timer2 = 0.5f;
			while (timer2 > 0f)
			{
				timer2 -= Time.deltaTime;
				characterController.SimpleMove(attackDirection * speedShotgunMelee);
				xForm.rotation = Quaternion.Slerp(xForm.rotation, base.targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			break;
		}
		case EnemyAnimation.GetHitBlocking:
		{
			anim["block_shotgun_gethit"].time = 0f;
			anim.CrossFade("block_shotgun_gethit", 0.1f);
			Vector3 hitDirection = (xForm.position - currentTarget.position).normalized;
			float timer3 = 7f / 30f;
			Quaternion targetRotation = Quaternion.LookRotation(hitDirection);
			while (timer3 > 0f)
			{
				timer3 -= Time.deltaTime;
				targetRotation = Quaternion.Slerp(xForm.rotation, targetRotation, Time.deltaTime * rotateSpeed * 100f);
				characterController.SimpleMove(hitDirection * speedHurt);
				yield return null;
			}
			yield return new WaitForSeconds(1f / 3f);
			break;
		}
		}
	}

	protected override IEnumerator Block()
	{
		float blockTimer = 0f;
		blocking = true;
		anim.CrossFade("block_shotgun_idle");
		while (!isDead)
		{
			blockTimer += Time.deltaTime;
			if (blockTimer > blockModeTime)
			{
				nextState = State.StateAttack;
				blocking = false;
				break;
			}
			RotateTowardsPosition(currentTarget.position, rotateSpeed);
			if ((xForm.position - currentTarget.position).sqrMagnitude > attackDistance * attackDistance)
			{
				nextState = State.StatePatrol;
				blocking = false;
				break;
			}
			if (AManager.PredatorInvisible && currentTarget == playerController.transform)
			{
				nextState = State.StateBerserkFire;
				blocking = false;
				lastSeenPosition = new Vector3(currentTarget.position.x, currentTarget.position.y, currentTarget.position.z);
				break;
			}
			yield return null;
		}
	}

	protected override IEnumerator Berserk()
	{
		anim["panic_fire"].time = 0f;
		anim.CrossFade("panic_fire");
		float berserkTime = anim["panic_fire"].length;
		float timeStartedFiring = Time.time;
		while (!isDead)
		{
			for (s = 0; s < shotsToFire; s++)
			{
				particleBulletsEmitter.Emit(1);
				StartCoroutine(ShowMuzzleFlash());
				if (Utils.SfxOn)
				{
					base.GetComponent<AudioSource>().PlayOneShot(soundShoot);
				}
				yield return new WaitForSeconds(shootRate);
			}
			if (!AManager.PredatorInvisible && (xForm.position - currentTarget.position).sqrMagnitude < attackShootDistance * attackShootDistance)
			{
				nextState = State.StateAttack;
				break;
			}
			if (Time.time - timeStartedFiring > berserkTime)
			{
				nextState = State.StateRunaway;
				break;
			}
			yield return null;
		}
	}

	public override void Activate(int indexShooterLevel)
	{
		if ((bool)muzzleFlash)
		{
			muzzleFlash.enabled = false;
		}
		if ((bool)particleBulletsEmitter)
		{
			particleBulletsEmitter.emit = false;
		}
		anim["panic_fire"].wrapMode = WrapMode.Once;
		anim["shoot_idle"].wrapMode = WrapMode.Loop;
		anim["shoot_loop"].wrapMode = WrapMode.Loop;
		anim["shotgun_melee"].wrapMode = WrapMode.Once;
		anim["block_shotgun_idle"].wrapMode = WrapMode.Loop;
		anim["block_shotgun_gethit"].wrapMode = WrapMode.Once;
		anim["strafe_L_walk_machinegun"].wrapMode = WrapMode.Loop;
		anim["strafe_R_walk_machinegun"].wrapMode = WrapMode.Loop;
		base.Activate(indexShooterLevel);
	}

	private IEnumerator ShowMuzzleFlash()
	{
		if ((bool)muzzleFlash)
		{
			muzzleFlash.enabled = true;
			yield return null;
			yield return null;
			muzzleFlash.enabled = false;
		}
		yield return null;
	}

	protected override IEnumerator Attack()
	{
		shootTimerMoveForwardInterval += Random.Range(-1f, 1f);
		float blockTimer = 0f;
		float windupTimer = 0f;
		while (!isDead)
		{
			blockTimer += Time.deltaTime;
			float predatorTargetSquareDistance = (xForm.position - currentTarget.position).sqrMagnitude;
			float angle = RotateTowardsPosition(currentTarget.position, rotateSpeed);
			if (predatorTargetSquareDistance < attackDistance * attackDistance)
			{
				if (AManager.PredatorInvisible && currentTarget == playerController.transform)
				{
					nextState = State.StateBerserkFire;
					lastSeenPosition = currentTarget.position;
					break;
				}
				if (SqrDistance(xForm.position, currentTarget.position) < meleeRadius * meleeRadius && angle > -10f && angle < 10f)
				{
					if (canPerformMeleeAttack)
					{
						StartCoroutine(CanPerformMeleeAttack());
						yield return StartCoroutine("PerformAnimation", EnemyAnimation.MeleeShotgunAttack);
					}
					else
					{
						if (Random.value < chanceToBlock && blockTimer > blockModeTime && canBlock)
						{
							nextState = State.StateBlock;
							break;
						}
						anim.CrossFade("idle_searching");
					}
				}
				if (predatorTargetSquareDistance < attackShootDistance * attackShootDistance && canShoot)
				{
					shootTimerMoveForward += Time.deltaTime;
					shootTimer += Time.deltaTime;
					if (shootTimer > shootRateSeconds)
					{
						if (windupTimer == 0f)
						{
							anim.CrossFade("shoot_idle");
							if (Utils.SfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMinigunWindUp);
							}
						}
						windupTimer += Time.deltaTime;
						if (AManager.PredatorInvisible && playerController.transform == currentTarget)
						{
							nextState = State.StateBerserkFire;
							lastSeenPosition = currentTarget.position;
							break;
						}
						RotateTowardsPosition(currentTarget.position, rotateSpeed);
						if (windupTimer > 1f)
						{
							for (s = 0; s < shotsToFire; s++)
							{
								particleBulletsEmitter.Emit(1);
								StartCoroutine(ShowMuzzleFlash());
								if (Utils.SfxOn)
								{
									base.GetComponent<AudioSource>().PlayOneShot(soundShoot);
								}
								anim.CrossFade("shoot_loop");
								yield return new WaitForSeconds(shootRate);
							}
							shootTimer = 0f;
							windupTimer = 0f;
						}
					}
					else
					{
						anim.CrossFade("shoot_idle");
					}
					if (shootTimerMoveForward > shootTimerMoveForwardInterval)
					{
						shootTimerMoveForward = 0f;
						canShoot = false;
						StartCoroutine(CanShootAgain());
					}
				}
				else if (predatorTargetSquareDistance > 1f)
				{
					particleBulletsEmitter.emit = false;
					if (angle > -5f && angle < 5f)
					{
						characterController.SimpleMove(xForm.forward * speedAttack);
						anim.CrossFade("jog_weapon_up");
					}
					else
					{
						anim.CrossFade("idle_searching");
					}
					if (characterController.velocity.sqrMagnitude < speedAttack * speedAttack * 0.5f && checkBlocking && lastColliderHit != null && foundObstacle)
					{
						foundObstacle = false;
						if (SelectGoAroundWaypoint())
						{
							yield return StartCoroutine("GoAround");
							anim.CrossFade("jog_weapon_up");
						}
					}
				}
				else
				{
					anim.CrossFade("shoot_idle");
				}
				yield return null;
				continue;
			}
			nextState = State.StatePatrol;
			break;
		}
	}

	protected override State stateIfPredatorNotVisibleButDetected()
	{
		if (currentTarget == playerController.transform)
		{
			return State.StateBerserkFire;
		}
		return State.StateAttack;
	}

	protected override State stateAfterGettingHurtAndPredatorInvisible()
	{
		if (currentTarget == playerController.transform)
		{
			return State.StateBerserkFire;
		}
		return State.StateAttack;
	}

	protected override void EnemyDied()
	{
		if ((bool)muzzleFlash)
		{
			muzzleFlash.enabled = false;
		}
		if ((bool)particleBulletsEmitter)
		{
			particleBulletsEmitter.emit = false;
		}
	}

	protected override void EnemyDiedCleanup(int index, DeathType deathType)
	{
		RemoveSelfFromArray();
		survivalMissionController.EnemyNikolaiDied(index, deathType);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(base.transform.position, meleeRadius);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(base.transform.position, attackDistance);
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(base.transform.position, attackShootDistance);
	}
}
