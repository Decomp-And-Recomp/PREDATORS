using System.Collections;
using UnityEngine;

public class BaseSuperPredator : BaseEnemy
{
	protected enum AttackAnimation
	{
		StrafeLeft = 0,
		StrafeRight = 1,
		RunTowardsPlayer = 2,
		RunAwayFromPlayer = 3
	}

	private const float animWristAttackLightLHit = 0.1f;

	private const float speedWristAttack = 1.8f;

	private const float animWristAttackLightLMove = 2f / 15f;

	private const float animWristAttackLightLEnd = 1f / 3f;

	private const float animWristAttackLightRHit = 0.1f;

	private const float animWristAttackLightRMove = 2f / 15f;

	private const float animWristAttackLightREnd = 1f / 3f;

	private const float animWristAttackHeavyRMove = 0.2f;

	private const float animWristAttackHeavyREnd = 0.6f;

	private const float animSpearAttackLightRStartIdle = 2f / 15f;

	private const float animSpearAttackLightRStartMove = 0.2f;

	private const float animSpearAttackLightREnd = 1f / 3f;

	private const float animSpearAttackLightLStartIdle = 2f / 15f;

	private const float animSpearAttackLightLStartMove = 0.2f;

	private const float animSpearAttackLightLEnd = 1f / 3f;

	private const float animSpearAttackHeavyRMove = 2f / 15f;

	private const float animSpearAttackHeavyREnd = 1f;

	private const float animWhipLightR_Idle = 0.2f;

	private const float animWhipLightR_Move1 = 0.1f;

	private const float animWhipLightR_Move2 = 1f / 15f;

	private const float animWhipLightR_Idle_End = 0.3f;

	private const float animWhipLightL_Idle = 0.2f;

	private const float animWhipLightL_Move1 = 0.1f;

	private const float animWhipLightL_Move2 = 1f / 15f;

	private const float animWhipLightL_Idle_End = 0.3f;

	private const float animWhipHeavyR_Move = 2f / 15f;

	private const float animWhipHeavyR_Idle2 = 1f / 15f;

	private const float animWhipHeavyR_Idle3 = 1f / 15f;

	private const float animBlockBreakIdle = 2f / 3f;

	private const float animBlockMoveBackMove = 1f / 3f;

	private const float animBlockMoveBackIdle = 1f / 6f;

	protected const float speedStrafe = 1.58f;

	public float standIdleTime = 4f;

	public float blockModeTime = 4f;

	public float meleeRadius = 2f;

	public float relocateTimeInterval = 10f;

	public float blockBreakMeleeDamage = 100f;

	public float attackRate = 1f;

	public float wristBladesDamage = 40f;

	public float chanceToDoubleCombo = 0.99f;

	public AudioClip soundYell;

	public AudioClip soundWristBladeAttack;

	public AudioClip soundWristBladeHitImpale;

	public TrailRenderer trailRendererHandR;

	public TrailRenderer trailRendererHandL;

	public GameObject combiStickMesh;

	public GameObject TrailRendererSpearFront;

	public GameObject TrailRendererSpearBack;

	public AudioClip soundChangeWeapon;

	public AudioClip soundSpearAttack;

	public AudioClip soundSpearHit;

	public GameObject WhipWeapon;

	public float whipDamage = 30f;

	public Transform WhipMidPoint;

	public Transform WhipEndPoint;

	public TrailRenderer WhipTrailRenderer;

	public AudioClip soundWhipAttack1;

	public AudioClip soundWhipAttack2;

	public AudioClip soundWhipHit;

	protected float whipDamageRadius = 2f;

	protected int blockHits;

	protected Animation whipAnim;

	public float spearDamage = 50f;

	public float spearDamageRadius = 1.25f;

	protected AttackAnimation attackAnimation;

	protected AttackInfo attackInfoSuperPredator;

	protected float runawayTimer = -1f;

	protected float wristDamageRadius = 1f;

	protected bool canAttack = true;

	protected float speedRun = 5f;

	protected bool isInBossMode;

	protected bool canRelocateWhenLowOnHealth = true;

	protected override string animGoAround
	{
		get
		{
			return "run_fwd";
		}
	}

	protected override string animPatrol
	{
		get
		{
			return "melee_walk_front";
		}
	}

	protected override ArrayList AlliesArray
	{
		get
		{
			return AManager.instance.predatorTargets;
		}
	}

	protected override ArrayList EnemyArray
	{
		get
		{
			return AManager.instance.humanTargets;
		}
	}

	protected virtual IEnumerator StandIdle()
	{
		anim.CrossFade("idle");
		yield return new WaitForSeconds(standIdleTime);
		nextState = State.StateAttack;
	}

	protected override IEnumerator EnemyStateLoop()
	{
		while (!isDead)
		{
			if (AManager.instance.CinematicInProgress)
			{
				yield return StartCoroutine("StandIdle");
			}
			else
			{
				switch (nextState)
				{
				case State.StateAttack:
					if (isInBossMode)
					{
						yield return StartCoroutine("WristAttack");
					}
					else
					{
						yield return StartCoroutine("Attack");
					}
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
					yield return StartCoroutine("Attack");
					break;
				case State.StateStrafe:
					yield return StartCoroutine("StrafeAround");
					break;
				case State.StateGoingIntoBossMode:
					yield return StartCoroutine("GoingIntoBossMode");
					break;
				case State.StateWristAttack:
					yield return StartCoroutine("WristAttack");
					break;
				default:
					yield return StartCoroutine("Patrol");
					break;
				}
			}
			yield return null;
		}
	}

	private IEnumerator DecreaseBlockHits()
	{
		yield return new WaitForSeconds(4f);
		blockHits = 0;
	}

	public override void ApplyDamage(AttackInfo attackInfo)
	{
		if (AManager.instance.CinematicInProgress)
		{
			return;
		}
		if (netgunCaptured)
		{
			netgunCaptured = false;
			if ((bool)netOnEnemy)
			{
				netOnEnemy.gameObject.SetActiveRecursively(false);
				netOnEnemy = null;
			}
		}
		if (nextState == State.StateRelocate)
		{
			if (blocking)
			{
				hitPoints -= attackInfo.Damage * blockingMultiplier;
				if (Utils.SfxOn)
				{
					base.GetComponent<AudioSource>().PlayOneShot(soundBlock);
				}
				if ((bool)particleSparks)
				{
					particleSparks.transform.position = xForm.position + Vector3.up;
					particleSparks.transform.rotation = Quaternion.LookRotation(xForm.position - currentTarget.position);
					particleSparks.Emit();
				}
			}
			else
			{
				hitPoints -= attackInfo.Damage;
				if (AManager.BloodOn)
				{
					StartCoroutine(SprayBlood(xForm.position + Vector3.up, AManager.PoolObjectType.BloodSprayPredatorAnimated));
				}
			}
			if (hitPoints <= 0f)
			{
				base.ApplyDamage(attackInfo);
			}
			return;
		}
		if (blocking)
		{
			if (attackInfo.Damage > blockingDamageHurtTreshold && Random.value < 0.9f)
			{
				StopCoroutine("ApplyDamageGrabbedCR");
				StopCoroutines();
				StartCoroutine("BlockBreak");
				return;
			}
			blockHits++;
			if (blockHits >= 5)
			{
				blockHits = 0;
				nextState = State.StateRelocate;
				StartEnemyStateLoop();
				return;
			}
			StopCoroutine("DecreaseBlockHits");
			StartCoroutine("DecreaseBlockHits");
			hitPoints -= attackInfo.Damage * blockingMultiplier;
			if (Utils.SfxOn)
			{
				base.GetComponent<AudioSource>().PlayOneShot(soundBlock);
			}
			if ((bool)particleSparks)
			{
				particleSparks.transform.position = xForm.position + Vector3.up;
				particleSparks.transform.rotation = Quaternion.LookRotation(xForm.position - currentTarget.position);
				particleSparks.Emit();
			}
			if (Random.value < 0.4f)
			{
				StopCoroutine("ApplyDamageGrabbedCR");
				StopCoroutines();
				StartCoroutine("BlockBreak");
			}
			return;
		}
		blockHits++;
		if (blockHits >= 5)
		{
			blockHits = 0;
			nextState = State.StateRelocate;
			StartEnemyStateLoop();
			return;
		}
		StopCoroutine("DecreaseBlockHits");
		StartCoroutine("DecreaseBlockHits");
		if (Random.value < chanceToBlock)
		{
			nextState = State.StateBlock;
			StartEnemyStateLoop();
		}
		else
		{
			chanceToBlock *= 1.1f;
			base.ApplyDamage(attackInfo);
		}
	}

	private IEnumerator BlockBreak()
	{
		if (Utils.SfxOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(soundBlock);
		}
		if ((bool)particleSparks)
		{
			particleSparks.transform.position = xForm.position + Vector3.up;
			particleSparks.transform.rotation = Quaternion.LookRotation(xForm.position - currentTarget.position);
			particleSparks.Emit();
		}
		yield return StartCoroutine("PerformAnimation", EnemyAnimation.BlockBreak);
		nextState = State.StateAttack;
		StartEnemyStateLoop();
	}

	protected override void StopCoroutines()
	{
		if ((bool)trailRendererHandL)
		{
			trailRendererHandL.gameObject.active = false;
		}
		if ((bool)trailRendererHandR)
		{
			trailRendererHandR.gameObject.active = false;
		}
		if ((bool)TrailRendererSpearBack)
		{
			TrailRendererSpearBack.gameObject.active = false;
		}
		if ((bool)TrailRendererSpearFront)
		{
			TrailRendererSpearFront.gameObject.active = false;
		}
		StopCoroutine("GoingIntoBossMode");
		StopCoroutine("BlockBreak");
		StopCoroutine("WristAttack");
		blocking = false;
		base.StopCoroutines();
	}

	protected override State stateAfterGettingHurtAndPredatorVisible()
	{
		return State.StateAttack;
	}

	protected IEnumerator CanAttackAgain()
	{
		yield return new WaitForSeconds(attackRate);
		canAttack = true;
	}

	protected IEnumerator CanRelocateWhenLowOnHealthAgain()
	{
		yield return new WaitForSeconds(relocateTimeInterval);
		canRelocateWhenLowOnHealth = true;
	}

	public override void Activate(int indexShooterLevel)
	{
		speedGoAround = 5f;
		chanceToBlock = initialChanceToBlock;
		attackInfoSuperPredator = new AttackInfo(20f);
		canRelocateWhenLowOnHealth = true;
		isInBossMode = false;
		speedPatrol = 1.58f;
		if ((bool)WhipWeapon)
		{
			whipAnim = WhipWeapon.GetComponent<Animation>();
			if ((bool)whipAnim)
			{
				whipAnim["whip_grapple_dash_pose"].wrapMode = WrapMode.ClampForever;
				whipAnim["whip_idle"].wrapMode = WrapMode.Loop;
				whipAnim["whip_attack_heavy_R_charge"].wrapMode = WrapMode.ClampForever;
				whipAnim["whip_attack_heavy_R_charge"].speed = 0.5f;
				whipAnim["whip_retract"].wrapMode = WrapMode.Loop;
			}
			else
			{
				Debug.LogError("WhipWeapon Animation not set right");
			}
		}
		base.Activate(indexShooterLevel);
		anim["attack_light_wrist_L"].wrapMode = WrapMode.Once;
		anim["attack_light_wrist_R"].wrapMode = WrapMode.Once;
		anim["attack_heavy_R"].wrapMode = WrapMode.Once;
		anim["yell"].wrapMode = WrapMode.Once;
		anim["block_move_back"].wrapMode = WrapMode.Once;
		anim["block_break"].wrapMode = WrapMode.Once;
	}

	protected virtual IEnumerator PerformAnimation(EnemyAnimation attackAnimationType)
	{
		anim.Stop("knife_block_upper_mix");
		Vector3 attackDirection = (currentTarget.position - xForm.position).normalized;
		switch (attackAnimationType)
		{
		case EnemyAnimation.KnifeAttackL:
		{
			if (Utils.SfxOn)
			{
				base.GetComponent<AudioSource>().PlayOneShot(soundWristBladeAttack);
			}
			anim["attack_light_wrist_L"].time = 0f;
			anim.CrossFade("attack_light_wrist_L", 0.1f);
			trailRendererHandL.gameObject.active = true;
			float timer5 = 0.1f;
			targetRotation = Quaternion.LookRotation(attackDirection);
			while (timer5 > 0f)
			{
				timer5 -= Time.deltaTime;
				characterController.Move(attackDirection * 1.8f * Time.deltaTime);
				xForm.rotation = Quaternion.Slerp(xForm.rotation, targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			attackInfoSuperPredator.Damage = wristBladesDamage;
			attackInfoSuperPredator.AnimationNr = 2;
			attackInfoSuperPredator.AttackerPosition = xForm.position;
			if ((bool)currentTarget && (xForm.position - currentTarget.position).sqrMagnitude < wristDamageRadius * wristDamageRadius)
			{
				currentTarget.SendMessage("ApplyDamage", attackInfoSuperPredator, SendMessageOptions.DontRequireReceiver);
			}
			timer5 = 2f / 15f;
			while (timer5 > 0f)
			{
				timer5 -= Time.deltaTime;
				characterController.Move(attackDirection * 1.8f * Time.deltaTime);
				yield return null;
			}
			trailRendererHandL.gameObject.active = false;
			yield return new WaitForSeconds(1f / 3f);
			break;
		}
		case EnemyAnimation.KnifeAttackR:
		{
			if (Utils.SfxOn)
			{
				base.GetComponent<AudioSource>().PlayOneShot(soundWristBladeAttack);
			}
			anim["attack_light_wrist_R"].time = 0f;
			anim.CrossFade("attack_light_wrist_R", 0.1f);
			trailRendererHandR.gameObject.active = true;
			float timer5 = 0.1f;
			targetRotation = Quaternion.LookRotation(attackDirection);
			while (timer5 > 0f)
			{
				timer5 -= Time.deltaTime;
				characterController.Move(attackDirection * 1.8f * Time.deltaTime);
				xForm.rotation = Quaternion.Slerp(xForm.rotation, targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			attackInfoSuperPredator.Damage = wristBladesDamage;
			attackInfoSuperPredator.AnimationNr = 3;
			attackInfoSuperPredator.AttackerPosition = xForm.position;
			if ((bool)currentTarget && (xForm.position - currentTarget.position).sqrMagnitude < wristDamageRadius * wristDamageRadius)
			{
				currentTarget.SendMessage("ApplyDamage", attackInfoSuperPredator, SendMessageOptions.DontRequireReceiver);
			}
			timer5 = 2f / 15f;
			while (timer5 > 0f)
			{
				timer5 -= Time.deltaTime;
				characterController.Move(attackDirection * 1.8f * Time.deltaTime);
				yield return null;
			}
			trailRendererHandR.gameObject.active = false;
			yield return new WaitForSeconds(1f / 3f);
			break;
		}
		case EnemyAnimation.KnifeAttackHeavy:
		{
			anim["attack_heavy_R"].time = 0f;
			anim.CrossFade("attack_heavy_R", 0.1f);
			trailRendererHandR.gameObject.active = true;
			if (Utils.SfxOn)
			{
				base.GetComponent<AudioSource>().PlayOneShot(soundWristBladeAttack);
			}
			float timer5 = 0.2f;
			targetRotation = Quaternion.LookRotation(attackDirection);
			while (timer5 > 0f)
			{
				timer5 -= Time.deltaTime;
				characterController.Move(attackDirection * 1.8f * Time.deltaTime);
				xForm.rotation = Quaternion.Slerp(xForm.rotation, targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			attackInfoSuperPredator.Damage = wristBladesDamage * 2f;
			attackInfoSuperPredator.AnimationNr = 1;
			attackInfoSuperPredator.AttackerPosition = xForm.position;
			if ((bool)currentTarget && (xForm.position - currentTarget.position).sqrMagnitude < wristDamageRadius * wristDamageRadius)
			{
				if (Utils.SfxOn)
				{
					base.GetComponent<AudioSource>().PlayOneShot(soundWristBladeHitImpale);
				}
				currentTarget.SendMessage("ApplyDamage", attackInfoSuperPredator, SendMessageOptions.DontRequireReceiver);
			}
			trailRendererHandR.gameObject.active = false;
			yield return new WaitForSeconds(0.6f);
			break;
		}
		case EnemyAnimation.SpearAttackLightL:
		{
			combiStickMesh.gameObject.active = true;
			if (Utils.SfxOn)
			{
				base.GetComponent<AudioSource>().PlayOneShot(soundSpearAttack);
			}
			anim["spear_attack_light_L"].time = 0f;
			anim.CrossFade("spear_attack_light_L", 0.1f);
			TrailRendererSpearFront.gameObject.active = true;
			float timer5 = 2f / 15f;
			targetRotation = Quaternion.LookRotation(attackDirection);
			while (timer5 > 0f)
			{
				timer5 -= Time.deltaTime;
				xForm.rotation = Quaternion.Slerp(xForm.rotation, targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			timer5 = 0.2f;
			targetRotation = Quaternion.LookRotation(attackDirection);
			while (timer5 > 0f)
			{
				timer5 -= Time.deltaTime;
				characterController.Move(attackDirection * 3.6f * Time.deltaTime);
				xForm.rotation = Quaternion.Slerp(xForm.rotation, targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			attackInfoSuperPredator.Damage = spearDamage;
			attackInfoSuperPredator.AnimationNr = 2;
			attackInfoSuperPredator.AttackerPosition = xForm.position;
			if ((bool)currentTarget && (xForm.position - currentTarget.position).sqrMagnitude < spearDamageRadius * spearDamageRadius)
			{
				if (Utils.SfxOn)
				{
					base.GetComponent<AudioSource>().PlayOneShot(soundSpearHit);
				}
				currentTarget.SendMessage("ApplyDamage", attackInfoSuperPredator, SendMessageOptions.DontRequireReceiver);
			}
			TrailRendererSpearFront.gameObject.active = false;
			yield return new WaitForSeconds(1f / 3f);
			break;
		}
		case EnemyAnimation.SpearAttackLightR:
		{
			combiStickMesh.gameObject.active = true;
			if (Utils.SfxOn)
			{
				base.GetComponent<AudioSource>().PlayOneShot(soundSpearAttack);
			}
			anim["spear_attack_light_R"].time = 0f;
			anim.CrossFade("spear_attack_light_R", 0.1f);
			TrailRendererSpearBack.gameObject.active = true;
			float timer5 = 2f / 15f;
			targetRotation = Quaternion.LookRotation(attackDirection);
			while (timer5 > 0f)
			{
				timer5 -= Time.deltaTime;
				xForm.rotation = Quaternion.Slerp(xForm.rotation, targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			timer5 = 0.2f;
			targetRotation = Quaternion.LookRotation(attackDirection);
			while (timer5 > 0f)
			{
				timer5 -= Time.deltaTime;
				characterController.Move(attackDirection * 3.6f * Time.deltaTime);
				xForm.rotation = Quaternion.Slerp(xForm.rotation, targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			attackInfoSuperPredator.Damage = spearDamage;
			attackInfoSuperPredator.AnimationNr = 3;
			attackInfoSuperPredator.AttackerPosition = xForm.position;
			if ((bool)currentTarget && (xForm.position - currentTarget.position).sqrMagnitude < spearDamageRadius * spearDamageRadius)
			{
				if (Utils.SfxOn)
				{
					base.GetComponent<AudioSource>().PlayOneShot(soundSpearHit);
				}
				currentTarget.SendMessage("ApplyDamage", attackInfoSuperPredator, SendMessageOptions.DontRequireReceiver);
			}
			TrailRendererSpearBack.gameObject.active = false;
			yield return new WaitForSeconds(1f / 3f);
			break;
		}
		case EnemyAnimation.SpearAttackHeavyR:
		{
			combiStickMesh.gameObject.active = true;
			TrailRendererSpearFront.gameObject.active = true;
			anim["spear_attack_heavy_R"].time = 0f;
			anim.CrossFade("spear_attack_heavy_R", 0.1f);
			if (Utils.SfxOn)
			{
				base.GetComponent<AudioSource>().PlayOneShot(soundSpearAttack);
			}
			float timer5 = 2f / 15f;
			targetRotation = Quaternion.LookRotation(attackDirection);
			while (timer5 > 0f)
			{
				timer5 -= Time.deltaTime;
				characterController.Move(attackDirection * 2.67f * Time.deltaTime);
				xForm.rotation = Quaternion.Slerp(xForm.rotation, targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			attackInfoSuperPredator.Damage = spearDamage * 2f;
			attackInfoSuperPredator.AnimationNr = 1;
			attackInfoSuperPredator.AttackerPosition = xForm.position;
			if ((bool)currentTarget && (xForm.position - currentTarget.position).sqrMagnitude < spearDamageRadius * spearDamageRadius)
			{
				if (Utils.SfxOn)
				{
					base.GetComponent<AudioSource>().PlayOneShot(soundSpearHit);
				}
				currentTarget.SendMessage("ApplyDamage", attackInfoSuperPredator, SendMessageOptions.DontRequireReceiver);
			}
			TrailRendererSpearFront.gameObject.active = false;
			yield return new WaitForSeconds(1f);
			break;
		}
		case EnemyAnimation.WhipAttackLightL:
		{
			WhipTrailRenderer.enabled = true;
			if (Utils.SfxOn)
			{
				base.GetComponent<AudioSource>().PlayOneShot(soundWhipAttack2);
			}
			anim["whip_attack_light_L"].time = 0f;
			anim.CrossFade("whip_attack_light_L", 0.1f);
			whipAnim["whip_attack_light_L"].time = 0f;
			whipAnim.CrossFade("whip_attack_light_L", 0.1f);
			float timer5 = 0.2f;
			targetRotation = Quaternion.LookRotation(attackDirection);
			while (timer5 > 0f)
			{
				timer5 -= Time.deltaTime;
				xForm.rotation = Quaternion.Slerp(xForm.rotation, targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			timer5 = 0.1f;
			targetRotation = Quaternion.LookRotation(attackDirection);
			while (timer5 > 0f)
			{
				timer5 -= Time.deltaTime;
				characterController.Move(attackDirection * 3.6f * Time.deltaTime);
				xForm.rotation = Quaternion.Slerp(xForm.rotation, targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			attackInfoSuperPredator.Damage = whipDamage;
			attackInfoSuperPredator.AnimationNr = 2;
			attackInfoSuperPredator.AttackerPosition = xForm.position;
			if ((bool)currentTarget && ((WhipEndPoint.position - currentTarget.position).sqrMagnitude < whipDamageRadius * whipDamageRadius || (WhipMidPoint.position - currentTarget.position).sqrMagnitude < whipDamageRadius * whipDamageRadius))
			{
				if (Utils.SfxOn)
				{
					base.GetComponent<AudioSource>().PlayOneShot(soundWhipHit);
				}
				currentTarget.SendMessage("ApplyDamage", attackInfoSuperPredator, SendMessageOptions.DontRequireReceiver);
			}
			timer5 = 1f / 15f;
			targetRotation = Quaternion.LookRotation(attackDirection);
			while (timer5 > 0f)
			{
				timer5 -= Time.deltaTime;
				characterController.Move(attackDirection * 3.6f * Time.deltaTime);
				xForm.rotation = Quaternion.Slerp(xForm.rotation, targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			yield return new WaitForSeconds(0.3f);
			break;
		}
		case EnemyAnimation.WhipAttackLightR:
		{
			WhipTrailRenderer.enabled = true;
			if (Utils.SfxOn)
			{
				base.GetComponent<AudioSource>().PlayOneShot(soundWhipAttack1);
			}
			anim["whip_attack_light_R"].time = 0f;
			anim.CrossFade("whip_attack_light_R", 0.1f);
			whipAnim["whip_attack_light_R"].time = 0f;
			whipAnim.CrossFade("whip_attack_light_R", 0.1f);
			float timer5 = 0.2f;
			targetRotation = Quaternion.LookRotation(attackDirection);
			while (timer5 > 0f)
			{
				timer5 -= Time.deltaTime;
				xForm.rotation = Quaternion.Slerp(xForm.rotation, targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			timer5 = 0.1f;
			targetRotation = Quaternion.LookRotation(attackDirection);
			while (timer5 > 0f)
			{
				timer5 -= Time.deltaTime;
				characterController.Move(attackDirection * 3.6f * Time.deltaTime);
				xForm.rotation = Quaternion.Slerp(xForm.rotation, targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			attackInfoSuperPredator.Damage = whipDamage;
			attackInfoSuperPredator.AnimationNr = 1;
			attackInfoSuperPredator.AttackerPosition = xForm.position;
			if ((bool)currentTarget && ((WhipEndPoint.position - currentTarget.position).sqrMagnitude < whipDamageRadius * whipDamageRadius || (WhipMidPoint.position - currentTarget.position).sqrMagnitude < whipDamageRadius * whipDamageRadius))
			{
				if (Utils.SfxOn)
				{
					base.GetComponent<AudioSource>().PlayOneShot(soundWhipHit);
				}
				currentTarget.SendMessage("ApplyDamage", attackInfoSuperPredator, SendMessageOptions.DontRequireReceiver);
			}
			timer5 = 1f / 15f;
			targetRotation = Quaternion.LookRotation(attackDirection);
			while (timer5 > 0f)
			{
				timer5 -= Time.deltaTime;
				characterController.Move(attackDirection * 3.6f * Time.deltaTime);
				xForm.rotation = Quaternion.Slerp(xForm.rotation, targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			yield return new WaitForSeconds(0.3f);
			break;
		}
		case EnemyAnimation.WhipAttackHeavyR:
		{
			WhipTrailRenderer.enabled = true;
			anim["whip_attack_heavy_R"].time = 0f;
			anim.CrossFade("whip_attack_heavy_R", 0.1f);
			whipAnim["whip_attack_heavy_R"].time = 0f;
			whipAnim.CrossFade("whip_attack_heavy_R", 0.1f);
			if (Utils.SfxOn)
			{
				base.GetComponent<AudioSource>().PlayOneShot(soundWhipAttack1);
			}
			float timer5 = 2f / 15f;
			targetRotation = Quaternion.LookRotation(attackDirection);
			while (timer5 > 0f)
			{
				timer5 -= Time.deltaTime;
				characterController.Move(attackDirection * 2.67f * Time.deltaTime);
				xForm.rotation = Quaternion.Slerp(xForm.rotation, targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			attackInfoSuperPredator.Damage = whipDamage * 3f;
			attackInfoSuperPredator.AnimationNr = 1;
			attackInfoSuperPredator.AttackerPosition = xForm.position;
			if ((bool)currentTarget && ((WhipEndPoint.position - currentTarget.position).sqrMagnitude < whipDamageRadius * whipDamageRadius || (WhipMidPoint.position - currentTarget.position).sqrMagnitude < whipDamageRadius * whipDamageRadius))
			{
				if (Utils.SfxOn)
				{
					base.GetComponent<AudioSource>().PlayOneShot(soundWhipHit);
				}
				currentTarget.SendMessage("ApplyDamage", attackInfoSuperPredator, SendMessageOptions.DontRequireReceiver);
			}
			timer5 = 1f / 15f;
			targetRotation = Quaternion.LookRotation(attackDirection);
			while (timer5 > 0f)
			{
				timer5 -= Time.deltaTime;
				xForm.rotation = Quaternion.Slerp(xForm.rotation, targetRotation, Time.deltaTime * rotateSpeed);
				yield return null;
			}
			if ((bool)currentTarget && ((WhipEndPoint.position - currentTarget.position).sqrMagnitude < whipDamageRadius * whipDamageRadius || (WhipMidPoint.position - currentTarget.position).sqrMagnitude < whipDamageRadius * whipDamageRadius))
			{
				if (Utils.SfxOn)
				{
					base.GetComponent<AudioSource>().PlayOneShot(soundWhipHit);
				}
				currentTarget.SendMessage("ApplyDamage", attackInfoSuperPredator, SendMessageOptions.DontRequireReceiver);
			}
			break;
		}
		case EnemyAnimation.BlockBreak:
			if (!currentTarget)
			{
				break;
			}
			if ((xForm.position - currentTarget.position).sqrMagnitude < wristDamageRadius * wristDamageRadius)
			{
				attackInfoSuperPredator.Damage = blockBreakMeleeDamage;
				attackInfoSuperPredator.AnimationNr = 1;
				attackInfoSuperPredator.AttackerPosition = xForm.position;
				currentTarget.SendMessage("HurtMoveBack", attackInfoSuperPredator, SendMessageOptions.DontRequireReceiver);
				anim["block_break"].time = 0f;
				anim.CrossFade("block_break", 0.1f);
				Vector3 hitDirection = (xForm.position - currentTarget.position).normalized;
				float timer5 = 2f / 3f;
				targetRotation = Quaternion.LookRotation(hitDirection);
				while (timer5 > 0f)
				{
					timer5 -= Time.deltaTime;
					targetRotation = Quaternion.Slerp(xForm.rotation, targetRotation, Time.deltaTime * rotateSpeed * 100f);
					yield return null;
				}
				nextState = State.StateAttack;
			}
			else
			{
				anim["block_move_back"].time = 0f;
				anim.CrossFade("block_move_back", 0.1f);
				Vector3 hitDirection2 = (xForm.position - currentTarget.position).normalized;
				float timer5 = 1f / 3f;
				targetRotation = Quaternion.LookRotation(hitDirection2);
				while (timer5 > 0f)
				{
					timer5 -= Time.deltaTime;
					targetRotation = Quaternion.Slerp(xForm.rotation, targetRotation, Time.deltaTime * rotateSpeed * 100f);
					characterController.SimpleMove(hitDirection2 * speedHurt);
					yield return null;
				}
				yield return new WaitForSeconds(1f / 6f);
				nextState = State.StateBlock;
			}
			break;
		}
	}

	protected override IEnumerator Block()
	{
		float blockTimer = 0f;
		blocking = true;
		anim.CrossFade("block");
		while (!isDead)
		{
			blockTimer += Time.deltaTime;
			if (blockTimer > blockModeTime)
			{
				nextState = State.StateAttack;
				blocking = false;
				chanceToBlock = initialChanceToBlock;
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
				nextState = State.StateAttack;
				blocking = false;
				lastSeenPosition = new Vector3(currentTarget.position.x, currentTarget.position.y, currentTarget.position.z);
				break;
			}
			yield return null;
		}
	}

	protected override State stateAfterGettingHurtAndPredatorInvisible()
	{
		return State.StateAttack;
	}

	protected virtual IEnumerator WristAttack()
	{
		float blockTimer = 0f;
		while (!isDead)
		{
			blockTimer += Time.deltaTime;
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
				if (characterController.velocity.sqrMagnitude < speedRun * speedRun * 0.5f && checkBlocking && lastColliderHit != null && foundObstacle)
				{
					foundObstacle = false;
					if (SelectGoAroundWaypoint())
					{
						yield return StartCoroutine("GoAround");
						anim.CrossFade("run_fwd");
					}
				}
			}
			yield return null;
		}
	}
}
