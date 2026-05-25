using UnityEngine;

public class HUD : MonoBehaviour
{
	public GameObject horizontalLinesBackground;

	public Transform GUI_Symbol1Parent;

	public Transform GUI_Symbol2Parent;

	public Transform GUI_Symbol3Parent;

	public Transform GUI_Symbol4Parent;

	public TextMesh GUI_BloodKillTextScore;

	public Transform GUI_BloodSplatScreen;

	public TextMesh missionStatus;

	public Transform LeftStick;

	public Transform BlockStick;

	public Transform AttackStick;

	public GameObject LeftControlPad;

	public GameObject GUI_Thermal;

	public GameObject GUI_PauseButton;

	public GameObject GUI_Cloak;

	public GameObject GUI_WeaponWristBlades_Active;

	public GameObject GUI_WeaponCombiStick_Active;

	public GameObject GUI_WeaponWhip_Active;

	public GameObject GUI_WeaponPlasmaGun_Active;

	public GameObject GUI_WeaponDisc_Active;

	public GameObject GUI_WeaponNetGun_Active;

	public GameObject GUI_WeaponWristBlades_Inactive;

	public GameObject GUI_WeaponCombiStick_Inactive;

	public GameObject GUI_WeaponWhip_Inactive;

	public GameObject GUI_WeaponPlasmaGun_Inactive;

	public GameObject GUI_WeaponDisc_Inactive;

	public GameObject GUI_WeaponNetGun_Inactive;

	public GameObject GUI_IconKills;

	public GameObject GUI_IconTrophies;

	public GameObject GUI_IconTimer;

	public GameObject GUI_IconDiscKills;

	public GameObject GUI_IconNetgunKills;

	public GameObject GUI_IconWavesRemaining;

	public GameObject[] symbol1;

	public GameObject[] symbol2;

	public GameObject[] symbol3;

	public GameObject[] symbol4;

	public Transform GUI_PainParent;

	public Transform GUI_PainParent_MinTransform;

	public Transform GUI_PainParent_MaxTransform;

	public GameObject GUI_Pain1;

	public GameObject GUI_Pain2;

	public GameObject GUI_Pain3;

	public GameObject GUI_Pain4;

	public Transform TriangleProgressInitialLocation;

	public GameObject HealthBarGameObject;

	public GameObject HealthBarGameObjectClanLeader;

	public TextMesh BloodKillText;

	public GameObject RainParticlesFront;

	public GameObject RainParticlesBack;

	public Transform GUI_ThermalFade;

	public Transform[] tipTextures;

	public Button buttonPause;
}
