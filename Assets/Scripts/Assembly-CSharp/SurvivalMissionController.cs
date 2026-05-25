using System.Collections;
using UnityEngine;

public class SurvivalMissionController : MonoBehaviour
{
	private const int charsPerLine = 1000;

	private const float timeToSpawnBoss = 30f;

	public bool showDebugText;

	public bool showEnemySpawnPoints = true;

	public bool showBossSpawnPoints = true;

	public TextMesh textBossName;

	public Transform[] EnemiesSpawnPositions;

	public Transform[] BossSpawnPositions;

	public Transform PoolRoyceEnemiesParent;

	public Transform PoolIsabeleEnemiesParent;

	public Transform PoolHanzoEnemiesParent;

	public Transform PoolNikolaiEnemiesParent;

	public Transform PoolCuchilloEnemiesParent;

	public Transform PoolStansEnemiesParent;

	public Transform PoolMombasaEnemiesParent;

	public Transform PoolNolanEnemiesParent;

	public Transform PoolSoldierRifleEnemiesParent;

	public Transform PoolSoldierMacheteEnemiesParent;

	public Transform PoolSniperEnemiesParent;

	public Transform PoolBlackPredatorEnemiesParent;

	public Transform PoolDogEnemiesParent;

	public Transform PoolFalconerEnemiesParent;

	public Transform PoolTrackerEnemiesParent;

	public float enemyRespawnAfterDeathDelay = 3f;

	public float spawnRadius = 4.5f;

	public float spawnTimeNewEnemies = 3f;

	public float timeToFirstEnemies = 5f;

	public TextMesh waveText;

	public AudioSource audioSource;

	public AudioClip soundBloodSplatScreen;

	public int maxSnipersSpawnableAtOnce = 2;

	private int waveIndex;

	private int currentSpawnedMinionsAndBoss;

	private Vector3 waveTextInitialLocalScale = Vector3.one;

	private bool missionStarted;

	private int spawnPositionsNumber;

	private int roycePoolCount;

	private int isabelePoolCount;

	private int hanzoPoolCount;

	private int bossPoolCount;

	private int nikolaiPoolCount;

	private int cuchilloPoolCount;

	private int stansPoolCount;

	private int mombasaPoolCount;

	private int nolanPoolCount;

	private int soldierRiflePoolCount;

	private int soldierMachetePoolCount;

	private int sniperPoolCount;

	private int blackPredPoolCount;

	private int dogPoolCount;

	private int falconerPoolCount;

	private int trackerPoolCount;

	private int spawnedBlackPredatorCount;

	private int spawnedRoyceCount;

	private int spawnedNikolaiCount;

	private int spawnedCuchilloCount;

	private int spawnedMombasaCount;

	private int spawnedStansCount;

	private int spawnedIsabeleEnemies;

	private int spawnedHanzoEnemies;

	private int spawnedNolandEnemies;

	private int spawnedSoldierRifleEnemies;

	private int spawnedSoldierMacheteEnemies;

	private int spawnedSniperCount;

	private int spawnedDogCount;

	private int spawnedFalconerCount;

	private int spawnedTrackerCount;

	private int totalSpawnedEnemies;

	private int spawnEnemiesMaxCurrent;

	private int missionEnemiesKilled;

	private int currentMission = 1;

	private int royceKilledCount;

	private int blackPredatorKilledCount;

	private int nikolaiKilledCount;

	private int mombasaKilledCount;

	private int cuchilloKilledCount;

	private int stansKilledCount;

	private int isabeleKilledCount;

	private int hanzoKilledCount;

	private int nolandKilledCount;

	private int soldierRifleKilledCount;

	private int soldierMacheteKilledCount;

	private int sniperKilledCount;

	private int dogKilledCount;

	private int falconerKilledCount;

	private int trackerKilledCount;

	private int nikolaiTrophyCount;

	private int royceTrophyCount;

	private int mombasaTrophyCount;

	private int cuchilloTrophyCount;

	private int stansTrophyCount;

	private int isabeleTrophyCount;

	private int hanzoTrophyCount;

	private int nolanTrophyCount;

	private int soldierTrophyCount;

	private int dogTrophyCount;

	private int blackPredatorTrophyCount;

	private int berserkerTrophyCount;

	private int extraSoldiersRifle;

	private int extraDogs;

	private int honorPointsOnCurrentMission;

	private ArrayList poolRoyceEnemies;

	private ArrayList poolIsabeleEnemies;

	private ArrayList poolHanzoEnemies;

	private ArrayList poolNikolaiEnemies;

	private ArrayList poolCuchilloEnemies;

	private ArrayList poolStansEnemies;

	private ArrayList poolMombasaEnemies;

	private ArrayList poolNolandEnemies;

	private ArrayList poolSoldierRifleEnemies;

	private ArrayList poolSoldierMacheteEnemies;

	private ArrayList poolSniperEnemies;

	private ArrayList poolDogEnemies;

	private ArrayList poolBlackPredEnemies;

	private ArrayList poolFalconerEnemies;

	private ArrayList poolTrackerEnemies;

	private ArrayList missions;

	private bool[] availableBlackPredators = new bool[20]
	{
		true, true, true, true, true, true, true, true, true, true,
		true, true, true, true, true, true, true, true, true, true
	};

	private bool[] availableRoyce = new bool[20]
	{
		true, true, true, true, true, true, true, true, true, true,
		true, true, true, true, true, true, true, true, true, true
	};

	private bool[] availableIsabele = new bool[20]
	{
		true, true, true, true, true, true, true, true, true, true,
		true, true, true, true, true, true, true, true, true, true
	};

	private bool[] availableHanzo = new bool[20]
	{
		true, true, true, true, true, true, true, true, true, true,
		true, true, true, true, true, true, true, true, true, true
	};

	private bool[] availableNikolai = new bool[20]
	{
		true, true, true, true, true, true, true, true, true, true,
		true, true, true, true, true, true, true, true, true, true
	};

	private bool[] availableCuchillo = new bool[20]
	{
		true, true, true, true, true, true, true, true, true, true,
		true, true, true, true, true, true, true, true, true, true
	};

	private bool[] availableMombasa = new bool[20]
	{
		true, true, true, true, true, true, true, true, true, true,
		true, true, true, true, true, true, true, true, true, true
	};

	private bool[] availableStans = new bool[20]
	{
		true, true, true, true, true, true, true, true, true, true,
		true, true, true, true, true, true, true, true, true, true
	};

	private bool[] availableNolands = new bool[20]
	{
		true, true, true, true, true, true, true, true, true, true,
		true, true, true, true, true, true, true, true, true, true
	};

	private bool[] availableSoldiersRifle = new bool[20]
	{
		true, true, true, true, true, true, true, true, true, true,
		true, true, true, true, true, true, true, true, true, true
	};

	private bool[] availableSoldiersMachete = new bool[20]
	{
		true, true, true, true, true, true, true, true, true, true,
		true, true, true, true, true, true, true, true, true, true
	};

	private bool[] availableSnipers = new bool[20]
	{
		true, true, true, true, true, true, true, true, true, true,
		true, true, true, true, true, true, true, true, true, true
	};

	private bool[] availableDogs = new bool[20]
	{
		true, true, true, true, true, true, true, true, true, true,
		true, true, true, true, true, true, true, true, true, true
	};

	private bool[] availableFalconers = new bool[20]
	{
		true, true, true, true, true, true, true, true, true, true,
		true, true, true, true, true, true, true, true, true, true
	};

	private bool[] availableTrackers = new bool[20]
	{
		true, true, true, true, true, true, true, true, true, true,
		true, true, true, true, true, true, true, true, true, true
	};

	private int verticalSplitKillsDone;

	private int doubleDiskKillsDone;

	private int superSliceKillsDone;

	private int bodiesCut;

	private int killsWristBlade;

	private int killsCombiStick;

	private int killsPlasmaGun;

	private int killsWhip;

	private int killsWhipOneWhipe;

	private int killsDisc;

	private bool missionCompleted;

	private bool spawnedBoss;

	private bool spawnedSecondBoss;

	private float visibleDistanceToPlayer = 15f;

	private float waveTimer;

	private PlayerController playerController;

	private ArrayList spawnEnemiesBestChance;

	private float timeMissionStarted;

	private int netGunKilledCount;

	private int stealthKills;

	private int opponentsImpaled;

	private int bodiesSplit;

	private int longestSurvivalTime;

	private int netGunCaptures;

	private int maxDiscCombo;

	private int currentSlot;

	private GameObject BloodSplatScreen
	{
		get
		{
			return AManager.instance.playerController.hud.GUI_BloodSplatScreen.gameObject;
		}
	}

	private TextMesh BloodKillTextScore
	{
		get
		{
			return AManager.instance.playerController.hud.GUI_BloodKillTextScore;
		}
	}

	public bool MissionCompleted
	{
		set
		{
			missionCompleted = value;
		}
	}

	public int CurrentWaveIndex
	{
		get
		{
			return waveIndex;
		}
	}

	public int StealthKills
	{
		get
		{
			return stealthKills;
		}
		set
		{
			stealthKills = value;
		}
	}

	public int OpponentsImpaled
	{
		get
		{
			return opponentsImpaled;
		}
		set
		{
			opponentsImpaled = value;
		}
	}

	public int BodiesSplit
	{
		get
		{
			return bodiesSplit;
		}
		set
		{
			bodiesSplit = value;
		}
	}

	public int MaxDiscCombo
	{
		get
		{
			return maxDiscCombo;
		}
		set
		{
			if (value > maxDiscCombo)
			{
				maxDiscCombo = value;
			}
		}
	}

	public int SpawnEnemiesMaxCurrent
	{
		set
		{
			spawnEnemiesMaxCurrent = value;
		}
	}

	public bool MissionStarted
	{
		get
		{
			return missionStarted;
		}
	}

	public bool WaveClear
	{
		get
		{
			return missionCompleted;
		}
		set
		{
			missionCompleted = value;
		}
	}

	public int NetGunKilledCount
	{
		get
		{
			return netGunKilledCount;
		}
		set
		{
			netGunKilledCount = value;
		}
	}

	public int StansKilledCount
	{
		get
		{
			return stansKilledCount;
		}
	}

	public TextMesh MissionStatus
	{
		get
		{
			return AManager.instance.playerController.hud.missionStatus;
		}
	}

	public int MissionEnemiesKilled
	{
		get
		{
			return missionEnemiesKilled;
		}
	}

	public bool BossIsSpawned
	{
		get
		{
			return spawnedBoss;
		}
	}

	public int ExtraSoldiersRifle
	{
		set
		{
			extraSoldiersRifle = value;
		}
	}

	public int HonorPointsOnCurrentMission
	{
		get
		{
			return honorPointsOnCurrentMission;
		}
	}

	private IEnumerator EnemyRoyceAvailableAfterDeath(int enemyIndex)
	{
		yield return new WaitForSeconds(enemyRespawnAfterDeathDelay);
		availableRoyce[enemyIndex] = true;
	}

	private IEnumerator EnemyIsabeleAvailableAfterDeath(int enemyIndex)
	{
		yield return new WaitForSeconds(enemyRespawnAfterDeathDelay);
		availableIsabele[enemyIndex] = true;
	}

	private IEnumerator EnemyHanzoAvailableAfterDeath(int enemyIndex)
	{
		yield return new WaitForSeconds(enemyRespawnAfterDeathDelay);
		availableHanzo[enemyIndex] = true;
	}

	private IEnumerator EnemyNikolaiAvailableAfterDeath(int enemyIndex)
	{
		yield return new WaitForSeconds(enemyRespawnAfterDeathDelay);
		availableNikolai[enemyIndex] = true;
	}

	private IEnumerator EnemyCuchilloAvailableAfterDeath(int enemyIndex)
	{
		yield return new WaitForSeconds(enemyRespawnAfterDeathDelay);
		availableCuchillo[enemyIndex] = true;
	}

	private IEnumerator EnemyStansAvailableAfterDeath(int enemyIndex)
	{
		yield return new WaitForSeconds(enemyRespawnAfterDeathDelay);
		availableStans[enemyIndex] = true;
	}

	private IEnumerator EnemyNolandAvailableAfterDeath(int enemyIndex)
	{
		yield return new WaitForSeconds(enemyRespawnAfterDeathDelay);
		availableNolands[enemyIndex] = true;
	}

	private IEnumerator EnemySoldierRifleAvailableAfterDeath(int enemyIndex)
	{
		yield return new WaitForSeconds(enemyRespawnAfterDeathDelay);
		availableSoldiersRifle[enemyIndex] = true;
	}

	private IEnumerator EnemySoldierMacheteAvailableAfterDeath(int enemyIndex)
	{
		yield return new WaitForSeconds(enemyRespawnAfterDeathDelay);
		availableSoldiersMachete[enemyIndex] = true;
	}

	private IEnumerator EnemyMombasaAvailableAfterDeath(int enemyIndex)
	{
		yield return new WaitForSeconds(enemyRespawnAfterDeathDelay);
		availableMombasa[enemyIndex] = true;
	}

	private IEnumerator EnemySniperAvailableAfterDeath(int enemyIndex)
	{
		yield return new WaitForSeconds(enemyRespawnAfterDeathDelay);
		availableSnipers[enemyIndex] = true;
	}

	private IEnumerator EnemyDogAvailableAfterDeath(int enemyIndex)
	{
		yield return new WaitForSeconds(enemyRespawnAfterDeathDelay);
		availableDogs[enemyIndex] = true;
	}

	private IEnumerator EnemyFalconerAvailableAfterDeath(int enemyIndex)
	{
		yield return new WaitForSeconds(enemyRespawnAfterDeathDelay);
		availableFalconers[enemyIndex] = true;
	}

	private IEnumerator EnemyTrackerAvailableAfterDeath(int enemyIndex)
	{
		yield return new WaitForSeconds(enemyRespawnAfterDeathDelay);
		availableTrackers[enemyIndex] = true;
	}

	public void EnemyBlackPredatorDied(int predatorIndex, DeathType deathType)
	{
		StartCoroutine(EnemyRoyceAvailableAfterDeath(predatorIndex));
		totalSpawnedEnemies--;
		blackPredatorKilledCount++;
		missionEnemiesKilled++;
		AddHonorPoints(100, deathType);
		if (deathType == DeathType.TrophyKill)
		{
			blackPredatorTrophyCount++;
		}
		if (!playerController.liteVersion)
		{
			CrystalUnityBasic.Instance.PostAchievement("765304491", true, "Black Death", false);
		}
		missionCompleted = EndMissionCondition(currentMission);
	}

	public void EnemyRoyceDied(int royceIndex, DeathType deathType)
	{
		StartCoroutine(EnemyRoyceAvailableAfterDeath(royceIndex));
		totalSpawnedEnemies--;
		royceKilledCount++;
		missionEnemiesKilled++;
		AddHonorPoints(40, deathType);
		if (deathType == DeathType.TrophyKill)
		{
			if (playerController.CurrentMission != 34 && !playerController.liteVersion)
			{
				CrystalUnityBasic.Instance.PostAchievement("419982032", true, "Elite", false);
			}
			royceTrophyCount++;
		}
		missionCompleted = EndMissionCondition(currentMission);
	}

	public void EnemyIsabeleDied(int isabeleIndex, DeathType deathType)
	{
		StartCoroutine(EnemyIsabeleAvailableAfterDeath(isabeleIndex));
		totalSpawnedEnemies--;
		isabeleKilledCount++;
		missionEnemiesKilled++;
		AddHonorPoints(30, deathType);
		if (deathType == DeathType.TrophyKill)
		{
			if (playerController.CurrentMission != 34 && !playerController.liteVersion)
			{
				CrystalUnityBasic.Instance.PostAchievement("419956371", true, "Deadeye", false);
			}
			isabeleTrophyCount++;
		}
		missionCompleted = EndMissionCondition(currentMission);
	}

	public void EnemyHanzoDied(int hanzoIndex, DeathType deathType)
	{
		StartCoroutine(EnemyHanzoAvailableAfterDeath(hanzoIndex));
		totalSpawnedEnemies--;
		hanzoKilledCount++;
		missionEnemiesKilled++;
		AddHonorPoints(50, deathType);
		if (deathType == DeathType.TrophyKill)
		{
			if (playerController.CurrentMission != 34 && !playerController.liteVersion)
			{
				CrystalUnityBasic.Instance.PostAchievement("419915964", true, "Assasin", false);
			}
			hanzoTrophyCount++;
		}
		missionCompleted = EndMissionCondition(currentMission);
	}

	public void EnemyNikolaiDied(int nikolaiIndex, DeathType deathType)
	{
		StartCoroutine(EnemyNikolaiAvailableAfterDeath(nikolaiIndex));
		totalSpawnedEnemies--;
		nikolaiKilledCount++;
		missionEnemiesKilled++;
		AddHonorPoints(60, deathType);
		if (deathType == DeathType.TrophyKill)
		{
			if (playerController.CurrentMission != 34 && !playerController.liteVersion)
			{
				CrystalUnityBasic.Instance.PostAchievement("419938467", true, "Firepower", false);
			}
			nikolaiTrophyCount++;
		}
		missionCompleted = EndMissionCondition(currentMission);
	}

	public void EnemyDogDied(int dogIndex, DeathType deathType)
	{
		StartCoroutine(EnemyDogAvailableAfterDeath(dogIndex));
		totalSpawnedEnemies--;
		dogKilledCount++;
		missionEnemiesKilled++;
		AddHonorPoints(30, deathType);
		if (deathType == DeathType.TrophyKill)
		{
			dogTrophyCount++;
		}
		missionCompleted = EndMissionCondition(currentMission);
	}

	public void EnemyFalconerDied(int falconerIndex, DeathType deathType)
	{
		StartCoroutine(EnemyFalconerAvailableAfterDeath(falconerIndex));
		totalSpawnedEnemies--;
		falconerKilledCount++;
		missionEnemiesKilled++;
		AddHonorPoints(80, deathType);
		if (deathType == DeathType.TrophyKill)
		{
			berserkerTrophyCount++;
		}
		if (!playerController.liteVersion)
		{
			CrystalUnityBasic.Instance.PostAchievement("765290755", true, "Winged Death", false);
		}
		missionCompleted = EndMissionCondition(currentMission);
	}

	public void EnemyTrackerDied(int trackerIndex, DeathType deathType)
	{
		StartCoroutine(EnemyTrackerAvailableAfterDeath(trackerIndex));
		totalSpawnedEnemies--;
		trackerKilledCount++;
		missionEnemiesKilled++;
		AddHonorPoints(80, deathType);
		if (deathType == DeathType.TrophyKill)
		{
			berserkerTrophyCount++;
		}
		if (!playerController.liteVersion)
		{
			CrystalUnityBasic.Instance.PostAchievement("765314469", true, "Flusher", false);
		}
		missionCompleted = EndMissionCondition(currentMission);
	}

	private IEnumerator ResetWhipKillsOneWhipeCount()
	{
		yield return new WaitForSeconds(1f);
		killsWhipOneWhipe = 0;
	}

	public void AddHonorPoints(int honorPointsEnemy, DeathType deathType)
	{
		if (deathType == DeathType.EnemyKilled)
		{
			return;
		}
		int num = 1;
		if (AManager.PredatorInvisible)
		{
			StealthKills++;
		}
		switch (playerController.CurrentWeaponType)
		{
		case 1:
		case 2:
			killsWristBlade++;
			break;
		case 3:
			killsCombiStick++;
			break;
		case 4:
			killsWhip++;
			killsWhipOneWhipe++;
			if (killsWhipOneWhipe >= 4 && !playerController.liteVersion)
			{
				CrystalUnityBasic.Instance.PostAchievement("765281649", true, "The Chopper", false);
			}
			StartCoroutine(ResetWhipKillsOneWhipeCount());
			break;
		case 5:
			killsPlasmaGun++;
			break;
		}
		switch (deathType)
		{
		case DeathType.BodyCut:
			bodiesCut++;
			num = 2;
			if (AManager.PredatorInvisible)
			{
				num *= 2;
			}
			break;
		case DeathType.VerticalCut:
			bodiesCut++;
			verticalSplitKillsDone++;
			if (verticalSplitKillsDone == 20 && !playerController.liteVersion)
			{
				CrystalUnityBasic.Instance.PostAchievement("765290496", true, "Banana Split", false);
			}
			num = 2;
			if (AManager.PredatorInvisible)
			{
				num *= 2;
			}
			break;
		case DeathType.DiscCut:
			bodiesCut++;
			killsDisc++;
			break;
		case DeathType.NetGunCaptured:
			num = 2;
			break;
		case DeathType.BodySplice:
			bodiesCut++;
			num = 3;
			if (AManager.PredatorInvisible)
			{
				num *= 2;
			}
			BodiesSplit++;
			break;
		case DeathType.TrophyKill:
			if (AManager.PredatorInvisible && !playerController.liteVersion)
			{
				CrystalUnityBasic.Instance.PostAchievement("419951172", true, "Death Shadow", false);
			}
			num = 6;
			break;
		case DeathType.DoubleSplice:
			killsDisc++;
			bodiesCut++;
			BodiesSplit++;
			doubleDiskKillsDone++;
			num = 4;
			break;
		case DeathType.SuperSplice:
			killsDisc++;
			bodiesCut++;
			superSliceKillsDone++;
			if (superSliceKillsDone == 3 && !playerController.liteVersion)
			{
				CrystalUnityBasic.Instance.PostAchievement("419891936", true, "Reap the Whirlwind", false);
			}
			BodiesSplit++;
			doubleDiskKillsDone++;
			num = 8;
			break;
		default:
			num = 1;
			if (AManager.PredatorInvisible)
			{
				num *= 2;
			}
			break;
		}
		ShowBloodSplatScreen(num, honorPointsEnemy);
		honorPointsOnCurrentMission += honorPointsEnemy * num;
	}

	public void EnemyCuchilloDied(int cuchilloIndex, DeathType deathType)
	{
		StartCoroutine(EnemyCuchilloAvailableAfterDeath(cuchilloIndex));
		totalSpawnedEnemies--;
		cuchilloKilledCount++;
		missionEnemiesKilled++;
		AddHonorPoints(55, deathType);
		if (deathType == DeathType.TrophyKill)
		{
			if (playerController.CurrentMission != 34 && !playerController.liteVersion)
			{
				CrystalUnityBasic.Instance.PostAchievement("419926836", true, "Merciless", false);
			}
			cuchilloTrophyCount++;
		}
		missionCompleted = EndMissionCondition(currentMission);
	}

	public void EnemyStanDied(int stansIndex, DeathType deathType)
	{
		StartCoroutine(EnemyStansAvailableAfterDeath(stansIndex));
		totalSpawnedEnemies--;
		stansKilledCount++;
		missionEnemiesKilled++;
		AddHonorPoints(35, deathType);
		if (deathType == DeathType.TrophyKill)
		{
			if (playerController.CurrentMission != 34 && !playerController.liteVersion)
			{
				CrystalUnityBasic.Instance.PostAchievement("419958342", true, "Executioner", false);
			}
			stansTrophyCount++;
		}
		missionCompleted = EndMissionCondition(currentMission);
	}

	public void EnemyNolanDied(int nolanIndex, DeathType deathType)
	{
		StartCoroutine(EnemyNolandAvailableAfterDeath(nolanIndex));
		totalSpawnedEnemies--;
		nolandKilledCount++;
		missionEnemiesKilled++;
		AddHonorPoints(65, deathType);
		if (deathType == DeathType.TrophyKill)
		{
			if (playerController.CurrentMission != 34 && !playerController.liteVersion)
			{
				CrystalUnityBasic.Instance.PostAchievement("419974236", true, "Evader", false);
			}
			nolanTrophyCount++;
		}
		missionCompleted = EndMissionCondition(currentMission);
	}

	public void EnemySoldierRifleDied(int soldierIndex, DeathType deathType)
	{
		StartCoroutine(EnemySoldierRifleAvailableAfterDeath(soldierIndex));
		totalSpawnedEnemies--;
		soldierRifleKilledCount++;
		missionEnemiesKilled++;
		AddHonorPoints(15, deathType);
		if (deathType == DeathType.TrophyKill)
		{
			soldierTrophyCount++;
		}
		missionCompleted = EndMissionCondition(currentMission);
	}

	public void EnemySniperDied(int sniperIndex, DeathType deathType)
	{
		StartCoroutine(EnemySniperAvailableAfterDeath(sniperIndex));
		totalSpawnedEnemies--;
		sniperKilledCount++;
		missionEnemiesKilled++;
		AddHonorPoints(15, deathType);
		if (deathType == DeathType.TrophyKill)
		{
			soldierTrophyCount++;
		}
		missionCompleted = EndMissionCondition(currentMission);
	}

	public void EnemySoldierMacheteDied(int soldierIndex, DeathType deathType)
	{
		StartCoroutine(EnemySoldierMacheteAvailableAfterDeath(soldierIndex));
		totalSpawnedEnemies--;
		soldierMacheteKilledCount++;
		missionEnemiesKilled++;
		AddHonorPoints(15, deathType);
		if (deathType == DeathType.TrophyKill)
		{
			soldierTrophyCount++;
		}
		missionCompleted = EndMissionCondition(currentMission);
	}

	public void EnemyMombasaDied(int mombasaIndex, DeathType deathType)
	{
		StartCoroutine(EnemyMombasaAvailableAfterDeath(mombasaIndex));
		totalSpawnedEnemies--;
		mombasaKilledCount++;
		missionEnemiesKilled++;
		AddHonorPoints(25, deathType);
		if (deathType == DeathType.TrophyKill)
		{
			if (playerController.CurrentMission != 34 && !playerController.liteVersion)
			{
				CrystalUnityBasic.Instance.PostAchievement("419968247", true, "Ruthless", false);
			}
			mombasaTrophyCount++;
		}
		missionCompleted = EndMissionCondition(currentMission);
	}

	public void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		if (showEnemySpawnPoints)
		{
			for (int i = 0; i < EnemiesSpawnPositions.Length; i++)
			{
				Gizmos.color += new Color(1f / (float)EnemiesSpawnPositions.Length, 0f, 0f);
				Gizmos.color -= new Color(0f, 1f / (float)EnemiesSpawnPositions.Length, 0f);
				Gizmos.DrawWireSphere(EnemiesSpawnPositions[i].position, spawnRadius);
				Gizmos.DrawWireSphere(EnemiesSpawnPositions[i].position, 0.2f);
			}
		}
		if (showBossSpawnPoints)
		{
			for (int j = 0; j < BossSpawnPositions.Length; j++)
			{
				Gizmos.color = Color.magenta;
				Gizmos.DrawWireSphere(BossSpawnPositions[j].position, 0.2f);
			}
		}
	}

	public int EnemiesTrophyCount()
	{
		return nikolaiTrophyCount + nolanTrophyCount + isabeleTrophyCount + soldierTrophyCount + hanzoTrophyCount + royceTrophyCount + stansTrophyCount + cuchilloTrophyCount + blackPredatorTrophyCount + dogTrophyCount + berserkerTrophyCount + mombasaTrophyCount;
	}

	private void InitEnemyArrays(ref int enemyPoolCount, Transform poolEnemiesParent, ref ArrayList poolEnemies)
	{
		if (poolEnemiesParent != null)
		{
			enemyPoolCount = poolEnemiesParent.childCount;
			poolEnemies = new ArrayList(enemyPoolCount);
			for (int i = 0; i < enemyPoolCount; i++)
			{
				poolEnemies.Add(poolEnemiesParent.GetChild(i));
			}
		}
	}

	private void Awake()
	{
		currentSlot = EncryptedPlayerPrefs.GetInt("PR_CurrentSlot");
		netGunKilledCount = 0;
		timeMissionStarted = Time.time;
		waveTextInitialLocalScale = waveText.transform.localScale;
		waveText.gameObject.active = false;
		spawnPositionsNumber = EnemiesSpawnPositions.Length;
		InitEnemyArrays(ref blackPredPoolCount, PoolBlackPredatorEnemiesParent, ref poolBlackPredEnemies);
		InitEnemyArrays(ref roycePoolCount, PoolRoyceEnemiesParent, ref poolRoyceEnemies);
		InitEnemyArrays(ref hanzoPoolCount, PoolHanzoEnemiesParent, ref poolHanzoEnemies);
		InitEnemyArrays(ref isabelePoolCount, PoolIsabeleEnemiesParent, ref poolIsabeleEnemies);
		InitEnemyArrays(ref cuchilloPoolCount, PoolCuchilloEnemiesParent, ref poolCuchilloEnemies);
		InitEnemyArrays(ref nikolaiPoolCount, PoolNikolaiEnemiesParent, ref poolNikolaiEnemies);
		InitEnemyArrays(ref mombasaPoolCount, PoolMombasaEnemiesParent, ref poolMombasaEnemies);
		InitEnemyArrays(ref stansPoolCount, PoolStansEnemiesParent, ref poolStansEnemies);
		InitEnemyArrays(ref nolanPoolCount, PoolNolanEnemiesParent, ref poolNolandEnemies);
		InitEnemyArrays(ref soldierRiflePoolCount, PoolSoldierRifleEnemiesParent, ref poolSoldierRifleEnemies);
		InitEnemyArrays(ref soldierMachetePoolCount, PoolSoldierMacheteEnemiesParent, ref poolSoldierMacheteEnemies);
		InitEnemyArrays(ref sniperPoolCount, PoolSniperEnemiesParent, ref poolSniperEnemies);
		InitEnemyArrays(ref dogPoolCount, PoolDogEnemiesParent, ref poolDogEnemies);
		InitEnemyArrays(ref falconerPoolCount, PoolFalconerEnemiesParent, ref poolFalconerEnemies);
		InitEnemyArrays(ref trackerPoolCount, PoolTrackerEnemiesParent, ref poolTrackerEnemies);
		missions = new ArrayList();
		missions.Add(new MissionConfiguration(3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5000, 0, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 2f));
		missions.Add(new MissionConfiguration(3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 50000, 0, 0, 0, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 0f));
		missions.Add(new MissionConfiguration(3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 50000, 0, 0, 0, 0, 0, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 0.01f));
		missions.Add(new MissionConfiguration(3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 50000, 0, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 0f));
		missions.Add(new MissionConfiguration(3, 0, 0, 0, 0, 0, 0, 0, 0, 50010, 5000, 0, "Bring honor to thy tribe", " GO ! ", 0f));
		missions.Add(new MissionConfiguration(3, 0, 0, 0, 0, 0, 0, 0, 0, 50010, 5000, 0, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 1f));
		missions.Add(new MissionConfiguration(3, 0, 0, 0, 0, 0, 0, 0, 0, 50010, 5000, 0, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 3f));
		missions.Add(new MissionConfiguration(5, 0, 0, 0, 0, 0, 0, 0, 0, 50010, 5000, 1000, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 0.2f));
		missions.Add(new MissionConfiguration(5, 0, 0, 0, 0, 0, 0, 0, 0, 50010, 5000, 1000, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 0.2f));
		missions.Add(new MissionConfiguration(5, 0, 0, 0, 0, 0, 0, 0, 0, 50010, 5000, 1000, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 0.3f));
		missions.Add(new MissionConfiguration(5, 0, 0, 0, 0, 0, 0, 0, 0, 50010, 5000, 1000, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 0.4f));
		missions.Add(new MissionConfiguration(5, 0, 0, 0, 0, 0, 10010, 0, 0, 50010, 5000, 1000, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 0.4f));
		missions.Add(new MissionConfiguration(5, 0, 0, 0, 0, 0, 10010, 0, 0, 50010, 5000, 1000, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 0.4f));
		missions.Add(new MissionConfiguration(5, 0, 0, 0, 0, 0, 10010, 0, 0, 50010, 5000, 1000, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 0.5f));
		missions.Add(new MissionConfiguration(5, 0, 0, 1000, 0, 0, 10010, 0, 0, 50010, 5000, 1000, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 0.5f));
		missions.Add(new MissionConfiguration(2, 0, 0, 1000, 0, 0, 10010, 0, 0, 50010, 5000, 1000, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 0.6f));
		missions.Add(new MissionConfiguration(5, 0, 0, 1000, 0, 0, 10010, 0, 0, 50010, 5000, 0, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 0.6f));
		missions.Add(new MissionConfiguration(5, 0, 0, 1000, 0, 0, 10010, 0, 0, 50010, 5000, 1000, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 0.7f));
		missions.Add(new MissionConfiguration(5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5000, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 0.7f));
		missions.Add(new MissionConfiguration(5, 1000, 0, 1000, 0, 0, 10010, 0, 0, 50010, 5000, 1000, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 0.7f));
		missions.Add(new MissionConfiguration(5, 1000, 1000, 1000, 0, 0, 10010, 0, 0, 50010, 5000, 1000, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 0.7f));
		missions.Add(new MissionConfiguration(5, 1000, 1000, 1000, 0, 1000, 10010, 0, 0, 50010, 5000, 1000, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 0.9f));
		missions.Add(new MissionConfiguration(5, 1000, 1000, 1000, 0, 1000, 10010, 1000, 0, 50010, 5000, 1000, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 0.9f));
		missions.Add(new MissionConfiguration(5, 1000, 1000, 1000, 1000, 1000, 10010, 1000, 0, 50010, 5000, 1000, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 1f));
		missions.Add(new MissionConfiguration(5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10000, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 1f));
		missions.Add(new MissionConfiguration(5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 20, 0, 0, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 1f));
		missions.Add(new MissionConfiguration(5, 0, 0, 0, 0, 0, 0, 0, 0, 50000, 5000, 0, 0, 0, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 1f));
		missions.Add(new MissionConfiguration(5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1000, 0, 0, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 1f));
		missions.Add(new MissionConfiguration(5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1000, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 1f));
		missions.Add(new MissionConfiguration(6, 1100, 1100, 1100, 1100, 1100, 1100, 1100, 1000, 50000, 5000, 5000, 0, 1000, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 1f));
		missions.Add(new MissionConfiguration(5, 1100, 1100, 1100, 1100, 1100, 1100, 1100, 1000, 50000, 5000, 5000, 0, 0, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 1f));
		missions.Add(new MissionConfiguration(5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1000, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 1f));
		missions.Add(new MissionConfiguration(5, 0, 0, 0, 0, 0, 0, 0, 0, 5000, 5000, 5000, 0, 0, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 1f));
		missions.Add(new MissionConfiguration(5, 1100, 1100, 1100, 1100, 1100, 1100, 1100, 1000, 50000, 5000, 5000, 0, 0, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 1f));
		missions.Add(new MissionConfiguration(5, 1100, 1100, 1100, 1100, 1100, 1100, 1100, 0, 50000, 5000, 5000, "Bring honor to thy tribe", Language.GetTxt("MISSION_COMPLETE"), 0f));
		verticalSplitKillsDone = EncryptedPlayerPrefs.GetInt("PR_VerticalSplits_S" + currentSlot, 0);
	}

	private void Start()
	{
		playerController = (PlayerController)GameObject.FindWithTag("Player").GetComponent(typeof(PlayerController));
		if (!playerController.liteVersion)
		{
			if (playerController.CurrentMission == 32 || playerController.CurrentMission == 33)
			{
				StartSpawningEnemies();
			}
			if (playerController.CurrentMission == 34)
			{
				StartCoroutine(StartSpawningEnemiesDelayed(5f));
			}
		}
	}

	private IEnumerator StartSpawningEnemiesDelayed(float delay)
	{
		yield return new WaitForSeconds(delay);
		StartSpawningEnemies();
	}

	public void StartSpawningEnemies()
	{
		if (!missionStarted)
		{
			AManager.instance.playerController.hud.missionStatus.text = string.Empty;
			missionStarted = true;
			StartCoroutine(MissionStart());
		}
	}

	private void SpawnEnemy(ArrayList enemyPool, ref int spawnedEnemyCount, bool[] available)
	{
		if (enemyPool == null)
		{
			return;
		}
		int count = enemyPool.Count;
		int num = -1;
		for (int i = 0; i < count; i++)
		{
			if (available[i])
			{
				num = i;
				break;
			}
		}
		if (num != -1)
		{
			Transform transform = (Transform)enemyPool[num];
			int num2 = Random.Range(0, spawnPositionsNumber);
			while ((EnemiesSpawnPositions[num2].position - AManager.targetPosition).sqrMagnitude < visibleDistanceToPlayer * visibleDistanceToPlayer)
			{
				num2 = Random.Range(0, spawnPositionsNumber);
			}
			Vector3 position = Random.insideUnitSphere * spawnRadius + EnemiesSpawnPositions[num2].position;
			position.y = 0.1f;
			transform.transform.position = position;
			transform.gameObject.SetActiveRecursively(true);
			transform.SendMessage("Activate", num);
			available[num] = false;
			totalSpawnedEnemies++;
			spawnedEnemyCount++;
		}
	}

	private void SpawnBoss(ArrayList enemyPool, ref int spawnedEnemyCount, bool[] available, ArrayList minionsPool, ref int spawnedMinionsCount, bool[] availableMinions, int minionsToSpawn)
	{
		int num = 0;
		SetAllEnemiesToRelocate();
		if (enemyPool != null)
		{
			int count = enemyPool.Count;
			int num2 = -1;
			for (int i = 0; i < count; i++)
			{
				if (available[i])
				{
					num2 = i;
					break;
				}
			}
			if (num2 != -1)
			{
				Transform transform = (Transform)enemyPool[num2];
				float num3 = -100f;
				for (int j = 0; j < BossSpawnPositions.Length; j++)
				{
					if ((BossSpawnPositions[j].position - AManager.targetPosition).sqrMagnitude > num3)
					{
						num = j;
						num3 = (BossSpawnPositions[j].position - AManager.targetPosition).sqrMagnitude;
					}
				}
				Vector3 position = BossSpawnPositions[num].position;
				transform.transform.position = position;
				transform.gameObject.SetActiveRecursively(true);
				transform.SendMessage("Activate", num2);
				transform.SendMessage("BossMode", SendMessageOptions.DontRequireReceiver);
				available[num2] = false;
				totalSpawnedEnemies++;
				spawnedEnemyCount++;
				currentSpawnedMinionsAndBoss++;
			}
		}
		if (minionsPool == null)
		{
			return;
		}
		if (BossSpawnPositions[num].childCount < minionsToSpawn)
		{
			minionsToSpawn = BossSpawnPositions[num].childCount;
		}
		int count2 = minionsPool.Count;
		for (int k = 0; k < minionsToSpawn; k++)
		{
			int num4 = -1;
			for (int l = 0; l < count2; l++)
			{
				if (availableMinions[l])
				{
					num4 = l;
					break;
				}
			}
			if (num4 != -1)
			{
				Transform transform2 = (Transform)minionsPool[num4];
				Vector3 position2 = BossSpawnPositions[num].GetChild(k).position;
				position2.y = 0f;
				transform2.transform.position = position2;
				transform2.gameObject.SetActiveRecursively(true);
				transform2.SendMessage("Activate", num4);
				availableMinions[num4] = false;
				totalSpawnedEnemies++;
				spawnedMinionsCount++;
				currentSpawnedMinionsAndBoss++;
			}
		}
	}

	public void ShowBloodSplatScreen(int multiplier, int honorPoints)
	{
		StopCoroutine("BloodSplatScreenHide");
		BloodSplatScreen.active = false;
		BloodKillTextScore.gameObject.active = false;
		if (multiplier > 1)
		{
			BloodKillTextScore.text = multiplier + " x " + honorPoints;
		}
		else
		{
			BloodKillTextScore.text = honorPoints.ToString();
		}
		if (Utils.SfxOn)
		{
			audioSource.PlayOneShot(soundBloodSplatScreen);
		}
		if (AManager.BloodOn)
		{
			BloodSplatScreen.transform.localEulerAngles = new Vector3(0f, 180f, Random.Range(0f, 360f));
			BloodSplatScreen.active = true;
		}
		BloodKillTextScore.gameObject.active = true;
		StartCoroutine("BloodSplatScreenHide", 2f);
	}

	private IEnumerator BloodSplatScreenHide(float displayDuration)
	{
		yield return new WaitForSeconds(displayDuration);
		BloodSplatScreen.active = false;
		BloodKillTextScore.gameObject.active = false;
	}

	private void SetMissionStatus(int wave)
	{
		switch (wave)
		{
		case 0:
			AManager.instance.playerController.hud.missionStatus.text = killsDisc + "/5";
			break;
		case 1:
			AManager.instance.playerController.hud.missionStatus.text = missionEnemiesKilled + "/10";
			break;
		case 2:
			AManager.instance.playerController.hud.missionStatus.text = missionEnemiesKilled + "/15";
			break;
		case 3:
			AManager.instance.playerController.hud.missionStatus.text = missionEnemiesKilled + "/15";
			break;
		case 4:
			AManager.instance.playerController.hud.missionStatus.text = missionEnemiesKilled + "/20";
			break;
		case 5:
			AManager.instance.playerController.hud.missionStatus.text = missionEnemiesKilled + "/15";
			break;
		case 6:
			AManager.instance.playerController.hud.missionStatus.text = EnemiesTrophyCount() + "/5";
			break;
		case 7:
			AManager.instance.playerController.hud.missionStatus.text = string.Format("{0:00}:{1:00}", (int)waveTimer / 60, (int)waveTimer % 60);
			break;
		case 8:
			AManager.instance.playerController.hud.missionStatus.text = missionEnemiesKilled + "/5";
			break;
		case 9:
			AManager.instance.playerController.hud.missionStatus.text = killsPlasmaGun + "/15";
			break;
		case 10:
			AManager.instance.playerController.hud.missionStatus.text = missionEnemiesKilled + "/30";
			break;
		case 11:
			AManager.instance.playerController.hud.missionStatus.text = killsCombiStick + "/20";
			break;
		case 12:
			AManager.instance.playerController.hud.missionStatus.text = (5 - waveIndex).ToString();
			break;
		case 13:
			AManager.instance.playerController.hud.missionStatus.text = missionEnemiesKilled + "/30";
			break;
		case 14:
			AManager.instance.playerController.hud.missionStatus.text = NetGunKilledCount + "/10";
			break;
		case 15:
			AManager.instance.playerController.hud.missionStatus.text = missionEnemiesKilled + "/15";
			break;
		case 16:
			AManager.instance.playerController.hud.missionStatus.text = killsDisc + "/20";
			break;
		case 17:
			AManager.instance.playerController.hud.missionStatus.text = bodiesCut + "/20";
			break;
		case 18:
			AManager.instance.playerController.hud.missionStatus.text = missionEnemiesKilled + "/10";
			break;
		case 19:
			AManager.instance.playerController.hud.missionStatus.text = missionEnemiesKilled + "/30";
			break;
		case 20:
			AManager.instance.playerController.hud.missionStatus.text = string.Format("{0:00}:{1:00}", (int)waveTimer / 60, (int)waveTimer % 60);
			break;
		case 21:
			AManager.instance.playerController.hud.missionStatus.text = missionEnemiesKilled + "/10";
			break;
		case 22:
			AManager.instance.playerController.hud.missionStatus.text = (5 - waveIndex).ToString();
			break;
		case 23:
			AManager.instance.playerController.hud.missionStatus.text = EnemiesTrophyCount() + "/15";
			break;
		case 24:
			AManager.instance.playerController.hud.missionStatus.text = string.Format("{0:00}:{1:00}", (int)waveTimer / 60, (int)waveTimer % 60);
			if (waveTimer < 120f)
			{
				spawnEnemiesMaxCurrent = 4;
			}
			if (waveTimer < 80f)
			{
				spawnEnemiesMaxCurrent = 5;
			}
			if (waveTimer < 40f)
			{
				spawnEnemiesMaxCurrent = 6;
			}
			break;
		case 25:
			AManager.instance.playerController.hud.missionStatus.text = missionEnemiesKilled + "/20";
			break;
		case 26:
			AManager.instance.playerController.hud.missionStatus.text = killsWhip + "/30";
			break;
		case 27:
		case 28:
			AManager.instance.playerController.hud.missionStatus.text = string.Empty;
			break;
		case 29:
			AManager.instance.playerController.hud.missionStatus.text = EnemiesTrophyCount() + "/15";
			break;
		case 30:
			AManager.instance.playerController.hud.missionStatus.text = string.Format("{0:00}:{1:00}", (int)waveTimer / 60, (int)waveTimer % 60);
			break;
		case 31:
			AManager.instance.playerController.hud.missionStatus.text = string.Empty;
			break;
		case 32:
			AManager.instance.playerController.hud.missionStatus.text = missionEnemiesKilled.ToString();
			break;
		case 33:
			AManager.instance.playerController.hud.missionStatus.text = waveIndex.ToString();
			break;
		case 34:
			AManager.instance.playerController.hud.missionStatus.text = missionEnemiesKilled.ToString();
			break;
		default:
			AManager.instance.playerController.hud.missionStatus.text = missionEnemiesKilled.ToString();
			break;
		}
	}

	public bool EndMissionCondition(int wave)
	{
		SetMissionStatus(wave);
		bool flag = false;
		switch (wave)
		{
		case 0:
			if (killsDisc >= 5 && !spawnedBoss)
			{
				playerController.HideKillsIconAndStatsMessage();
				spawnedBoss = true;
				SpawnBoss(poolNikolaiEnemies, ref spawnedNikolaiCount, availableNikolai, poolSoldierRifleEnemies, ref spawnedSoldierRifleEnemies, availableSoldiersRifle, 2);
				textBossName.text = "NIKOLAI";
			}
			return nikolaiKilledCount >= 1;
		case 1:
			return missionEnemiesKilled >= 10;
		case 2:
			return missionEnemiesKilled >= 15;
		case 3:
			return missionEnemiesKilled >= 15;
		case 4:
			return missionEnemiesKilled >= 20;
		case 5:
			return missionEnemiesKilled >= 15;
		case 6:
			return EnemiesTrophyCount() >= 5;
		case 7:
			return waveTimer <= 0f;
		case 8:
			return missionEnemiesKilled >= 5;
		case 9:
			return killsPlasmaGun >= 15;
		case 10:
			if (missionEnemiesKilled >= 30 && !spawnedBoss)
			{
				spawnedBoss = true;
				playerController.HideKillsIconAndStatsMessage();
				SpawnBoss(poolStansEnemies, ref spawnedStansCount, availableStans, poolSoldierRifleEnemies, ref spawnedSoldierRifleEnemies, availableSoldiersRifle, 2);
				textBossName.text = "STANS";
			}
			return stansKilledCount >= 1;
		case 11:
			return killsCombiStick >= 20;
		case 12:
			return false;
		case 13:
			if (missionEnemiesKilled >= 30 && !spawnedBoss)
			{
				playerController.HideKillsIconAndStatsMessage();
				spawnedBoss = true;
				SpawnBoss(poolRoyceEnemies, ref spawnedRoyceCount, availableRoyce, poolSoldierMacheteEnemies, ref spawnedSoldierMacheteEnemies, availableSoldiersMachete, 2);
				textBossName.text = "ROYCE";
			}
			return royceKilledCount >= 1;
		case 14:
			return NetGunKilledCount >= 10;
		case 15:
			return missionEnemiesKilled >= 15;
		case 16:
			return killsDisc >= 20;
		case 17:
			if (bodiesCut >= 20 && !spawnedBoss)
			{
				playerController.HideKillsIconAndStatsMessage();
				spawnedBoss = true;
				SpawnBoss(poolHanzoEnemies, ref spawnedHanzoEnemies, availableHanzo, poolSoldierMacheteEnemies, ref spawnedSoldierMacheteEnemies, availableSoldiersMachete, 2);
				textBossName.text = "HANZO";
			}
			return hanzoKilledCount >= 1;
		case 18:
			if (sniperKilledCount >= 10 && !spawnedBoss)
			{
				playerController.HideKillsIconAndStatsMessage();
				spawnedBoss = true;
				SpawnBoss(poolIsabeleEnemies, ref spawnedIsabeleEnemies, availableIsabele, poolSniperEnemies, ref spawnedSniperCount, availableSnipers, 2);
				textBossName.text = "ISABELLE";
			}
			return isabeleKilledCount >= 1;
		case 19:
			if (missionEnemiesKilled >= 30 && !spawnedBoss)
			{
				playerController.HideKillsIconAndStatsMessage();
				spawnedBoss = true;
				SpawnBoss(poolMombasaEnemies, ref spawnedMombasaCount, availableMombasa, poolSoldierRifleEnemies, ref spawnedSoldierRifleEnemies, availableSoldiersRifle, 3);
				textBossName.text = "MOMBASA";
			}
			return mombasaKilledCount >= 1;
		case 20:
			if (waveTimer <= 0f && !spawnedBoss)
			{
				playerController.HideKillsIconAndStatsMessage();
				spawnedBoss = true;
				SpawnBoss(poolCuchilloEnemies, ref spawnedCuchilloCount, availableCuchillo, poolRoyceEnemies, ref spawnedRoyceCount, availableRoyce, 1);
				textBossName.text = "CUCHILLO";
			}
			return cuchilloKilledCount >= 1;
		case 21:
			if (missionEnemiesKilled >= 10 && !spawnedBoss)
			{
				playerController.HideKillsIconAndStatsMessage();
				spawnedBoss = true;
				SpawnBoss(poolNikolaiEnemies, ref spawnedNikolaiCount, availableNikolai, poolHanzoEnemies, ref spawnedHanzoEnemies, availableHanzo, 1);
				textBossName.text = "NIKOLAI";
			}
			return nikolaiKilledCount >= 1;
		case 22:
			return false;
		case 23:
			if (EnemiesTrophyCount() >= 15 && !spawnedBoss)
			{
				playerController.HideKillsIconAndStatsMessage();
				spawnedBoss = true;
				SpawnBoss(poolNolandEnemies, ref spawnedNolandEnemies, availableNolands, poolSoldierMacheteEnemies, ref spawnedSoldierMacheteEnemies, availableSoldiersMachete, 3);
				textBossName.text = "NOLAND";
			}
			return nolandKilledCount >= 1;
		case 24:
			return false;
		case 25:
			if (missionEnemiesKilled >= 20 && !spawnedBoss)
			{
				playerController.HideKillsIconAndStatsMessage();
				spawnedBoss = true;
				extraDogs = 5000;
				spawnEnemiesMaxCurrent = 3;
				SpawnBoss(poolTrackerEnemies, ref spawnedTrackerCount, availableTrackers, poolDogEnemies, ref spawnedDogCount, availableDogs, 2);
				textBossName.text = "TRACKER PREDATOR";
			}
			return trackerKilledCount >= 1;
		case 26:
			return killsWhip >= 30;
		case 27:
			if (!spawnedBoss)
			{
				playerController.HideKillsIconAndStatsMessage();
				spawnedBoss = true;
				spawnTimeNewEnemies = 30f;
				SpawnBoss(poolFalconerEnemies, ref spawnedFalconerCount, availableFalconers, poolSoldierMacheteEnemies, ref spawnedSoldierMacheteEnemies, availableSoldiersMachete, 0);
				textBossName.text = "FALCONER PREDATOR";
			}
			return falconerKilledCount >= 1;
		case 28:
			if (!spawnedBoss)
			{
				playerController.HideKillsIconAndStatsMessage();
				spawnedBoss = true;
				spawnTimeNewEnemies = 10f;
				SpawnBoss(poolFalconerEnemies, ref spawnedFalconerCount, availableFalconers, poolSoldierMacheteEnemies, ref spawnedSoldierMacheteEnemies, availableSoldiersMachete, 0);
				textBossName.text = string.Empty;
			}
			if (Time.time - timeMissionStarted > 3f && !spawnedSecondBoss)
			{
				playerController.HideKillsIconAndStatsMessage();
				spawnedSecondBoss = true;
				SpawnBoss(poolTrackerEnemies, ref spawnedTrackerCount, availableTrackers, poolDogEnemies, ref spawnedDogCount, availableDogs, 2);
				textBossName.text = string.Empty;
			}
			return falconerKilledCount >= 1 && trackerKilledCount >= 1;
		case 29:
			return EnemiesTrophyCount() >= 15;
		case 30:
			return false;
		case 31:
			if (!spawnedBoss)
			{
				playerController.HideKillsIconAndStatsMessage();
				spawnedBoss = true;
				spawnTimeNewEnemies = 10f;
				SpawnBoss(poolBlackPredEnemies, ref spawnedBlackPredatorCount, availableBlackPredators, poolSoldierMacheteEnemies, ref spawnedSoldierMacheteEnemies, availableSoldiersMachete, 0);
				textBossName.text = "MR. BLACK";
			}
			return blackPredatorKilledCount >= 1;
		case 32:
			return false;
		case 33:
			return false;
		default:
			return false;
		}
	}

	private IEnumerator StartTimingSurvivalTime()
	{
		waveTimer = 0f;
		while (true)
		{
			waveTimer += Time.deltaTime;
			SetMissionStatus(currentMission);
			yield return null;
		}
	}

	private IEnumerator StartWaveTimer(float amount)
	{
		missionCompleted = false;
		waveTimer = amount;
		SetMissionStatus(currentMission);
		while (waveTimer >= 0f)
		{
			waveTimer -= Time.deltaTime;
			SetMissionStatus(currentMission);
			yield return null;
		}
		if (currentMission != 20)
		{
			missionCompleted = true;
		}
	}

	private bool CanSpawnSniper()
	{
		if (currentMission == 18)
		{
			return true;
		}
		int num = 0;
		for (int i = 0; i < sniperPoolCount; i++)
		{
			if (!availableSnipers[i])
			{
				num++;
			}
		}
		if (num >= maxSnipersSpawnableAtOnce)
		{
			return false;
		}
		return true;
	}

	private IEnumerator SpawnEnemiesMercenary(MissionConfiguration currentMissionConfiguration)
	{
		totalSpawnedEnemies = 0;
		spawnedRoyceCount = 0;
		spawnEnemiesBestChance = new ArrayList();
		spawnEnemiesMaxCurrent = currentMissionConfiguration.maxEnemiesAtATime;
		while (true)
		{
			int enemiesToSpawn = spawnEnemiesMaxCurrent - totalSpawnedEnemies;
			for (int i = 0; i < enemiesToSpawn; i++)
			{
				if (!AManager.instance.CinematicInProgress)
				{
					spawnEnemiesBestChance.Clear();
					if (spawnedDogCount < currentMissionConfiguration.dogCount + extraDogs)
					{
						spawnEnemiesBestChance.Add(EnemyType.Dog);
					}
					if (spawnedBlackPredatorCount < currentMissionConfiguration.superBlackPredatorCount)
					{
						spawnEnemiesBestChance.Add(EnemyType.MrBlack);
					}
					if (spawnedFalconerCount < currentMissionConfiguration.falconerCount)
					{
						spawnEnemiesBestChance.Add(EnemyType.Falconer);
					}
					if (spawnedTrackerCount < currentMissionConfiguration.trackerCount)
					{
						spawnEnemiesBestChance.Add(EnemyType.Tracker);
					}
					if (spawnedRoyceCount < currentMissionConfiguration.royceCount)
					{
						spawnEnemiesBestChance.Add(EnemyType.Royce);
					}
					if (spawnedCuchilloCount < currentMissionConfiguration.cuchilloCount)
					{
						spawnEnemiesBestChance.Add(EnemyType.Cuchillo);
					}
					if (spawnedHanzoEnemies < currentMissionConfiguration.hanzoCount)
					{
						spawnEnemiesBestChance.Add(EnemyType.Hanzo);
					}
					if (spawnedIsabeleEnemies < currentMissionConfiguration.isabeleCount)
					{
						spawnEnemiesBestChance.Add(EnemyType.Isabelle);
					}
					if (spawnedMombasaCount < currentMissionConfiguration.mombasaCount)
					{
						spawnEnemiesBestChance.Add(EnemyType.Mombasa);
					}
					if (spawnedNikolaiCount < currentMissionConfiguration.nikolaiCount)
					{
						spawnEnemiesBestChance.Add(EnemyType.Nikolai);
					}
					if (spawnedNolandEnemies < currentMissionConfiguration.nolanCount)
					{
						spawnEnemiesBestChance.Add(EnemyType.Noland);
					}
					if (spawnedSoldierRifleEnemies < currentMissionConfiguration.soldierRifleCount + extraSoldiersRifle)
					{
						spawnEnemiesBestChance.Add(EnemyType.SoldierRifle);
					}
					if (spawnedSoldierMacheteEnemies < currentMissionConfiguration.soldierMacheteCount)
					{
						spawnEnemiesBestChance.Add(EnemyType.SoldierMachete);
					}
					if (spawnedSniperCount < currentMissionConfiguration.sniperCount && CanSpawnSniper())
					{
						spawnEnemiesBestChance.Add(EnemyType.Sniper);
					}
					if (spawnedStansCount < currentMissionConfiguration.stansCount)
					{
						spawnEnemiesBestChance.Add(EnemyType.Stans);
					}
					if (spawnedBlackPredatorCount < currentMissionConfiguration.superBlackPredatorCount)
					{
						spawnEnemiesBestChance.Add(EnemyType.MrBlack);
					}
					int spawnableEnemiesCount = spawnEnemiesBestChance.Count;
					EnemyType enemyTypeToSpawn = ((spawnableEnemiesCount > 0) ? ((EnemyType)(int)spawnEnemiesBestChance[Random.Range(0, spawnableEnemiesCount)]) : EnemyType.None);
					if (currentMission == 29 && spawnedDogCount - dogKilledCount < 3)
					{
						enemyTypeToSpawn = EnemyType.Dog;
					}
					switch (enemyTypeToSpawn)
					{
					case EnemyType.Falconer:
						SpawnEnemy(poolFalconerEnemies, ref spawnedFalconerCount, availableFalconers);
						break;
					case EnemyType.Tracker:
						SpawnEnemy(poolTrackerEnemies, ref spawnedTrackerCount, availableTrackers);
						break;
					case EnemyType.Dog:
						SpawnEnemy(poolDogEnemies, ref spawnedDogCount, availableDogs);
						break;
					case EnemyType.MrBlack:
						SpawnEnemy(poolBlackPredEnemies, ref spawnedBlackPredatorCount, availableBlackPredators);
						break;
					case EnemyType.Cuchillo:
						SpawnEnemy(poolCuchilloEnemies, ref spawnedCuchilloCount, availableCuchillo);
						break;
					case EnemyType.Hanzo:
						SpawnEnemy(poolHanzoEnemies, ref spawnedHanzoEnemies, availableHanzo);
						break;
					case EnemyType.Isabelle:
						SpawnEnemy(poolIsabeleEnemies, ref spawnedIsabeleEnemies, availableIsabele);
						break;
					case EnemyType.Mombasa:
						SpawnEnemy(poolMombasaEnemies, ref spawnedMombasaCount, availableMombasa);
						break;
					case EnemyType.Nikolai:
						SpawnEnemy(poolNikolaiEnemies, ref spawnedNikolaiCount, availableNikolai);
						break;
					case EnemyType.Noland:
						SpawnEnemy(poolNolandEnemies, ref spawnedNolandEnemies, availableNolands);
						break;
					case EnemyType.Royce:
						SpawnEnemy(poolRoyceEnemies, ref spawnedRoyceCount, availableRoyce);
						break;
					case EnemyType.SoldierRifle:
						SpawnEnemy(poolSoldierRifleEnemies, ref spawnedSoldierRifleEnemies, availableSoldiersRifle);
						break;
					case EnemyType.SoldierMachete:
						SpawnEnemy(poolSoldierMacheteEnemies, ref spawnedSoldierMacheteEnemies, availableSoldiersMachete);
						break;
					case EnemyType.Sniper:
						SpawnEnemy(poolSniperEnemies, ref spawnedSniperCount, availableSnipers);
						break;
					case EnemyType.Stans:
						SpawnEnemy(poolStansEnemies, ref spawnedStansCount, availableStans);
						break;
					}
				}
				yield return new WaitForSeconds(0.1f);
			}
			yield return new WaitForSeconds(spawnTimeNewEnemies);
		}
	}

	private void SpawnRandomBoss(bool noHumans)
	{
		if (noHumans)
		{
			if (Random.value < 0.5f)
			{
				SpawnBoss(poolTrackerEnemies, ref spawnedTrackerCount, availableTrackers, poolDogEnemies, ref spawnedDogCount, availableDogs, 2);
				textBossName.text = "TRACKER PREDATOR";
			}
			else
			{
				SpawnBoss(poolFalconerEnemies, ref spawnedFalconerCount, availableFalconers, poolDogEnemies, ref spawnedDogCount, availableDogs, 2);
				textBossName.text = "FALCONER PREDATOR";
			}
			return;
		}
		spawnEnemiesBestChance.Clear();
		spawnEnemiesBestChance.Add(EnemyType.Royce);
		spawnEnemiesBestChance.Add(EnemyType.Cuchillo);
		spawnEnemiesBestChance.Add(EnemyType.Hanzo);
		spawnEnemiesBestChance.Add(EnemyType.Isabelle);
		spawnEnemiesBestChance.Add(EnemyType.Mombasa);
		spawnEnemiesBestChance.Add(EnemyType.Nikolai);
		spawnEnemiesBestChance.Add(EnemyType.Noland);
		spawnEnemiesBestChance.Add(EnemyType.Stans);
		int count = spawnEnemiesBestChance.Count;
		switch ((EnemyType)((count > 0) ? ((int)spawnEnemiesBestChance[Random.Range(0, count)]) : 0))
		{
		case EnemyType.Cuchillo:
			SpawnBoss(poolCuchilloEnemies, ref spawnedCuchilloCount, availableCuchillo, poolRoyceEnemies, ref spawnedRoyceCount, availableRoyce, 1);
			textBossName.text = "CUCHILLO";
			break;
		case EnemyType.Hanzo:
			SpawnBoss(poolHanzoEnemies, ref spawnedHanzoEnemies, availableHanzo, poolSoldierMacheteEnemies, ref spawnedSoldierMacheteEnemies, availableSoldiersMachete, 2);
			textBossName.text = "HANZO";
			break;
		case EnemyType.Isabelle:
			SpawnBoss(poolIsabeleEnemies, ref spawnedIsabeleEnemies, availableIsabele, poolSniperEnemies, ref spawnedSniperCount, availableSnipers, 2);
			textBossName.text = "ISABELLE";
			break;
		case EnemyType.Mombasa:
			SpawnBoss(poolMombasaEnemies, ref spawnedMombasaCount, availableMombasa, poolSoldierRifleEnemies, ref spawnedSoldierRifleEnemies, availableSoldiersRifle, 3);
			textBossName.text = "MOMBASA";
			break;
		case EnemyType.Nikolai:
			SpawnBoss(poolNikolaiEnemies, ref spawnedNikolaiCount, availableNikolai, poolSoldierRifleEnemies, ref spawnedSoldierRifleEnemies, availableSoldiersRifle, 2);
			textBossName.text = "NIKOLAI";
			break;
		case EnemyType.Noland:
			SpawnBoss(poolNolandEnemies, ref spawnedNolandEnemies, availableNolands, poolSoldierMacheteEnemies, ref spawnedSoldierMacheteEnemies, availableSoldiersMachete, 3);
			textBossName.text = "NOLAND";
			break;
		case EnemyType.Royce:
			SpawnBoss(poolRoyceEnemies, ref spawnedRoyceCount, availableRoyce, poolSoldierMacheteEnemies, ref spawnedSoldierMacheteEnemies, availableSoldiersMachete, 2);
			textBossName.text = "ROYCE";
			break;
		case EnemyType.Stans:
			SpawnBoss(poolStansEnemies, ref spawnedStansCount, availableStans, poolSoldierRifleEnemies, ref spawnedSoldierRifleEnemies, availableSoldiersRifle, 2);
			textBossName.text = "STANS";
			break;
		case EnemyType.SoldierRifle:
			break;
		}
	}

	private IEnumerator ShowWaveText()
	{
		waveText.gameObject.active = true;
		waveText.transform.localScale = waveTextInitialLocalScale;
		waveText.GetComponent<Renderer>().material.color = new Color(waveText.GetComponent<Renderer>().material.color.r, waveText.GetComponent<Renderer>().material.color.g, waveText.GetComponent<Renderer>().material.color.b, 0f);
		float scaleIncrement = 1.01f;
		float maxScale = 0.4f;
		float alphaIncrement = 0.15f;
		while (waveText.GetComponent<Renderer>().material.color.a < 1f)
		{
			waveText.GetComponent<Renderer>().material.color = new Color(waveText.GetComponent<Renderer>().material.color.r, waveText.GetComponent<Renderer>().material.color.g, waveText.GetComponent<Renderer>().material.color.b, waveText.GetComponent<Renderer>().material.color.a + alphaIncrement);
			float newScaleX2 = waveText.transform.localScale.x * scaleIncrement;
			float newScaleY2 = waveText.transform.localScale.y * scaleIncrement;
			newScaleX2 = Mathf.Clamp(newScaleX2, waveTextInitialLocalScale.x, maxScale);
			newScaleY2 = Mathf.Clamp(newScaleY2, waveTextInitialLocalScale.y, maxScale);
			waveText.transform.localScale = new Vector3(newScaleX2, newScaleY2, 1f);
			yield return null;
		}
		float timerGrow = 0.7f;
		while (timerGrow > 0f)
		{
			float newScaleX2 = waveText.transform.localScale.x * scaleIncrement;
			float newScaleY2 = waveText.transform.localScale.y * scaleIncrement;
			newScaleX2 = Mathf.Clamp(newScaleX2, waveTextInitialLocalScale.x, maxScale);
			newScaleY2 = Mathf.Clamp(newScaleY2, waveTextInitialLocalScale.y, maxScale);
			waveText.transform.localScale = new Vector3(newScaleX2, newScaleY2, 1f);
			timerGrow -= Time.deltaTime;
			yield return null;
		}
		while (waveText.GetComponent<Renderer>().material.color.a > 0f)
		{
			waveText.GetComponent<Renderer>().material.color = new Color(waveText.GetComponent<Renderer>().material.color.r, waveText.GetComponent<Renderer>().material.color.g, waveText.GetComponent<Renderer>().material.color.b, waveText.GetComponent<Renderer>().material.color.a - alphaIncrement);
			float newScaleX2 = waveText.transform.localScale.x * scaleIncrement;
			float newScaleY2 = waveText.transform.localScale.y * scaleIncrement;
			newScaleX2 = Mathf.Clamp(newScaleX2, waveTextInitialLocalScale.x, maxScale);
			newScaleY2 = Mathf.Clamp(newScaleY2, waveTextInitialLocalScale.y, maxScale);
			waveText.transform.localScale = new Vector3(newScaleX2, newScaleY2, 1f);
			yield return null;
		}
		waveText.gameObject.active = false;
		yield return null;
	}

	private IEnumerator MissionStart()
	{
		if (playerController.liteVersion)
		{
			currentMission = 0;
		}
		else
		{
			currentMission = playerController.CurrentMission;
		}
		if (currentMission == 7 || currentMission == 20 || currentMission == 30 || currentMission == 24)
		{
			waveTimer = 180f;
		}
		if (currentMission == 24)
		{
			spawnEnemiesMaxCurrent = 3;
		}
		SetMissionStatus(currentMission);
		yield return new WaitForSeconds(timeToFirstEnemies);
		missionEnemiesKilled = 0;
		switch (currentMission)
		{
		case 12:
		{
			ArrayList waves2 = new ArrayList
			{
				new MissionConfiguration(5, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, Language.GetTxt("WAVE") + " 1", Language.GetTxt("WAVE") + " 1 " + Language.GetTxt("CLEAR"), 0.5f),
				new MissionConfiguration(5, 0, 0, 0, 0, 0, 1, 0, 0, 2, 2, 2, Language.GetTxt("WAVE") + " 2", Language.GetTxt("WAVE") + " 2 " + Language.GetTxt("CLEAR"), 0.6f),
				new MissionConfiguration(5, 0, 0, 0, 0, 0, 1, 0, 0, 3, 2, 2, Language.GetTxt("WAVE") + " 3", Language.GetTxt("WAVE") + " 3 " + Language.GetTxt("CLEAR"), 0.7f),
				new MissionConfiguration(5, 0, 0, 0, 0, 0, 1, 0, 0, 3, 4, 3, Language.GetTxt("WAVE") + " 4", Language.GetTxt("WAVE") + " 4 " + Language.GetTxt("CLEAR"), 0.8f),
				new MissionConfiguration(5, 0, 0, 0, 0, 0, 2, 0, 0, 4, 3, 3, Language.GetTxt("WAVE") + " 5", Language.GetTxt("WAVE") + " 5 " + Language.GetTxt("CLEAR"), 0.9f)
			};
			int bodyCount3 = 0;
			for (waveIndex = 0; waveIndex < waves2.Count; waveIndex++)
			{
				bodyCount3 += missionEnemiesKilled;
				missionEnemiesKilled = 0;
				waveText.text = ((MissionConfiguration)waves2[waveIndex]).message;
				AManager.difficultyLevel = ((MissionConfiguration)waves2[waveIndex]).difficultyLevel;
				yield return StartCoroutine(ShowWaveText());
				StartCoroutine("SpawnEnemiesMercenary", (MissionConfiguration)waves2[waveIndex]);
				SetMissionStatus(currentMission);
				while (((MissionConfiguration)waves2[waveIndex]).getTotalEnemyCount() > missionEnemiesKilled)
				{
					yield return new WaitForSeconds(0.1f);
				}
				StopCoroutine("SpawnEnemiesMercenary");
				waveText.text = ((MissionConfiguration)waves2[waveIndex]).endMessage;
				yield return StartCoroutine(ShowWaveText());
				spawnedCuchilloCount = 0;
				spawnedHanzoEnemies = 0;
				spawnedIsabeleEnemies = 0;
				spawnedMombasaCount = 0;
				spawnedNikolaiCount = 0;
				spawnedNolandEnemies = 0;
				spawnedRoyceCount = 0;
				spawnedSoldierRifleEnemies = 0;
				spawnedStansCount = 0;
				spawnedSniperCount = 0;
				spawnedSoldierMacheteEnemies = 0;
			}
			missionEnemiesKilled = bodyCount3;
			SetMissionSuccessStats();
			yield break;
		}
		case 22:
		{
			ArrayList waves = new ArrayList
			{
				new MissionConfiguration(5, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 2, Language.GetTxt("WAVE") + " 1", Language.GetTxt("WAVE") + " 1 " + Language.GetTxt("CLEAR"), 0.5f),
				new MissionConfiguration(5, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 2, Language.GetTxt("WAVE") + " 2", Language.GetTxt("WAVE") + " 2 " + Language.GetTxt("CLEAR"), 0.6f),
				new MissionConfiguration(5, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 3, Language.GetTxt("WAVE") + " 3", Language.GetTxt("WAVE") + " 3 " + Language.GetTxt("CLEAR"), 0.7f),
				new MissionConfiguration(5, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 4, Language.GetTxt("WAVE") + " 4", Language.GetTxt("WAVE") + " 4 " + Language.GetTxt("CLEAR"), 0.8f),
				new MissionConfiguration(5, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 5, Language.GetTxt("WAVE") + " 5", Language.GetTxt("WAVE") + " 5 " + Language.GetTxt("CLEAR"), 0.9f)
			};
			int bodyCount = 0;
			for (waveIndex = 0; waveIndex < waves.Count; waveIndex++)
			{
				bodyCount += missionEnemiesKilled;
				missionEnemiesKilled = 0;
				waveText.text = ((MissionConfiguration)waves[waveIndex]).message;
				AManager.difficultyLevel = ((MissionConfiguration)waves[waveIndex]).difficultyLevel;
				yield return StartCoroutine(ShowWaveText());
				StartCoroutine("SpawnEnemiesMercenary", (MissionConfiguration)waves[waveIndex]);
				SetMissionStatus(currentMission);
				while (((MissionConfiguration)waves[waveIndex]).getTotalEnemyCount() > missionEnemiesKilled)
				{
					yield return new WaitForSeconds(0.3f);
				}
				StopCoroutine("SpawnEnemiesMercenary");
				waveText.text = ((MissionConfiguration)waves[waveIndex]).endMessage;
				yield return StartCoroutine(ShowWaveText());
				spawnedCuchilloCount = 0;
				spawnedHanzoEnemies = 0;
				spawnedIsabeleEnemies = 0;
				spawnedMombasaCount = 0;
				spawnedNikolaiCount = 0;
				spawnedNolandEnemies = 0;
				spawnedRoyceCount = 0;
				spawnedSoldierRifleEnemies = 0;
				spawnedStansCount = 0;
				spawnedSniperCount = 0;
				spawnedSoldierMacheteEnemies = 0;
			}
			missionEnemiesKilled = bodyCount;
			SetMissionSuccessStats();
			yield break;
		}
		case 32:
			SetMissionStatus(currentMission);
			waveText.text = ((MissionConfiguration)missions[currentMission]).message;
			AManager.difficultyLevel = ((MissionConfiguration)missions[currentMission]).difficultyLevel;
			if (playerController.MaxLevelUnlocked >= 10)
			{
				((MissionConfiguration)missions[currentMission]).stansCount = 50000;
			}
			if (playerController.MaxLevelUnlocked >= 13)
			{
				((MissionConfiguration)missions[currentMission]).royceCount = 50000;
			}
			if (playerController.MaxLevelUnlocked >= 17)
			{
				((MissionConfiguration)missions[currentMission]).hanzoCount = 50000;
			}
			if (playerController.MaxLevelUnlocked >= 18)
			{
				((MissionConfiguration)missions[currentMission]).isabeleCount = 50000;
			}
			if (playerController.MaxLevelUnlocked >= 19)
			{
				((MissionConfiguration)missions[currentMission]).mombasaCount = 50000;
			}
			if (playerController.MaxLevelUnlocked >= 20)
			{
				((MissionConfiguration)missions[currentMission]).cuchilloCount = 50000;
			}
			if (playerController.MaxLevelUnlocked >= 21)
			{
				((MissionConfiguration)missions[currentMission]).nikolaiCount = 50000;
			}
			if (playerController.MaxLevelUnlocked >= 23)
			{
				((MissionConfiguration)missions[currentMission]).nolanCount = 50000;
			}
			StartCoroutine("SpawnEnemiesMercenary", (MissionConfiguration)missions[currentMission]);
			yield break;
		case 33:
		{
			int bodyCount2 = 0;
			int currentTotalEnemiesToSpawn2 = 0;
			MissionConfiguration currentWave = new MissionConfiguration(5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, Language.GetTxt("WAVE"), Language.GetTxt("WAVE") + Language.GetTxt("CLEAR"), 1f);
			for (waveIndex = 1; waveIndex < 1000000; waveIndex++)
			{
				bodyCount2 += missionEnemiesKilled;
				missionEnemiesKilled = 0;
				currentSpawnedMinionsAndBoss = 0;
				waveText.text = Language.GetTxt("WAVE") + " " + waveIndex;
				AManager.difficultyLevel = Mathf.Clamp(0.5f + 0.05f * (float)waveIndex, 0f, 6f);
				yield return StartCoroutine(ShowWaveText());
				bool noHumans = playerController.MaxLevelUnlocked >= 27 && Random.value <= 0.3f;
				if (noHumans)
				{
					currentWave.dogCount = Random.Range(3, 5) + (int)((float)(Random.Range(3, 5) * waveIndex) / 3f);
					currentWave.hanzoCount = 0;
					currentWave.royceCount = 0;
					currentWave.nikolaiCount = 0;
					currentWave.mombasaCount = 0;
					currentWave.stansCount = 0;
					currentWave.cuchilloCount = 0;
					currentWave.nolanCount = 0;
					currentWave.isabeleCount = 0;
					currentWave.soldierMacheteCount = 0;
					currentWave.soldierRifleCount = 0;
					currentWave.sniperCount = 0;
					currentWave.falconerCount = 0;
					currentWave.trackerCount = 0;
				}
				else
				{
					currentWave.hanzoCount = Random.Range(0, 2) + (int)((float)(Random.Range(0, 2) * waveIndex) / 3f);
					currentWave.royceCount = Random.Range(0, 2) + (int)((float)(Random.Range(0, 2) * waveIndex) / 3f);
					currentWave.nikolaiCount = Random.Range(0, 2) + (int)((float)(Random.Range(0, 2) * waveIndex) / 3f);
					currentWave.mombasaCount = Random.Range(0, 2) + (int)((float)(Random.Range(0, 2) * waveIndex) / 3f);
					currentWave.stansCount = Random.Range(0, 2) + (int)((float)(Random.Range(0, 2) * waveIndex) / 3f);
					currentWave.cuchilloCount = Random.Range(0, 2) + (int)((float)(Random.Range(0, 2) * waveIndex) / 3f);
					currentWave.nolanCount = Random.Range(0, 2) + (int)((float)(Random.Range(0, 2) * waveIndex) / 3f);
					currentWave.isabeleCount = Random.Range(0, 2) + (int)((float)(Random.Range(0, 2) * waveIndex) / 3f);
					currentWave.soldierMacheteCount = Random.Range(1, 4) + (int)((float)(Random.Range(1, 4) * waveIndex) / 3f);
					currentWave.soldierRifleCount = Random.Range(1, 4) + (int)((float)(Random.Range(1, 4) * waveIndex) / 3f);
					currentWave.sniperCount = Random.Range(1, 4) + (int)((float)(Random.Range(1, 4) * waveIndex) / 3f);
					currentWave.dogCount = 0;
					currentWave.falconerCount = 0;
					currentWave.trackerCount = 0;
				}
				currentTotalEnemiesToSpawn2 = currentWave.getTotalEnemyCount();
				StartCoroutine("SpawnEnemiesMercenary", currentWave);
				SetMissionStatus(currentMission);
				while (currentTotalEnemiesToSpawn2 > missionEnemiesKilled)
				{
					yield return new WaitForSeconds(0.3f);
				}
				StopCoroutine("SpawnEnemiesMercenary");
				missionEnemiesKilled = 0;
				SpawnRandomBoss(noHumans);
				while (currentSpawnedMinionsAndBoss > missionEnemiesKilled)
				{
					yield return new WaitForSeconds(0.3f);
				}
				yield return new WaitForSeconds(2f);
				waveText.text = Language.GetTxt("WAVE") + " " + waveIndex + " " + Language.GetTxt("CLEAR");
				yield return StartCoroutine(ShowWaveText());
				spawnedCuchilloCount = 0;
				spawnedHanzoEnemies = 0;
				spawnedIsabeleEnemies = 0;
				spawnedMombasaCount = 0;
				spawnedNikolaiCount = 0;
				spawnedNolandEnemies = 0;
				spawnedRoyceCount = 0;
				spawnedSoldierRifleEnemies = 0;
				spawnedStansCount = 0;
				spawnedSniperCount = 0;
				spawnedSoldierMacheteEnemies = 0;
				spawnedDogCount = 0;
				spawnedFalconerCount = 0;
				spawnedTrackerCount = 0;
			}
			missionEnemiesKilled = bodyCount2;
			SetMissionSuccessStats();
			yield break;
		}
		}
		SetMissionStatus(currentMission);
		AManager.instance.MaximumMeleesEngaged = 1;
		if (currentMission == 7 || currentMission == 20 || currentMission == 30 || currentMission == 24)
		{
			StartCoroutine(StartWaveTimer(180f));
		}
		if (currentMission == 24)
		{
			AManager.instance.MaximumMeleesEngaged = 3;
		}
		else if (currentMission == 25)
		{
			AManager.instance.MaximumMeleesEngaged = 2;
		}
		waveText.text = ((MissionConfiguration)missions[currentMission]).message;
		AManager.difficultyLevel = ((MissionConfiguration)missions[currentMission]).difficultyLevel;
		StartCoroutine("SpawnEnemiesMercenary", (MissionConfiguration)missions[currentMission]);
		missionCompleted = EndMissionCondition(currentMission);
		if (currentMission == 20)
		{
			while (!EndMissionCondition(currentMission))
			{
				yield return new WaitForSeconds(0.2f);
			}
		}
		else
		{
			while (!missionCompleted)
			{
				yield return new WaitForSeconds(0.2f);
			}
		}
		StopCoroutine("SpawnEnemiesMercenary");
		SetMissionSuccessStats();
	}

	private void SetMissionSuccessStats()
	{
		int @int = EncryptedPlayerPrefs.GetInt("PR_CurrentSlot");
		waveText.text = string.Empty;
		waveText.text = Language.GetTxt("MISSION_COMPLETE");
		TextMesh textMesh = waveText;
		string text = textMesh.text;
		textMesh.text = text + "\n" + Language.GetTxt("HONOR_POINTS_GAINED") + " " + honorPointsOnCurrentMission;
		TextMesh textMesh2 = waveText;
		text = textMesh2.text;
		textMesh2.text = text + "\n" + Language.GetTxt("HONOR_POINTS_TOTAL") + " " + (EncryptedPlayerPrefs.GetInt("PR_HonorPoints_S" + @int, 0) + honorPointsOnCurrentMission);
		TextMesh textMesh3 = waveText;
		text = textMesh3.text;
		textMesh3.text = text + "\n" + Language.GetTxt("TROPHY_KILLS") + " " + EnemiesTrophyCount();
		TextMesh textMesh4 = waveText;
		text = textMesh4.text;
		textMesh4.text = text + "\n" + Language.GetTxt("TOTAL_KILLS") + " " + missionEnemiesKilled;
		SetAllEnemiesToRelocate();
		AManager.instance.CinematicInProgress = true;
		playerController.setMissionSuccessToTrue(waveText.text);
		int int2 = EncryptedPlayerPrefs.GetInt("PR_LastMissionUnlocked_S" + @int);
		EncryptedPlayerPrefs.SetInt("PR_LastMissionUnlocked_S" + @int, Mathf.Max(currentMission, int2));
	}

	private void SetAllEnemiesToRelocate()
	{
		for (int i = 0; i < poolCuchilloEnemies.Count; i++)
		{
			if (!availableCuchillo[i])
			{
				((Transform)poolCuchilloEnemies[i]).SendMessage("RelocateStopOtherCoroutines", SendMessageOptions.DontRequireReceiver);
			}
		}
		for (int i = 0; i < poolDogEnemies.Count; i++)
		{
			if (!availableDogs[i])
			{
				((Transform)poolDogEnemies[i]).SendMessage("RelocateStopOtherCoroutines", SendMessageOptions.DontRequireReceiver);
			}
		}
		for (int i = 0; i < poolSniperEnemies.Count; i++)
		{
			if (!availableSnipers[i])
			{
				((Transform)poolSniperEnemies[i]).SendMessage("RelocateStopOtherCoroutines", SendMessageOptions.DontRequireReceiver);
			}
		}
		for (int i = 0; i < poolHanzoEnemies.Count; i++)
		{
			if (!availableHanzo[i])
			{
				((Transform)poolHanzoEnemies[i]).SendMessage("RelocateStopOtherCoroutines", SendMessageOptions.DontRequireReceiver);
			}
		}
		for (int i = 0; i < poolIsabeleEnemies.Count; i++)
		{
			if (!availableIsabele[i])
			{
				((Transform)poolIsabeleEnemies[i]).SendMessage("RelocateStopOtherCoroutines", SendMessageOptions.DontRequireReceiver);
			}
		}
		for (int i = 0; i < poolMombasaEnemies.Count; i++)
		{
			if (!availableMombasa[i])
			{
				((Transform)poolMombasaEnemies[i]).SendMessage("RelocateStopOtherCoroutines", SendMessageOptions.DontRequireReceiver);
			}
		}
		for (int i = 0; i < poolNikolaiEnemies.Count; i++)
		{
			if (!availableNikolai[i])
			{
				((Transform)poolNikolaiEnemies[i]).SendMessage("RelocateStopOtherCoroutines", SendMessageOptions.DontRequireReceiver);
			}
		}
		for (int i = 0; i < poolNolandEnemies.Count; i++)
		{
			if (!availableNolands[i])
			{
				((Transform)poolNolandEnemies[i]).SendMessage("RelocateStopOtherCoroutines", SendMessageOptions.DontRequireReceiver);
			}
		}
		for (int i = 0; i < poolRoyceEnemies.Count; i++)
		{
			if (!availableRoyce[i])
			{
				((Transform)poolRoyceEnemies[i]).SendMessage("RelocateStopOtherCoroutines", SendMessageOptions.DontRequireReceiver);
			}
		}
		for (int i = 0; i < poolSoldierRifleEnemies.Count; i++)
		{
			if (!availableSoldiersRifle[i])
			{
				((Transform)poolSoldierRifleEnemies[i]).SendMessage("RelocateStopOtherCoroutines", SendMessageOptions.DontRequireReceiver);
			}
		}
		for (int i = 0; i < poolSoldierMacheteEnemies.Count; i++)
		{
			if (!availableSoldiersMachete[i])
			{
				((Transform)poolSoldierMacheteEnemies[i]).SendMessage("RelocateStopOtherCoroutines", SendMessageOptions.DontRequireReceiver);
			}
		}
		for (int i = 0; i < poolStansEnemies.Count; i++)
		{
			if (!availableStans[i])
			{
				((Transform)poolStansEnemies[i]).SendMessage("RelocateStopOtherCoroutines", SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public void SavePlayerStats()
	{
		string key = "PR_NikolaiKills_S" + currentSlot;
		EncryptedPlayerPrefs.SetInt(key, EncryptedPlayerPrefs.GetInt(key, 0) + nikolaiKilledCount);
		key = "PR_IsabeleKills_S" + currentSlot;
		EncryptedPlayerPrefs.SetInt(key, EncryptedPlayerPrefs.GetInt(key, 0) + isabeleKilledCount);
		key = "PR_RoyceKills_S" + currentSlot;
		EncryptedPlayerPrefs.SetInt(key, EncryptedPlayerPrefs.GetInt(key, 0) + royceKilledCount);
		key = "PR_CuchilloKills_S" + currentSlot;
		EncryptedPlayerPrefs.SetInt(key, EncryptedPlayerPrefs.GetInt(key, 0) + cuchilloKilledCount);
		key = "PR_HanzoKills_S" + currentSlot;
		EncryptedPlayerPrefs.SetInt(key, EncryptedPlayerPrefs.GetInt(key, 0) + hanzoKilledCount);
		key = "PR_StansKills_S" + currentSlot;
		EncryptedPlayerPrefs.SetInt(key, EncryptedPlayerPrefs.GetInt(key, 0) + stansKilledCount);
		key = "PR_NolanKills_S" + currentSlot;
		EncryptedPlayerPrefs.SetInt(key, EncryptedPlayerPrefs.GetInt(key, 0) + nolandKilledCount);
		key = "PR_SoldierKills_S" + currentSlot;
		EncryptedPlayerPrefs.SetInt(key, EncryptedPlayerPrefs.GetInt(key, 0) + soldierRifleKilledCount + soldierMacheteKilledCount + sniperKilledCount);
		key = "PR_MombasaKills_S" + currentSlot;
		EncryptedPlayerPrefs.SetInt(key, EncryptedPlayerPrefs.GetInt(key, 0) + mombasaKilledCount);
		key = "PR_BlackPredatorKills_S" + currentSlot;
		EncryptedPlayerPrefs.SetInt(key, EncryptedPlayerPrefs.GetInt(key, 0) + blackPredatorKilledCount);
		key = "PR_DogKills_S" + currentSlot;
		int @int = EncryptedPlayerPrefs.GetInt(key, 0);
		EncryptedPlayerPrefs.SetInt(key, @int + dogKilledCount);
		if (@int + dogKilledCount < 50 || !playerController.liteVersion)
		{
		}
		key = "PR_BerserkerKills_S" + currentSlot;
		EncryptedPlayerPrefs.SetInt(key, EncryptedPlayerPrefs.GetInt(key, 0) + berserkerTrophyCount);
		string key2 = "PR_NikolaiTrophies_S" + currentSlot;
		EncryptedPlayerPrefs.SetInt(key2, EncryptedPlayerPrefs.GetInt(key2, 0) + nikolaiTrophyCount);
		key2 = "PR_IsabeleTrophies_S" + currentSlot;
		EncryptedPlayerPrefs.SetInt(key2, EncryptedPlayerPrefs.GetInt(key2, 0) + isabeleTrophyCount);
		key2 = "PR_RoyceTrophies_S" + currentSlot;
		EncryptedPlayerPrefs.SetInt(key2, EncryptedPlayerPrefs.GetInt(key2, 0) + royceTrophyCount);
		key2 = "PR_BlackPredatorTrophies_S" + currentSlot;
		EncryptedPlayerPrefs.SetInt(key2, EncryptedPlayerPrefs.GetInt(key2, 0) + blackPredatorTrophyCount);
		key2 = "PR_DogTrophies_S" + currentSlot;
		EncryptedPlayerPrefs.SetInt(key2, EncryptedPlayerPrefs.GetInt(key2, 0) + dogTrophyCount);
		key2 = "PR_BerserkerTrophies_S" + currentSlot;
		EncryptedPlayerPrefs.SetInt(key2, EncryptedPlayerPrefs.GetInt(key2, 0) + berserkerTrophyCount);
		key2 = "PR_CuchilloTrophies_S" + currentSlot;
		EncryptedPlayerPrefs.SetInt(key2, EncryptedPlayerPrefs.GetInt(key2, 0) + cuchilloTrophyCount);
		key2 = "PR_HanzoTrophies_S" + currentSlot;
		EncryptedPlayerPrefs.SetInt(key2, EncryptedPlayerPrefs.GetInt(key2, 0) + hanzoTrophyCount);
		key2 = "PR_StansTrophies_S" + currentSlot;
		EncryptedPlayerPrefs.SetInt(key2, EncryptedPlayerPrefs.GetInt(key2, 0) + stansTrophyCount);
		key2 = "PR_NolanTrophies_S" + currentSlot;
		EncryptedPlayerPrefs.SetInt(key2, EncryptedPlayerPrefs.GetInt(key2, 0) + nolanTrophyCount);
		key2 = "PR_SoldierTrophies_S" + currentSlot;
		EncryptedPlayerPrefs.SetInt(key2, EncryptedPlayerPrefs.GetInt(key2, 0) + soldierTrophyCount);
		key2 = "PR_MombasaTrophies_S" + currentSlot;
		EncryptedPlayerPrefs.SetInt(key2, EncryptedPlayerPrefs.GetInt(key2, 0) + mombasaTrophyCount);
		int int2 = EncryptedPlayerPrefs.GetInt("PR_HonorPointsTotalEver_S" + currentSlot, 0);
		if (!playerController.liteVersion)
		{
			EncryptedPlayerPrefs.SetInt("PR_HonorPointsTotalEver_S" + currentSlot, int2 + honorPointsOnCurrentMission);
		}
		EncryptedPlayerPrefs.SetInt("PR_HonorPoints_S" + currentSlot, EncryptedPlayerPrefs.GetInt("PR_HonorPoints_S" + currentSlot, 0) + honorPointsOnCurrentMission);
		EncryptedPlayerPrefs.SetInt("PR_StealthKills_S" + currentSlot, EncryptedPlayerPrefs.GetInt("PR_StealthKills_S" + currentSlot, 0) + StealthKills);
		EncryptedPlayerPrefs.SetInt("PR_OponentsImpaled_S" + currentSlot, EncryptedPlayerPrefs.GetInt("PR_OponentsImpaled_S" + currentSlot, 0) + OpponentsImpaled);
		EncryptedPlayerPrefs.SetInt("PR_BodiesSplit_S" + currentSlot, EncryptedPlayerPrefs.GetInt("PR_BodiesSplit_S" + currentSlot, 0) + BodiesSplit);
		EncryptedPlayerPrefs.SetInt("PR_NetGunCaptures_S" + currentSlot, EncryptedPlayerPrefs.GetInt("PR_NetGunCaptures_S" + currentSlot, 0) + NetGunKilledCount);
		int num = EncryptedPlayerPrefs.GetInt("PR_TotalKillsWristblade_S" + currentSlot, 0) + killsWristBlade;
		EncryptedPlayerPrefs.SetInt("PR_TotalKillsWristblade_S" + currentSlot, num);
		int num2 = EncryptedPlayerPrefs.GetInt("PR_TotalKillsWhip_S" + currentSlot, 0) + killsWhip;
		EncryptedPlayerPrefs.SetInt("PR_TotalKillsWhip_S" + currentSlot, num2);
		if (num2 < 100 || !playerController.liteVersion)
		{
		}
		if (num > 1000)
		{
			if (playerController.liteVersion)
			{
			}
		}
		else if (num > 100 && playerController.liteVersion)
		{
		}
		num = EncryptedPlayerPrefs.GetInt("PR_TotalKillsPlasmagun_S" + currentSlot, 0) + killsPlasmaGun;
		EncryptedPlayerPrefs.SetInt("PR_TotalKillsPlasmagun_S" + currentSlot, num);
		if (num <= 100 || !playerController.liteVersion)
		{
		}
		EncryptedPlayerPrefs.SetInt("PR_VerticalSplits_S" + currentSlot, verticalSplitKillsDone);
		num = EncryptedPlayerPrefs.GetInt("PR_TotalKillsCombistick_S" + currentSlot, 0) + killsCombiStick;
		EncryptedPlayerPrefs.SetInt("PR_TotalKillsCombistick_S" + currentSlot, num);
		if (num <= 100 || !playerController.liteVersion)
		{
		}
		if (MaxDiscCombo > EncryptedPlayerPrefs.GetInt("PR_MaxDiskCombo_S" + currentSlot, 0))
		{
			EncryptedPlayerPrefs.SetInt("PR_MaxDiskCombo_S" + currentSlot, MaxDiscCombo);
		}
	}
}
