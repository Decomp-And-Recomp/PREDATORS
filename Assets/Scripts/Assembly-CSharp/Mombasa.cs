public class Mombasa : SoldierAutoRifle
{
	protected override void EnemyDiedCleanup(int index, DeathType deathType)
	{
		RemoveSelfFromArray();
		survivalMissionController.EnemyMombasaDied(index, deathType);
	}
}
