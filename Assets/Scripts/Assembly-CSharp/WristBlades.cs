using UnityEngine;

public class WristBlades : GearOnTheWall
{
	public WristBlades(Transform aObjectTouchedTransform, TextMesh aUpgradeMesh, TextMesh aDamageTextMesh, TextMesh aPriceTextMesh, TextMesh aNameTextMesh, TextMesh aWeaponDescriptionMesh, TextMesh aWeaponLevelMesh, TextMesh aHPMesh, string aName, int aLevel, string aPrefName, int[] aDamage, int[] aPrice)
	{
		damage = aDamage;
		price = aPrice;
		priceMesh = aPriceTextMesh;
		damageMesh = aDamageTextMesh;
		nameMesh = aNameTextMesh;
		levelMesh = aWeaponLevelMesh;
		descriptionMesh = aWeaponDescriptionMesh;
		objecTouchedTransform = aObjectTouchedTransform;
		honorPointsMesh = aHPMesh;
		upgradeOrEquipMeshWithCollider = aUpgradeMesh;
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
		return level < GameConstants.WRIST_DAMAGE.Length - 1;
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
		nameMesh.text = Language.GetTxt("WRIST_BLADES_MULTILINE");
		descriptionMesh.text = Language.GetTxt("WRIST_BLADES_DESCRIPTION", 58);
		levelMesh.text = Language.GetTxt("LEVEL") + " " + level;
		priceMesh.text = string.Empty + Price;
		honorPointsMesh.text = string.Empty + MissionSelect.availableHonorPoints;
		upgradeOrEquipMeshWithCollider.text = string.Empty + Language.GetTxt("UPGRADE");
		damageMesh.text = Language.GetTxt("DAMAGE");
		GearOnTheWall.updateRow1(GameConstants.WRIST_DAMAGE[base.Level], GameConstants.WRIST_DAMAGE[base.NextLevel]);
		if (base.Level == 0)
		{
			damageMesh.gameObject.SetActiveRecursively(false);
			priceMesh.gameObject.SetActiveRecursively(false);
			priceMesh.gameObject.SetActiveRecursively(false);
			levelMesh.gameObject.SetActiveRecursively(false);
			objecTouchedTransform.GetComponent<Renderer>().material.color = GearOnTheWall.weaponColorLocked;
		}
		if (!canBeUpgraded())
		{
			upgradeOrEquipMeshWithCollider.gameObject.SetActiveRecursively(false);
			priceMesh.gameObject.SetActiveRecursively(false);
		}
	}
}
