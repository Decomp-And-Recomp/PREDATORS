using System.Collections;
using UnityEngine;

public class Isabele : BaseEnemy
{
	public enum SniperType
	{
		Isabelle = 0,
		Sniper = 1
	}

	private const float animGetHitBlockingMoveStart = 7f / 30f;

	private const float animGetHitBlockingIdleEnd = 1f / 3f;

	public SniperType sniperType = SniperType.Sniper;

	public LineRenderer LaserLineRenderer;

	public float aimingRotateSpeed = 500f;

	public float blockModeTime = 4f;

	public float angleForWhichTheLaserIsVisible = 10f;

	public AudioClip sniperAquiredTargetSound;

	public AudioClip soundShoot;

	public TriangleTarget sniperTarget;

	public ParticleEmitter particleBulletsEmitter;

	private LayerMask laserLineCullingMask = 256;

	protected override string animPatrol
	{
		get
		{
			return "walk_weapon_up";
		}
	}

	private IEnumerator PerformAnimation(EnemyAnimation attackAnimationType)
	{
		if (attackAnimationType == EnemyAnimation.GetHitBlocking)
		{
			anim["block_shotgun_gethit"].time = 0f;
			anim.CrossFade("block_shotgun_gethit", 0.1f);
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
		}
	}

	protected override void EnemyDied()
	{
		if ((bool)LaserLineRenderer)
		{
			LaserLineRenderer.enabled = false;
		}
		if ((bool)sniperTarget)
		{
			sniperTarget.gameObject.SetActiveRecursively(false);
		}
		if ((bool)particleBulletsEmitter)
		{
			particleBulletsEmitter.emit = false;
		}
	}

	protected override IEnumerator Relocate()
	{
		if (AManager.instance.CinematicInProgress)
		{
			LaserLineRenderer.enabled = false;
			sniperTarget.gameObject.SetActiveRecursively(false);
		}
		for (int i = 0; i < waypoints.Length; i++)
		{
			if (SqrDistance(waypoints[i].position, currentTarget.position) > attackShootDistance * attackShootDistance && Vector3.Dot(currentTarget.position - xForm.position, waypoints[i].position - xForm.position) < 0f)
			{
				currentWaypoint = waypoints[i];
				break;
			}
		}
		while (!isDead)
		{
			float angle = RotateTowardsPosition(currentWaypoint.position, rotateSpeed);
			direction = xForm.TransformDirection(Vector3.forward).normalized;
			if (angle > -10f && angle < 10f)
			{
				characterController.SimpleMove(direction * speedAttack);
				anim.CrossFade("jog_weapon_lowered");
			}
			else
			{
				anim.CrossFade("idle_searching");
			}
			if (SqrDistance(xForm.position, currentTarget.position) > attackShootDistance * attackShootDistance && !AManager.instance.CinematicInProgress && !Physics.Linecast(xForm.position, currentTarget.position, laserLineCullingMask))
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
					anim.CrossFade("jog_weapon_lowered");
				}
			}
			yield return null;
		}
	}

	protected override void EnemyDiedCleanup(int index, DeathType deathType)
	{
		RemoveSelfFromArray();
		if (sniperType == SniperType.Isabelle)
		{
			survivalMissionController.EnemyIsabeleDied(index, deathType);
		}
		else if (sniperType == SniperType.Sniper)
		{
			survivalMissionController.EnemySniperDied(index, deathType);
		}
	}

	public override void Activate(int indexShooterLevel)
	{
		if (sniperType == SniperType.Isabelle)
		{
			enemyType = EnemyType.Isabelle;
		}
		else if (sniperType == SniperType.Sniper)
		{
			enemyType = EnemyType.Sniper;
		}
		animationInfo.Enemy_Type = enemyType;
		sniperTarget.gameObject.SetActiveRecursively(false);
		if ((bool)particleBulletsEmitter)
		{
			particleBulletsEmitter.emit = false;
		}
		if ((bool)LaserLineRenderer)
		{
			LaserLineRenderer.enabled = false;
		}
		anim["standup_to_crouch"].wrapMode = WrapMode.Once;
		anim["crouch_pose"].wrapMode = WrapMode.ClampForever;
		anim["crouch_idle"].wrapMode = WrapMode.Loop;
		anim["crouch_getup"].wrapMode = WrapMode.Once;
		anim["crouch_shoot"].wrapMode = WrapMode.Once;
		base.Activate(indexShooterLevel);
		anim["die_fall_back"].wrapMode = WrapMode.Once;
	}

	protected override IEnumerator Attack()
	{
		float angle = RotateTowardsPosition(currentTarget.position, aimingRotateSpeed);
		while (!isDead)
		{
			float dotProd = Vector3.Dot((xForm.position + xForm.forward - xForm.position).normalized, (currentTarget.position - xForm.position).normalized);
			angle = RotateTowardsPosition(currentTarget.position, aimingRotateSpeed * Mathf.Clamp(1f - Mathf.Abs(dotProd), 0.01f, 1f));
			anim.CrossFade("crouch_pose");
			LaserLineRenderer.SetPosition(0, xForm.position + Vector3.up);
			float distanceToPredator = (xForm.position - currentTarget.position).sqrMagnitude;
			LaserLineRenderer.SetPosition(1, xForm.position + xForm.forward * Mathf.Sqrt(distanceToPredator) + Vector3.up);
			if (!LaserLineRenderer.enabled && Mathf.Abs(angle) < angleForWhichTheLaserIsVisible)
			{
				LaserLineRenderer.enabled = true;
			}
			if (LaserLineRenderer.enabled && (angle > angleForWhichTheLaserIsVisible || angle < 0f - angleForWhichTheLaserIsVisible))
			{
				LaserLineRenderer.enabled = false;
				sniperTarget.gameObject.SetActiveRecursively(false);
			}
			if (distanceToPredator < attackShootDistance * attackShootDistance || Physics.Linecast(xForm.position, currentTarget.position, laserLineCullingMask))
			{
				nextState = State.StateRelocate;
				LaserLineRenderer.enabled = false;
				break;
			}
			if ((double)angle > -0.5 && (double)angle < 0.5)
			{
				shootTimer += Time.deltaTime;
				if (shootTimer > shootRateSeconds)
				{
					sniperTarget.SetTarget(currentTarget.transform);
					sniperTarget.gameObject.SetActiveRecursively(true);
					if (Utils.SfxOn)
					{
						base.GetComponent<AudioSource>().PlayOneShot(sniperAquiredTargetSound);
					}
					yield return new WaitForSeconds(0.3f);
					if (Utils.SfxOn)
					{
						base.GetComponent<AudioSource>().PlayOneShot(soundShoot);
					}
					particleBulletsEmitter.Emit(1);
					yield return new WaitForSeconds(0.1f);
					LaserLineRenderer.enabled = false;
					shootTimer = 0f;
					yield return new WaitForSeconds(0.5f);
					yield return StartCoroutine(LooseTargetSight());
				}
			}
			if ((xForm.position - currentTarget.position).sqrMagnitude > attackDistance * attackDistance)
			{
				nextState = State.StatePatrol;
				LaserLineRenderer.enabled = false;
				break;
			}
			if (AManager.PredatorInvisible && currentTarget == playerController.transform)
			{
				nextState = State.StatePanic;
				LaserLineRenderer.enabled = false;
				break;
			}
			yield return null;
		}
	}

	private IEnumerator LooseTargetSight()
	{
		Vector3 laserPos1 = currentTarget.position;
		LaserLineRenderer.enabled = true;
		if (Random.value <= 0.5f)
		{
			laserPos1 += base.transform.right * 2f;
		}
		else
		{
			laserPos1 -= base.transform.right * 2f;
		}
		laserPos1.y = 1f;
		LaserLineRenderer.SetPosition(1, laserPos1);
		sniperTarget.gameObject.SetActiveRecursively(false);
		Vector3 startPos = laserPos1;
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime;
			laserPos1.x = Mathf.Lerp(startPos.x, currentTarget.position.x, t);
			laserPos1.z = Mathf.Lerp(startPos.z, currentTarget.position.z, t);
			LaserLineRenderer.SetPosition(1, laserPos1);
			yield return null;
		}
	}

	private IEnumerator RotateTowardsRandomWaypoint()
	{
		Vector3 waypointPosition = Vector3.zero;
		float minDotProd = 1f;
		for (int i = 0; i < waypoints.Length; i++)
		{
			float dotProd = Vector3.Dot(waypoints[i].position - xForm.position, currentTarget.position - xForm.position);
			if (dotProd < minDotProd)
			{
				minDotProd = dotProd;
				waypointPosition = waypoints[i].position;
			}
		}
		while (true)
		{
			RotateTowardsPosition(waypointPosition, rotateSpeed);
			float angle = AngleBetweenXformAnd(currentTarget.position);
			if (Mathf.Abs(angle) > angleForWhichTheLaserIsVisible * 2f)
			{
				break;
			}
			yield return null;
		}
	}

	public override void NetGunCaptured()
	{
		base.NetGunCaptured();
		LaserLineRenderer.enabled = false;
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

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(base.transform.position, attackDistance);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(base.transform.position, attackShootDistance);
	}
}
