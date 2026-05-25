using System.Collections;
using UnityEngine;

public class AManager : MonoBehaviour
{
	public enum PoolObjectType
	{
		ExplosionPlasmaGunBlue = 0,
		NetOnEnemy = 1,
		BloodSprayAnimated = 2,
		BloodSplat = 3,
		BloodPool = 4,
		VisibilityLantern = 5,
		CutEnemyHumanVerticalL = 6,
		CutEnemyHumanVerticalR = 7,
		CutEnemyIsabelleVerticalL = 8,
		CutEnemyIsabelleVerticalR = 9,
		CutEnemyBerserkersNoHead = 10,
		CutDogVerticalL = 11,
		CutDogVerticalR = 12,
		BloodSprayPredatorAnimated = 13,
		BloodSplatPredator = 14,
		ExplosionPlasmaGunYellow = 15,
		ExplosionPlasmaGunRed = 16,
		BloodSprayHeavy = 17,
		BloodSpraySmall = 18,
		BloodGloop = 19
	}

	public Transform poolExplosionPlasmaGunBlueParent;

	public Transform poolExplosionPlasmaGunYellowParent;

	public Transform poolExplosionPlasmaGunRedParent;

	public Transform poolBloodSprayAnimatedParent;

	public Transform poolBloodSprayHeavyParent;

	public Transform poolBloodSpraySmallParent;

	public Transform poolBloodSplatParent;

	public Transform poolBloodSprayPredatorAnimatedParent;

	public Transform poolBloodSplatPredatorParent;

	public Transform poolBloodGloopParent;

	public Transform poolBloodPoolParent;

	public Transform poolNetOnEnemyParent;

	public Transform poolVisibilityLanternParent;

	public Transform poolCutEnemyHumanVerticalLParent;

	private int poolCutEnemyHumanVerticalLCount;

	private Transform[] poolCutEnemyHumanVerticalL;

	private int currentCutEnemyHumanVerticalLIndex;

	private int poolBloodGloopCount;

	private Transform[] poolBloodGloop;

	private int currentBloodGloopIndex;

	public Transform poolCutEnemyHumanVerticalRParent;

	private int poolCutEnemyHumanVerticalRCount;

	private Transform[] poolCutEnemyHumanVerticalR;

	private int currentCutEnemyHumanVerticalRIndex;

	public Transform poolCutDogVerticalLParent;

	private int poolCutDogVerticalLCount;

	private Transform[] poolCutDogVerticalL;

	private int currentCutDogVerticalLIndex;

	public Transform poolCutDogVerticalRParent;

	private int poolCutDogVerticalRCount;

	private Transform[] poolCutDogVerticalR;

	private int currentCutDogVerticalRIndex;

	public Transform CutEnemyVerticalLIsabelle;

	public Transform CutEnemyVerticalRIsabelle;

	public Transform CutEnemyNoHeadBerserkers;

	public ArrayList humanTargets = new ArrayList();

	public ArrayList predatorTargets = new ArrayList();

	private static bool predatorInvisible = false;

	private static bool bloodOn = true;

	public float timeToDeactivate = 8f;

	public float timeToDeactivateBlood = 18f;

	public static Vector3 targetPosition = Vector3.zero;

	public static float difficultyLevel = 0f;

	public PlayerController playerController;

	private Transform target;

	private int poolExplosionPlasmaGunBlueCount;

	private int poolExplosionPlasmaGunYellowCount;

	private int poolExplosionPlasmaGunRedCount;

	private int poolBloodSprayAnimatedCount;

	private int poolBloodSprayHeavyCount;

	private int poolBloodSpraySmallCount;

	private int poolBloodSplatCount;

	private int poolBloodSprayPredatorAnimatedCount;

	private int poolBloodSplatPredatorCount;

	private int poolBloodPoolCount;

	private int poolNetOnEnemyCount;

	private int poolVisibilityLanternCount;

	private Transform[] poolExplosionPlasmaGunBlue;

	private Transform[] poolExplosionPlasmaGunYellow;

	private Transform[] poolExplosionPlasmaGunRed;

	private Transform[] poolBloodSprayAnimated;

	private Transform[] poolBloodSprayHeavy;

	private Transform[] poolBloodSpraySmall;

	private Transform[] poolBloodSplat;

	private Transform[] poolBloodSprayPredatorAnimated;

	private Transform[] poolBloodSplatPredator;

	private Transform[] poolBloodPool;

	private Transform[] poolNetOnEnemy;

	private Transform[] poolVisibilityLantern;

	private static bool highDetail = false;

	private bool cinematicInProgress;

	private int currentExplosionPlasmaGunBlueIndex;

	private int currentExplosionPlasmaGunYellowIndex;

	private int currentExplosionPlasmaGunRedIndex;

	private int currentBloodSprayHeavyIndex;

	private int currentBloodSpraySmallIndex;

	private int currentBloodSprayAnimatedIndex;

	private int currentBloodSplatIndex;

	private int currentBloodSprayPredatorAnimatedIndex;

	private int currentBloodSplatPredatorIndex;

	private int currentBloodPoolIndex;

	private int currentNetOnEnemyIndex;

	private int currentVisibilityLanternIndex;

	private static int totalEnemiesKilled = 0;

	private int totalDamageReceivers;

	private int extraSpawnedDamageReceivers;

	private int killedDamageReceivers;

	private int scoreMultiplier;

	private int totalScorePoints;

	private int maximumMeleesEngaged = 1;

	private int meleesEngagedCount;

	private static AManager s_Instance = null;

	public bool CinematicInProgress
	{
		get
		{
			return cinematicInProgress;
		}
		set
		{
			cinematicInProgress = value;
		}
	}

	public int MeleesEngagedCount
	{
		get
		{
			return meleesEngagedCount;
		}
		set
		{
			meleesEngagedCount = value;
		}
	}

	public int MaximumMeleesEngaged
	{
		get
		{
			return maximumMeleesEngaged;
		}
		set
		{
			maximumMeleesEngaged = value;
		}
	}

	public static AManager instance
	{
		get
		{
			if (s_Instance == null)
			{
				s_Instance = Object.FindObjectOfType(typeof(AManager)) as AManager;
				if (s_Instance == null)
				{
					Debug.Log("Could not locate an AManager object. You have to have exactly one AManager in the scene.");
				}
			}
			return s_Instance;
		}
	}

	public int TotalDamageReceivers
	{
		get
		{
			return totalDamageReceivers;
		}
	}

	public int ExtraSpawnedDamageReceivers
	{
		get
		{
			return extraSpawnedDamageReceivers;
		}
		set
		{
			extraSpawnedDamageReceivers = value;
		}
	}

	public int Score
	{
		get
		{
			return totalScorePoints;
		}
	}

	public int KilledDamageReceivers
	{
		get
		{
			return killedDamageReceivers;
		}
		set
		{
			killedDamageReceivers = value;
		}
	}

	public static bool HighDetail
	{
		get
		{
			return highDetail;
		}
	}

	public static bool BloodOn
	{
		get
		{
			return bloodOn;
		}
		set
		{
			bloodOn = value;
		}
	}

	public static int TotalKilledEnemies
	{
		get
		{
			return totalEnemiesKilled;
		}
		set
		{
			totalEnemiesKilled = value;
		}
	}

	public static bool PredatorInvisible
	{
		get
		{
			return predatorInvisible;
		}
		set
		{
			predatorInvisible = value;
		}
	}

	public void increaseMeleesEngagedCount()
	{
		meleesEngagedCount++;
	}

	public void decreaseMeleesEngagedCount()
	{
		meleesEngagedCount--;
	}

	public void AddDamageReceiver()
	{
		totalDamageReceivers++;
	}

	private void Awake()
	{
		highDetail = PlatformDependent.GetHighDetail();
		predatorInvisible = false;
		totalEnemiesKilled = 0;
		meleesEngagedCount = 0;
	}

	private void Start()
	{
		if ((bool)poolExplosionPlasmaGunBlueParent)
		{
			poolExplosionPlasmaGunBlueCount = poolExplosionPlasmaGunBlueParent.childCount;
			poolExplosionPlasmaGunBlue = new Transform[poolExplosionPlasmaGunBlueCount];
			for (int i = 0; i < poolExplosionPlasmaGunBlueCount; i++)
			{
				poolExplosionPlasmaGunBlue[i] = poolExplosionPlasmaGunBlueParent.GetChild(i);
			}
		}
		if ((bool)poolExplosionPlasmaGunYellowParent)
		{
			poolExplosionPlasmaGunYellowCount = poolExplosionPlasmaGunYellowParent.childCount;
			poolExplosionPlasmaGunYellow = new Transform[poolExplosionPlasmaGunYellowCount];
			for (int i = 0; i < poolExplosionPlasmaGunYellowCount; i++)
			{
				poolExplosionPlasmaGunYellow[i] = poolExplosionPlasmaGunYellowParent.GetChild(i);
			}
		}
		if ((bool)poolExplosionPlasmaGunRedParent)
		{
			poolExplosionPlasmaGunRedCount = poolExplosionPlasmaGunRedParent.childCount;
			poolExplosionPlasmaGunRed = new Transform[poolExplosionPlasmaGunRedCount];
			for (int i = 0; i < poolExplosionPlasmaGunRedCount; i++)
			{
				poolExplosionPlasmaGunRed[i] = poolExplosionPlasmaGunRedParent.GetChild(i);
			}
		}
		if ((bool)poolBloodSprayAnimatedParent)
		{
			poolBloodSprayAnimatedCount = poolBloodSprayAnimatedParent.childCount;
			poolBloodSprayAnimated = new Transform[poolBloodSprayAnimatedCount];
			for (int i = 0; i < poolBloodSprayAnimatedCount; i++)
			{
				poolBloodSprayAnimated[i] = poolBloodSprayAnimatedParent.GetChild(i);
			}
		}
		if ((bool)poolBloodSprayHeavyParent)
		{
			poolBloodSprayHeavyCount = poolBloodSprayHeavyParent.childCount;
			poolBloodSprayHeavy = new Transform[poolBloodSprayHeavyCount];
			for (int i = 0; i < poolBloodSprayHeavyCount; i++)
			{
				poolBloodSprayHeavy[i] = poolBloodSprayHeavyParent.GetChild(i);
			}
		}
		if ((bool)poolBloodSpraySmallParent)
		{
			poolBloodSpraySmallCount = poolBloodSpraySmallParent.childCount;
			poolBloodSpraySmall = new Transform[poolBloodSpraySmallCount];
			for (int i = 0; i < poolBloodSpraySmallCount; i++)
			{
				poolBloodSpraySmall[i] = poolBloodSpraySmallParent.GetChild(i);
			}
		}
		if ((bool)poolBloodGloopParent)
		{
			poolBloodGloopCount = poolBloodGloopParent.childCount;
			poolBloodGloop = new Transform[poolBloodGloopCount];
			for (int i = 0; i < poolBloodGloopCount; i++)
			{
				poolBloodGloop[i] = poolBloodGloopParent.GetChild(i);
			}
		}
		if ((bool)poolBloodSplatParent)
		{
			poolBloodSplatCount = poolBloodSplatParent.childCount;
			poolBloodSplat = new Transform[poolBloodSplatCount];
			for (int i = 0; i < poolBloodSplatCount; i++)
			{
				poolBloodSplat[i] = poolBloodSplatParent.GetChild(i);
			}
		}
		if ((bool)poolBloodSprayPredatorAnimatedParent)
		{
			poolBloodSprayPredatorAnimatedCount = poolBloodSprayPredatorAnimatedParent.childCount;
			poolBloodSprayPredatorAnimated = new Transform[poolBloodSprayPredatorAnimatedCount];
			for (int i = 0; i < poolBloodSprayPredatorAnimatedCount; i++)
			{
				poolBloodSprayPredatorAnimated[i] = poolBloodSprayPredatorAnimatedParent.GetChild(i);
			}
		}
		if ((bool)poolBloodSplatPredatorParent)
		{
			poolBloodSplatPredatorCount = poolBloodSplatPredatorParent.childCount;
			poolBloodSplatPredator = new Transform[poolBloodSplatPredatorCount];
			for (int i = 0; i < poolBloodSplatPredatorCount; i++)
			{
				poolBloodSplatPredator[i] = poolBloodSplatPredatorParent.GetChild(i);
			}
		}
		if ((bool)poolBloodPoolParent)
		{
			poolBloodPoolCount = poolBloodPoolParent.childCount;
			poolBloodPool = new Transform[poolBloodPoolCount];
			for (int i = 0; i < poolBloodPoolCount; i++)
			{
				poolBloodPool[i] = poolBloodPoolParent.GetChild(i);
			}
		}
		if ((bool)poolNetOnEnemyParent)
		{
			poolNetOnEnemyCount = poolNetOnEnemyParent.childCount;
			poolNetOnEnemy = new Transform[poolNetOnEnemyCount];
			for (int i = 0; i < poolNetOnEnemyCount; i++)
			{
				poolNetOnEnemy[i] = poolNetOnEnemyParent.GetChild(i);
			}
		}
		if ((bool)poolVisibilityLanternParent)
		{
			poolVisibilityLanternCount = poolVisibilityLanternParent.childCount;
			poolVisibilityLantern = new Transform[poolVisibilityLanternCount];
			for (int i = 0; i < poolVisibilityLanternCount; i++)
			{
				poolVisibilityLantern[i] = poolVisibilityLanternParent.GetChild(i);
			}
		}
		if ((bool)poolCutEnemyHumanVerticalLParent)
		{
			poolCutEnemyHumanVerticalLCount = poolCutEnemyHumanVerticalLParent.childCount;
			poolCutEnemyHumanVerticalL = new Transform[poolCutEnemyHumanVerticalLCount];
			for (int i = 0; i < poolCutEnemyHumanVerticalLCount; i++)
			{
				poolCutEnemyHumanVerticalL[i] = poolCutEnemyHumanVerticalLParent.GetChild(i);
			}
		}
		if ((bool)poolCutEnemyHumanVerticalRParent)
		{
			poolCutEnemyHumanVerticalRCount = poolCutEnemyHumanVerticalRParent.childCount;
			poolCutEnemyHumanVerticalR = new Transform[poolCutEnemyHumanVerticalRCount];
			for (int i = 0; i < poolCutEnemyHumanVerticalRCount; i++)
			{
				poolCutEnemyHumanVerticalR[i] = poolCutEnemyHumanVerticalRParent.GetChild(i);
			}
		}
		if ((bool)poolCutDogVerticalLParent)
		{
			poolCutDogVerticalLCount = poolCutDogVerticalLParent.childCount;
			poolCutDogVerticalL = new Transform[poolCutDogVerticalLCount];
			for (int i = 0; i < poolCutDogVerticalLCount; i++)
			{
				poolCutDogVerticalL[i] = poolCutDogVerticalLParent.GetChild(i);
			}
		}
		if ((bool)poolCutDogVerticalRParent)
		{
			poolCutDogVerticalRCount = poolCutDogVerticalRParent.childCount;
			poolCutDogVerticalR = new Transform[poolCutDogVerticalRCount];
			for (int i = 0; i < poolCutDogVerticalRCount; i++)
			{
				poolCutDogVerticalR[i] = poolCutDogVerticalRParent.GetChild(i);
			}
		}
		if (!playerController)
		{
			playerController = (PlayerController)GameObject.FindWithTag("Player").GetComponent(typeof(PlayerController));
		}
		target = playerController.transform;
		StartCoroutine(UpdateTargetPosition());
	}

	private IEnumerator UpdateTargetPosition()
	{
		while (true)
		{
			targetPosition = target.position;
			yield return null;
		}
	}

	private void OnApplicationQuit()
	{
		s_Instance = null;
	}

	public Transform GetPoolObject(PoolObjectType poolObjectType)
	{
		Transform result = null;
		switch (poolObjectType)
		{
		case PoolObjectType.ExplosionPlasmaGunBlue:
			if ((bool)poolExplosionPlasmaGunBlueParent)
			{
				result = poolExplosionPlasmaGunBlue[currentExplosionPlasmaGunBlueIndex];
				if (currentExplosionPlasmaGunBlueIndex < poolExplosionPlasmaGunBlueCount - 1)
				{
					currentExplosionPlasmaGunBlueIndex++;
				}
				else
				{
					currentExplosionPlasmaGunBlueIndex = 0;
				}
			}
			break;
		case PoolObjectType.ExplosionPlasmaGunYellow:
			if ((bool)poolExplosionPlasmaGunYellowParent)
			{
				result = poolExplosionPlasmaGunYellow[currentExplosionPlasmaGunYellowIndex];
				if (currentExplosionPlasmaGunYellowIndex < poolExplosionPlasmaGunYellowCount - 1)
				{
					currentExplosionPlasmaGunYellowIndex++;
				}
				else
				{
					currentExplosionPlasmaGunYellowIndex = 0;
				}
			}
			break;
		case PoolObjectType.ExplosionPlasmaGunRed:
			if ((bool)poolExplosionPlasmaGunRedParent)
			{
				result = poolExplosionPlasmaGunRed[currentExplosionPlasmaGunRedIndex];
				if (currentExplosionPlasmaGunRedIndex < poolExplosionPlasmaGunRedCount - 1)
				{
					currentExplosionPlasmaGunRedIndex++;
				}
				else
				{
					currentExplosionPlasmaGunRedIndex = 0;
				}
			}
			break;
		case PoolObjectType.BloodSprayAnimated:
			if ((bool)poolBloodSprayAnimatedParent)
			{
				result = poolBloodSprayAnimated[currentBloodSprayAnimatedIndex];
				if (currentBloodSprayAnimatedIndex < poolBloodSprayAnimatedCount - 1)
				{
					currentBloodSprayAnimatedIndex++;
				}
				else
				{
					currentBloodSprayAnimatedIndex = 0;
				}
			}
			break;
		case PoolObjectType.BloodSprayHeavy:
			if ((bool)poolBloodSprayHeavyParent)
			{
				result = poolBloodSprayHeavy[currentBloodSprayHeavyIndex];
				if (currentBloodSprayHeavyIndex < poolBloodSprayHeavyCount - 1)
				{
					currentBloodSprayHeavyIndex++;
				}
				else
				{
					currentBloodSprayHeavyIndex = 0;
				}
			}
			break;
		case PoolObjectType.BloodGloop:
			if ((bool)poolBloodGloopParent)
			{
				result = poolBloodGloop[currentBloodGloopIndex];
				if (currentBloodGloopIndex < poolBloodGloopCount - 1)
				{
					currentBloodGloopIndex++;
				}
				else
				{
					currentBloodGloopIndex = 0;
				}
			}
			break;
		case PoolObjectType.BloodSpraySmall:
			if ((bool)poolBloodSpraySmallParent)
			{
				result = poolBloodSpraySmall[currentBloodSpraySmallIndex];
				if (currentBloodSpraySmallIndex < poolBloodSpraySmallCount - 1)
				{
					currentBloodSpraySmallIndex++;
				}
				else
				{
					currentBloodSpraySmallIndex = 0;
				}
			}
			break;
		case PoolObjectType.BloodSplat:
			if ((bool)poolBloodSplatParent)
			{
				result = poolBloodSplat[currentBloodSplatIndex];
				if (currentBloodSplatIndex < poolBloodSplatCount - 1)
				{
					currentBloodSplatIndex++;
				}
				else
				{
					currentBloodSplatIndex = 0;
				}
			}
			break;
		case PoolObjectType.BloodSprayPredatorAnimated:
			if ((bool)poolBloodSprayPredatorAnimatedParent)
			{
				result = poolBloodSprayPredatorAnimated[currentBloodSprayPredatorAnimatedIndex];
				if (currentBloodSprayPredatorAnimatedIndex < poolBloodSprayPredatorAnimatedCount - 1)
				{
					currentBloodSprayPredatorAnimatedIndex++;
				}
				else
				{
					currentBloodSprayPredatorAnimatedIndex = 0;
				}
			}
			break;
		case PoolObjectType.BloodSplatPredator:
			if ((bool)poolBloodSplatPredatorParent)
			{
				result = poolBloodSplatPredator[currentBloodSplatPredatorIndex];
				if (currentBloodSplatPredatorIndex < poolBloodSplatPredatorCount - 1)
				{
					currentBloodSplatPredatorIndex++;
				}
				else
				{
					currentBloodSplatPredatorIndex = 0;
				}
			}
			break;
		case PoolObjectType.BloodPool:
			if ((bool)poolBloodPoolParent)
			{
				result = poolBloodPool[currentBloodPoolIndex];
				if (currentBloodPoolIndex < poolBloodPoolCount - 1)
				{
					currentBloodPoolIndex++;
				}
				else
				{
					currentBloodPoolIndex = 0;
				}
			}
			break;
		case PoolObjectType.NetOnEnemy:
			if ((bool)poolNetOnEnemyParent)
			{
				result = poolNetOnEnemy[currentNetOnEnemyIndex];
				if (currentNetOnEnemyIndex < poolNetOnEnemyCount - 1)
				{
					currentNetOnEnemyIndex++;
				}
				else
				{
					currentNetOnEnemyIndex = 0;
				}
			}
			break;
		case PoolObjectType.VisibilityLantern:
			if ((bool)poolVisibilityLanternParent)
			{
				result = poolVisibilityLantern[currentVisibilityLanternIndex];
				if (currentVisibilityLanternIndex < poolVisibilityLanternCount - 1)
				{
					currentVisibilityLanternIndex++;
				}
				else
				{
					currentVisibilityLanternIndex = 0;
				}
			}
			break;
		case PoolObjectType.CutEnemyHumanVerticalL:
			if ((bool)poolCutEnemyHumanVerticalLParent)
			{
				result = poolCutEnemyHumanVerticalL[currentCutEnemyHumanVerticalLIndex];
				if (currentCutEnemyHumanVerticalLIndex < poolCutEnemyHumanVerticalLCount - 1)
				{
					currentCutEnemyHumanVerticalLIndex++;
				}
				else
				{
					currentCutEnemyHumanVerticalLIndex = 0;
				}
			}
			break;
		case PoolObjectType.CutEnemyHumanVerticalR:
			if ((bool)poolCutEnemyHumanVerticalRParent)
			{
				result = poolCutEnemyHumanVerticalR[currentCutEnemyHumanVerticalRIndex];
				if (currentCutEnemyHumanVerticalRIndex < poolCutEnemyHumanVerticalRCount - 1)
				{
					currentCutEnemyHumanVerticalRIndex++;
				}
				else
				{
					currentCutEnemyHumanVerticalRIndex = 0;
				}
			}
			break;
		case PoolObjectType.CutDogVerticalL:
			if ((bool)poolCutDogVerticalLParent)
			{
				result = poolCutDogVerticalL[currentCutDogVerticalLIndex];
				if (currentCutDogVerticalLIndex < poolCutDogVerticalLCount - 1)
				{
					currentCutDogVerticalLIndex++;
				}
				else
				{
					currentCutDogVerticalLIndex = 0;
				}
			}
			break;
		case PoolObjectType.CutDogVerticalR:
			if ((bool)poolCutDogVerticalRParent)
			{
				result = poolCutDogVerticalR[currentCutDogVerticalRIndex];
				if (currentCutDogVerticalRIndex < poolCutDogVerticalRCount - 1)
				{
					currentCutDogVerticalRIndex++;
				}
				else
				{
					currentCutDogVerticalRIndex = 0;
				}
			}
			break;
		case PoolObjectType.CutEnemyIsabelleVerticalL:
			result = CutEnemyVerticalLIsabelle;
			break;
		case PoolObjectType.CutEnemyIsabelleVerticalR:
			result = CutEnemyVerticalRIsabelle;
			break;
		case PoolObjectType.CutEnemyBerserkersNoHead:
			result = CutEnemyNoHeadBerserkers;
			break;
		}
		return result;
	}

	private IEnumerator Deactivate(Transform objectToDeactivate, float timeLeft)
	{
		yield return new WaitForSeconds(timeLeft);
		objectToDeactivate.gameObject.SetActiveRecursively(false);
	}
}
