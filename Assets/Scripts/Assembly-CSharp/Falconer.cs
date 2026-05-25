using System.Collections;
using UnityEngine;

public class Falconer : BaseSuperPredator
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

	private const float falconMaxDistanceFromPlayer = 6f;

	private const float falconMinDistanceFromPlayer = 4f;

	public GameObject wristBladeMesh;

	public Falcon[] falcons;

	public GameObject poolFalconTrails;

	public AudioClip soundBirdCall;

	public Transform demoFalconsTarget;

	private bool wristAttackMode;

	protected float falconsAttackInterval = 12f;

	private bool engagedWithThePlayer;

	protected float moveForwardTimer = 2f;

	private bool canDoFalconsAttack = true;

	protected virtual string runToAttackAnimation
	{
		get
		{
			return "run_fwd";
		}
	}

	public override void Activate(int indexShooterLevel)
	{
		base.Activate(indexShooterLevel);
		wristBladeMesh.active = false;
		combiStickMesh.SetActiveRecursively(false);
		mustSwitchToBossMode = false;
		trailRendererHandL.gameObject.active = false;
		trailRendererHandR.gameObject.active = false;
		wristAttackMode = false;
		canDoFalconsAttack = true;
		anim["spear_attack_light_R"].wrapMode = WrapMode.Once;
		anim["spear_attack_light_L"].wrapMode = WrapMode.Once;
		anim["spear_attack_heavy_R"].wrapMode = WrapMode.Once;
		anim["grab_start"].wrapMode = WrapMode.Once;
		demoFalconsTarget.position = xForm.position + xForm.forward * 10f;
		StartCoroutine(FalconAttack(demoFalconsTarget));
		StopCoroutine("Patrol");
		anim.CrossFade("idle");
	}

	protected IEnumerator CanDoFalconsAttackAgain()
	{
		yield return new WaitForSeconds(falconsAttackInterval + falconsAttackInterval * Random.Range(-0.3f, 0.3f));
		canDoFalconsAttack = true;
	}

	private IEnumerator ActivateAllFalcons(bool demoDive, Transform target)
	{
		for (int i = 0; i < falcons.Length; i++)
		{
			if (demoDive)
			{
				target.position = xForm.position + xForm.forward * i;
			}
			falcons[i].Activate(target, demoDive);
			yield return new WaitForSeconds(0.2f);
		}
	}

	protected IEnumerator FalconAttack(Transform target)
	{
		canDoFalconsAttack = false;
		StartCoroutine(CanDoFalconsAttackAgain());
		if (Utils.SfxOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(soundBirdCall);
		}
		anim.CrossFade("grab_start");
		yield return new WaitForSeconds(anim["grab_start"].length / 2f);
		poolFalconTrails.transform.position = trailRendererHandL.transform.position;
		poolFalconTrails.SetActiveRecursively(true);
		if (nextState != State.StateFalconsAttack)
		{
			StartCoroutine(ActivateAllFalcons(true, target));
		}
		else
		{
			StartCoroutine(ActivateAllFalcons(false, target));
		}
		yield return new WaitForSeconds(anim["grab_start"].length / 2f);
		float timer3 = 0f;
		while (falcons[falcons.Length - 1].gameObject.active)
		{
			RotateTowardsPosition(currentTarget.position, rotateSpeed * 2f);
			float distanceToPlayer = SqrDistance(xForm.position, currentTarget.position);
			if (distanceToPlayer > 36f)
			{
				anim.CrossFade("melee_walk_front");
				timer3 = moveForwardTimer + moveForwardTimer * Random.Range(-0.5f, 0.5f);
				while (timer3 > 0f)
				{
					RotateTowardsPosition(currentTarget.position, rotateSpeed * 2f);
					characterController.SimpleMove(xForm.forward * 1.58f);
					timer3 -= Time.deltaTime;
					yield return null;
				}
			}
			else if (distanceToPlayer < 16f)
			{
				anim.CrossFade("melee_walk_back");
				timer3 = moveForwardTimer + moveForwardTimer * Random.Range(-0.5f, 0.5f);
				while (timer3 > 0f)
				{
					RotateTowardsPosition(currentTarget.position, rotateSpeed * 2f);
					characterController.SimpleMove(-xForm.forward * 1.58f);
					timer3 -= Time.deltaTime;
					yield return null;
				}
			}
			else
			{
				anim.CrossFade("idle");
				yield return new WaitForSeconds(moveForwardTimer + moveForwardTimer * Random.Range(-0.5f, 0.5f));
			}
			yield return null;
		}
		anim.CrossFade("idle");
		if (nextState != State.StateFalconsAttack)
		{
			StopCoroutine("EnemyStateLoop");
			StartCoroutine("EnemyStateLoop");
		}
		nextState = State.StateAttack;
	}

	protected IEnumerator StrafeAround()
	{
		engagedWithThePlayer = currentTarget == playerController.transform;
		float strafeTimer = 0f;
		string animation = "melee_walk_front";
		StrafeActions strafe = StrafeActions.MoveFront;
		while (!isDead)
		{
			if (canDoFalconsAttack && Random.value > 0.5f && engagedWithThePlayer)
			{
				nextState = State.StateFalconsAttack;
				break;
			}
			if ((xForm.position - currentTarget.position).sqrMagnitude > attackDistance * attackDistance)
			{
				nextState = State.StatePatrol;
				break;
			}
			if ((currentTarget.position - xForm.position).sqrMagnitude < meleeRadius * meleeRadius && canAttack)
			{
				nextState = State.StateAttack;
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
			wristBladeMesh.active = false;
			combiStickMesh.SetActiveRecursively(false);
			switch (nextState)
			{
			case State.StatePatrol:
				yield return StartCoroutine("Patrol");
				break;
			case State.StateBlock:
				yield return StartCoroutine("Block");
				break;
			default:
				if (wristAttackMode)
				{
					yield return StartCoroutine("WristAttack");
				}
				else
				{
					yield return StartCoroutine("Attack");
				}
				break;
			case State.StateRelocate:
				yield return StartCoroutine("Relocate");
				break;
			case State.StateStrafe:
				yield return StartCoroutine("StrafeAround");
				break;
			case State.StateGoingIntoBossMode:
				yield return StartCoroutine("GoingIntoBossMode");
				break;
			case State.StateFalconsAttack:
				yield return StartCoroutine("FalconAttack", playerController.transform);
				break;
			}
			yield return null;
		}
	}

	protected override IEnumerator GoingIntoBossMode()
	{
		if (Utils.SfxOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(soundYell);
		}
		anim["yell"].time = 0f;
		anim.CrossFade("yell");
		yield return new WaitForSeconds(anim["yell"].length);
		nextState = State.StateAttack;
		wristAttackMode = true;
	}

	protected override void StopCoroutines()
	{
		StopCoroutine("FalconAttack");
		base.StopCoroutines();
	}

	protected override IEnumerator WristAttack()
	{
		engagedWithThePlayer = currentTarget == playerController.transform;
		if (!wristBladeMesh.active)
		{
			wristBladeMesh.active = true;
			if (Utils.SfxOn)
			{
				base.GetComponent<AudioSource>().PlayOneShot(soundChangeWeapon);
			}
		}
		combiStickMesh.SetActiveRecursively(false);
		while (!isDead)
		{
			if (canDoFalconsAttack && Random.value > 0.5f && engagedWithThePlayer)
			{
				nextState = State.StateFalconsAttack;
				break;
			}
			RotateTowardsPosition(currentTarget.position, rotateSpeed);
			if (SqrDistance(xForm.position, currentTarget.position) < meleeRadius * meleeRadius)
			{
				canAttack = false;
				StartCoroutine(CanAttackAgain());
				yield return StartCoroutine("PerformAnimation", EnemyAnimation.KnifeAttackR);
				if (SqrDistance(xForm.position, currentTarget.position) < meleeRadius * meleeRadius && Random.value < chanceToDoubleCombo)
				{
					yield return StartCoroutine("PerformAnimation", EnemyAnimation.KnifeAttackL);
				}
				if (Random.value > 0.5f)
				{
					yield return StartCoroutine("PerformAnimation", EnemyAnimation.KnifeAttackHeavy);
				}
			}
			else
			{
				direction = xForm.TransformDirection(Vector3.forward);
				characterController.SimpleMove(direction * speedRun);
				anim.CrossFade("run_fwd");
			}
			yield return null;
		}
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
		engagedWithThePlayer = currentTarget == playerController.transform;
		while (!isDead)
		{
			if (canDoFalconsAttack && Random.value > 0.5f && engagedWithThePlayer)
			{
				nextState = State.StateFalconsAttack;
				break;
			}
			angle = RotateTowardsPosition(currentWaypoint.position, rotateSpeed * 4f);
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
			else if (mustSwitchToBossMode && !isInBossMode && !AManager.instance.CinematicInProgress)
			{
				isInBossMode = true;
				nextState = State.StateGoingIntoBossMode;
				break;
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

	protected override IEnumerator Attack()
	{
		engagedWithThePlayer = currentTarget == playerController.transform;
		if (Utils.SfxOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(soundChangeWeapon);
		}
		wristBladeMesh.active = false;
		combiStickMesh.active = true;
		while (!isDead)
		{
			if (canDoFalconsAttack && Random.value > 0.5f && engagedWithThePlayer)
			{
				nextState = State.StateFalconsAttack;
				break;
			}
			if (hitPoints <= fullHitPoints / 2f && canRelocateWhenLowOnHealth)
			{
				canRelocateWhenLowOnHealth = false;
				StartCoroutine(CanRelocateWhenLowOnHealthAgain());
				nextState = State.StateRelocate;
				break;
			}
			RotateTowardsPosition(currentTarget.position, rotateSpeed);
			if (SqrDistance(xForm.position, currentTarget.position) < meleeRadius * meleeRadius)
			{
				canAttack = false;
				StartCoroutine(CanAttackAgain());
				yield return StartCoroutine("PerformAnimation", EnemyAnimation.SpearAttackLightR);
				if (SqrDistance(xForm.position, currentTarget.position) < meleeRadius * meleeRadius && Random.value < chanceToDoubleCombo)
				{
					yield return StartCoroutine("PerformAnimation", EnemyAnimation.SpearAttackLightL);
					if (SqrDistance(xForm.position, currentTarget.position) < meleeRadius * meleeRadius && Random.value < 0.99f)
					{
						yield return StartCoroutine("PerformAnimation", EnemyAnimation.SpearAttackHeavyR);
					}
				}
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
				if (mustSwitchToBossMode && !isInBossMode)
				{
					isInBossMode = true;
					nextState = State.StateGoingIntoBossMode;
					break;
				}
			}
			yield return null;
		}
	}

	protected override void EnemyDiedCleanup(int index, DeathType deathType)
	{
		RemoveSelfFromArray();
		survivalMissionController.EnemyFalconerDied(index, deathType);
	}
}
