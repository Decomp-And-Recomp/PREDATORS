using System.Collections;
using UnityEngine;

public class PredatorTracker : BaseSuperPredator
{
	private enum StrafeActions
	{
		MoveBack = 0,
		MoveFront = 1,
		StrafeLeft = 2,
		StrafeRight = 3,
		Stay = 4
	}

	private const float chanceToTripleCombo = 0.99f;

	private const float strafeNotEngagedTime = 5f;

	private const float strafeRadius = 4f;

	protected bool engagedInCombat;

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
			return "run_fwd";
		}
	}

	public override void Activate(int indexShooterLevel)
	{
		WhipTrailRenderer.enabled = false;
		base.Activate(indexShooterLevel);
		anim["whip_attack_light_R"].wrapMode = WrapMode.Once;
		anim["whip_attack_light_L"].wrapMode = WrapMode.Once;
		anim["whip_attack_heavy_R"].wrapMode = WrapMode.Once;
		nextState = State.StateIdle;
		StartEnemyStateLoop();
	}

	protected IEnumerator StrafeAround()
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
			if ((MeleesEngagedCountOk() || (currentTarget.position - xForm.position).sqrMagnitude < meleeRadius * meleeRadius) && canAttack)
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
			if (strafeTimer > Random.Range(2.5f, 5f))
			{
				strafeTimer = 0f;
				strafe = ((!RandomBool()) ? StrafeActions.Stay : (RandomBool() ? ((!(SqrDistance(xForm.position, currentTarget.position) < 16f)) ? StrafeActions.MoveFront : StrafeActions.MoveBack) : ((!RandomBool()) ? StrafeActions.StrafeRight : StrafeActions.StrafeLeft)));
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
			characterController.SimpleMove(direction * 1.58f);
			if (characterController.velocity.sqrMagnitude < 1.2482f && checkBlocking && lastColliderHit != null && foundObstacle)
			{
				foundObstacle = false;
				if (SelectGoAroundWaypoint())
				{
					yield return StartCoroutine("GoAround");
					anim.CrossFade(animation);
				}
			}
			if (strafe != StrafeActions.Stay && (double)characterController.velocity.sqrMagnitude < 1.2482000589370728)
			{
				strafeTimer = 5f;
			}
			yield return null;
		}
	}

	protected override IEnumerator EnemyStateLoop()
	{
		while (!isDead)
		{
			whipAnim.CrossFade("whip_retract");
			switch (nextState)
			{
			case State.StatePatrol:
				yield return StartCoroutine("Patrol");
				break;
			case State.StateRelocate:
				yield return StartCoroutine("Relocate");
				break;
			case State.StateStrafe:
				yield return StartCoroutine("StrafeAround");
				break;
			case State.StateIdle:
				yield return StartCoroutine("StandIdle");
				break;
			case State.StateBlock:
				yield return StartCoroutine("Block");
				break;
			default:
				yield return StartCoroutine("Attack");
				break;
			}
			yield return null;
		}
	}

	protected override void StopCoroutines()
	{
		StopCoroutine("StandIdle");
		base.StopCoroutines();
	}

	protected override IEnumerator StandIdle()
	{
		anim.CrossFade("idle_stand");
		whipAnim.CrossFade("whip_idle");
		yield return new WaitForSeconds(standIdleTime);
		float timer = 3f;
		anim.CrossFade("melee_walk_front");
		while (timer > 0f)
		{
			characterController.SimpleMove(xForm.forward * speedPatrol);
			yield return null;
			timer -= Time.deltaTime;
		}
		nextState = State.StateAttack;
	}

	protected override IEnumerator Relocate()
	{
		for (int i = 0; i < waypoints.Length; i++)
		{
			if (SqrDistance(waypoints[i].position, currentTarget.position) > attackShootDistance * attackShootDistance && Vector3.Dot(currentTarget.position - xForm.position, waypoints[i].position - xForm.position) < 0f)
			{
				currentWaypoint = waypoints[i];
				break;
			}
		}
		float angle = 0f;
		runawayTimer = 7f;
		while (!isDead)
		{
			angle = RotateTowardsPosition(currentWaypoint.position, rotateSpeed * 2f);
			direction = xForm.TransformDirection(Vector3.forward).normalized;
			if (angle > -5f && angle < 5f)
			{
				characterController.SimpleMove(direction * speedRun);
				anim.CrossFade("run_fwd");
			}
			else
			{
				anim.CrossFade("idle");
			}
			runawayTimer -= Time.deltaTime;
			if (runawayTimer <= 0f && !AManager.instance.CinematicInProgress)
			{
				nextState = State.StateAttack;
				break;
			}
			if ((double)(xForm.position - currentWaypoint.position).sqrMagnitude < 0.1)
			{
				if (destinationWaypoint != null)
				{
					currentWaypoint = destinationWaypoint;
					destinationWaypoint = null;
				}
				else
				{
					ChooseRandomWaypoint();
				}
			}
			if (characterController.velocity.sqrMagnitude < speedAttack * speedAttack * 0.5f && checkBlocking && lastColliderHit != null && foundObstacle)
			{
				foundObstacle = false;
				if (SelectGoAroundWaypoint())
				{
					yield return StartCoroutine("GoAround");
					anim.CrossFade("run_fwd");
				}
			}
			yield return null;
		}
	}

	private IEnumerator HideWhip()
	{
		whipAnim.CrossFade("whip_retract");
		yield return new WaitForSeconds(1f);
		WhipTrailRenderer.enabled = false;
	}

	protected override IEnumerator Attack()
	{
		if (Utils.SfxOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(soundChangeWeapon);
		}
		while (!isDead)
		{
			if (hitPoints <= fullHitPoints / 2f && canRelocateWhenLowOnHealth)
			{
				canRelocateWhenLowOnHealth = false;
				StartCoroutine(CanRelocateWhenLowOnHealthAgain());
				nextState = State.StateRelocate;
				break;
			}
			RotateTowardsPosition(currentTarget.position, rotateSpeed);
			if (!canAttack)
			{
				anim.CrossFade(blockWalkForwardAnimation);
			}
			else if (SqrDistance(xForm.position, currentTarget.position) < meleeRadius * meleeRadius)
			{
				canAttack = false;
				StartCoroutine(CanAttackAgain());
				StopCoroutine("HideWhip");
				yield return StartCoroutine("PerformAnimation", EnemyAnimation.WhipAttackLightR);
				if (SqrDistance(xForm.position, currentTarget.position) < meleeRadius * meleeRadius && Random.value < chanceToDoubleCombo)
				{
					yield return StartCoroutine("PerformAnimation", EnemyAnimation.WhipAttackLightL);
					if (SqrDistance(xForm.position, currentTarget.position) < meleeRadius * meleeRadius && Random.value < 0.99f)
					{
						yield return StartCoroutine("PerformAnimation", EnemyAnimation.WhipAttackHeavyR);
					}
				}
				StartCoroutine("HideWhip");
			}
			else
			{
				direction = xForm.TransformDirection(Vector3.forward);
				characterController.SimpleMove(direction * speedRun);
				anim.CrossFade(runToAttackAnimation);
				if (characterController.velocity.sqrMagnitude < speedRun * speedRun * 0.5f && checkBlocking && lastColliderHit != null && foundObstacle)
				{
					foundObstacle = false;
					if (SelectGoAroundWaypoint())
					{
						yield return StartCoroutine("GoAround");
						anim.CrossFade(runToAttackAnimation);
					}
				}
			}
			yield return null;
		}
	}

	protected override void EnemyDiedCleanup(int index, DeathType deathType)
	{
		RemoveSelfFromArray();
		WhipTrailRenderer.enabled = false;
		survivalMissionController.EnemyTrackerDied(index, deathType);
	}
}
