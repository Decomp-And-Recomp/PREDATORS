using UnityEngine;

public class MissionSquare
{
	protected Collider collider;

	private int missionNumber;

	private Transform unlockedNotSelected;

	private Transform unlockedSelected;

	private Transform lockedSelected;

	private Transform lockedNotSelected;

	private TextMesh number;

	public virtual Collider SquareCollider
	{
		get
		{
			return collider;
		}
	}

	public virtual GameObject gameObject
	{
		get
		{
			return collider.gameObject;
		}
	}

	public virtual Transform transform
	{
		get
		{
			return collider.transform;
		}
	}

	public MissionSquare(Collider aCollider, int aMissionNumber)
	{
		collider = aCollider;
		missionNumber = aMissionNumber;
		for (int i = 0; i < 5; i++)
		{
			Transform child = collider.transform.GetChild(i);
			if (child.name == "ulns")
			{
				unlockedNotSelected = child;
			}
			else if (child.name == "uls")
			{
				unlockedSelected = child;
			}
			else if (child.name == "lns")
			{
				lockedNotSelected = child;
			}
			else if (child.name == "ls")
			{
				lockedSelected = child;
			}
			else if (child.name == "number")
			{
				number = (TextMesh)child.GetComponent(typeof(TextMesh));
				number.text = string.Empty + missionNumber;
			}
		}
	}

	public MissionSquare()
	{
	}

	public MissionSquare(Collider aCollider)
	{
		collider = aCollider;
		for (int i = 0; i < collider.transform.childCount; i++)
		{
			Transform child = collider.transform.GetChild(i);
			if (child.name == "ulns")
			{
				unlockedNotSelected = child;
			}
			else if (child.name == "uls")
			{
				unlockedSelected = child;
			}
			else if (child.name == "lns")
			{
				lockedNotSelected = child;
			}
			else if (child.name == "ls")
			{
				lockedSelected = child;
			}
		}
	}

	public MissionSquare(Collider aCollider, int aMissionNumber, Transform aUnlockedNotSelected, Transform aUnlockedSelected, Transform aLockedNotSelected, Transform aLockedSelected)
	{
		collider = aCollider;
		missionNumber = aMissionNumber;
		unlockedNotSelected = aUnlockedNotSelected;
		unlockedSelected = aUnlockedSelected;
		lockedNotSelected = aLockedNotSelected;
		lockedSelected = aLockedSelected;
	}

	public void SetUnlockedNotSelected()
	{
		unlockedSelected.gameObject.SetActiveRecursively(false);
		unlockedNotSelected.gameObject.SetActiveRecursively(true);
		lockedSelected.gameObject.SetActiveRecursively(false);
		lockedNotSelected.gameObject.SetActiveRecursively(false);
		if ((bool)number)
		{
			number.gameObject.SetActiveRecursively(true);
		}
	}

	public void SetUnlockedSelected()
	{
		unlockedSelected.gameObject.SetActiveRecursively(true);
		unlockedNotSelected.gameObject.SetActiveRecursively(false);
		lockedSelected.gameObject.SetActiveRecursively(false);
		lockedNotSelected.gameObject.SetActiveRecursively(false);
		if ((bool)number)
		{
			number.gameObject.SetActiveRecursively(true);
		}
	}

	public void SetLockedNotSelected()
	{
		unlockedSelected.gameObject.SetActiveRecursively(false);
		unlockedNotSelected.gameObject.SetActiveRecursively(false);
		lockedSelected.gameObject.SetActiveRecursively(false);
		lockedNotSelected.gameObject.SetActiveRecursively(true);
		if ((bool)number)
		{
			number.gameObject.SetActiveRecursively(false);
		}
	}

	public void SetLockedSelected()
	{
		unlockedSelected.gameObject.SetActiveRecursively(false);
		unlockedNotSelected.gameObject.SetActiveRecursively(false);
		lockedSelected.gameObject.SetActiveRecursively(true);
		lockedNotSelected.gameObject.SetActiveRecursively(false);
		if ((bool)number)
		{
			number.gameObject.SetActiveRecursively(true);
		}
	}
}
