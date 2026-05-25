using System.Collections;
using UnityEngine;

public class BaseEnemy : Creature
{
	public enum EnemyAnimation
	{
		MeleeShotgunAttack = 1,
		GetHitBlocking = 2,
		KnifeAttackL = 3,
		KnifeAttackR = 4,
		KnifeAttackHeavy = 5,
		GetHitBlockingKnife = 6,
		SpearAttackLightL = 7,
		SpearAttackLightR = 8,
		SpearAttackHeavyR = 9,
		DogAttackRunning = 10,
		DogAttackMelee = 11,
		WhipAttackLightR = 12,
		WhipAttackLightL = 13,
		WhipAttackHeavyR = 14,
		BlockBreak = 15
	}

	protected enum State
	{
		StateBerserkFire = 0,
		StateStrafe = 1,
		StateRelocate = 2,
		StatePatrol = 3,
		StateAttack = 4,
		StateRunaway = 5,
		StatePanic = 6,
		StateBlock = 7,
		StateGoingIntoBossMode = 8,
		StateWristAttack = 9,
		StateCombiAttack = 10,
		StateAxeAttack = 11,
		StateFalconsAttack = 12,
		StateIdle = 13
	}

	protected const float minShootDistanceSquare = 1f;

	private const float animGrabbedStartLength = 1.2f;

	private const float animGrabbedHitHigh = 0.6f;

	private const float animGrabbedHitLow = 0.6f;

	private const float animGrabbedHeadOffStart = 1.5f;

	public GameObject deadHead;

	public Transform BoneMixChest;

	public Transform poolWaypointsParent;

	public TriangleTarget BossCircle;

	public TriangleTarget TrophyKillIcon;

	public Transform radarDot3D;

	public float originalFullHitPoints = 150f;

	public float maxFullHitPoints = 400f;

	protected float cloakDetectionRadius = 3.5f;

	protected float cloakDetectionAngle = 20f;

	public float dieSetParticlesInactiveTime = 1f;

	public float rotateSpeed = 180f;

	public float attackDistance = 17f;

	public float panicTime = 3.96f;

	public float attackShootDistance = 10f;

	public float attackShootDistanceRandomRange = 3f;

	public float freakOutRadius = 20f;

	protected float speedAttack = 3.652f;

	protected float speedGoAround = 3.17f;

	protected float speedJog = 3.652f;

	protected float speedRunAway = 3.17f;

	protected float speedPatrol = 1f;

	public float attackSpeedRandomRange = 0.5f;

	public float shootRateSeconds = 0.7f;

	public float shootRateRandomRange = 0.1f;

	private float maxGrabbedTime = 20f;

	public Vector3 meleeOffset = new Vector3(0f, 0f, 1f);

	public float meleeRadiusDamage = 1f;

	protected float meleeDamage = 30f;

	protected bool visible = true;

	public float canPerformMeleeAttackDelay = 3f;

	public float chanceToBlock = 0.25f;

	protected float chanceToBreakLooseGrabbed;

	public Transform BodyCutUpperHalf;

	public Transform BodyCutLowerHalf;

	public Transform BodyCutNoHead;

	private Transform BodyCutVerticallyL;

	private Transform BodyCutVerticallyR;

	public AudioClip soundHit;

	public AudioClip soundHit2;

	public AudioClip soundBlock;

	public AudioClip soundDie;

	public AudioClip soundDieCut;

	public ParticleEmitter particleSparks;

	private Camera mainCamera;

	private float iconWidth = 0.1f;

	private float iconHeight = 0.1f;

	protected EnemyType enemyType = EnemyType.SoldierMachete;

	protected bool wasCapturedByNetGunAtLeastOnce;

	protected float initialChanceToBlock;

	protected float initialChanceToBreakLoose;

	protected bool trophyKillPossible = true;

	protected bool isBoss;

	protected bool canShoot = true;

	protected int shooterIndex;

	protected AttackInfo attackInfoEnemy;

	protected AttackInfo attackInfoFromApplyDamage;

	protected float shootTimer = 5f;

	protected float shootTimerMoveForward = 0.5f;

	protected float fullHitPoints = 500f;

	protected float controllerRadius = 1f;

	protected float speedHurt = 1.8f;

	protected float speedKnifeAttack = 1.8f;

	protected float speedShotgunMelee = 1.8f;

	protected float speedGetGrabbed = 10f;

	protected AnimationInfo animationInfo;

	protected Vector3 bloodSprayOffset = new Vector3(0f, 1f, -0.7f);

	protected Vector3 grabbedVictimOffset = new Vector3(0f, 0f, 0.4f);

	protected State nextState = State.StatePatrol;

	protected Transform currentTarget;

	protected Vector3 lastSeenPosition;

	protected bool canBlock = true;

	protected float canBlockAgainTimer = 5f;

	protected bool mustSwitchToBossMode;

	protected Color originalColor;

	protected ControllerColliderHit lastColliderHit;

	protected Transform currentWaypoint;

	protected Transform lastWaypoint;

	protected Transform destinationWaypoint;

	protected Transform[] waypoints;

	protected PlayerController playerController;

	protected Animation anim;

	protected Vector3 direction;

	protected Vector3 targetDir = Vector3.forward;

	protected Vector3 collisionNormal = Vector3.forward;

	protected SurvivalMissionController survivalMissionController;

	protected bool grabbedVictim;

	protected bool blocking;

	protected float blockingMultiplier = 0.3f;

	protected float blockingDamageHurtTreshold = 25f;

	protected float initialBlockingDamageHurtTreshold = 25f;

	protected Quaternion targetRotation;

	protected bool canPerformMeleeAttack = true;

	protected bool checkBlocking;

	protected float discHitMultiplier = 8f;

	protected float discTorqueMultiplier = 6f;

	protected Transform netOnEnemy;

	protected bool netgunCaptured;

	protected bool seekPredator;

	protected bool goingAroundObstacles;

	private float wayPointRadiusSqr = 0.5f;

	private float maxBlockedTimerGoAround = 2f;

	protected bool foundObstacle;

	protected ArrayList colliderChildren;

	protected Transform bestTransformRight;

	protected Transform bestTransformLeft;

	protected Transform bestTransform;

	protected Transform tempTransform;

	protected Transform waypoint;

	private Collider groundCollider;

	protected bool canShowRadarDot;

	protected LineRenderer visibilityLineRenderer;

	private float distanceEnemyLosesSightOfPlayerWhenDetected = 6f;

	public bool Blocking
	{
		get
		{
			return blocking;
		}
	}

	public float HitPoints
	{
		get
		{
			return hitPoints;
		}
	}

	public bool Dead
	{
		get
		{
			return isDead;
		}
	}

	protected virtual ArrayList EnemyArray
	{
		get
		{
			return AManager.instance.predatorTargets;
		}
	}

	protected virtual ArrayList AlliesArray
	{
		get
		{
			return AManager.instance.humanTargets;
		}
	}

	protected virtual string animGetHurtLow1
	{
		get
		{
			return "hurt_low1";
		}
	}

	protected virtual string animGetHurtLow2
	{
		get
		{
			return "hurt_low2";
		}
	}

	protected virtual string animGetHurtR1
	{
		get
		{
			return "hurt_R1";
		}
	}

	protected virtual string animGetHurtR2
	{
		get
		{
			return "hurt_R2";
		}
	}

	protected virtual string animGetHurtL1
	{
		get
		{
			return "hurt_L1";
		}
	}

	protected virtual string animGetHurtL2
	{
		get
		{
			return "hurt_L2";
		}
	}

	protected virtual string animGetHurtHigh1
	{
		get
		{
			return "hurt_high1";
		}
	}

	protected virtual string animGetHurtHigh2
	{
		get
		{
			return "hurt_high2";
		}
	}

	protected virtual string animPatrol
	{
		get
		{
			return "walk_searching";
		}
	}

	protected virtual string animJog
	{
		get
		{
			return "jog_weapon_lowered";
		}
	}

	protected virtual string animGrabbedBreakLoose
	{
		get
		{
			return "grabbed_break_loose";
		}
	}

	protected virtual string animGoAround
	{
		get
		{
			return "jog_weapon_lowered";
		}
	}

	protected virtual string animDieFallBack
	{
		get
		{
			return "die_fall_back";
		}
	}

	protected virtual string animNetGunCaptured
	{
		get
		{
			return "netgun_captured";
		}
	}

	protected virtual string animDieFallFront
	{
		get
		{
			return "die_fall_front";
		}
	}

	public bool GrabbedVictim
	{
		get
		{
			return grabbedVictim;
		}
		set
		{
			grabbedVictim = value;
		}
	}

	protected void SetCurrentTarget()
	{
		ArrayList enemyArray = EnemyArray;
		if (AManager.PredatorInvisible)
		{
			float num = AngleBetweenXformAnd(AManager.targetPosition);
			if ((xForm.position - AManager.targetPosition).sqrMagnitude < cloakDetectionRadius * cloakDetectionRadius && num < cloakDetectionAngle && num > 0f - cloakDetectionAngle)
			{
				lastSeenPosition = AManager.targetPosition;
				nextState = stateIfPredatorNotVisibleButDetected();
				currentTarget = playerController.transform;
				return;
			}
			enemyArray.Remove(playerController.transform);
		}
		if (enemyArray.Count > 0)
		{
			Transform transform = (Transform)enemyArray[0];
			float num2 = (xForm.position - transform.position).sqrMagnitude;
			for (int i = 1; i < enemyArray.Count; i++)
			{
				Transform transform2 = (Transform)enemyArray[i];
				float sqrMagnitude = (xForm.position - transform2.position).sqrMagnitude;
				if (num2 > sqrMagnitude)
				{
					num2 = sqrMagnitude;
					transform = transform2;
				}
			}
			currentTarget = transform;
		}
		else
		{
			ChooseRandomWaypoint();
			currentTarget = currentWaypoint;
		}
		if (AManager.PredatorInvisible)
		{
			enemyArray.Add(playerController.transform);
		}
	}

	public virtual void BossMode()
	{
		if ((bool)BossCircle)
		{
			BossCircle.gameObject.active = true;
			BossCircle.SetTarget(base.transform);
		}
		chanceToBlock *= 2f;
		chanceToBreakLooseGrabbed = 0.5f;
		blockingDamageHurtTreshold *= 2f;
		hitPoints = maxFullHitPoints;
		fullHitPoints = hitPoints;
		seekPredator = true;
		isBoss = true;
		trophyKillPossible = false;
		mainCamera.SendMessage("FocusOnCharacter", base.transform, SendMessageOptions.DontRequireReceiver);
	}

	private void OnDisable()
	{
		if ((bool)radarDot3D)
		{
			radarDot3D.GetComponent<Renderer>().enabled = false;
		}
	}

	private void OnEnable()
	{
		if ((bool)radarDot3D)
		{
			if (!radarDot3D.GetComponent<Collider>())
			{
				radarDot3D.gameObject.AddComponent<BoxCollider>();
			}
			iconWidth = ((BoxCollider)radarDot3D.GetComponent<Collider>()).size.x / 2f * radarDot3D.localScale.x;
			iconHeight = ((BoxCollider)radarDot3D.GetComponent<Collider>()).size.y / 2f * radarDot3D.localScale.y;
			Object.Destroy(radarDot3D.GetComponent<Collider>());
			StartCoroutine(DisplayRadarDot());
		}
	}

	private IEnumerator DisplayRadarDot()
	{
		radarDot3D.gameObject.active = true;
		Vector3 viewToWorld = mainCamera.ViewportToWorldPoint(new Vector3(1f, 1f, mainCamera.nearClipPlane));
		radarDot3D.position = viewToWorld;
		float width3D = Mathf.Abs(radarDot3D.localPosition.x) - iconWidth;
		float height3D = Mathf.Abs(radarDot3D.localPosition.y) - iconHeight;
		mainCamera.nearClipPlane = 0.15f;
		float depth3D = mainCamera.nearClipPlane * 2f;
		while (true)
		{
			Vector3 screenPos = mainCamera.WorldToViewportPoint(xForm.position);
			if (screenPos.x < 0.99f && screenPos.x > 0.01f && screenPos.y > 0.01f && screenPos.y < 0.99f)
			{
				radarDot3D.GetComponent<Renderer>().enabled = false;
				if (!Dead)
				{
					playerController.AddEnemyTarget(xForm);
				}
			}
			else
			{
				if (!Dead)
				{
					playerController.RemoveEnemyTarget(xForm);
				}
				radarDot3D.GetComponent<Renderer>().enabled = true;
				screenPos.x -= 0.5f;
				screenPos.y -= 0.5f;
				float maxAmountDiv = Mathf.Max(Mathf.Abs(screenPos.x), Mathf.Abs(screenPos.y));
				screenPos /= maxAmountDiv;
				screenPos.x *= width3D;
				screenPos.y *= height3D;
				screenPos.z = depth3D;
				radarDot3D.localPosition = screenPos;
			}
			yield return null;
		}
	}

	protected IEnumerator CanBlockAgain()
	{
		yield return new WaitForSeconds(canBlockAgainTimer);
		canBlock = true;
	}

	protected IEnumerator SprayBlood(Vector3 bloodPosition, AManager.PoolObjectType bloodType)
	{
		Transform bloodEffect = ((!(this is BaseSuperPredator)) ? AManager.instance.GetPoolObject(bloodType) : AManager.instance.GetPoolObject(AManager.PoolObjectType.BloodSprayPredatorAnimated));
		if ((bool)bloodEffect)
		{
			bloodEffect.position = bloodPosition;
			bloodEffect.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
		}
		Transform bloodSplat = ((!(this is BaseSuperPredator)) ? AManager.instance.GetPoolObject(AManager.PoolObjectType.BloodSplat) : AManager.instance.GetPoolObject(AManager.PoolObjectType.BloodSplatPredator));
		if ((bool)bloodSplat)
		{
			yield return new WaitForSeconds(0.2f);
			bloodSplat.position = new Vector3(bloodPosition.x, 0.01f, bloodPosition.z);
			float randomFloat = Random.Range(0.1f, 0.5f);
			bloodSplat.localScale = new Vector3(randomFloat, randomFloat, randomFloat);
			bloodSplat.localEulerAngles = new Vector3(270f, Random.Range(0f, 360f), 0f);
			bloodSplat.gameObject.active = true;
		}
	}

	private void GetAllWaypoints()
	{
		if ((bool)poolWaypointsParent && waypoints == null)
		{
			int childCount = poolWaypointsParent.childCount;
			waypoints = new Transform[childCount];
			for (int i = 0; i < childCount; i++)
			{
				waypoints[i] = poolWaypointsParent.GetChild(i);
			}
		}
	}

	protected void AddSelfToTargetArray()
	{
		AlliesArray.Add(base.transform);
		SetCurrentTarget();
	}

	protected override void Awake()
	{
		base.Awake();
		animationInfo = new AnimationInfo("die_fall_front");
		mainCamera = Camera.main;
		wasCapturedByNetGunAtLeastOnce = false;
		initialBlockingDamageHurtTreshold = blockingDamageHurtTreshold;
		initialChanceToBlock = chanceToBlock;
		initialChanceToBreakLoose = chanceToBreakLooseGrabbed;
		xForm = base.transform;
		characterController = (CharacterController)GetComponent("CharacterController");
		survivalMissionController = (SurvivalMissionController)GameObject.Find("/Level").GetComponent(typeof(SurvivalMissionController));
		if (!survivalMissionController)
		{
			Debug.LogError("KamikazeEnemy: ERROR! NO SurvivalLevel SCRIPT FOUND.");
		}
		controllerRadius = characterController.radius;
		attackShootDistance += Random.Range(0f - attackShootDistanceRandomRange, attackShootDistanceRandomRange);
		shootRateSeconds += Random.Range(0f - shootRateRandomRange, shootRateRandomRange);
		GetAllWaypoints();
		ChooseRandomWaypoint();
		if (!playerController)
		{
			playerController = (PlayerController)GameObject.FindWithTag("Player").GetComponent(typeof(PlayerController));
		}
		anim = (Animation)GetComponent(typeof(Animation));
		colliderChildren = new ArrayList();
		attackInfoEnemy = new AttackInfo(0.5f, xForm.position, 1);
		groundCollider = (Collider)GameObject.Find("Collider_Ground").GetComponent(typeof(Collider));
		if (!groundCollider)
		{
		}
		if (this is Cuchillo)
		{
			enemyType = EnemyType.Cuchillo;
		}
		else if (this is Dog)
		{
			enemyType = EnemyType.Dog;
		}
		else if (this is Falconer)
		{
			enemyType = EnemyType.Falconer;
		}
		else if (this is PredatorTracker)
		{
			enemyType = EnemyType.Tracker;
		}
		else if (this is Hanzo)
		{
			enemyType = EnemyType.Hanzo;
		}
		else if (this is Isabele)
		{
			enemyType = EnemyType.Isabelle;
		}
		else if (this is Mombasa)
		{
			enemyType = EnemyType.Mombasa;
		}
		else if (this is Nikolai)
		{
			enemyType = EnemyType.Nikolai;
		}
		else if (this is Noland)
		{
			enemyType = EnemyType.Noland;
		}
		else if (this is Royce)
		{
			enemyType = EnemyType.Royce;
		}
		else if (this is SoldierAutoRifle)
		{
			enemyType = EnemyType.SoldierRifle;
		}
		else if (this is Stans)
		{
			enemyType = EnemyType.Stans;
		}
		else if (this is SuperBlackPredator)
		{
			enemyType = EnemyType.MrBlack;
		}
		animationInfo.Enemy_Type = enemyType;
	}

	private void Deactivate()
	{
		if (!isDead)
		{
			StartCoroutine(Die());
		}
	}

	private IEnumerator CheckIfPredatorDetected()
	{
		while (!isDead)
		{
			if (!AManager.PredatorInvisible && (xForm.position - AManager.targetPosition).sqrMagnitude < distanceEnemyLosesSightOfPlayerWhenDetected * distanceEnemyLosesSightOfPlayerWhenDetected)
			{
				int currentMission = playerController.CurrentMission;
				switch (currentMission)
				{
				case 8:
				case 15:
				case 29:
					if (!survivalMissionController.EndMissionCondition(currentMission))
					{
						yield return StartCoroutine(ShowVisibilityLineRendererForEnemiesOnScreen());
					}
					break;
				case 21:
					if (!survivalMissionController.BossIsSpawned)
					{
						yield return StartCoroutine(ShowVisibilityLineRendererForEnemiesOnScreen());
					}
					break;
				}
			}
			yield return null;
			yield return null;
			yield return null;
			yield return null;
			yield return null;
		}
	}

	public virtual void Activate(int indexShooterLevel)
	{
		AddSelfToTargetArray();
		if (playerController.CurrentMission == 6 || playerController.CurrentMission == 7 || playerController.CurrentMission == 9 || playerController.CurrentMission == 29)
		{
			seekPredator = false;
		}
		else
		{
			seekPredator = true;
		}
		StopCoroutine("CheckIfPredatorDetected");
		int currentMission = playerController.CurrentMission;
		if (currentMission == 8 || currentMission == 15 || currentMission == 21 || currentMission == 29)
		{
			StartCoroutine("CheckIfPredatorDetected");
		}
		blockingDamageHurtTreshold = initialBlockingDamageHurtTreshold;
		visibilityLineRenderer = null;
		grabbedVictim = false;
		goingAroundObstacles = false;
		checkBlocking = false;
		lastColliderHit = null;
		foundObstacle = false;
		canShoot = true;
		blocking = false;
		canBlock = true;
		canPerformMeleeAttack = true;
		StopCoroutine("ApplyDamageGrabbedCR");
		StopCoroutines();
		shooterIndex = indexShooterLevel;
		chanceToBlock = initialChanceToBlock;
		chanceToBreakLooseGrabbed = initialChanceToBreakLoose;
		trophyKillPossible = true;
		isBoss = false;
		isDead = false;
		canShowRadarDot = true;
		characterController.radius = controllerRadius;
		fullHitPoints = originalFullHitPoints + originalFullHitPoints * AManager.difficultyLevel;
		fullHitPoints = Mathf.Clamp(fullHitPoints, originalFullHitPoints, maxFullHitPoints);
		hitPoints = fullHitPoints;
		anim.wrapMode = WrapMode.Loop;
		anim[animDieFallFront].wrapMode = WrapMode.ClampForever;
		anim[animGetHurtL1].wrapMode = WrapMode.Once;
		anim[animGetHurtL2].wrapMode = WrapMode.Once;
		anim[animGetHurtLow1].wrapMode = WrapMode.Once;
		anim[animGetHurtLow1].wrapMode = WrapMode.Once;
		anim[animGetHurtR1].wrapMode = WrapMode.Once;
		anim[animGetHurtR2].wrapMode = WrapMode.Once;
		if ((bool)anim["die_thrown_back"])
		{
			anim["die_thrown_back"].wrapMode = WrapMode.ClampForever;
		}
		anim["grabbed_loop"].wrapMode = WrapMode.Loop;
		anim["grabbed_released_killed"].wrapMode = WrapMode.Once;
		anim[animGrabbedBreakLoose].wrapMode = WrapMode.Once;
		anim["grabbed_start"].wrapMode = WrapMode.Once;
		if ((bool)anim["grabbed_hurt_high"])
		{
			anim["grabbed_hurt_high"].wrapMode = WrapMode.Once;
		}
		if ((bool)anim["get_up_face_up"])
		{
			anim["get_up_face_up"].wrapMode = WrapMode.Once;
		}
		anim[animDieFallBack].wrapMode = WrapMode.ClampForever;
		if ((bool)anim["coward_loop"])
		{
			anim["coward_loop"].wrapMode = WrapMode.Loop;
		}
		if ((bool)anim["jog_weapon_up"])
		{
			anim["jog_weapon_up"].wrapMode = WrapMode.Loop;
		}
		if ((bool)anim["walk_weapon_up"])
		{
			anim["walk_weapon_up"].wrapMode = WrapMode.Loop;
		}
		if ((bool)anim["idle_searching"])
		{
			anim["idle_searching"].wrapMode = WrapMode.Loop;
		}
		if ((bool)anim["run_searching"])
		{
			anim["run_searching"].wrapMode = WrapMode.Loop;
		}
		if ((bool)anim["walk_searching"])
		{
			anim["walk_searching"].wrapMode = WrapMode.Loop;
		}
		if ((bool)anim["fall_back"])
		{
			anim["fall_back"].wrapMode = WrapMode.Once;
		}
		if ((bool)anim["back_getup"])
		{
			anim["back_getup"].wrapMode = WrapMode.Once;
		}
		if ((bool)anim["coward_stand_loop"])
		{
			anim["coward_stand_loop"].wrapMode = WrapMode.Loop;
		}
		if ((bool)anim["walk_back"])
		{
			anim["walk_back"].wrapMode = WrapMode.Loop;
		}
		anim["grabbed_head_off_start"].wrapMode = WrapMode.ClampForever;
		if ((bool)anim["grabbed_hurt_low"])
		{
			anim["grabbed_hurt_low"].wrapMode = WrapMode.Once;
		}
		if ((bool)anim[animNetGunCaptured])
		{
			anim[animNetGunCaptured].wrapMode = WrapMode.Loop;
		}
		if ((bool)anim["idle_searching"])
		{
			anim.Play("idle_searching");
		}
		if ((bool)anim["hurt_low1"])
		{
			anim["hurt_low1"].layer = 5;
		}
		Vector3 forward = currentTarget.position - xForm.position;
		forward.y = 0f;
		xForm.rotation = Quaternion.LookRotation(forward);
		nextState = State.StatePatrol;
		base.gameObject.layer = 11;
		StartCoroutine(ActivationTimer());
		StartCoroutine("EnemyStateLoop");
	}

	private IEnumerator ActivationTimer()
	{
		yield return new WaitForSeconds(2f);
		checkBlocking = true;
	}

	protected void ChooseRandomWaypoint()
	{
		currentWaypoint = waypoints[(int)(Random.value * (float)(waypoints.Length - 1))];
	}

	private void OnControllerColliderHit(ControllerColliderHit colliderHit)
	{
		if (colliderHit.collider != groundCollider && lastColliderHit != colliderHit)
		{
			if (colliderHit.gameObject.CompareTag("Obstacle"))
			{
				foundObstacle = true;
				collisionNormal = colliderHit.normal;
				lastColliderHit = colliderHit;
			}
			else
			{
				lastColliderHit = null;
			}
		}
	}

	protected void RemoveSelfFromArray()
	{
		ArrayList alliesArray = AlliesArray;
		alliesArray.Remove(base.transform);
		ArrayList enemyArray = EnemyArray;
		enemyArray.Remove(base.transform);
	}

	protected virtual void EnemyDiedCleanup(int shooterIndex, DeathType deathType)
	{
		RemoveSelfFromArray();
	}

	protected virtual void EnemyDied()
	{
	}

	protected bool RandomBool()
	{
		return (double)Random.value > 0.5;
	}

	private IEnumerator ShowVisibilityLineRendererForEnemiesOnScreen()
	{
		if (!visibilityLineRenderer)
		{
			visibilityLineRenderer = (LineRenderer)AManager.instance.GetPoolObject(AManager.PoolObjectType.VisibilityLantern).GetComponent(typeof(LineRenderer));
		}
		if ((bool)visibilityLineRenderer)
		{
			visibilityLineRenderer.gameObject.active = true;
			StopCoroutine("UpdateVisibilityLineRendererPosition");
			StartCoroutine("UpdateVisibilityLineRendererPosition");
			yield return StartCoroutine(CountDownVisibilityLineRenderer(1f));
		}
	}

	private IEnumerator UpdateVisibilityLineRendererPosition()
	{
		while (visibilityLineRenderer.gameObject.active)
		{
			visibilityLineRenderer.SetPosition(0, xForm.position + Vector3.up * 1.2f);
			visibilityLineRenderer.SetPosition(1, AManager.targetPosition + Vector3.up * 1.2f);
			yield return null;
		}
	}

	private IEnumerator CountDownVisibilityLineRenderer(float initialDelay)
	{
		float timer = initialDelay;
		bool stillDetected = true;
		while (timer > 0f && stillDetected)
		{
			if (AManager.PredatorInvisible || (xForm.position - AManager.targetPosition).sqrMagnitude > distanceEnemyLosesSightOfPlayerWhenDetected * distanceEnemyLosesSightOfPlayerWhenDetected || isDead || grabbedVictim)
			{
				stillDetected = false;
				break;
			}
			timer -= Time.deltaTime;
			yield return null;
		}
		for (int i = 0; i < 3; i++)
		{
			timer = 0.1f;
			visibilityLineRenderer.enabled = false;
			while (timer > 0f && stillDetected)
			{
				if (AManager.PredatorInvisible || (xForm.position - AManager.targetPosition).sqrMagnitude > distanceEnemyLosesSightOfPlayerWhenDetected * distanceEnemyLosesSightOfPlayerWhenDetected || isDead || grabbedVictim)
				{
					stillDetected = false;
					break;
				}
				timer -= Time.deltaTime;
				yield return null;
			}
			timer = 0.1f;
			visibilityLineRenderer.enabled = true;
			while (timer > 0f && stillDetected)
			{
				if (AManager.PredatorInvisible || (xForm.position - AManager.targetPosition).sqrMagnitude > distanceEnemyLosesSightOfPlayerWhenDetected * distanceEnemyLosesSightOfPlayerWhenDetected || isDead || grabbedVictim)
				{
					stillDetected = false;
					break;
				}
				timer -= Time.deltaTime;
				yield return null;
			}
		}
		for (int j = 0; j < 4; j++)
		{
			timer = 1f / 15f;
			visibilityLineRenderer.enabled = false;
			while (timer > 0f && stillDetected)
			{
				if (AManager.PredatorInvisible || (xForm.position - AManager.targetPosition).sqrMagnitude > distanceEnemyLosesSightOfPlayerWhenDetected * distanceEnemyLosesSightOfPlayerWhenDetected || isDead || grabbedVictim)
				{
					stillDetected = false;
					break;
				}
				timer -= Time.deltaTime;
				yield return null;
			}
			timer = 1f / 15f;
			visibilityLineRenderer.enabled = true;
			while (timer > 0f && stillDetected)
			{
				if (AManager.PredatorInvisible || (xForm.position - AManager.targetPosition).sqrMagnitude > distanceEnemyLosesSightOfPlayerWhenDetected * distanceEnemyLosesSightOfPlayerWhenDetected || isDead || grabbedVictim)
				{
					stillDetected = false;
					break;
				}
				timer -= Time.deltaTime;
				yield return null;
			}
		}
		if (stillDetected)
		{
			playerController.setMissionFailedToTrue();
		}
		else
		{
			visibilityLineRenderer.gameObject.active = false;
		}
	}

	protected IEnumerator CanPerformMeleeAttack()
	{
		canPerformMeleeAttack = false;
		yield return new WaitForSeconds(canPerformMeleeAttackDelay);
		canPerformMeleeAttack = true;
	}

	protected virtual IEnumerator EnemyStateLoop()
	{
		while (!isDead)
		{
			if (AManager.instance.CinematicInProgress)
			{
				yield return StartCoroutine("Patrol");
			}
			else
			{
				if (AManager.PredatorInvisible)
				{
					float predatorAngle = AngleBetweenXformAnd(AManager.targetPosition);
					if ((xForm.position - AManager.targetPosition).sqrMagnitude < cloakDetectionRadius * cloakDetectionRadius && predatorAngle < cloakDetectionAngle && predatorAngle > 0f - cloakDetectionAngle)
					{
						lastSeenPosition = AManager.targetPosition;
						nextState = stateIfPredatorNotVisibleButDetected();
					}
				}
				switch (nextState)
				{
				case State.StateAxeAttack:
					yield return StartCoroutine("AxeAttack");
					break;
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
				case State.StateGoingIntoBossMode:
					yield return StartCoroutine("GoingIntoBossMode");
					break;
				case State.StateWristAttack:
					yield return StartCoroutine("WristAttack");
					break;
				}
			}
			yield return null;
		}
	}

	protected virtual IEnumerator GoingIntoBossMode()
	{
		yield return null;
	}

	protected IEnumerator GetHurtBlocking()
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
		yield return StartCoroutine("PerformAnimation", EnemyAnimation.GetHitBlocking);
		if (AManager.PredatorInvisible && currentTarget == playerController.transform)
		{
			FreakOut();
			yield break;
		}
		nextState = stateAfterGettingHurtAndPredatorVisible();
		StartEnemyStateLoop();
	}

	protected virtual IEnumerator GetHurt(int animNr)
	{
		if (Random.value < 0.5f)
		{
			if (Utils.SfxOn)
			{
				base.GetComponent<AudioSource>().PlayOneShot(soundHit);
			}
		}
		else if (Utils.SfxOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(soundHit2);
		}
		Vector3 hitDirection = (xForm.position - currentTarget.position).normalized;
		if (AManager.BloodOn)
		{
			hitDirection = SprayOrientedBlood(animNr == 1, hitDirection, xForm.TransformPoint(bloodSprayOffset));
		}
		switch (animNr)
		{
		case 1:
			if (Random.value < 0.5f)
			{
				anim.CrossFade(animGetHurtLow1, 0.1f);
			}
			else
			{
				anim.CrossFade(animGetHurtLow2, 0.1f);
			}
			break;
		case 2:
			if (Random.value < 0.5f)
			{
				anim.CrossFade(animGetHurtR1, 0.1f);
			}
			else
			{
				anim.CrossFade(animGetHurtR2, 0.1f);
			}
			break;
		case 3:
			if (Random.value < 0.5f)
			{
				anim.CrossFade(animGetHurtL1, 0.1f);
			}
			else
			{
				anim.CrossFade(animGetHurtL2, 0.1f);
			}
			break;
		case 4:
			if (Random.value < 0.5f)
			{
				anim.CrossFade(animGetHurtHigh1, 0.1f);
			}
			else
			{
				anim.CrossFade(animGetHurtHigh2, 0.1f);
			}
			break;
		default:
			if (Random.value < 0.5f)
			{
				anim.CrossFade(animGetHurtLow1, 0.1f);
			}
			else
			{
				anim.CrossFade(animGetHurtLow2, 0.1f);
			}
			break;
		}
		float timer = 0.3f;
		Quaternion targetRotation = Quaternion.LookRotation(hitDirection);
		while (timer > 0f)
		{
			timer -= Time.deltaTime;
			targetRotation = Quaternion.Slerp(xForm.rotation, targetRotation, Time.deltaTime * rotateSpeed * 100f);
			characterController.SimpleMove(hitDirection * speedHurt);
			yield return null;
		}
		yield return new WaitForSeconds(0.2f);
		if (AManager.PredatorInvisible && currentTarget == playerController.transform)
		{
			FreakOut();
			yield break;
		}
		nextState = stateAfterGettingHurtAndPredatorVisible();
		StartEnemyStateLoop();
	}

	private Vector3 SprayOrientedBlood(bool heavyAttack, Vector3 hitDirection, Vector3 bloodPosition)
	{
		Transform transform = ((this is BaseSuperPredator) ? AManager.instance.GetPoolObject(AManager.PoolObjectType.BloodSprayPredatorAnimated) : ((!heavyAttack) ? AManager.instance.GetPoolObject(AManager.PoolObjectType.BloodSprayAnimated) : AManager.instance.GetPoolObject(AManager.PoolObjectType.BloodSprayHeavy)));
		if ((bool)transform)
		{
			transform.position = bloodPosition;
			transform.rotation = Quaternion.LookRotation(hitDirection);
			transform.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
		}
		return hitDirection;
	}

	protected virtual IEnumerator Patrol()
	{
		ChooseRandomWaypoint();
		while (!isDead)
		{
			Vector3 movementTargetPosition = ((!seekPredator || AManager.PredatorInvisible) ? currentWaypoint.position : currentTarget.position);
			RotateTowardsPosition(movementTargetPosition, rotateSpeed);
			float predatorAngle = AngleBetweenXformAnd(AManager.targetPosition);
			characterController.SimpleMove(xForm.forward * speedPatrol);
			anim.CrossFade(animPatrol);
			if ((double)(xForm.position - movementTargetPosition).sqrMagnitude < 0.1)
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
			if (!AManager.PredatorInvisible)
			{
				if (currentTarget == playerController.transform)
				{
					if ((xForm.position - currentTarget.position).sqrMagnitude < attackShootDistance * attackShootDistance)
					{
						if (!AManager.instance.CinematicInProgress)
						{
							nextState = stateIfPredatorVisibleAndInRange();
							break;
						}
						anim.CrossFade("idle_searching");
						yield return new WaitForSeconds(2f);
					}
				}
				else if ((xForm.position - currentTarget.position).sqrMagnitude < attackShootDistance * attackShootDistance)
				{
					if (!AManager.instance.CinematicInProgress)
					{
						nextState = stateIfPredatorVisibleAndInRange();
						break;
					}
					anim.CrossFade("idle_searching");
					yield return new WaitForSeconds(2f);
				}
			}
			else if ((xForm.position - AManager.targetPosition).sqrMagnitude < cloakDetectionRadius * cloakDetectionRadius && predatorAngle < cloakDetectionAngle && predatorAngle > 0f - cloakDetectionAngle)
			{
				lastSeenPosition = AManager.targetPosition;
				if (!AManager.instance.CinematicInProgress)
				{
					nextState = stateIfPredatorNotVisibleButDetected();
					currentTarget = playerController.transform;
					break;
				}
				anim.CrossFade("idle_searching");
				yield return new WaitForSeconds(2f);
			}
			else if ((xForm.position - currentTarget.position).sqrMagnitude < attackShootDistance * attackShootDistance && currentTarget != playerController.transform)
			{
				if (!AManager.instance.CinematicInProgress)
				{
					nextState = stateIfPredatorVisibleAndInRange();
					break;
				}
				anim.CrossFade("idle_searching");
				yield return new WaitForSeconds(2f);
			}
			if (characterController.velocity.sqrMagnitude < speedPatrol * speedPatrol * 0.5f && checkBlocking && lastColliderHit != null && foundObstacle)
			{
				foundObstacle = false;
				if (SelectGoAroundWaypoint())
				{
					yield return StartCoroutine("GoAround");
					anim.CrossFade(animPatrol);
				}
			}
			yield return null;
		}
	}

	protected IEnumerator GoAround()
	{
		goingAroundObstacles = true;
		float runSpeed = speedGoAround;
		float blockedTimer = 0f;
		targetDir = currentWaypoint.position - xForm.position;
		targetRotation = Quaternion.LookRotation(targetDir);
		while (!isDead)
		{
			anim[animGoAround].speed = runSpeed / speedAttack;
			runSpeed = Mathf.Clamp(runSpeed + Time.deltaTime * 5f, speedGoAround, speedAttack);
			if (targetDir.sqrMagnitude < wayPointRadiusSqr)
			{
				lastColliderHit = null;
				break;
			}
			xForm.rotation = Quaternion.Slerp(xForm.rotation, targetRotation, Time.deltaTime * 3f);
			targetDir.Normalize();
			characterController.SimpleMove(targetDir * runSpeed);
			anim.CrossFade(animGoAround);
			targetDir = currentWaypoint.position - xForm.position;
			if (characterController.velocity.sqrMagnitude < speedAttack * speedAttack * 0.5f)
			{
				blockedTimer += Time.deltaTime;
			}
			if (blockedTimer > maxBlockedTimerGoAround)
			{
				lastColliderHit = null;
				break;
			}
			yield return null;
		}
		anim[animGoAround].speed = 1f;
		goingAroundObstacles = false;
	}

	protected IEnumerator CanShootAgain()
	{
		yield return new WaitForSeconds(2f + Random.Range(-1f, 2f));
		canShoot = true;
	}

	protected virtual IEnumerator Panic()
	{
		float panicTimer = 0f;
		anim.CrossFade("panic_fire");
		while (!isDead)
		{
			RotateTowardsPosition(lastSeenPosition, rotateSpeed);
			float predatorAngle = AngleBetweenXformAnd(currentTarget.position);
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
				nextState = stateIfPredatorNotVisibleButDetected();
				currentTarget = playerController.transform;
				break;
			}
			yield return null;
		}
	}

	protected IEnumerator RunAway()
	{
		float maxDist = 0f;
		int goodIndex = 0;
		for (int i = 0; i < waypoints.Length; i++)
		{
			float dist = SqrDistance(xForm.position, waypoints[i].position);
			if ((double)Vector3.Dot(waypoints[i].position - xForm.position, lastSeenPosition - xForm.position) < 0.0 && dist > maxDist)
			{
				maxDist = dist;
				goodIndex = i;
			}
		}
		currentWaypoint = waypoints[goodIndex];
		while (!isDead)
		{
			float angle = RotateTowardsPosition(currentWaypoint.position, rotateSpeed * 5f);
			float predatorAngle = AngleBetweenXformAnd(currentTarget.position);
			if (angle > -0.5f && angle < 0.5f)
			{
				direction = xForm.TransformDirection(Vector3.forward).normalized;
				characterController.SimpleMove(direction * speedRunAway);
				anim.CrossFade("run_searching");
			}
			if ((double)(xForm.position - currentWaypoint.position).sqrMagnitude < 0.1)
			{
				if (!(destinationWaypoint != null))
				{
					nextState = State.StatePatrol;
					break;
				}
				currentWaypoint = destinationWaypoint;
				destinationWaypoint = null;
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
				nextState = stateIfPredatorNotVisibleButDetected();
				currentTarget = playerController.transform;
				break;
			}
			if (characterController.velocity.sqrMagnitude < speedRunAway * speedRunAway * 0.5f && checkBlocking && lastColliderHit != null && foundObstacle)
			{
				foundObstacle = false;
				if (SelectGoAroundWaypoint())
				{
					yield return StartCoroutine("GoAround");
					anim.CrossFade("run_searching");
				}
			}
			yield return null;
		}
	}

	protected virtual IEnumerator Block()
	{
		yield return null;
	}

	protected virtual IEnumerator Berserk()
	{
		yield return null;
	}

	protected virtual IEnumerator Attack()
	{
		while (!isDead)
		{
			float angle = RotateTowardsPosition(currentTarget.position, rotateSpeed);
			if ((xForm.position - currentTarget.position).sqrMagnitude < attackShootDistance * attackShootDistance)
			{
				nextState = State.StateRelocate;
				break;
			}
			if (angle > -5f && angle < 5f)
			{
				shootTimer += Time.deltaTime;
				if (shootTimer > shootRateSeconds)
				{
					shootTimer = 0f;
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

	protected IEnumerator Die()
	{
		base.gameObject.layer = 0;
		if (Random.value < 0.5f)
		{
			if (Utils.SfxOn)
			{
				AudioSource.PlayClipAtPoint(soundHit, xForm.position);
			}
		}
		else if (Utils.SfxOn)
		{
			AudioSource.PlayClipAtPoint(soundHit2, xForm.position);
		}
		yield return null;
		if (Utils.SfxOn)
		{
			AudioSource.PlayClipAtPoint(soundDie, xForm.position);
		}
		DieSetParameters();
		if (attackInfoFromApplyDamage != null)
		{
			if (attackInfoFromApplyDamage.PredatorAttack)
			{
				if (wasCapturedByNetGunAtLeastOnce)
				{
					EnemyDiedCleanup(shooterIndex, DeathType.NetGunCaptured);
				}
				else
				{
					EnemyDiedCleanup(shooterIndex, DeathType.Normal);
				}
			}
			else
			{
				EnemyDiedCleanup(shooterIndex, DeathType.EnemyKilled);
			}
		}
		else if (wasCapturedByNetGunAtLeastOnce)
		{
			EnemyDiedCleanup(shooterIndex, DeathType.NetGunCaptured);
		}
		else
		{
			EnemyDiedCleanup(shooterIndex, DeathType.Normal);
		}
		attackInfoFromApplyDamage = null;
		yield return new WaitForSeconds(3f);
		base.gameObject.SetActiveRecursively(false);
	}

	protected virtual IEnumerator Relocate()
	{
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
			if (angle > -5f && angle < 5f)
			{
				characterController.SimpleMove(direction * speedJog);
				anim.CrossFade(animJog);
			}
			else
			{
				anim.CrossFade("idle_searching");
			}
			if (SqrDistance(xForm.position, currentTarget.position) > attackShootDistance * attackShootDistance)
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
					anim.CrossFade(animJog);
				}
			}
			yield return null;
		}
	}

	protected float SqrDistance(Vector3 a, Vector3 b)
	{
		return (a - b).sqrMagnitude;
	}

	protected float RotateTowardsPosition(Vector3 targetPos, float rotateSpeed)
	{
		if (!xForm)
		{
			Debug.LogError("no xForm set");
		}
		Vector3 vector = xForm.InverseTransformPoint(targetPos);
		float num = Mathf.Atan2(vector.x, vector.z) * 57.29578f;
		float num2 = rotateSpeed * Time.deltaTime;
		float yAngle = Mathf.Clamp(num, 0f - num2, num2);
		xForm.Rotate(0f, yAngle, 0f);
		return num;
	}

	protected float AngleBetweenXformAnd(Vector3 targetPos)
	{
		Vector3 vector = xForm.InverseTransformPoint(targetPos);
		return Mathf.Atan2(vector.x, vector.z) * 57.29578f;
	}

	protected Transform GetBestObstacleWaypoint()
	{
		if (lastColliderHit != null)
		{
			colliderChildren.Clear();
			int childCount = lastColliderHit.transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				Transform child = lastColliderHit.transform.GetChild(i);
				if (child.CompareTag("Waypoint"))
				{
					colliderChildren.Add(child);
				}
			}
			collisionNormal.Normalize();
			Vector3 vector = Vector3.Cross(collisionNormal, Vector3.up);
			Vector3 lhs = -vector;
			float num = 1000f;
			float num2 = 1000f;
			Transform transform = currentWaypoint;
			Transform transform2 = currentWaypoint;
			Transform transform3 = currentWaypoint;
			Transform transform4 = currentWaypoint;
			for (int j = 0; j < colliderChildren.Count; j++)
			{
				transform4 = (Transform)colliderChildren[j];
				float num3 = Vector3.Dot(lhs, (transform4.position - xForm.position).normalized);
				if (num3 < num)
				{
					num = num3;
					transform = transform4;
				}
				num3 = Vector3.Dot(vector, (transform4.position - xForm.position).normalized);
				if (num3 < num2)
				{
					num2 = num3;
					transform2 = transform4;
				}
			}
			transform3 = ((!((transform2.position - xForm.position).sqrMagnitude < (transform.position - xForm.position).sqrMagnitude)) ? transform : transform2);
			if (transform3 != null)
			{
				destinationWaypoint = currentWaypoint;
				lastWaypoint = currentWaypoint;
				return transform3;
			}
			destinationWaypoint = currentWaypoint;
			lastWaypoint = currentWaypoint;
			return waypoints[(int)(Random.value * (float)(waypoints.Length - 1))];
		}
		return waypoints[(int)(Random.value * (float)(waypoints.Length - 1))];
	}

	protected IEnumerator UpdateCurrentTarget()
	{
		while (!isDead)
		{
			SetCurrentTarget();
			yield return new WaitForSeconds(2f);
		}
	}

	protected bool SelectGoAroundWaypoint()
	{
		colliderChildren.Clear();
		int childCount = lastColliderHit.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			waypoint = lastColliderHit.transform.GetChild(i);
			if (waypoint.CompareTag("Waypoint"))
			{
				colliderChildren.Add(waypoint);
			}
		}
		collisionNormal.Normalize();
		Vector3 vector = Vector3.Cross(collisionNormal, Vector3.up);
		Vector3 lhs = -vector;
		float num = 1000f;
		float num2 = 1000f;
		bestTransformRight = currentWaypoint;
		bestTransformLeft = currentWaypoint;
		bestTransform = null;
		tempTransform = null;
		for (int j = 0; j < colliderChildren.Count; j++)
		{
			tempTransform = (Transform)colliderChildren[j];
			float num3 = Vector3.Dot(lhs, (tempTransform.position - xForm.position).normalized);
			if (num3 < num)
			{
				num = num3;
				bestTransformRight = tempTransform;
			}
			num3 = Vector3.Dot(vector, (tempTransform.position - xForm.position).normalized);
			if (num3 < num2)
			{
				num2 = num3;
				bestTransformLeft = tempTransform;
			}
		}
		if ((bestTransformLeft.position - xForm.position).sqrMagnitude < (bestTransformRight.position - xForm.position).sqrMagnitude)
		{
			bestTransform = bestTransformLeft;
		}
		else
		{
			bestTransform = bestTransformRight;
		}
		if (bestTransform != null)
		{
			destinationWaypoint = currentWaypoint;
			lastWaypoint = currentWaypoint;
			currentWaypoint = bestTransform;
			return true;
		}
		destinationWaypoint = currentWaypoint;
		lastWaypoint = currentWaypoint;
		if (seekPredator && !AManager.PredatorInvisible)
		{
			currentWaypoint = currentTarget;
		}
		else
		{
			currentWaypoint = waypoints[(int)(Random.value * (float)(waypoints.Length - 1))];
		}
		return false;
	}

	protected virtual State stateAfterGettingHurtAndPredatorVisible()
	{
		if (Random.value < chanceToBlock && canBlock)
		{
			return State.StateBlock;
		}
		return State.StateAttack;
	}

	protected virtual State stateAfterGettingHurtAndPredatorInvisible()
	{
		if (currentTarget == playerController.transform)
		{
			return State.StatePanic;
		}
		return State.StateAttack;
	}

	protected virtual State stateIfPredatorNotVisibleButDetected()
	{
		if (currentTarget == playerController.transform)
		{
			return State.StateRelocate;
		}
		return State.StateAttack;
	}

	protected virtual State stateIfPredatorVisibleAndInRange()
	{
		return State.StateAttack;
	}

	protected void DieSetParameters()
	{
		if ((bool)visibilityLineRenderer)
		{
			visibilityLineRenderer.gameObject.active = false;
		}
		canShowRadarDot = false;
		if (wasCapturedByNetGunAtLeastOnce)
		{
			survivalMissionController.NetGunKilledCount++;
		}
		playerController.RemoveEnemyTarget(base.transform);
		if ((bool)BossCircle && BossCircle.targetXForm == base.transform)
		{
			BossCircle.gameObject.active = false;
		}
		if ((bool)TrophyKillIcon && TrophyKillIcon.targetXForm == base.transform)
		{
			TrophyKillIcon.gameObject.active = false;
		}
		EnemyDied();
		AManager.TotalKilledEnemies++;
		if (AManager.BloodOn && !(this is BaseSuperPredator))
		{
			Transform poolObject = AManager.instance.GetPoolObject(AManager.PoolObjectType.BloodPool);
			if ((bool)poolObject)
			{
				poolObject.gameObject.SetActiveRecursively(true);
				poolObject.position = xForm.position;
				poolObject.localEulerAngles = new Vector3(270f, Random.Range(0f, 360f), 0f);
			}
		}
		if (AManager.PredatorInvisible)
		{
			Collider[] array = Physics.OverlapSphere(xForm.position, freakOutRadius);
			Collider[] array2 = array;
			foreach (Collider collider in array2)
			{
				collider.SendMessage("FreakOut", SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public virtual void CutInHalf(Vector3 attackerPosition, DeathType deathType)
	{
		BodyCutLowerHalf.transform.position = xForm.position;
		BodyCutUpperHalf.transform.position = xForm.position;
		BodyCutLowerHalf.transform.rotation = xForm.rotation;
		BodyCutUpperHalf.transform.rotation = xForm.rotation;
		BodyCutUpperHalf.gameObject.SetActiveRecursively(true);
		BodyCutLowerHalf.gameObject.SetActiveRecursively(true);
		animationInfo.AnimationName = animDieFallBack;
		BodyCutUpperHalf.SendMessage("PlayAnim", animationInfo, SendMessageOptions.DontRequireReceiver);
		animationInfo.AnimationName = animDieFallFront;
		BodyCutLowerHalf.SendMessage("PlayAnim", animationInfo, SendMessageOptions.DontRequireReceiver);
		DieSetParameters();
		EnemyDiedCleanup(shooterIndex, deathType);
		base.gameObject.SetActiveRecursively(false);
	}

	private IEnumerator CutVertically(Vector3 attackerPosition, DeathType deathType)
	{
		if ((bool)anim["idle_searching"])
		{
			anim.CrossFade("idle_searching", 0.1f);
		}
		float timer = 0.2f;
		Quaternion targetRotation = Quaternion.LookRotation(attackerPosition - xForm.position);
		if (Quaternion.Angle(xForm.rotation, targetRotation) > 90f)
		{
			targetRotation = Quaternion.LookRotation(xForm.position - attackerPosition);
		}
		while (timer > 0f && Quaternion.Angle(xForm.rotation, targetRotation) > 10f)
		{
			timer -= Time.deltaTime;
			xForm.rotation = Quaternion.Slerp(xForm.rotation, targetRotation, Time.deltaTime * rotateSpeed * 150f);
			yield return null;
		}
		switch (enemyType)
		{
		case EnemyType.Isabelle:
			BodyCutVerticallyL = AManager.instance.GetPoolObject(AManager.PoolObjectType.CutEnemyIsabelleVerticalL);
			break;
		case EnemyType.Dog:
			BodyCutVerticallyL = AManager.instance.GetPoolObject(AManager.PoolObjectType.CutDogVerticalL);
			break;
		default:
			BodyCutVerticallyL = AManager.instance.GetPoolObject(AManager.PoolObjectType.CutEnemyHumanVerticalL);
			break;
		}
		if ((bool)BodyCutVerticallyL)
		{
			BodyCutVerticallyL.transform.position = xForm.position;
			BodyCutVerticallyL.transform.rotation = xForm.rotation;
			BodyCutVerticallyL.gameObject.active = true;
			animationInfo.AnimationName = "cut_vertically_L";
			BodyCutVerticallyL.SendMessage("PlayAnimOnEnemy", animationInfo, SendMessageOptions.DontRequireReceiver);
		}
		else
		{
			Debug.LogError("Base Enemy: Body Cut Vertically L missing");
		}
		switch (enemyType)
		{
		case EnemyType.Isabelle:
			BodyCutVerticallyR = AManager.instance.GetPoolObject(AManager.PoolObjectType.CutEnemyIsabelleVerticalR);
			break;
		case EnemyType.Dog:
			BodyCutVerticallyR = AManager.instance.GetPoolObject(AManager.PoolObjectType.CutDogVerticalR);
			break;
		default:
			BodyCutVerticallyR = AManager.instance.GetPoolObject(AManager.PoolObjectType.CutEnemyHumanVerticalR);
			break;
		}
		if ((bool)BodyCutVerticallyR)
		{
			BodyCutVerticallyR.transform.position = xForm.position;
			BodyCutVerticallyR.transform.rotation = xForm.rotation;
			BodyCutVerticallyR.gameObject.active = true;
			animationInfo.AnimationName = "cut_vertically_R";
			BodyCutVerticallyR.SendMessage("PlayAnimOnEnemy", animationInfo, SendMessageOptions.DontRequireReceiver);
		}
		else
		{
			Debug.LogError("Base Enemy: Body Cut Vertically R missing");
		}
		DieSetParameters();
		EnemyDiedCleanup(shooterIndex, deathType);
		base.gameObject.SetActiveRecursively(false);
	}

	protected bool MeleesEngagedCountOk()
	{
		if (currentTarget == playerController.transform)
		{
			return AManager.instance.MeleesEngagedCount < AManager.instance.MaximumMeleesEngaged;
		}
		return true;
	}

	public virtual void NetGunCaptured()
	{
		StopCoroutine("ApplyDamageGrabbedCR");
		StopCoroutines();
		StartCoroutine("NetGunCapturedCR");
	}

	protected virtual IEnumerator NetGunCapturedCR()
	{
		anim.CrossFade(animNetGunCaptured, 0.1f);
		if (!netOnEnemy)
		{
			netOnEnemy = AManager.instance.GetPoolObject(AManager.PoolObjectType.NetOnEnemy);
			if ((bool)netOnEnemy)
			{
				netOnEnemy.position = xForm.position;
				netOnEnemy.rotation = xForm.rotation;
				netOnEnemy.gameObject.SetActiveRecursively(true);
			}
		}
		netgunCaptured = true;
		playerController.CheckEnemyTargetsClosest();
		wasCapturedByNetGunAtLeastOnce = true;
		yield return new WaitForSeconds(playerController.NetGunCapturedTime);
		netgunCaptured = false;
		if ((bool)netOnEnemy)
		{
			netOnEnemy.gameObject.SetActiveRecursively(false);
			netOnEnemy = null;
		}
		nextState = stateAfterGettingHurtAndPredatorVisible();
		StartEnemyStateLoop();
	}

	protected virtual void DiscCut(Vector3 attackerPosition, DeathType deathType)
	{
		CutInHalf(attackerPosition, deathType);
	}

	public virtual void MeleeCut(AttackInfo attackInfo, DeathType deathType)
	{
		CutInHalf(attackInfo.AttackerPosition, DeathType.BodyCut);
	}

	public void ApplyDamageBlackPredatorPlasma(AttackInfo attackInfo)
	{
		ApplyDamage(attackInfo);
	}

	protected virtual void MustSwitchToBossMode()
	{
		mustSwitchToBossMode = true;
		hitPoints = fullHitPoints;
	}

	protected virtual void PlayDeathAnimation()
	{
		anim.Stop();
		anim[animDieFallBack].time = 0f;
		anim.Play(animDieFallBack, PlayMode.StopAll);
	}

	public virtual void ApplyDamage(AttackInfo attackInfo)
	{
		if (isDead || grabbedVictim)
		{
			return;
		}
		if (blocking)
		{
			hitPoints -= attackInfo.Damage * blockingMultiplier;
		}
		else
		{
			hitPoints -= attackInfo.Damage;
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
		if (hitPoints <= 0f)
		{
			if ((this is SuperBlackPredator || this is Falconer || (this is Royce && isBoss)) && !mustSwitchToBossMode)
			{
				MustSwitchToBossMode();
				return;
			}
			if (isBoss && !trophyKillPossible)
			{
				trophyKillPossible = true;
				blocking = false;
				chanceToBlock = 0f;
				chanceToBreakLooseGrabbed = 0f;
				fullHitPoints *= 10f;
				hitPoints = fullHitPoints;
				if ((bool)TrophyKillIcon)
				{
					TrophyKillIcon.gameObject.SetActiveRecursively(true);
					TrophyKillIcon.SetTarget(base.transform);
				}
				return;
			}
			isDead = true;
			if (attackInfo.AnimationNr == 6 && !(this is Dog))
			{
				playerController.PeopleKilledByDisk++;
			}
			if (attackInfo.AnimationNr == 5)
			{
				if (!(this is Dog))
				{
					if (AManager.BloodOn)
					{
						StartCoroutine(SprayBlood(xForm.position + Vector3.up, AManager.PoolObjectType.BloodSprayAnimated));
					}
					playerController.ShowBloodSplatScreen(DeathType.BodyCut);
				}
				if (Utils.SfxOn)
				{
					AudioSource.PlayClipAtPoint(soundDieCut, xForm.position);
				}
				MeleeCut(attackInfo, DeathType.BodyCut);
				return;
			}
			if (attackInfo.AnimationNr == 6)
			{
				if (Utils.SfxOn)
				{
					AudioSource.PlayClipAtPoint(soundDieCut, xForm.position);
				}
				if (playerController.PeopleKilledByDisk > 2)
				{
					DiscCut(attackInfo.AttackerPosition, DeathType.SuperSplice);
				}
				else if (playerController.PeopleKilledByDisk == 2)
				{
					DiscCut(attackInfo.AttackerPosition, DeathType.DoubleSplice);
				}
				else
				{
					DiscCut(attackInfo.AttackerPosition, DeathType.DiscCut);
				}
				return;
			}
			if (attackInfo.AnimationNr == 7 && !(this is BaseSuperPredator))
			{
				if (Utils.SfxOn)
				{
					AudioSource.PlayClipAtPoint(soundDieCut, xForm.position);
				}
				playerController.ShowBloodSplatScreen(DeathType.VerticalCut);
				StartCoroutine(CutVertically(attackInfo.AttackerPosition, DeathType.VerticalCut));
				return;
			}
			characterController.radius = 0f;
			StopAllCoroutines();
			if (AManager.BloodOn)
			{
				StartCoroutine(SprayBlood(xForm.position + Vector3.up, AManager.PoolObjectType.BloodSprayAnimated));
			}
			if (Utils.SfxOn)
			{
				AudioSource.PlayClipAtPoint(soundDie, xForm.position);
			}
			PlayDeathAnimation();
			StopCoroutine("ApplyDamageGrabbedCR");
			StopCoroutines();
			attackInfoFromApplyDamage = attackInfo;
			StartCoroutine("Die");
		}
		else if (blocking && attackInfo.Damage < blockingDamageHurtTreshold)
		{
			StopCoroutine("ApplyDamageGrabbedCR");
			StopCoroutines();
			StartCoroutine("GetHurtBlocking");
		}
		else
		{
			if (blocking)
			{
				blocking = false;
				canBlock = false;
				StartCoroutine(CanBlockAgain());
			}
			if (AManager.BloodOn)
			{
				StartCoroutine(SprayBlood(xForm.position + Vector3.up, AManager.PoolObjectType.BloodSprayAnimated));
			}
			StopCoroutine("ApplyDamageGrabbedCR");
			StopCoroutines();
			StartCoroutine("GetHurt", attackInfo.AnimationNr);
		}
	}

	public void GrabStart(AttackInfo attackInfo)
	{
		if (isDead)
		{
			return;
		}
		if (Utils.SfxOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(soundHit);
		}
		hitPoints -= attackInfo.Damage;
		if (hitPoints <= 0f)
		{
			if ((this is SuperBlackPredator || this is Falconer || (this is Royce && isBoss)) && !mustSwitchToBossMode)
			{
				MustSwitchToBossMode();
			}
			else if (isBoss && !trophyKillPossible)
			{
				trophyKillPossible = true;
				blocking = false;
				chanceToBlock = 0f;
				chanceToBreakLooseGrabbed = 0f;
				fullHitPoints *= 10f;
				hitPoints = fullHitPoints;
				if ((bool)TrophyKillIcon)
				{
					TrophyKillIcon.gameObject.SetActiveRecursively(true);
					TrophyKillIcon.SetTarget(base.transform);
				}
			}
			else
			{
				isDead = true;
				characterController.radius = 0f;
				StopAllCoroutines();
				if (Utils.SfxOn)
				{
					AudioSource.PlayClipAtPoint(soundDie, xForm.position);
				}
				anim.Stop();
				anim.Play(animDieFallBack, PlayMode.StopAll);
				StartCoroutine("Die");
			}
		}
		else if (!blocking)
		{
			if (AManager.BloodOn)
			{
				StartCoroutine(SprayBlood(xForm.position + Vector3.up, AManager.PoolObjectType.BloodSprayAnimated));
			}
			StopCoroutine("ApplyDamageGrabbedCR");
			StopCoroutines();
			StartCoroutine("GrabStartCR");
		}
	}

	public void GrappleStart()
	{
		if (isDead)
		{
			return;
		}
		if (Utils.SfxOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(soundHit);
		}
		if (!blocking)
		{
			if (AManager.BloodOn)
			{
				StartCoroutine(SprayBlood(xForm.position + Vector3.up, AManager.PoolObjectType.BloodSprayAnimated));
			}
			StopCoroutine("ApplyDamageGrabbedCR");
			StopCoroutines();
			StartCoroutine("GrappleStartCR");
		}
	}

	private IEnumerator GrappleStartCR()
	{
		anim.CrossFade(animNetGunCaptured, 0.1f);
		float timer = 5f;
		while ((AManager.targetPosition - xForm.position).sqrMagnitude > 0.25f && timer > 0f)
		{
			Vector3 attackDirection = (AManager.targetPosition - xForm.position).normalized;
			timer -= Time.deltaTime;
			xForm.rotation = Quaternion.Slerp(xForm.rotation, Quaternion.LookRotation(attackDirection), Time.deltaTime * rotateSpeed * 150f);
			yield return null;
		}
		if (!grabbedVictim)
		{
			StartEnemyStateLoop();
		}
	}

	public void GrabbedStop()
	{
		if (!grabbedVictim)
		{
			return;
		}
		StopCoroutine("GrabVictimIdleCR");
		grabbedVictim = false;
		StopAllCoroutines();
		if (!isDead)
		{
			if (AManager.PredatorInvisible)
			{
				FreakOut();
				return;
			}
			nextState = stateAfterGettingHurtAndPredatorVisible();
			StartEnemyStateLoop();
		}
	}

	public void GrabbedStopTest()
	{
		if (grabbedVictim)
		{
			grabbedVictim = false;
			nextState = State.StateRelocate;
			StartEnemyStateLoop();
		}
	}

	private IEnumerator GrabVictimIdleCR()
	{
		if (grabbedVictim && !isDead)
		{
			while (anim.IsPlaying("grabbed_start") && grabbedVictim)
			{
				yield return null;
			}
			anim.CrossFade("grabbed_loop", 0.1f);
			float timer = maxGrabbedTime;
			while (timer > 0f && grabbedVictim)
			{
				timer -= Time.deltaTime;
				yield return null;
			}
			grabbedVictim = false;
		}
		if (AManager.PredatorInvisible)
		{
			FreakOut();
			yield break;
		}
		nextState = stateAfterGettingHurtAndPredatorVisible();
		StartEnemyStateLoop();
	}

	protected void StartEnemyStateLoop()
	{
		StopCoroutines();
		StartCoroutine("EnemyStateLoop");
	}

	private IEnumerator TryToBreakLoose(float chanceToBreakLoose)
	{
		if (grabbedVictim && Random.value < chanceToBreakLoose)
		{
			grabbedVictim = false;
			playerController.EnemyBrokeLooseFromGrabbing();
			blocking = true;
			StopCoroutine("GrabStartCR");
			StopCoroutine("ApplyDamageGrabbedCR");
			anim.CrossFade(animGrabbedBreakLoose, 0.1f);
			yield return new WaitForSeconds(anim[animGrabbedBreakLoose].length);
			blocking = false;
			if (AManager.PredatorInvisible)
			{
				FreakOut();
				yield break;
			}
			nextState = stateAfterGettingHurtAndPredatorVisible();
			StartEnemyStateLoop();
		}
	}

	protected virtual void StopCoroutines()
	{
		if (netgunCaptured)
		{
			netgunCaptured = false;
			if ((bool)netOnEnemy)
			{
				netOnEnemy.gameObject.SetActiveRecursively(false);
				netOnEnemy = null;
			}
		}
		StopCoroutine("GrabStartCR");
		StopCoroutine("GrabVictimIdleCR");
		StopCoroutine("GrabbedGoTowardsPlayer");
		StopCoroutine("PerformAnimation");
		StopCoroutine("Block");
		StopCoroutine("Berserk");
		StopCoroutine("Patrol");
		StopCoroutine("Panic");
		StopCoroutine("RunAway");
		StopCoroutine("Relocate");
		StopCoroutine("Attack");
		StopCoroutine("GetHurt");
		StopCoroutine("EnemyStateLoop");
		StopCoroutine("StrafeAround");
		StopCoroutine("GetHurtBlocking");
		StopCoroutine("TryToBreakLoose");
		StopCoroutine("NetGunCapturedCR");
		StopCoroutine("GoAround");
		StopCoroutine("GoingIntoBossMode");
		StopCoroutine("AxeAttack");
		StopCoroutine("WristAttack");
		StopCoroutine("GrappleStartCR");
	}

	public void RelocateStopOtherCoroutines()
	{
		nextState = State.StateRelocate;
		StartEnemyStateLoop();
	}

	private IEnumerator GrabStartCR()
	{
		if (this is SuperBlackPredator && !trophyKillPossible)
		{
			nextState = State.StateBlock;
			StartEnemyStateLoop();
			yield break;
		}
		grabbedVictim = true;
		if (netgunCaptured)
		{
			netgunCaptured = false;
			if ((bool)netOnEnemy)
			{
				netOnEnemy.gameObject.SetActiveRecursively(false);
				netOnEnemy = null;
			}
		}
		anim["grabbed_start"].time = 0f;
		anim.CrossFade("grabbed_start", 0.1f);
		StartCoroutine("GrabbedGoTowardsPlayer");
		yield return StartCoroutine("TryToBreakLoose", chanceToBreakLooseGrabbed);
		StopCoroutine("GrabVictimIdleCR");
		StartCoroutine("GrabVictimIdleCR");
	}

	private IEnumerator GrabbedGoTowardsPlayer()
	{
		float timer = 4f;
		while ((AManager.targetPosition - xForm.position).sqrMagnitude > 0.25f && timer > 0f)
		{
			Vector3 attackDirection = (AManager.targetPosition - xForm.position).normalized;
			characterController.SimpleMove(attackDirection * speedGetGrabbed);
			timer -= Time.deltaTime;
			xForm.rotation = Quaternion.Slerp(xForm.rotation, Quaternion.LookRotation(attackDirection), Time.deltaTime * rotateSpeed * 150f);
			yield return null;
		}
	}

	public void ApplyDamageGrabbed(AttackInfo attackInfo)
	{
		if (grabbedVictim && !blocking)
		{
			StopCoroutine("GrabStartCR");
			StopCoroutine("GrabVictimIdleCR");
			StopCoroutine("ApplyDamageGrabbedCR");
			StartCoroutine("ApplyDamageGrabbedCR", attackInfo);
		}
	}

	private IEnumerator GrabbedBreakDelay()
	{
		blocking = true;
		yield return new WaitForSeconds(3f);
		blocking = false;
	}

	private void DieGrabbed()
	{
		isDead = true;
		characterController.radius = 0f;
		StopCoroutine("ApplyDamageGrabbedCR");
		StopCoroutines();
		if (Random.value < 0.5f)
		{
			if (Utils.SfxOn)
			{
				AudioSource.PlayClipAtPoint(soundHit, xForm.position);
			}
		}
		else if (Utils.SfxOn)
		{
			AudioSource.PlayClipAtPoint(soundHit2, xForm.position);
		}
		if (Utils.SfxOn)
		{
			AudioSource.PlayClipAtPoint(soundDie, xForm.position);
		}
		anim.Stop();
		anim["grabbed_released_killed"].time = 0f;
		anim.CrossFade("grabbed_released_killed", 0.1f);
		StartCoroutine("Die");
	}

	public void CutHeadOff()
	{
		if (enemyType == EnemyType.Falconer || enemyType == EnemyType.Tracker || enemyType == EnemyType.MrBlack)
		{
			BodyCutNoHead = AManager.instance.GetPoolObject(AManager.PoolObjectType.CutEnemyBerserkersNoHead);
			if ((bool)BodyCutNoHead)
			{
				BodyCutNoHead.transform.position = xForm.position;
				BodyCutNoHead.transform.rotation = xForm.rotation;
				BodyCutNoHead.gameObject.active = true;
				animationInfo.AnimationName = "grabbed_head_off_end";
				BodyCutNoHead.SendMessage("PlayAnimOnEnemy", animationInfo, SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				Debug.LogError("base enemy: bodyCutNoHead not found for Berserker Predators");
			}
		}
		else
		{
			BodyCutNoHead.transform.position = xForm.position;
			BodyCutNoHead.transform.rotation = xForm.rotation;
			BodyCutNoHead.gameObject.SetActiveRecursively(true);
			animationInfo.AnimationName = "grabbed_head_off_end";
			BodyCutNoHead.SendMessage("PlayAnim", animationInfo, SendMessageOptions.DontRequireReceiver);
		}
		playerController.ShowBloodSplatScreen(DeathType.TrophyKill);
		DieSetParameters();
		grabbedVictim = false;
		EnemyDiedCleanup(shooterIndex, DeathType.TrophyKill);
		base.gameObject.SetActiveRecursively(false);
	}

	private IEnumerator ApplyDamageGrabbedCR(AttackInfo attackInfo)
	{
		if (grabbedVictim && !isDead)
		{
			switch (attackInfo.AnimationNr)
			{
			case 1:
				if (grabbedVictim)
				{
					yield return StartCoroutine("TryToBreakLoose", chanceToBreakLooseGrabbed);
				}
				if (!grabbedVictim)
				{
					break;
				}
				if (Random.value < 0.5f)
				{
					if (Utils.SfxOn)
					{
						base.GetComponent<AudioSource>().PlayOneShot(soundHit);
					}
				}
				else if (Utils.SfxOn)
				{
					base.GetComponent<AudioSource>().PlayOneShot(soundHit2);
				}
				if (AManager.BloodOn)
				{
					SprayOrientedBlood(false, BoneMixChest.position - currentTarget.position, BoneMixChest.position);
				}
				hitPoints -= attackInfo.Damage;
				if (hitPoints <= 0f)
				{
					if ((this is SuperBlackPredator || this is Falconer || (this is Royce && isBoss)) && !mustSwitchToBossMode)
					{
						MustSwitchToBossMode();
					}
					else if (isBoss && !trophyKillPossible)
					{
						trophyKillPossible = true;
						blocking = false;
						chanceToBlock = 0f;
						chanceToBreakLooseGrabbed = 0f;
						fullHitPoints *= 10f;
						hitPoints = fullHitPoints;
						if ((bool)TrophyKillIcon)
						{
							TrophyKillIcon.gameObject.SetActiveRecursively(true);
							TrophyKillIcon.SetTarget(base.transform);
						}
					}
					else
					{
						playerController.EnemyBrokeLooseFromGrabbing();
						DieGrabbed();
					}
				}
				else
				{
					float timer3 = 0.6f;
					anim.CrossFade("grabbed_hurt_high", 0.1f);
					while (grabbedVictim && timer3 > 0f)
					{
						timer3 -= Time.deltaTime;
						yield return null;
					}
				}
				break;
			case 2:
				if (grabbedVictim)
				{
					yield return StartCoroutine("TryToBreakLoose", chanceToBreakLooseGrabbed);
				}
				if (!grabbedVictim)
				{
					break;
				}
				hitPoints -= attackInfo.Damage;
				if (Random.value < 0.5f)
				{
					if (Utils.SfxOn)
					{
						base.GetComponent<AudioSource>().PlayOneShot(soundHit);
					}
				}
				else if (Utils.SfxOn)
				{
					base.GetComponent<AudioSource>().PlayOneShot(soundHit2);
				}
				if (AManager.BloodOn)
				{
					SprayOrientedBlood(false, BoneMixChest.position - currentTarget.position, BoneMixChest.position);
				}
				if (hitPoints <= 0f)
				{
					if ((this is SuperBlackPredator || this is Falconer || (this is Royce && isBoss)) && !mustSwitchToBossMode)
					{
						MustSwitchToBossMode();
					}
					else if (isBoss && !trophyKillPossible)
					{
						trophyKillPossible = true;
						blocking = false;
						chanceToBlock = 0f;
						chanceToBreakLooseGrabbed = 0f;
						fullHitPoints *= 10f;
						hitPoints = fullHitPoints;
						if ((bool)TrophyKillIcon)
						{
							TrophyKillIcon.gameObject.SetActiveRecursively(true);
							TrophyKillIcon.SetTarget(base.transform);
						}
					}
					else
					{
						playerController.EnemyBrokeLooseFromGrabbing();
						DieGrabbed();
					}
				}
				else
				{
					float timer3 = 0.6f;
					anim.CrossFade("grabbed_hurt_low", 0.1f);
					while (grabbedVictim && timer3 > 0f)
					{
						timer3 -= Time.deltaTime;
						yield return null;
					}
				}
				break;
			case 3:
				if (grabbedVictim)
				{
					if (trophyKillPossible)
					{
						StopCoroutine("TryToBreakLoose");
						BodyCutLowerHalf.transform.position = xForm.position;
						BodyCutUpperHalf.transform.position = xForm.position;
						BodyCutLowerHalf.transform.rotation = xForm.rotation;
						BodyCutUpperHalf.transform.rotation = xForm.rotation;
						BodyCutUpperHalf.gameObject.SetActiveRecursively(true);
						BodyCutLowerHalf.gameObject.SetActiveRecursively(true);
						animationInfo.AnimationName = "grabbed_rip_top";
						BodyCutUpperHalf.SendMessage("PlayAnim", animationInfo, SendMessageOptions.DontRequireReceiver);
						animationInfo.AnimationName = "grabbed_rip_bottom";
						BodyCutLowerHalf.SendMessage("PlayAnim", animationInfo, SendMessageOptions.DontRequireReceiver);
						DieSetParameters();
						grabbedVictim = false;
						EnemyDiedCleanup(shooterIndex, DeathType.BodySplice);
						base.gameObject.SetActiveRecursively(false);
					}
					else
					{
						yield return StartCoroutine("TryToBreakLoose", 1f);
					}
				}
				break;
			case 4:
				if (!grabbedVictim)
				{
					break;
				}
				if (trophyKillPossible)
				{
					StopCoroutine("TryToBreakLoose");
					anim.CrossFade("grabbed_head_off_start", 0.1f);
					float timer3 = 3f;
					while (grabbedVictim && timer3 > 0f)
					{
						timer3 -= Time.deltaTime;
						yield return null;
					}
				}
				else
				{
					yield return StartCoroutine("TryToBreakLoose", 1f);
				}
				break;
			}
		}
		if (grabbedVictim)
		{
			StopCoroutine("GrabVictimIdleCR");
			StartCoroutine("GrabVictimIdleCR");
		}
	}

	public void FreakOut()
	{
		if (!grabbedVictim)
		{
			StopCoroutine("ApplyDamageGrabbedCR");
			StopCoroutines();
			nextState = stateAfterGettingHurtAndPredatorInvisible();
			lastSeenPosition = currentTarget.position;
			StartEnemyStateLoop();
		}
	}
}
