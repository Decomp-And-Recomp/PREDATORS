public class AnimationInfo
{
	private EnemyType enemyType;

	private string animationName;

	public EnemyType Enemy_Type
	{
		get
		{
			return enemyType;
		}
		set
		{
			enemyType = value;
		}
	}

	public string AnimationName
	{
		get
		{
			return animationName;
		}
		set
		{
			animationName = value;
		}
	}

	public AnimationInfo(EnemyType aEnemyType, string aAnimationName)
	{
		enemyType = aEnemyType;
		animationName = aAnimationName;
	}

	public AnimationInfo(string aAnimationName)
	{
		animationName = aAnimationName;
	}
}
