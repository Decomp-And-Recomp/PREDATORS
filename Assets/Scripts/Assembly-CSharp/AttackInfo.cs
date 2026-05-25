using UnityEngine;

public class AttackInfo
{
	private float damage;

	private Vector3 attackerPosition;

	private int animationNr;

	private bool predatorAttack;

	public float Damage
	{
		get
		{
			return damage;
		}
		set
		{
			damage = value;
		}
	}

	public bool PredatorAttack
	{
		get
		{
			return predatorAttack;
		}
		set
		{
			predatorAttack = value;
		}
	}

	public Vector3 AttackerPosition
	{
		get
		{
			return attackerPosition;
		}
		set
		{
			attackerPosition = value;
		}
	}

	public int AnimationNr
	{
		get
		{
			return animationNr;
		}
		set
		{
			animationNr = value;
		}
	}

	public AttackInfo(float aDamage, Vector3 aAttackerPosition, int aAnimation, bool aPredatorAttack)
	{
		predatorAttack = aPredatorAttack;
		damage = aDamage;
		attackerPosition = aAttackerPosition;
		animationNr = aAnimation;
	}

	public AttackInfo(float aDamage, Vector3 aAttackerPosition, int aAnimation)
	{
		damage = aDamage;
		attackerPosition = aAttackerPosition;
		animationNr = aAnimation;
	}

	public AttackInfo(float aDamage)
	{
		damage = aDamage;
		attackerPosition = Vector3.zero;
		animationNr = 1;
	}

	public AttackInfo(float aDamage, bool aPredatorAttack)
	{
		predatorAttack = aPredatorAttack;
		damage = aDamage;
		attackerPosition = Vector3.zero;
		animationNr = 1;
	}
}
