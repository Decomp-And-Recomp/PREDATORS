using System.Collections;
using UnityEngine;

public class Dog : BaseEnemy
{
	protected enum StrafeActions
	{
		MoveBack = 0,
		MoveFront = 1,
		StrafeLeft = 2,
		StrafeRight = 3,
		Stay = 4
	}

	private const float animAttackLightStartIdle = 2f / 15f;

	private const float animAttackLightStartJump = 0.1f;

	private const float animAttackLightEndJump = 1f / 6f;

	private const float animAttackLightEndIdle = 0.1f;

	private const float animAttackRunningStartMove = 0.1f;

	private const float animAttackRunningEndIdle = 0.5f;

	public AudioClip soundDogLightAttack;

	public AudioClip soundDogHeavyAttack;

	public AudioClip soundDogIdle;

	public float meleeRadius = 2f;

	public float speedRunning = 8f;

	public float strafeNotEngagedTime = 5f;

	public float strafeRadius = 4f;

	public float chanceToDoubleCombo = 0.99f;

	private float attackRate = 1f;

	protected bool canAttack = true;

	protected bool engagedInCombat;

	protected float speedStrafe = 1.58f;

	protected override string animPatrol
	{
		get
		{
			return "walk";
		}
	}

	protected override string animJog
	{
		get
		{
			return "run";
		}
	}

	protected override string animGoAround
	{
		get
		{
			return "run";
		}
	}

	protected string animAttack
	{
		get
		{
			return "attack";
		}
	}

	protected string animWalkForwardBlocking
	{
		get
		{
			return "walk";
		}
	}

	protected string animAttackRunning
	{
		get
		{
			return "run_attack";
		}
	}

	protected string animRun
	{
		get
		{
			return "run";
		}
	}

	protected override string animNetGunCaptured
	{
		get
		{
			return "idle_searching";
		}
	}

	protected override string animGetHurtLow1
	{
		get
		{
			return "get_hit_R";
		}
	}

	protected override string animGetHurtLow2
	{
		get
		{
			return "get_hit_L";
		}
	}

	protected override string animGetHurtR1
	{
		get
		{
			return "get_hit_R";
		}
	}

	protected override string animGetHurtR2
	{
		get
		{
			return "get_hit_R";
		}
	}

	protected override string animGetHurtL1
	{
		get
		{
			return "get_hit_L";
		}
	}

	protected override string animGetHurtL2
	{
		get
		{
			return "get_hit_L";
		}
	}

	protected override string animGetHurtHigh1
	{
		get
		{
			return "get_hit_R";
		}
	}

	protected override string animGetHurtHigh2
	{
		get
		{
			return "get_hit_L";
		}
	}

	protected override string animDieFallFront
	{
		get
		{
			return "die_fall_front";
		}
	}

	protected override string animGrabbedBreakLoose
	{
		get
		{
			return "grabbed_break_loose";
		}
	}

	protected override ArrayList EnemyArray
	{
		get
		{
			return AManager.instance.humanTargets;
		}
	}

	protected override ArrayList AlliesArray
	{
		get
		{
			return AManager.instance.predatorTargets;
		}
	}

	private float RandomInRange(float min, float max)
	{
		return min + Random.value * (max - min);
	}

	public override void Activate(int indexShooterLevel)
	{
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
		anim["walk"].speed = 1.2f;
		anim[animAttack].wrapMode = WrapMode.Once;
		anim[animAttackRunning].wrapMode = WrapMode.Once;
		anim[animWalkForwardBlocking].wrapMode = WrapMode.Loop;
		speedJog = speedRunning;
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
			anim.CrossFade("shake");
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
			else if ((xForm.position - AManager.targetPosition).sqrMagnitude < cloakDetectionRadius * cloakDetectionRadius && predatorAngle < cloakDetectionAngle && predatorAngle > 0f - cloakDetectionAngle)
			{
				lastSeenPosition = AManager.targetPosition;
				nextState = State.StateAttack;
				break;
			}
			yield return null;
		}
	}

	protected override IEnumerator NetGunCapturedCR()
	{
		anim.CrossFade("shake", 0.1f);
		yield return new WaitForSeconds(2f / 3f);
		nextState = stateAfterGettingHurtAndPredatorVisible();
		StartEnemyStateLoop();
	}

	private IEnumerator PerformAnimation(EnemyAnimation attackAnimationType)
	{
		switch (attackAnimationType)
		{
		case EnemyAnimation.DogAttackMelee:
		{
			anim.Play(animAttack);
			float timer = 2f / 15f;
			Vector3 attackDirection3 = currentTarget.position - xForm.position;
			targetRotation = Quaternion.LookRotation(attackDirection3);
			while (timer > 0f)
			{
				timer -= Time.deltaTime;
				xForm.rotation = Quaternion.Slerp(xForm.rotation, targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			if (Utils.SfxOn)
			{
				base.GetComponent<AudioSource>().PlayOneShot(soundDogLightAttack);
			}
			timer = 0.1f;
			attackDirection3 = (currentTarget.position - xForm.position).normalized;
			targetRotation = Quaternion.LookRotation(attackDirection3);
			while (timer > 0f)
			{
				timer -= Time.deltaTime;
				characterController.SimpleMove(attackDirection3 * 3f);
				xForm.rotation = Quaternion.Slerp(xForm.rotation, targetRotation, Time.deltaTime * rotateSpeed);
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
			timer = 1f / 6f;
			attackDirection3 = (currentTarget.position - xForm.position).normalized;
			targetRotation = Quaternion.LookRotation(attackDirection3);
			while (timer > 0f)
			{
				timer -= Time.deltaTime;
				characterController.SimpleMove(attackDirection3 * 3f);
				xForm.rotation = Quaternion.Slerp(xForm.rotation, targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			yield return new WaitForSeconds(0.1f);
			break;
		}
		case EnemyAnimation.DogAttackRunning:
		{
			anim[animAttackRunning].time = 0f;
			anim.CrossFade(animAttackRunning, 0.1f);
			float timer2 = 0.1f;
			Vector3 attackDirection = (currentTarget.position - xForm.position).normalized;
			targetRotation = Quaternion.LookRotation(attackDirection);
			while (timer2 > 0f)
			{
				timer2 -= Time.deltaTime;
				characterController.SimpleMove(attackDirection * 3f);
				xForm.rotation = Quaternion.Slerp(xForm.rotation, targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			if (Utils.SfxOn)
			{
				base.GetComponent<AudioSource>().PlayOneShot(soundDogHeavyAttack);
			}
			Collider[] colliders = Physics.OverlapSphere(xForm.TransformPoint(meleeOffset), meleeRadiusDamage);
			Collider[] array = colliders;
			foreach (Collider hit in array)
			{
				if (hit == currentTarget.GetComponent<Collider>())
				{
					attackInfoEnemy.Damage = meleeDamage * 2f;
					attackInfoEnemy.AttackerPosition = xForm.position;
					attackInfoEnemy.AnimationNr = 1;
					currentTarget.SendMessage("ApplyDamage", attackInfoEnemy, SendMessageOptions.DontRequireReceiver);
					break;
				}
			}
			yield return new WaitForSeconds(0.5f);
			break;
		}
		}
	}

	private IEnumerator StrafeAround()
	{
		float strafeTimer = 0f;
		string animation = "walk_weapon_up";
		StrafeActions strafe = StrafeActions.MoveFront;
		Vector3 crossVector3 = Vector3.zero;
		float squaredAttackDistance = attackDistance * attackDistance;
		while (!isDead)
		{
			float squaredDistanceToPlayer = (xForm.position - currentTarget.position).sqrMagnitude;
			if (squaredDistanceToPlayer > squaredAttackDistance)
			{
				nextState = State.StatePatrol;
				break;
			}
			if (MeleesEngagedCountOk() && canAttack)
			{
				nextState = State.StateAttack;
				break;
			}
			strafeTimer += Time.deltaTime;
			if (strafeTimer > Random.Range(strafeNotEngagedTime / 2f, strafeNotEngagedTime))
			{
				strafeTimer = 0f;
				if (squaredDistanceToPlayer < squaredAttackDistance / 16f)
				{
					strafe = StrafeActions.MoveBack;
				}
				else if (squaredDistanceToPlayer > squaredAttackDistance / 4f)
				{
					strafe = StrafeActions.MoveFront;
				}
				else
				{
					float randomValue = Random.value;
					if (randomValue >= 0f && randomValue < 0.33f)
					{
						strafe = StrafeActions.Stay;
					}
					else if (randomValue >= 0.33f && randomValue < 0.66f)
					{
						strafe = StrafeActions.StrafeLeft;
					}
					else if (randomValue >= 0.66f && randomValue < 1f)
					{
						strafe = StrafeActions.StrafeRight;
					}
				}
			}
			animation = "walk";
			switch (strafe)
			{
			case StrafeActions.MoveBack:
				crossVector3 = (xForm.position - currentTarget.position).normalized;
				direction = xForm.TransformDirection(Vector3.forward);
				RotateTowardsPosition(xForm.position + crossVector3, rotateSpeed);
				break;
			case StrafeActions.MoveFront:
				direction = xForm.TransformDirection(Vector3.forward);
				RotateTowardsPosition(currentTarget.position, rotateSpeed);
				break;
			case StrafeActions.StrafeLeft:
				crossVector3 = Vector3.Cross(Vector3.up, currentTarget.position - xForm.position).normalized;
				direction = xForm.TransformDirection(Vector3.forward);
				RotateTowardsPosition(xForm.position + crossVector3, rotateSpeed);
				break;
			case StrafeActions.StrafeRight:
				crossVector3 = -Vector3.Cross(Vector3.up, currentTarget.position - xForm.position).normalized;
				direction = xForm.TransformDirection(Vector3.forward);
				RotateTowardsPosition(xForm.position + crossVector3, rotateSpeed);
				break;
			case StrafeActions.Stay:
				direction = Vector3.zero;
				animation = "idle_searching";
				RotateTowardsPosition(currentTarget.position, rotateSpeed);
				break;
			default:
				direction = Vector3.zero;
				animation = "idle_searching";
				RotateTowardsPosition(currentTarget.position, rotateSpeed);
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
		if (AManager.instance.MeleesEngagedCount > 1)
		{
			yield return new WaitForSeconds(attackRate * 1.5f);
		}
		else
		{
			yield return new WaitForSeconds(attackRate);
		}
		canAttack = true;
	}

	protected override IEnumerator EnemyStateLoop()
	{
		while (!isDead)
		{
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
					StopCoroutine("UpdateCurrentTarget");
					StartCoroutine("UpdateCurrentTarget");
					yield return StartCoroutine("Attack");
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
					yield return StartCoroutine("StrafeAround");
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
		bool didHeavyAttack = false;
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
			RotateTowardsPosition(currentTarget.position, rotateSpeed);
			if (!canAttack)
			{
				anim.CrossFade("idle_searching", 0.1f);
			}
			else if (SqrDistance(xForm.position, currentTarget.position) < meleeRadius * meleeRadius && !AManager.instance.CinematicInProgress)
			{
				if (!didHeavyAttack)
				{
					yield return StartCoroutine("PerformAnimation", EnemyAnimation.DogAttackRunning);
					didHeavyAttack = true;
				}
				else
				{
					canAttack = false;
					StartCoroutine(CanAttackAgain());
					yield return StartCoroutine("PerformAnimation", EnemyAnimation.DogAttackMelee);
					if (SqrDistance(xForm.position, currentTarget.position) < meleeRadius * meleeRadius && Random.value < chanceToDoubleCombo)
					{
						yield return StartCoroutine("PerformAnimation", EnemyAnimation.DogAttackMelee);
					}
				}
			}
			else
			{
				didHeavyAttack = false;
				direction = xForm.TransformDirection(Vector3.forward);
				characterController.SimpleMove(direction * speedRunning);
				anim.CrossFade(animRun);
				if (characterController.velocity.sqrMagnitude < speedRunning * speedRunning * 0.5f && checkBlocking && lastColliderHit != null && foundObstacle)
				{
					foundObstacle = false;
					if (SelectGoAroundWaypoint())
					{
						yield return StartCoroutine("GoAround");
						anim.CrossFade(animRun);
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
			yield return null;
		}
	}

	protected override State stateAfterGettingHurtAndPredatorInvisible()
	{
		if (currentTarget == playerController.transform)
		{
			return State.StateRelocate;
		}
		return State.StateAttack;
	}

	protected override State stateAfterGettingHurtAndPredatorVisible()
	{
		return stateIfPredatorVisibleAndInRange();
	}

	protected override State stateIfPredatorNotVisibleButDetected()
	{
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

	protected override void PlayDeathAnimation()
	{
		anim.Stop();
		if (Random.value < 0.5f)
		{
			anim.Play(animDieFallBack, PlayMode.StopAll);
		}
		else
		{
			anim.Play(animDieFallFront, PlayMode.StopAll);
		}
	}

	protected override void EnemyDiedCleanup(int index, DeathType deathType)
	{
		RemoveSelfFromArray();
		if (engagedInCombat)
		{
			engagedInCombat = false;
			AManager.instance.decreaseMeleesEngagedCount();
		}
		survivalMissionController.EnemyDogDied(index, deathType);
	}

	protected override void DiscCut(Vector3 attackerPosition, DeathType deathType)
	{
		characterController.radius = 0f;
		StopAllCoroutines();
		if (Utils.SfxOn)
		{
			AudioSource.PlayClipAtPoint(soundDie, xForm.position);
		}
		PlayDeathAnimation();
		StopCoroutine("ApplyDamageGrabbedCR");
		StopCoroutines();
		StartCoroutine("Die");
	}

	public override void MeleeCut(AttackInfo attackInfo, DeathType deathType)
	{
		DiscCut(attackInfo.AttackerPosition, deathType);
	}
}
