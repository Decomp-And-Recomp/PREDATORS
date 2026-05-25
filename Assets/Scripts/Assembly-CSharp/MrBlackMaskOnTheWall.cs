using UnityEngine;

public class MrBlackMaskOnTheWall : MaskOnTheWall
{
	public MrBlackMaskOnTheWall(int aMaskUnlocked, Transform aObjectTouchedTransform, TextMesh aButtonTransform, TextMesh aHealthTextMesh, TextMesh aEnergyTextMesh, TextMesh aAgilityMesh, TextMesh aHonorPointsMesh, TextMesh aNameTextMesh, TextMesh aWeaponDescriptionMesh, TextMesh aLevelMesh, TextMesh aPriceMesh, string aName, string aDescription, string aPrefName, int aMaskType, int[] aHealth, int[] aEnergy, int[] aSpeed)
	{
		priceMesh = aPriceMesh;
		maskUnlocked = aMaskUnlocked;
		health = aHealth;
		levelMesh = aLevelMesh;
		honorPointsMesh = aHonorPointsMesh;
		objecTouchedTransform = aObjectTouchedTransform;
		energyMesh = aEnergyTextMesh;
		healthMesh = aHealthTextMesh;
		speedMesh = aAgilityMesh;
		nameMesh = aNameTextMesh;
		maskType = aMaskType;
		descriptionMesh = aWeaponDescriptionMesh;
		speed = aSpeed;
		energy = aEnergy;
		upgradeOrEquipMeshWithCollider = aButtonTransform;
		weaponName = aName;
		description = aDescription;
		prefName = aPrefName;
		updateText();
		energyMesh.gameObject.SetActiveRecursively(false);
		healthMesh.gameObject.SetActiveRecursively(false);
		speedMesh.gameObject.SetActiveRecursively(false);
		upgradeOrEquipMeshWithCollider.gameObject.SetActiveRecursively(false);
		levelMesh.gameObject.SetActiveRecursively(false);
		honorPointsMesh.gameObject.SetActiveRecursively(false);
		nameMesh.gameObject.SetActiveRecursively(false);
		descriptionMesh.gameObject.SetActiveRecursively(false);
		priceMesh.gameObject.SetActiveRecursively(false);
		if (maskUnlocked == 0)
		{
			objecTouchedTransform.GetComponent<Renderer>().material.color = GearOnTheWall.weaponColorLocked;
		}
		else if (maskType != EncryptedPlayerPrefs.GetInt(GameConstants.MASK_NUMBER_S + EncryptedPlayerPrefs.GetInt("PR_CurrentSlot"), 0))
		{
			upgradeOrEquipMeshWithCollider.text = Language.GetTxt("EQUIP");
		}
		else
		{
			objecTouchedTransform.GetComponent<Renderer>().material.color = GearOnTheWall.weaponColorUnlocked;
		}
	}

	public override void upgrade()
	{
		if (EncryptedPlayerPrefs.GetInt(prefName, 0) == 1)
		{
			EncryptedPlayerPrefs.SetInt(GameConstants.MASK_NUMBER_S + EncryptedPlayerPrefs.GetInt("PR_CurrentSlot", 0), maskType);
			upgradeOrEquipMeshWithCollider.gameObject.SetActiveRecursively(false);
		}
		updateText();
	}

	public override void updateText()
	{
		energyMesh.gameObject.SetActiveRecursively(true);
		healthMesh.gameObject.SetActiveRecursively(true);
		speedMesh.gameObject.SetActiveRecursively(true);
		upgradeOrEquipMeshWithCollider.gameObject.SetActiveRecursively(true);
		levelMesh.gameObject.SetActiveRecursively(true);
		honorPointsMesh.gameObject.SetActiveRecursively(true);
		nameMesh.gameObject.SetActiveRecursively(true);
		descriptionMesh.gameObject.SetActiveRecursively(true);
		energyMesh.text = Language.GetTxt("ENERGY");
		healthMesh.text = Language.GetTxt("HEALTH");
		speedMesh.text = Language.GetTxt("SPEED");
		honorPointsMesh.text = string.Empty + MissionSelect.availableHonorPoints;
		upgradeOrEquipMeshWithCollider.text = string.Empty + Language.GetTxt("UPGRADE");
		speedMesh.text = Language.GetTxt("SPEED");
		upgradeOrEquipMeshWithCollider.text = Language.GetTxt("EQUIP");
		levelMesh.text = string.Empty;
		nameMesh.text = weaponName + "\n" + Language.GetTxt("MASK");
		descriptionMesh.text = description;
		priceMesh.text = string.Empty + Price;
		GearOnTheWall.updateRow1(health[maskType], 0);
		GearOnTheWall.updateRow2(energy[maskType], 0);
		GearOnTheWall.updateRow3(speed[maskType], 0);
		if (maskUnlocked == 0)
		{
			levelMesh.text = Language.GetTxt("LOCKED");
			upgradeOrEquipMeshWithCollider.gameObject.SetActiveRecursively(false);
			return;
		}
		priceMesh.gameObject.SetActiveRecursively(false);
		if (maskType != EncryptedPlayerPrefs.GetInt(GameConstants.MASK_NUMBER_S + EncryptedPlayerPrefs.GetInt("PR_CurrentSlot"), 0))
		{
			upgradeOrEquipMeshWithCollider.text = Language.GetTxt("EQUIP");
			return;
		}
		upgradeOrEquipMeshWithCollider.gameObject.SetActiveRecursively(false);
		levelMesh.text = Language.GetTxt("EQUIPED");
	}
}
