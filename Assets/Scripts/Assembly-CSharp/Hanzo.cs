using System.Collections;
using UnityEngine;

public class Hanzo : BaseEnemy
{
	protected enum StrafeActions
	{
		MoveBack = 0,
		MoveFront = 1,
		StrafeLeft = 2,
		StrafeRight = 3,
		Stay = 4
	}

	private const float animGrabbedStartLength = 1.2f;

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

	public AudioClip soundKnife1;

	public AudioClip soundKnife2;

	public float blockModeTime = 5f;

	public float meleeRadius = 2f;

	public float strafeNotEngagedTime = 5f;

	public float strafeRadius = 4f;

	public float chanceToDoubleCombo = 0.99f;

	public float chanceToTripleCombo = 0.99f;

	public float attackRate = 2f;

	protected bool canAttack = true;

	protected bool engagedInCombat;

	protected float speedStrafe = 1.58f;

	protected virtual float AnimKnifeAttackLStartIdle
	{
		get
		{
			return 0.2f;
		}
	}

	protected virtual float AnimKnifeAttackLMove
	{
		get
		{
			return 2f / 15f;
		}
	}

	protected virtual float AnimKnifeAttackLEndIdle
	{
		get
		{
			return 7f / 30f;
		}
	}

	protected virtual float AnimKnifeAttackRStartIdle
	{
		get
		{
			return 0.2f;
		}
	}

	protected virtual float AnimKnifeAttackRMove
	{
		get
		{
			return 2f / 15f;
		}
	}

	protected virtual float AnimKnifeAttackREndIdle
	{
		get
		{
			return 7f / 30f;
		}
	}

	protected virtual float AnimKnifeAttackHeavyMove
	{
		get
		{
			return 1f / 3f;
		}
	}

	protected virtual float AnimKnifeAttackHeavyEndIdle
	{
		get
		{
			return 0.1f;
		}
	}

	protected virtual string getHitBlockingAnimation
	{
		get
		{
			return "knife_block_gethit";
		}
	}

	protected virtual string attackLAnimation
	{
		get
		{
			return "knife_attack_lightL";
		}
	}

	protected virtual string attackRAnimation
	{
		get
		{
			return "knife_attack_lightR";
		}
	}

	protected virtual string heavyAttackAnimation
	{
		get
		{
			return "knife_attack_heavy";
		}
	}

	protected virtual string blockWalkForwardAnimation
	{
		get
		{
			return "knife_idle_searching";
		}
	}

	protected virtual string runToAttackAnimation
	{
		get
		{
			return "jog_weapon_up";
		}
	}

	protected virtual string blockIdleAnimation
	{
		get
		{
			return "knife_block_idle";
		}
	}

	protected virtual string meleeWalkFrontAnimation
	{
		get
		{
			return "melee_walk_front";
		}
	}

	protected virtual string blockUpperMixAnimation
	{
		get
		{
			return "knife_block_upper_mix";
		}
	}

	private float RandomInRange(float min, float max)
	{
		return min + Random.value * (max - min);
	}

	public override void Activate(int indexShooterLevel)
	{
		meleeDamage = 70f;
		if ((bool)anim["melee_strafe_L"])
		{
			anim["melee_strafe_L"].wrapMode = WrapMode.Loop;
		}
		if ((bool)anim["melee_strafe_R"])
		{
			anim["melee_strafe_R"].wrapMode = WrapMode.Loop;
		}
		if ((bool)anim["melee_walk_back"])
		{
			anim["melee_walk_back"].wrapMode = WrapMode.Loop;
		}
		anim[meleeWalkFrontAnimation].wrapMode = WrapMode.Loop;
		anim[attackLAnimation].wrapMode = WrapMode.Once;
		anim[attackRAnimation].wrapMode = WrapMode.Once;
		anim[heavyAttackAnimation].wrapMode = WrapMode.Once;
		anim[blockIdleAnimation].wrapMode = WrapMode.Loop;
		anim[getHitBlockingAnimation].wrapMode = WrapMode.Once;
		anim[blockWalkForwardAnimation].wrapMode = WrapMode.Loop;
		if ((bool)anim[blockUpperMixAnimation])
		{
			anim[blockUpperMixAnimation].wrapMode = WrapMode.Loop;
		}
		if ((bool)BoneMixChest && (bool)anim[blockUpperMixAnimation])
		{
			anim[blockUpperMixAnimation].AddMixingTransform(BoneMixChest, true);
			anim[blockUpperMixAnimation].layer = 10;
		}
		if ((bool)anim[blockUpperMixAnimation])
		{
			anim.Stop(blockUpperMixAnimation);
		}
		canAttack = true;
		engagedInCombat = false;
		base.Activate(indexShooterLevel);
	}

	protected override IEnumerator Panic()
	{
		float panicTimer = 0f;
		while (!isDead)
		{
			RotateTowardsPosition(lastSeenPosition, rotateSpeed);
			float predatorAngle = AngleBetweenXformAnd(currentTarget.position);
			anim.CrossFade("coward_loop");
			panicTimer += Time.deltaTime;
			if (panicTimer > panicTime)
			{
				nextState = State.StateRunaway;
				break;
			}
			if (!AManager.PredatorInvisible)
			{
				if ((xForm.position - currentTarget.position).sqrMagnitude < attackShootDistance * attackShootDistance)
				{
					nextState = stateIfPredatorVisibleAndInRange();
					break;
				}
			}
			else if ((xForm.position - currentTarget.position).sqrMagnitude < cloakDetectionRadius * cloakDetectionRadius && predatorAngle < cloakDetectionAngle && predatorAngle > 0f - cloakDetectionAngle)
			{
				lastSeenPosition = currentTarget.position;
				nextState = State.StateRunaway;
				break;
			}
			yield return null;
		}
	}

	private IEnumerator PerformAnimation(EnemyAnimation attackAnimationType)
	{
		anim.Stop(blockUpperMixAnimation);
		switch (attackAnimationType)
		{
		case EnemyAnimation.GetHitBlocking:
		{
			anim[getHitBlockingAnimation].time = 0f;
			anim.CrossFade(getHitBlockingAnimation, 0.1f);
			Vector3 hitDirection = (xForm.position - currentTarget.position).normalized;
			float timer = 7f / 30f;
			Quaternion targetRotation = Quaternion.LookRotation(hitDirection);
			while (timer > 0f)
			{
				timer -= Time.deltaTime;
				targetRotation = Quaternion.Slerp(xForm.rotation, targetRotation, Time.deltaTime * rotateSpeed * 100f);
				characterController.SimpleMove(hitDirection * speedHurt);
				yield return null;
			}
			yield return new WaitForSeconds(1f / 3f);
			break;
		}
		case EnemyAnimation.KnifeAttackL:
		{
			anim[attackLAnimation].time = 0f;
			anim.CrossFade(attackLAnimation, 0.1f);
			float timer4 = AnimKnifeAttackLStartIdle;
			Vector3 attackDirection3 = currentTarget.position - xForm.position;
			base.targetRotation = Quaternion.LookRotation(attackDirection3);
			while (timer4 > 0f)
			{
				timer4 -= Time.deltaTime;
				xForm.rotation = Quaternion.Slerp(xForm.rotation, base.targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			if (Utils.SfxOn)
			{
				base.GetComponent<AudioSource>().PlayOneShot(soundKnife1);
			}
			timer4 = AnimKnifeAttackLMove;
			attackDirection3 = (currentTarget.position - xForm.position).normalized;
			base.targetRotation = Quaternion.LookRotation(attackDirection3);
			while (timer4 > 0f)
			{
				timer4 -= Time.deltaTime;
				characterController.SimpleMove(attackDirection3 * 5.7f);
				xForm.rotation = Quaternion.Slerp(xForm.rotation, base.targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			Collider[] colliders = Physics.OverlapSphere(xForm.TransformPoint(meleeOffset), meleeRadiusDamage);
			Collider[] array3 = colliders;
			foreach (Collider hit in array3)
			{
				if (hit == currentTarget.GetComponent<Collider>())
				{
					attackInfoEnemy.Damage = meleeDamage * 1.5f;
					attackInfoEnemy.AttackerPosition = xForm.position;
					attackInfoEnemy.AnimationNr = 1;
					currentTarget.SendMessage("ApplyDamage", attackInfoEnemy, SendMessageOptions.DontRequireReceiver);
					break;
				}
			}
			yield return new WaitForSeconds(AnimKnifeAttackLEndIdle);
			break;
		}
		case EnemyAnimation.KnifeAttackR:
		{
			anim[attackRAnimation].time = 0f;
			anim.CrossFade(attackRAnimation, 0.1f);
			float timer7 = AnimKnifeAttackRStartIdle;
			Vector3 attackDirection5 = currentTarget.position - xForm.position;
			base.targetRotation = Quaternion.LookRotation(attackDirection5);
			while (timer7 > 0f)
			{
				timer7 -= Time.deltaTime;
				xForm.rotation = Quaternion.Slerp(xForm.rotation, base.targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			if (Utils.SfxOn)
			{
				base.GetComponent<AudioSource>().PlayOneShot(soundKnife2);
			}
			timer7 = AnimKnifeAttackRMove;
			attackDirection5 = (currentTarget.position - xForm.position).normalized;
			base.targetRotation = Quaternion.LookRotation(attackDirection5);
			while (timer7 > 0f)
			{
				timer7 -= Time.deltaTime;
				characterController.SimpleMove(attackDirection5 * 3.45f);
				xForm.rotation = Quaternion.Slerp(xForm.rotation, base.targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			Collider[] colliders2 = Physics.OverlapSphere(xForm.TransformPoint(meleeOffset), meleeRadiusDamage);
			Collider[] array2 = colliders2;
			foreach (Collider hit2 in array2)
			{
				if (hit2 == currentTarget.GetComponent<Collider>())
				{
					attackInfoEnemy.Damage = meleeDamage;
					attackInfoEnemy.AttackerPosition = xForm.position;
					attackInfoEnemy.AnimationNr = 1;
					currentTarget.SendMessage("ApplyDamage", attackInfoEnemy, SendMessageOptions.DontRequireReceiver);
					break;
				}
			}
			yield return new WaitForSeconds(AnimKnifeAttackREndIdle);
			break;
		}
		case EnemyAnimation.KnifeAttackHeavy:
		{
			anim[heavyAttackAnimation].time = 0f;
			anim.CrossFade(heavyAttackAnimation, 0.1f);
			float timer2 = AnimKnifeAttackHeavyMove;
			Vector3 attackDirection2 = (currentTarget.position - xForm.position).normalized;
			base.targetRotation = Quaternion.LookRotation(attackDirection2);
			while (timer2 > 0f)
			{
				timer2 -= Time.deltaTime;
				characterController.SimpleMove(attackDirection2 * 2.46f);
				xForm.rotation = Quaternion.Slerp(xForm.rotation, base.targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			if (Utils.SfxOn)
			{
				base.GetComponent<AudioSource>().PlayOneShot(soundKnife1);
			}
			timer2 = AnimKnifeAttackHeavyEndIdle;
			attackDirection2 = currentTarget.position - xForm.position;
			base.targetRotation = Quaternion.LookRotation(attackDirection2);
			while (timer2 > 0f)
			{
				timer2 -= Time.deltaTime;
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
			break;
		}
		}
	}

	private IEnumerator StrafeAround()
	{
		float strafeTimer = 0f;
		string animation = "walk_weapon_up";
		StrafeActions strafe = StrafeActions.MoveFront;
		while (!isDead)
		{
			if ((xForm.position - currentTarget.position).sqrMagnitude > attackDistance * attackDistance)
			{
				nextState = State.StatePatrol;
				break;
			}
			if (MeleesEngagedCountOk() && canAttack)
			{
				nextState = State.StateAttack;
				break;
			}
			if (AManager.PredatorInvisible && currentTarget == playerController.transform)
			{
				nextState = State.StatePanic;
				lastSeenPosition = new Vector3(currentTarget.position.x, currentTarget.position.y, currentTarget.position.z);
				break;
			}
			strafeTimer += Time.deltaTime;
			RotateTowardsPosition(currentTarget.position, rotateSpeed);
			if (strafeTimer > RandomInRange(strafeNotEngagedTime / 2f, strafeNotEngagedTime))
			{
				strafeTimer = 0f;
				strafe = ((!RandomBool()) ? StrafeActions.Stay : (RandomBool() ? ((!(SqrDistance(xForm.position, currentTarget.position) < strafeRadius * strafeRadius)) ? StrafeActions.MoveFront : StrafeActions.MoveBack) : ((!RandomBool()) ? StrafeActions.StrafeRight : StrafeActions.StrafeLeft)));
			}
			switch (strafe)
			{
			case StrafeActions.MoveBack:
				direction = xForm.TransformDirection(Vector3.back);
				animation = "melee_walk_back";
				break;
			case StrafeActions.MoveFront:
				direction = xForm.TransformDirection(Vector3.forward);
				animation = "melee_walk_front";
				break;
			case StrafeActions.StrafeLeft:
				direction = Vector3.Cross(Vector3.up, currentTarget.position - xForm.position).normalized;
				animation = "melee_strafe_R";
				break;
			case StrafeActions.StrafeRight:
				direction = -Vector3.Cross(Vector3.up, currentTarget.position - xForm.position).normalized;
				animation = "melee_strafe_L";
				break;
			case StrafeActions.Stay:
				direction = Vector3.zero;
				animation = "knife_idle_searching";
				break;
			default:
				direction = Vector3.zero;
				animation = "knife_idle_searching";
				break;
			}
			anim.CrossFade(animation);
			characterController.SimpleMove(direction * speedStrafe);
			if (characterController.velocity.sqrMagnitude < speedStrafe * speedStrafe * 0.5f && checkBlocking && lastColliderHit != null && foundObstacle)
			{
				foundObstacle = false;
				if (SelectGoAroundWaypoint())
				{
					yield return StartCoroutine("GoAround");
					anim.CrossFade(animation);
				}
			}
			if (strafe != StrafeActions.Stay && (double)characterController.velocity.sqrMagnitude < (double)(speedStrafe * speedStrafe) * 0.5)
			{
				strafeTimer = strafeNotEngagedTime;
			}
			yield return null;
		}
	}

	protected IEnumerator CanAttackAgain()
	{
		yield return new WaitForSeconds(attackRate);
		canAttack = true;
	}

	protected override IEnumerator EnemyStateLoop()
	{
		while (!isDead)
		{
			anim.Stop(blockUpperMixAnimation);
			if (engagedInCombat)
			{
				engagedInCombat = false;
				AManager.instance.decreaseMeleesEngagedCount();
			}
			if (AManager.instance.CinematicInProgress)
			{
				yield return StartCoroutine("Patrol");
			}
			else
			{
				switch (nextState)
				{
				case State.StateAttack:
					StopCoroutine("UpdateCurrentTarget");
					StartCoroutine("UpdateCurrentTarget");
					yield return StartCoroutine("Attack");
					break;
				case State.StatePatrol:
					yield return StartCoroutine("Patrol");
					break;
				case State.StatePanic:
					yield return StartCoroutine("Panic");
					break;
				case State.StateRunaway:
					yield return StartCoroutine("RunAway");
					break;
				case State.StateRelocate:
					yield return StartCoroutine("Relocate");
					break;
				case State.StateBlock:
					yield return StartCoroutine("Block");
					break;
				case State.StateBerserkFire:
					yield return StartCoroutine("Berserk");
					break;
				case State.StateStrafe:
					yield return StartCoroutine("StrafeAround");
					break;
				}
			}
			yield return null;
		}
	}

	protected override IEnumerator Attack()
	{
		float blockTimer = 0f;
		while (!isDead)
		{
			if (currentTarget == playerController.transform && !engagedInCombat)
			{
				if (!MeleesEngagedCountOk())
				{
					nextState = State.StateStrafe;
					break;
				}
				engagedInCombat = true;
				AManager.instance.increaseMeleesEngagedCount();
			}
			blockTimer += Time.deltaTime;
			if (Random.value < chanceToBlock && blockTimer > blockModeTime && canBlock)
			{
				nextState = State.StateBlock;
				break;
			}
			RotateTowardsPosition(currentTarget.position, rotateSpeed);
			if (!canAttack)
			{
				anim.CrossFade(blockWalkForwardAnimation);
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
				anim.CrossFade(runToAttackAnimation);
				if (characterController.velocity.sqrMagnitude < speedAttack * speedAttack * 0.5f && checkBlocking && lastColliderHit != null && foundObstacle)
				{
					foundObstacle = false;
					if (SelectGoAroundWaypoint())
					{
						yield return StartCoroutine("GoAround");
						anim.CrossFade(runToAttackAnimation);
					}
				}
			}
			if ((xForm.position - currentTarget.position).sqrMagnitude > attackDistance * attackDistance)
			{
				nextState = State.StatePatrol;
				if (engagedInCombat)
				{
					engagedInCombat = false;
					AManager.instance.decreaseMeleesEngagedCount();
				}
				break;
			}
			if (AManager.PredatorInvisible && currentTarget == playerController.transform)
			{
				nextState = State.StatePanic;
				if (engagedInCombat)
				{
					engagedInCombat = false;
					AManager.instance.decreaseMeleesEngagedCount();
				}
				break;
			}
			yield return null;
		}
	}

	protected override IEnumerator Block()
	{
		if (engagedInCombat)
		{
			engagedInCombat = false;
			AManager.instance.decreaseMeleesEngagedCount();
		}
		float blockTimer = 0f;
		while (!isDead)
		{
			blockTimer += Time.deltaTime;
			if (blockTimer > blockModeTime)
			{
				blocking = false;
				nextState = stateIfPredatorVisibleAndInRange();
				break;
			}
			RotateTowardsPosition(currentTarget.position, rotateSpeed);
			if (SqrDistance(xForm.position, currentTarget.position) < meleeRadius * meleeRadius)
			{
				blocking = true;
				anim.Stop(blockUpperMixAnimation);
				anim.CrossFade(blockIdleAnimation);
			}
			else
			{
				blocking = true;
				direction = xForm.TransformDirection(Vector3.forward);
				characterController.SimpleMove(direction * speedStrafe);
				anim.CrossFade(blockUpperMixAnimation);
				anim.CrossFade(meleeWalkFrontAnimation);
				if (characterController.velocity.sqrMagnitude < speedStrafe * speedStrafe * 0.5f && checkBlocking && lastColliderHit != null && foundObstacle)
				{
					foundObstacle = false;
					if (SelectGoAroundWaypoint())
					{
						yield return StartCoroutine("GoAround");
						anim.CrossFade(blockUpperMixAnimation);
						anim.CrossFade(meleeWalkFrontAnimation);
					}
				}
			}
			if ((xForm.position - currentTarget.position).sqrMagnitude > attackDistance * attackDistance)
			{
				blocking = false;
				nextState = State.StatePatrol;
				if (engagedInCombat)
				{
					engagedInCombat = false;
					AManager.instance.decreaseMeleesEngagedCount();
				}
				break;
			}
			if (AManager.PredatorInvisible && currentTarget == playerController.transform)
			{
				blocking = false;
				nextState = State.StatePanic;
				if (engagedInCombat)
				{
					engagedInCombat = false;
					AManager.instance.decreaseMeleesEngagedCount();
				}
				break;
			}
			yield return null;
		}
	}

	protected override State stateAfterGettingHurtAndPredatorVisible()
	{
		return stateIfPredatorVisibleAndInRange();
	}

	protected override State stateIfPredatorNotVisibleButDetected()
	{
		if (currentTarget == playerController.transform)
		{
			return State.StateRunaway;
		}
		return State.StateAttack;
	}

	protected override State stateIfPredatorVisibleAndInRange()
	{
		if (MeleesEngagedCountOk())
		{
			return State.StateAttack;
		}
		return State.StateStrafe;
	}

	protected override void EnemyDiedCleanup(int index, DeathType deathType)
	{
		RemoveSelfFromArray();
		anim.Stop(blockUpperMixAnimation);
		if (engagedInCombat)
		{
			engagedInCombat = false;
			AManager.instance.decreaseMeleesEngagedCount();
		}
		survivalMissionController.EnemyHanzoDied(index, deathType);
	}
}
