using System.Collections;
using UnityEngine;

public class Royce : BaseEnemy
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

	public float attackRate = 2f;

	public float chanceToDoubleCombo = 0.99f;

	public AudioClip soundYell;

	public AudioClip soundShoot;

	public AudioClip soundAxe1;

	public AudioClip soundAxe2;

	private bool canAttack = true;

	public MeshRenderer muzzleFlash;

	public ParticleEmitter particleBulletsEmitter;

	public GameObject axe;

	public GameObject machinegun;

	private float shootTimerMoveForwardInterval = 3f;

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
			Collider[] array3 = colliders;
			foreach (Collider hit in array3)
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
			float timer4 = 7f / 30f;
			Quaternion targetRotation = Quaternion.LookRotation(hitDirection);
			while (timer4 > 0f)
			{
				timer4 -= Time.deltaTime;
				targetRotation = Quaternion.Slerp(xForm.rotation, targetRotation, Time.deltaTime * rotateSpeed * 100f);
				characterController.SimpleMove(hitDirection * speedHurt);
				yield return null;
			}
			yield return new WaitForSeconds(1f / 3f);
			break;
		}
		case EnemyAnimation.KnifeAttackL:
		{
			anim["knife_attack_lightL"].time = 0f;
			anim.CrossFade("knife_attack_lightL", 0.1f);
			float timer7 = 0.2f;
			Vector3 attackDirection4 = currentTarget.position - xForm.position;
			base.targetRotation = Quaternion.LookRotation(attackDirection4);
			while (timer7 > 0f)
			{
				timer7 -= Time.deltaTime;
				xForm.rotation = Quaternion.Slerp(xForm.rotation, base.targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			if (Utils.SfxOn)
			{
				base.GetComponent<AudioSource>().PlayOneShot(soundAxe1);
			}
			timer7 = 2f / 15f;
			attackDirection4 = (currentTarget.position - xForm.position).normalized;
			base.targetRotation = Quaternion.LookRotation(attackDirection4);
			while (timer7 > 0f)
			{
				timer7 -= Time.deltaTime;
				characterController.SimpleMove(attackDirection4 * 5.7f);
				xForm.rotation = Quaternion.Slerp(xForm.rotation, base.targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			Collider[] colliders2 = Physics.OverlapSphere(xForm.TransformPoint(meleeOffset), meleeRadiusDamage);
			Collider[] array2 = colliders2;
			foreach (Collider hit2 in array2)
			{
				if (hit2 == currentTarget.GetComponent<Collider>())
				{
					attackInfoEnemy.Damage = meleeDamage * 1.5f;
					attackInfoEnemy.AttackerPosition = xForm.position;
					attackInfoEnemy.AnimationNr = 1;
					currentTarget.SendMessage("ApplyDamage", attackInfoEnemy, SendMessageOptions.DontRequireReceiver);
					break;
				}
			}
			yield return new WaitForSeconds(7f / 30f);
			break;
		}
		case EnemyAnimation.KnifeAttackR:
		{
			anim["knife_attack_lightR"].time = 0f;
			anim.CrossFade("knife_attack_lightR", 0.1f);
			float timer3 = 0.2f;
			Vector3 attackDirection2 = currentTarget.position - xForm.position;
			base.targetRotation = Quaternion.LookRotation(attackDirection2);
			while (timer3 > 0f)
			{
				timer3 -= Time.deltaTime;
				xForm.rotation = Quaternion.Slerp(xForm.rotation, base.targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			if (Utils.SfxOn)
			{
				base.GetComponent<AudioSource>().PlayOneShot(soundAxe2);
			}
			timer3 = 2f / 15f;
			attackDirection2 = (currentTarget.position - xForm.position).normalized;
			base.targetRotation = Quaternion.LookRotation(attackDirection2);
			while (timer3 > 0f)
			{
				timer3 -= Time.deltaTime;
				characterController.SimpleMove(attackDirection2 * 3.45f);
				xForm.rotation = Quaternion.Slerp(xForm.rotation, base.targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			Collider[] colliders3 = Physics.OverlapSphere(xForm.TransformPoint(meleeOffset), meleeRadiusDamage);
			Collider[] array = colliders3;
			foreach (Collider hit3 in array)
			{
				if (hit3 == currentTarget.GetComponent<Collider>())
				{
					attackInfoEnemy.Damage = meleeDamage;
					attackInfoEnemy.AttackerPosition = xForm.position;
					attackInfoEnemy.AnimationNr = 1;
					currentTarget.SendMessage("ApplyDamage", attackInfoEnemy, SendMessageOptions.DontRequireReceiver);
					break;
				}
			}
			yield return new WaitForSeconds(7f / 30f);
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
				if (mustSwitchToBossMode)
				{
					nextState = State.StateAxeAttack;
					machinegun.active = false;
					axe.active = true;
				}
				else
				{
					nextState = State.StateAttack;
				}
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
				if (mustSwitchToBossMode)
				{
					nextState = State.StateAxeAttack;
					machinegun.active = false;
					axe.active = true;
				}
				else
				{
					nextState = State.StateBerserkFire;
				}
				blocking = false;
				lastSeenPosition = new Vector3(currentTarget.position.x, currentTarget.position.y, currentTarget.position.z);
				break;
			}
			yield return null;
		}
	}

	protected override IEnumerator Berserk()
	{
		float berserkTime = Random.value * 8f;
		float berserkTimer = 0f;
		while (!isDead)
		{
			RotateTowardsPosition(lastSeenPosition + Random.insideUnitSphere * 8f, rotateSpeed);
			shootTimer += Time.deltaTime;
			if ((double)shootTimer > 0.3)
			{
				particleBulletsEmitter.Emit();
				if (Utils.SfxOn)
				{
					base.GetComponent<AudioSource>().PlayOneShot(soundShoot);
				}
				anim["panic_fire"].time = 0f;
				anim.Play("panic_fire");
				shootTimer = 0f;
			}
			if (!AManager.PredatorInvisible && (xForm.position - currentTarget.position).sqrMagnitude < attackShootDistance * attackShootDistance)
			{
				if (mustSwitchToBossMode)
				{
					nextState = State.StateAxeAttack;
					machinegun.active = false;
					axe.active = true;
				}
				else
				{
					nextState = State.StateAttack;
				}
				break;
			}
			if (berserkTimer > berserkTime)
			{
				nextState = State.StateRunaway;
				break;
			}
			berserkTimer += Time.deltaTime;
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
		machinegun.active = true;
		axe.active = false;
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

	protected override IEnumerator GoingIntoBossMode()
	{
		if (Utils.SfxOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(soundYell);
		}
		anim.CrossFade("block_break");
		yield return new WaitForSeconds(anim["block_break"].length);
		nextState = State.StateAxeAttack;
		machinegun.active = false;
		axe.active = true;
	}

	private IEnumerator CanAttackAgain()
	{
		yield return new WaitForSeconds(attackRate);
		canAttack = true;
	}

	protected IEnumerator AxeAttack()
	{
		float blockTimer = 0f;
		while (!isDead)
		{
			blockTimer += Time.deltaTime;
			if (Random.value < chanceToBlock && blockTimer > blockModeTime && canBlock)
			{
				nextState = State.StateBlock;
				break;
			}
			RotateTowardsPosition(currentTarget.position, rotateSpeed);
			if (!canAttack)
			{
				anim.CrossFade("knife_idle_searching");
			}
			if (SqrDistance(xForm.position, currentTarget.position) < meleeRadius * meleeRadius)
			{
				canAttack = false;
				StartCoroutine(CanAttackAgain());
				yield return StartCoroutine("PerformAnimation", EnemyAnimation.KnifeAttackR);
				if (SqrDistance(xForm.position, currentTarget.position) < meleeRadius * meleeRadius && Random.value < chanceToDoubleCombo)
				{
					yield return StartCoroutine("PerformAnimation", EnemyAnimation.KnifeAttackL);
				}
			}
			else
			{
				direction = xForm.TransformDirection(Vector3.forward);
				characterController.SimpleMove(direction * speedAttack);
				anim.CrossFade("jog_weapon_up");
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
			if ((xForm.position - currentTarget.position).sqrMagnitude > attackDistance * attackDistance)
			{
				nextState = State.StatePatrol;
				break;
			}
			if (AManager.PredatorInvisible && currentTarget == playerController.transform)
			{
				nextState = State.StatePanic;
				break;
			}
			yield return null;
		}
	}

	protected override IEnumerator Attack()
	{
		shootTimerMoveForwardInterval += Random.Range(-1f, 1f);
		float blockTimer = 0f;
		while (!isDead)
		{
			blockTimer += Time.deltaTime;
			float predatorTargetSquareDistance = (xForm.position - currentTarget.position).sqrMagnitude;
			float angle = RotateTowardsPosition(currentTarget.position, rotateSpeed);
			if (mustSwitchToBossMode)
			{
				nextState = State.StateGoingIntoBossMode;
				break;
			}
			if (predatorTargetSquareDistance < attackDistance * attackDistance)
			{
				if (AManager.PredatorInvisible && currentTarget == playerController.transform)
				{
					if (mustSwitchToBossMode)
					{
						nextState = State.StateAxeAttack;
						machinegun.active = false;
						axe.active = true;
					}
					else
					{
						nextState = State.StateBerserkFire;
					}
					lastSeenPosition = currentTarget.position;
					break;
				}
				if (predatorTargetSquareDistance < meleeRadius * meleeRadius && angle > -10f && angle < 10f)
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
						particleBulletsEmitter.Emit();
						StartCoroutine(ShowMuzzleFlash());
						if (Utils.SfxOn)
						{
							base.GetComponent<AudioSource>().PlayOneShot(soundShoot);
						}
						anim.CrossFade("shoot_loop");
						shootTimer = 0f;
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
					direction = xForm.TransformDirection(Vector3.forward * speedAttack);
					anim.CrossFade("jog_weapon_up");
					characterController.SimpleMove(direction);
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

	protected override State stateAfterGettingHurtAndPredatorVisible()
	{
		if (Random.value < chanceToBlock && canBlock)
		{
			return State.StateBlock;
		}
		if (mustSwitchToBossMode)
		{
			machinegun.active = false;
			axe.active = true;
			return State.StateAxeAttack;
		}
		return State.StateAttack;
	}

	protected override State stateIfPredatorNotVisibleButDetected()
	{
		if (currentTarget == playerController.transform)
		{
			if (mustSwitchToBossMode)
			{
				machinegun.active = false;
				axe.active = true;
				return State.StateAxeAttack;
			}
			return State.StateBerserkFire;
		}
		return State.StateAttack;
	}

	protected override State stateIfPredatorVisibleAndInRange()
	{
		if (mustSwitchToBossMode)
		{
			machinegun.active = false;
			axe.active = true;
			return State.StateAxeAttack;
		}
		return State.StateAttack;
	}

	protected override State stateAfterGettingHurtAndPredatorInvisible()
	{
		if (currentTarget == playerController.transform)
		{
			if (mustSwitchToBossMode)
			{
				machinegun.active = false;
				axe.active = true;
				return State.StateAxeAttack;
			}
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
		survivalMissionController.EnemyRoyceDied(index, deathType);
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
