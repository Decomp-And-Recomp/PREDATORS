using UnityEngine;

public class CombiStick : GearOnTheWall
{
	public CombiStick(string aDescription, Transform aObjectTouchedTransform, TextMesh aButtonTransform, TextMesh aDamageTextMesh, TextMesh aRangeTextMesh, TextMesh aPriceTextMesh, TextMesh aNameTextMesh, TextMesh aWeaponDescriptionMesh, TextMesh aWeaponLevelMesh, TextMesh aHonorPointsMesh, string aName, int aLevel, string aPrefName, int[] aDamage, int[] aRange, int[] aPrice)
	{
		description = aDescription;
		damage = aDamage;
		range = aRange;
		honorPointsMesh = aHonorPointsMesh;
		price = aPrice;
		objecTouchedTransform = aObjectTouchedTransform;
		priceMesh = aPriceTextMesh;
		damageMesh = aDamageTextMesh;
		rangeMesh = aRangeTextMesh;
		nameMesh = aNameTextMesh;
		levelMesh = aWeaponLevelMesh;
		descriptionMesh = aWeaponDescriptionMesh;
		upgradeOrEquipMeshWithCollider = aButtonTransform;
		base.Level = aLevel;
		if (base.Level <= 0)
		{
			objecTouchedTransform.GetComponent<Renderer>().material.color = GearOnTheWall.weaponColorLocked;
		}
		weaponName = aName;
		prefName = aPrefName;
	}

	public override bool canBeUpgraded()
	{
		return level < GameConstants.SPEAR_DAMAGE.Length - 1;
	}

	public override void updateText()
	{
		damageMesh.gameObject.SetActiveRecursively(true);
		upgradeOrEquipMeshWithCollider.gameObject.SetActiveRecursively(true);
		priceMesh.gameObject.SetActiveRecursively(true);
		priceMesh.gameObject.SetActiveRecursively(true);
		levelMesh.gameObject.SetActiveRecursively(true);
		honorPointsMesh.gameObject.SetActiveRecursively(true);
		nameMesh.gameObject.SetActiveRecursively(true);
		descriptionMesh.gameObject.SetActiveRecursively(true);
		rangeMesh.gameObject.SetActiveRecursively(true);
		nameMesh.text = base.Name;
		descriptionMesh.text = description;
		levelMesh.text = Language.GetTxt("LEVEL") + " " + level;
		priceMesh.text = string.Empty + Price;
		honorPointsMesh.text = string.Empty + MissionSelect.availableHonorPoints;
		upgradeOrEquipMeshWithCollider.text = string.Empty + Language.GetTxt("UPGRADE");
		damageMesh.text = Language.GetTxt("DAMAGE");
		rangeMesh.text = Language.GetTxt("RANGE");
		GearOnTheWall.updateRow1(damage[base.Level], damage[base.NextLevel]);
		GearOnTheWall.updateRow2(range[base.Level], range[base.NextLevel]);
		if (base.Level == 0)
		{
			levelMesh.text = Language.GetTxt("LOCKED");
			upgradeOrEquipMeshWithCollider.gameObject.SetActiveRecursively(false);
			damageMesh.gameObject.SetActiveRecursively(false);
			priceMesh.gameObject.SetActiveRecursively(false);
			rangeMesh.gameObject.SetActiveRecursively(false);
		}
		if (!canBeUpgraded())
		{
			upgradeOrEquipMeshWithCollider.gameObject.SetActiveRecursively(false);
			priceMesh.gameObject.SetActiveRecursively(false);
		}
	}
}
