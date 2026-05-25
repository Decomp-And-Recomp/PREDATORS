using System.Collections;
using UnityEngine;

public class SuperBlackPredator : BaseSuperPredator
{
	public enum MaterialMode
	{
		Normal = 0,
		Cloaked = 1,
		ThermalCloaked = 2,
		Thermal = 3
	}

	public AudioClip soundShoot;

	public MeshRenderer muzzleFlash;

	public ParticleEmitter particleBulletsEmitter;

	public GameObject AOCircle;

	public AudioClip soundCloakOff;

	public AudioClip soundCloakOn;

	public Material healthBarMaterial;

	public Material materialThermalCloaked;

	public Material materialCloak;

	public Material materialThermal;

	public Material materialNormal;

	public Renderer berserkerMeshRenderer;

	public GameObject healthBarMesh;

	public Color ThermalCloakedMaterialColor = Color.blue;

	public GameObject PredatorFace;

	public GameObject PredatorMask;

	private bool healthGrowingInProgress;

	private Color initialBerserkerMaterialColor;

	private bool cloakModeOn;

	private void SwitchMaterialMode(Material aMaterial, Color aColor)
	{
		berserkerMeshRenderer.material = aMaterial;
		PredatorFace.GetComponent<Renderer>().material = aMaterial;
		PredatorMask.GetComponent<Renderer>().material = aMaterial;
		berserkerMeshRenderer.material.color = aColor;
		PredatorFace.GetComponent<Renderer>().material.color = aColor;
		PredatorMask.GetComponent<Renderer>().material.color = aColor;
	}

	public void PlayerSwitchedThermalVision(bool playerThermalVisionOn)
	{
		if (playerThermalVisionOn)
		{
			if (cloakModeOn)
			{
				SwitchMaterialMode(materialThermalCloaked, ThermalCloakedMaterialColor);
			}
			else
			{
				SwitchMaterialMode(materialThermal, initialBerserkerMaterialColor);
			}
		}
		else if (cloakModeOn)
		{
			SwitchMaterialMode(materialCloak, initialBerserkerMaterialColor);
		}
		else
		{
			SwitchMaterialMode(materialNormal, initialBerserkerMaterialColor);
		}
	}

	private void Start()
	{
		initialBerserkerMaterialColor = materialNormal.color;
	}

	public override void Activate(int indexShooterLevel)
	{
		base.Activate(indexShooterLevel);
		healthBarMaterial.SetColor("_TintColor", new Color(2f / 15f, 0.85490197f, 1f));
		playerController.SuperPredatorTarget = base.transform;
		if ((bool)PredatorFace && (bool)PredatorMask)
		{
			PredatorMask.active = true;
			PredatorFace.active = false;
		}
		trophyKillPossible = false;
		healthBarMesh.active = true;
		mustSwitchToBossMode = false;
		UpdateHealth();
		trailRendererHandL.gameObject.active = false;
		trailRendererHandR.gameObject.active = false;
		anim["plasma_shoot"].wrapMode = WrapMode.Once;
		anim["plasma_shoot"].AddMixingTransform(BoneMixChest, true);
		anim["plasma_shoot"].blendMode = AnimationBlendMode.Additive;
		anim["plasma_shoot"].layer = 10;
		speedRun *= 1.1f;
		anim["run_fwd"].speed = 1.1f;
		anim["run_bck"].speed = 1.1f;
		anim["strafe_L"].speed = 1.1f;
		anim["strafe_R"].speed = 1.1f;
		if (cloakModeOn)
		{
			SwitchCloakMode();
		}
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

	public IEnumerator ShowMuzzleFlash()
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

	private IEnumerator GrowHealthBar()
	{
		float timer = 3f;
		healthGrowingInProgress = true;
		while (timer > 0f)
		{
			healthBarMaterial.SetTextureOffset("_MainTex", new Vector2(timer / 3f * 0.5f, 0f));
			yield return null;
			timer -= Time.deltaTime;
		}
		healthGrowingInProgress = false;
	}

	private void UpdateHealth()
	{
		if (trophyKillPossible)
		{
			healthBarMesh.active = false;
		}
		else if (!healthGrowingInProgress)
		{
			healthBarMaterial.SetTextureOffset("_MainTex", new Vector2((1f - hitPoints / fullHitPoints) * 0.5f, 0f));
		}
	}

	protected override void StopCoroutines()
	{
		base.StopCoroutines();
		StopCoroutine("OffsetCloakMaterial");
	}

	private void SwitchCloakMode()
	{
		if (cloakModeOn)
		{
			if (Utils.SfxOn)
			{
				base.GetComponent<AudioSource>().PlayOneShot(soundCloakOff);
			}
			if ((bool)AOCircle)
			{
				AOCircle.active = true;
			}
			cloakModeOn = false;
			playerController.SuperPredatorIsInvisible = false;
			if (playerController.ThermalVisionMode)
			{
				SwitchMaterialMode(materialThermal, initialBerserkerMaterialColor);
			}
			else
			{
				SwitchMaterialMode(materialNormal, initialBerserkerMaterialColor);
			}
			return;
		}
		if (Utils.SfxOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(soundCloakOn);
		}
		cloakModeOn = true;
		if ((bool)AOCircle)
		{
			AOCircle.active = false;
		}
		StopCoroutine("RegenerateHealth");
		StartCoroutine("RegenerateHealth");
		playerController.SuperPredatorIsInvisible = true;
		StartCoroutine(OffsetCloakMaterial());
		if (playerController.ThermalVisionMode)
		{
			SwitchMaterialMode(materialThermalCloaked, ThermalCloakedMaterialColor);
		}
		else
		{
			SwitchMaterialMode(materialCloak, initialBerserkerMaterialColor);
		}
	}

	private IEnumerator OffsetCloakMaterial()
	{
		float offsetSpeed = 0.1f;
		while (cloakModeOn)
		{
			float rot = Mathf.Repeat(Time.time * offsetSpeed, 1f);
			materialCloak.SetTextureOffset("_NormalMap", new Vector2(0f - rot, rot));
			materialCloak.SetTextureOffset("_NormalMap2", new Vector2(rot, 0f - rot));
			yield return null;
		}
	}

	private IEnumerator RegenerateHealth()
	{
		float rate = 1f;
		float initialDelay = 1f;
		yield return new WaitForSeconds(initialDelay);
		while (cloakModeOn)
		{
			if (hitPoints < fullHitPoints)
			{
				hitPoints += rate;
			}
			UpdateHealth();
			yield return null;
		}
	}

	protected override State stateAfterGettingHurtAndPredatorVisible()
	{
		if (nextState == State.StateRelocate)
		{
			return State.StateRelocate;
		}
		return State.StateAttack;
	}

	protected override State stateAfterGettingHurtAndPredatorInvisible()
	{
		return stateAfterGettingHurtAndPredatorVisible();
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
		if (!cloakModeOn)
		{
			SwitchCloakMode();
		}
		while (!isDead)
		{
			angle = RotateTowardsPosition(currentWaypoint.position, rotateSpeed * 2f);
			direction = xForm.TransformDirection(Vector3.forward).normalized;
			if (angle > -5f && angle < 5f)
			{
				anim.CrossFade("run_fwd");
				characterController.SimpleMove(direction * speedRun);
			}
			else
			{
				anim.CrossFade("idle_searching");
			}
			runawayTimer -= Time.deltaTime;
			if (runawayTimer <= 0f && !AManager.instance.CinematicInProgress)
			{
				if (mustSwitchToBossMode && !isInBossMode)
				{
					nextState = State.StateGoingIntoBossMode;
				}
				else
				{
					nextState = State.StateAttack;
				}
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
			if (characterController.velocity.sqrMagnitude < speedRun * speedRun * 0.5f && checkBlocking && lastColliderHit != null && foundObstacle)
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

	private IEnumerator TurnToCamera()
	{
		Quaternion initialRotation = xForm.rotation;
		float t = 1f;
		while (t > 0f)
		{
			xForm.rotation = Quaternion.Lerp(Quaternion.LookRotation(Vector3.back), initialRotation, t);
			t -= Time.deltaTime;
			yield return null;
		}
	}

	protected override void MustSwitchToBossMode()
	{
		base.MustSwitchToBossMode();
		healthBarMaterial.SetColor("_TintColor", new Color(43f / 51f, 0.8784314f, 0f));
		StartCoroutine(GrowHealthBar());
	}

	public override void ApplyDamage(AttackInfo attackInfo)
	{
		if (cloakModeOn)
		{
			SwitchCloakMode();
		}
		base.ApplyDamage(attackInfo);
		UpdateHealth();
	}

	protected override IEnumerator GoingIntoBossMode()
	{
		if (Utils.SfxOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(soundYell);
		}
		isInBossMode = true;
		StartCoroutine(TurnToCamera());
		if (cloakModeOn)
		{
			SwitchCloakMode();
		}
		anim["yell"].time = 0f;
		anim.CrossFade("yell");
		Camera.main.SendMessage("FocusOnSceneElement", base.transform, SendMessageOptions.DontRequireReceiver);
		if ((bool)PredatorFace && (bool)PredatorMask)
		{
			PredatorMask.active = false;
			PredatorFace.active = true;
		}
		yield return new WaitForSeconds(anim["yell"].length);
		nextState = State.StateAttack;
	}

	protected override void EnemyDiedCleanup(int index, DeathType deathType)
	{
		RemoveSelfFromArray();
		healthBarMesh.active = false;
		survivalMissionController.EnemyBlackPredatorDied(index, deathType);
		healthBarMaterial.SetColor("_TintColor", new Color(2f / 15f, 0.85490197f, 1f));
	}

	private void OnApplicationQuit()
	{
		healthBarMaterial.SetColor("_TintColor", new Color(2f / 15f, 0.85490197f, 1f));
	}

	protected override IEnumerator Attack()
	{
		attackAnimation = AttackAnimation.RunTowardsPlayer;
		float strafeTimer = 0f;
		float angle = 0f;
		float predatorTargetSquareDistance = 0f;
		float squareAttackShootDistance = attackShootDistance * attackShootDistance;
		float squareAttackMeleeDistance = meleeRadius * meleeRadius;
		float blockedSpeed = speedPatrol * speedPatrol * 0.2f;
		while (!isDead)
		{
			predatorTargetSquareDistance = (xForm.position - currentTarget.position).sqrMagnitude;
			angle = RotateTowardsPosition(currentTarget.position, rotateSpeed);
			if (predatorTargetSquareDistance < squareAttackShootDistance)
			{
				if (predatorTargetSquareDistance < squareAttackMeleeDistance && angle > -10f && angle < 10f)
				{
					if (canPerformMeleeAttack)
					{
						StartCoroutine(CanPerformMeleeAttack());
						yield return StartCoroutine("PerformAnimation", EnemyAnimation.KnifeAttackHeavy);
						attackAnimation = AttackAnimation.RunAwayFromPlayer;
					}
				}
				else
				{
					strafeTimer -= Time.deltaTime;
					shootTimer += Time.deltaTime;
					if (shootTimer > shootRateSeconds)
					{
						anim.Play("plasma_shoot");
						particleBulletsEmitter.Emit(1);
						StartCoroutine(ShowMuzzleFlash());
						if (Utils.SfxOn)
						{
							base.GetComponent<AudioSource>().PlayOneShot(soundShoot);
						}
						shootTimer = 0f;
					}
					if (strafeTimer < 0f || characterController.velocity.sqrMagnitude < blockedSpeed)
					{
						strafeTimer = Random.Range(2f, 5f);
						if (Random.value > 0.5f)
						{
							attackAnimation = AttackAnimation.StrafeLeft;
						}
						else
						{
							attackAnimation = AttackAnimation.StrafeRight;
						}
					}
				}
			}
			else
			{
				attackAnimation = AttackAnimation.RunTowardsPlayer;
				strafeTimer = 0f;
			}
			if (mustSwitchToBossMode && !isInBossMode)
			{
				runawayTimer = 5f + Random.Range(-2f, 2f);
				nextState = State.StateRelocate;
				break;
			}
			if (hitPoints <= fullHitPoints / 2f)
			{
				runawayTimer = 7f + Random.Range(-2f, 2f);
				nextState = State.StateRelocate;
				break;
			}
			switch (attackAnimation)
			{
			case AttackAnimation.RunAwayFromPlayer:
				anim.CrossFade("run_bck");
				direction = xForm.TransformDirection(Vector3.back * speedPatrol);
				break;
			case AttackAnimation.RunTowardsPlayer:
				anim.CrossFade("run_fwd");
				direction = xForm.TransformDirection(Vector3.forward * speedAttack);
				if (characterController.velocity.sqrMagnitude < blockedSpeed && checkBlocking && lastColliderHit != null && foundObstacle)
				{
					foundObstacle = false;
					if (SelectGoAroundWaypoint())
					{
						yield return StartCoroutine("GoAround");
						anim.CrossFade("run_fwd");
					}
				}
				break;
			case AttackAnimation.StrafeLeft:
				anim.CrossFade("strafe_R");
				direction = xForm.TransformDirection(Vector3.right * speedAttack);
				break;
			case AttackAnimation.StrafeRight:
				anim.CrossFade("strafe_L");
				direction = xForm.TransformDirection(Vector3.left * speedAttack);
				break;
			}
			characterController.SimpleMove(direction);
			yield return null;
		}
	}
}
