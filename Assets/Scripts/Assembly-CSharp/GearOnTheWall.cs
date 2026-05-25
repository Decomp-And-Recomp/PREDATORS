using UnityEngine;

public class GearOnTheWall
{
	public static Transform[] row1Values;

	public static Transform[] row2Values;

	public static Transform[] row3Values;

	public static Transform[] row1ValuesEmpty;

	public static Transform[] row2ValuesEmpty;

	public static Transform[] row3ValuesEmpty;

	public static Color weaponColorLocked;

	public static Color weaponColorUnlocked;

	protected string prefName;

	protected int level;

	protected int[] damage;

	protected int[] range;

	protected int[] speed;

	protected int[] price;

	protected string weaponName;

	protected TextMesh upgradeOrEquipMeshWithCollider;

	protected TextMesh priceMesh;

	protected TextMesh damageMesh;

	protected TextMesh rangeMesh;

	protected TextMesh speedMesh;

	protected TextMesh honorPointsMesh;

	protected TextMesh nameMesh;

	protected TextMesh descriptionMesh;

	protected TextMesh levelMesh;

	protected Transform objecTouchedTransform;

	protected string description;

	public Transform GearTransform
	{
		get
		{
			return objecTouchedTransform;
		}
	}

	public virtual int Price
	{
		get
		{
			return price[Mathf.Clamp(level, 0, price.Length - 1)];
		}
	}

	public string Name
	{
		get
		{
			return weaponName;
		}
		set
		{
			weaponName = value;
		}
	}

	public int Damage
	{
		get
		{
			return damage[Mathf.Clamp(level, 0, price.Length - 1)];
		}
	}

	public int Range
	{
		get
		{
			return range[Mathf.Clamp(level, 0, price.Length - 1)];
		}
	}

	public int Speed
	{
		get
		{
			return speed[Mathf.Clamp(level, 0, price.Length - 1)];
		}
	}

	public TextMesh PriceMesh
	{
		get
		{
			return priceMesh;
		}
	}

	public TextMesh DamageMesh
	{
		get
		{
			return damageMesh;
		}
	}

	public TextMesh RangeMesh
	{
		get
		{
			return rangeMesh;
		}
	}

	public TextMesh SpeedMesh
	{
		get
		{
			return speedMesh;
		}
	}

	public int NextLevel
	{
		get
		{
			return Mathf.Clamp(Level + 1, 0, GameConstants.WRIST_DAMAGE.Length - 1);
		}
	}

	public TextMesh ButtonTransform
	{
		get
		{
			return upgradeOrEquipMeshWithCollider;
		}
	}

	public int Level
	{
		get
		{
			return level;
		}
		set
		{
			level = Mathf.Clamp(value, 0, price.Length);
		}
	}

	public GearOnTheWall(string aDescription, Transform aObjectTouchedTransform, TextMesh aButtonTransform, TextMesh aDamageTextMesh, TextMesh aRangeTextMesh, TextMesh aSpeedTextMesh, TextMesh aPriceTextMesh, TextMesh aNameTextMesh, TextMesh aWeaponDescriptionMesh, TextMesh aWeaponLevelMesh, TextMesh aHonorPointsMesh, string aName, int aLevel, string aPrefName, int[] aDamage, int[] aRange, int[] aSpeed, int[] aPrice)
	{
		descriptionMesh = aWeaponDescriptionMesh;
		description = aDescription;
		damage = aDamage;
		range = aRange;
		speed = aSpeed;
		price = aPrice;
		objecTouchedTransform = aObjectTouchedTransform;
		priceMesh = aPriceTextMesh;
		damageMesh = aDamageTextMesh;
		rangeMesh = aRangeTextMesh;
		speedMesh = aSpeedTextMesh;
		nameMesh = aNameTextMesh;
		levelMesh = aWeaponLevelMesh;
		honorPointsMesh = aHonorPointsMesh;
		upgradeOrEquipMeshWithCollider = aButtonTransform;
		Level = aLevel;
		if (Level <= 0)
		{
			objecTouchedTransform.GetComponent<Renderer>().material.color = weaponColorLocked;
		}
		weaponName = aName;
		prefName = aPrefName;
	}

	public GearOnTheWall()
	{
	}

	public virtual void activateAndRefresh(Transform statsPanel, Transform weaponDescriptionLocation, Transform rot)
	{
		statsPanel.position = weaponDescriptionLocation.position;
		statsPanel.localScale = weaponDescriptionLocation.localScale;
		statsPanel.rotation = rot.rotation;
		statsPanel.gameObject.active = true;
		updateText();
	}

	protected static void updateRow1(int amount, int amountNextLevel)
	{
		for (int i = 0; i < row1Values.Length; i++)
		{
			row1Values[i].GetComponent<Renderer>().enabled = false;
			row1ValuesEmpty[i].GetComponent<Renderer>().enabled = false;
		}
		for (int j = amount; j < amountNextLevel; j++)
		{
			row1ValuesEmpty[j].GetComponent<Renderer>().enabled = true;
		}
		for (int k = 0; k < amount; k++)
		{
			row1Values[k].GetComponent<Renderer>().enabled = true;
		}
	}

	protected static void updateRow2(int amount, int amountNextLevel)
	{
		for (int i = 0; i < row2Values.Length; i++)
		{
			row2Values[i].GetComponent<Renderer>().enabled = false;
			row2ValuesEmpty[i].GetComponent<Renderer>().enabled = false;
		}
		for (int j = amount; j < amountNextLevel; j++)
		{
			row2ValuesEmpty[j].GetComponent<Renderer>().enabled = true;
		}
		for (int k = 0; k < amount; k++)
		{
			row2Values[k].GetComponent<Renderer>().enabled = true;
		}
	}

	protected static void updateRow3(int amount, int amountNextLevel)
	{
		for (int i = 0; i < row3Values.Length; i++)
		{
			row3Values[i].GetComponent<Renderer>().enabled = false;
			row3ValuesEmpty[i].GetComponent<Renderer>().enabled = false;
		}
		for (int j = amount; j < amountNextLevel; j++)
		{
			row3ValuesEmpty[j].GetComponent<Renderer>().enabled = true;
		}
		for (int k = 0; k < amount; k++)
		{
			row3Values[k].GetComponent<Renderer>().enabled = true;
		}
	}

	public virtual bool canBeUpgraded()
	{
		return level < damage.Length - 1;
	}

	public virtual void upgrade()
	{
		if (MissionSelect.availableHonorPoints >= Price)
		{
			MissionSelect.availableHonorPoints -= Price;
			Level++;
			EncryptedPlayerPrefs.SetInt(prefName, Level);
			EncryptedPlayerPrefs.SetInt("PR_HonorPoints_S" + EncryptedPlayerPrefs.GetInt("PR_CurrentSlot"), MissionSelect.availableHonorPoints);
		}
		updateText();
	}

	public static void printArray(string title, int[] array)
	{
		for (int i = 0; i < array.Length; i++)
		{
		}
	}

	public virtual void updateText()
	{
		upgradeOrEquipMeshWithCollider.gameObject.SetActiveRecursively(true);
		damageMesh.gameObject.SetActiveRecursively(true);
		rangeMesh.gameObject.SetActiveRecursively(true);
		speedMesh.gameObject.SetActiveRecursively(true);
		priceMesh.gameObject.SetActiveRecursively(true);
		levelMesh.gameObject.SetActiveRecursively(true);
		descriptionMesh.gameObject.SetActiveRecursively(true);
		nameMesh.gameObject.SetActiveRecursively(true);
		honorPointsMesh.gameObject.SetActiveRecursively(true);
		damageMesh.text = Language.GetTxt("DAMAGE");
		rangeMesh.text = Language.GetTxt("ENERGY");
		speedMesh.text = Language.GetTxt("SPEED");
		priceMesh.text = string.Empty + Price;
		nameMesh.text = Name;
		descriptionMesh.text = description;
		honorPointsMesh.text = string.Empty + MissionSelect.availableHonorPoints;
		levelMesh.text = Language.GetTxt("LEVEL") + " " + Level;
		upgradeOrEquipMeshWithCollider.text = Language.GetTxt("UPGRADE");
		updateRow1(damage[Level], damage[NextLevel]);
		updateRow2(range[Level], range[NextLevel]);
		updateRow3(speed[Level], speed[NextLevel]);
		if (Level == 0)
		{
			levelMesh.text = Language.GetTxt("LOCKED");
			upgradeOrEquipMeshWithCollider.gameObject.SetActiveRecursively(false);
			damageMesh.gameObject.SetActiveRecursively(false);
			rangeMesh.gameObject.SetActiveRecursively(false);
			speedMesh.gameObject.SetActiveRecursively(false);
			priceMesh.gameObject.SetActiveRecursively(false);
		}
		if (!canBeUpgraded())
		{
			upgradeOrEquipMeshWithCollider.gameObject.SetActiveRecursively(false);
			priceMesh.gameObject.SetActiveRecursively(false);
		}
	}
}
