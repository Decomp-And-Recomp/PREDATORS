public class MissionConfiguration
{
	public int maxEnemiesAtATime = 5;

	public int hanzoCount;

	public int isabeleCount;

	public int royceCount;

	public int nikolaiCount;

	public int mombasaCount;

	public int stansCount;

	public int nolanCount;

	public int soldierRifleCount;

	public int soldierMacheteCount;

	public int cuchilloCount;

	public int sniperCount;

	public int superBlackPredatorCount;

	public int dogCount;

	public int falconerCount;

	public int trackerCount;

	public string message;

	public string endMessage;

	public float difficultyLevel;

	public MissionConfiguration(int aMaxEnemiesAtATime, int aHanzoCount, int aIsabeleCount, int aRoyceCount, int aNikolaiCount, int aMombasaCount, int aStansCount, int aCuchilloCount, int aNolanCount, int aSoldierRifleCount, int aSoldierMacheteCount, int aSniperCount, string aMessage, string aEndMessage, float aDifficultyLevel)
	{
		maxEnemiesAtATime = aMaxEnemiesAtATime;
		hanzoCount = aHanzoCount;
		isabeleCount = aIsabeleCount;
		royceCount = aRoyceCount;
		nikolaiCount = aNikolaiCount;
		mombasaCount = aMombasaCount;
		stansCount = aStansCount;
		nolanCount = aNolanCount;
		soldierRifleCount = aSoldierRifleCount;
		soldierMacheteCount = aSoldierMacheteCount;
		cuchilloCount = aCuchilloCount;
		sniperCount = aSniperCount;
		message = aMessage;
		endMessage = aEndMessage;
		difficultyLevel = aDifficultyLevel;
	}

	public MissionConfiguration(int aMaxEnemiesAtATime, int aHanzoCount, int aIsabeleCount, int aRoyceCount, int aNikolaiCount, int aMombasaCount, int aStansCount, int aCuchilloCount, int aNolanCount, int aSoldierRifleCount, int aSoldierMacheteCount, int aSniperCount, int aBlackPredatorCount, string aMessage, string aEndMessage, float aDifficultyLevel)
	{
		maxEnemiesAtATime = aMaxEnemiesAtATime;
		hanzoCount = aHanzoCount;
		isabeleCount = aIsabeleCount;
		royceCount = aRoyceCount;
		nikolaiCount = aNikolaiCount;
		mombasaCount = aMombasaCount;
		stansCount = aStansCount;
		nolanCount = aNolanCount;
		soldierRifleCount = aSoldierRifleCount;
		soldierMacheteCount = aSoldierMacheteCount;
		cuchilloCount = aCuchilloCount;
		sniperCount = aSniperCount;
		superBlackPredatorCount = aBlackPredatorCount;
		message = aMessage;
		endMessage = aEndMessage;
		difficultyLevel = aDifficultyLevel;
	}

	public MissionConfiguration(int aMaxEnemiesAtATime, int aHanzoCount, int aIsabeleCount, int aRoyceCount, int aNikolaiCount, int aMombasaCount, int aStansCount, int aCuchilloCount, int aNolanCount, int aSoldierRifleCount, int aSoldierMacheteCount, int aSniperCount, int aBlackPredatorCount, int aDogCount, string aMessage, string aEndMessage, float aDifficultyLevel)
	{
		maxEnemiesAtATime = aMaxEnemiesAtATime;
		hanzoCount = aHanzoCount;
		isabeleCount = aIsabeleCount;
		royceCount = aRoyceCount;
		nikolaiCount = aNikolaiCount;
		mombasaCount = aMombasaCount;
		stansCount = aStansCount;
		nolanCount = aNolanCount;
		soldierRifleCount = aSoldierRifleCount;
		soldierMacheteCount = aSoldierMacheteCount;
		cuchilloCount = aCuchilloCount;
		sniperCount = aSniperCount;
		superBlackPredatorCount = aBlackPredatorCount;
		dogCount = aDogCount;
		message = aMessage;
		endMessage = aEndMessage;
		difficultyLevel = aDifficultyLevel;
	}

	public MissionConfiguration(int aMaxEnemiesAtATime, int aHanzoCount, int aIsabeleCount, int aRoyceCount, int aNikolaiCount, int aMombasaCount, int aStansCount, int aCuchilloCount, int aNolanCount, int aSoldierRifleCount, int aSoldierMacheteCount, int aSniperCount, int aBlackPredatorCount, int aDogCount, int aFalconerCount, int aTrackerCount, string aMessage, string aEndMessage, float aDifficultyLevel)
	{
		maxEnemiesAtATime = aMaxEnemiesAtATime;
		hanzoCount = aHanzoCount;
		isabeleCount = aIsabeleCount;
		royceCount = aRoyceCount;
		nikolaiCount = aNikolaiCount;
		mombasaCount = aMombasaCount;
		stansCount = aStansCount;
		nolanCount = aNolanCount;
		soldierRifleCount = aSoldierRifleCount;
		soldierMacheteCount = aSoldierMacheteCount;
		cuchilloCount = aCuchilloCount;
		sniperCount = aSniperCount;
		superBlackPredatorCount = aBlackPredatorCount;
		dogCount = aDogCount;
		falconerCount = aFalconerCount;
		trackerCount = aTrackerCount;
		message = aMessage;
		endMessage = aEndMessage;
		difficultyLevel = aDifficultyLevel;
	}

	public int getTotalEnemyCount()
	{
		return hanzoCount + isabeleCount + royceCount + mombasaCount + stansCount + nikolaiCount + cuchilloCount + soldierRifleCount + soldierMacheteCount + sniperCount + nolanCount + superBlackPredatorCount + dogCount + falconerCount + trackerCount;
	}
}
