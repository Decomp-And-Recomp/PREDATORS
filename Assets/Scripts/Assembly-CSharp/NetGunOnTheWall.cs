using UnityEngine;

public class NetGunOnTheWall : GearOnTheWall
{
	public NetGunOnTheWall(Transform aObjectTouchedTransform, TextMesh aButtonTransform, TextMesh aSpeedTextMesh, TextMesh aPriceTextMesh, TextMesh aNameTextMesh, TextMesh aWeaponDescriptionMesh, TextMesh aWeaponLevelMesh, TextMesh aHonorPointsMesh, string aName, int aLevel, string aPrefName, int[] aSpeed, int[] aPrice)
	{
		honorPointsMesh = aHonorPointsMesh;
		speed = aSpeed;
		price = aPrice;
		objecTouchedTransform = aObjectTouchedTransform;
		priceMesh = aPriceTextMesh;
		speedMesh = aSpeedTextMesh;
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

	public override void updateText()
	{
		nameMesh.gameObject.SetActiveRecursively(true);
		levelMesh.gameObject.SetActiveRecursively(true);
		honorPointsMesh.gameObject.SetActiveRecursively(true);
		descriptionMesh.gameObject.SetActiveRecursively(true);
		speedMesh.gameObject.SetActiveRecursively(true);
		priceMesh.gameObject.SetActiveRecursively(true);
		upgradeOrEquipMeshWithCollider.gameObject.SetActiveRecursively(true);
		nameMesh.text = Language.GetTxt("NET_GUN_MULTILINE");
		descriptionMesh.text = Language.GetTxt("NET_GUN_DESCRIPTION", 56);
		levelMesh.text = Language.GetTxt("LEVEL") + " " + base.Level;
		honorPointsMesh.text = string.Empty + MissionSelect.availableHonorPoints;
		upgradeOrEquipMeshWithCollider.text = string.Empty + Language.GetTxt("UPGRADE");
		speedMesh.text = Language.GetTxt("SPEED");
		priceMesh.text = string.Empty + Price;
		GearOnTheWall.updateRow1(speed[base.Level], speed[base.NextLevel]);
		if (base.Level == 0)
		{
			priceMesh.gameObject.SetActiveRecursively(false);
			speedMesh.gameObject.SetActiveRecursively(false);
			levelMesh.text = Language.GetTxt("LOCKED");
			upgradeOrEquipMeshWithCollider.gameObject.SetActiveRecursively(false);
		}
		if (!canBeUpgraded())
		{
			upgradeOrEquipMeshWithCollider.gameObject.SetActiveRecursively(false);
			priceMesh.gameObject.SetActiveRecursively(false);
		}
	}

	public override bool canBeUpgraded()
	{
		return level < GameConstants.NET_GUN_SPEED.Length - 1;
	}
}
