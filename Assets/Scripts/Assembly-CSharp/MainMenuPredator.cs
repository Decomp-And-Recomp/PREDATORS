using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPredator : MonoBehaviour
{
	public enum MenuState
	{
		MainMenu = 0,
		OptionsMenu = 1,
		ExtrasMenu = 2,
		SlotSelectMenu = 3,
		InstructionsMenu = 4,
		CharactersMenu = 5,
		StoryMenu = 6
	}

	private const float swipeAreaHeight = 250f;

	private MainMenuPD instructionsMenu;

	public MainMenuPD iPadInstructions;

	public MainMenuPD iPhoneInstructions;

	public bool liteVersion;

	public Transform mainMenuSlidePanel;

	public Transform menuEULASlidePanel;

	public Transform avpSlidePanel;

	public GameObject avpBanner;

	public GUITexture mouseCursor;

	public GameObject EULAGroup;

	public GameObject MainMenuGroup;

	public GameObject StoryGroup;

	public GameObject OptionsGroup;

	public GameObject SlotSelectGroup;

	public GameObject ExtrasGroup;

	public GameObject CreditsGroup1;

	public GameObject CreditsGroup2;

	public GameObject CharactersGroup;

	public GameObject CharactersDetailGroup;

	public GameObject RRPresentsLogo;

	public Collider mainMenuContinueButton;

	public Collider mainMenuNewGameButton;

	public Collider mainMenuLeaderboardButton;

	public Collider mainMenuExtrasButton;

	public Button buttonQuit;

	public Button buttonQuitConfirm;

	public Button buttonQuitCancel;

	public Button buttonAVPSlideIn;

	public Button buttonAVPStore;

	public Button buttonAVPTrailer;

	public Collider buttonBackEULA;

	public Collider buttonEULA;

	public Collider buttonPP;

	public Collider buttonTOS;

	private TextMesh textMainMenuContinue;

	private TextMesh textMainMenuNewGame;

	private TextMesh textOptions;

	private TextMesh textMainMenuExtras;

	public TextMesh textQuitMessageConfirm;

	public TextMesh storyText;

	public GameObject storyTexture1;

	public GameObject storyTexture2;

	public GameObject storyTexture3;

	public GameObject storyTexture4;

	private int storySlideIndex;

	public GameObject OverwriteSlot1;

	public GameObject OverwriteSlot2;

	public GameObject OverwriteSlot3;

	public GameObject OverwriteSlot4;

	public Collider OverwriteSlot1Yes;

	public Collider OverwriteSlot1No;

	public Collider OverwriteSlot2Yes;

	public Collider OverwriteSlot2No;

	public Collider OverwriteSlot3Yes;

	public Collider OverwriteSlot3No;

	public Collider OverwriteSlot4Yes;

	public Collider OverwriteSlot4No;

	public Collider slotsSlot1Button;

	public Collider slotsSlot2Button;

	public Collider slotsSlot3Button;

	public Collider slotsSlot4Button;

	public Collider slotsBackButton;

	public TextMesh slot1Text;

	public TextMesh slot2Text;

	public TextMesh slot3Text;

	public TextMesh slot4Text;

	public TextMesh slot1HonorPointsText;

	public TextMesh slot2HonorPointsText;

	public TextMesh slot3HonorPointsText;

	public TextMesh slot4HonorPointsText;

	public TextMesh slotsMenuNameText;

	private TextMesh slot1OverwriteYes;

	private TextMesh slot1OverwriteNo;

	private TextMesh slot2OverwriteYes;

	private TextMesh slot2OverwriteNo;

	private TextMesh slot3OverwriteYes;

	private TextMesh slot3OverwriteNo;

	private TextMesh slot4OverwriteYes;

	private TextMesh slot4OverwriteNo;

	public TextMesh slot1ContinueAsk;

	public TextMesh slot2ContinueAsk;

	public TextMesh slot3ContinueAsk;

	public TextMesh slot4ContinueAsk;

	public TextMesh optionsMenuNameText;

	public GameObject optionsLanguagePickGroup;

	public Collider optionsBloodButton;

	public Collider optionsMusicButton;

	public Collider optionsSfxButton;

	public GameObject optionsLanguageButton;

	public Collider optionsLanguagePickButton;

	public LanguageButton[] languageButtons;

	public Collider optionsBackButton;

	public Transform extrasSlidePanel;

	public Collider extrasInstructionsButton;

	public Collider extrasCharactersButton;

	public Collider extrasCreditsButton;

	public Collider extrasHelsingsFireButton;

	public Collider extrasGuerrillaButton;

	public Collider extrasOptionsButton;

	public Collider creditsButtonArea;

	public Collider extrasBackButton;

	public TextMesh textExtrasMenuName;

	private TextMesh textExtrasInstructions;

	private TextMesh textExtrasCharactersBio;

	private TextMesh textExtrasCredits;

	private TextMesh textExtrasBack;

	public float reboundSpeed = 20f;

	public float acceleration = 1f;

	public float scrollMax = 0.1f;

	public Transform charactersSlidePanel;

	public Transform charactersSlideTexts;

	public Collider charactersRoyceButton;

	public Collider charactersStansButton;

	public Collider charactersCuchilloButton;

	public Collider charactersIsabelleButton;

	public Collider charactersHanzoButton;

	public Collider charactersNikolaiButton;

	public Collider charactersMombasaButton;

	public Collider charactersNolandButton;

	public Collider charactersFalconerButton;

	public Collider charactersFlusherButton;

	public Collider charactersBerserkerButton;

	public Collider charactersBackButton;

	public GameObject charactersRoyceImage;

	public GameObject charactersStansImage;

	public GameObject charactersCuchilloImage;

	public GameObject charactersIsabelleImage;

	public GameObject charactersHanzoImage;

	public GameObject charactersNikolaiImage;

	public GameObject charactersMombasaImage;

	public GameObject charactersNolandImage;

	public GameObject charactersTrackerImage;

	public GameObject charactersFalconerImage;

	public GameObject charactersMrBlackImage;

	public TextMesh charactersDescriptionText;

	public TextMesh charactersNameText;

	public float dragTolerance = 5f;

	private TextMesh charactersBackText;

	private int charactersLengthDescriptionIpad = 33;

	private int charactersLengthDescriptionIphone = 39;

	private int instructionSlideIndex;

	public float slideMenuTime = 1f;

	public AudioClip soundMenuClick;

	public AudioClip soundMenuClickError;

	public AudioClip soundSlide;

	public AudioClip soundMenuClickBack;

	public AudioClip soundMenuPanelAppear;

	public AudioClip soundMenuPanelDisappear;

	private Vector2 touchPosition = Vector2.zero;

	private Collider hitCollider;

	private RaycastHit hitInfo;

	private RaycastHit[] hitsInfo;

	private Camera cameraMain;

	private bool newGameMode;

	private int maxMissionUnlockedForSlot1;

	private int maxMissionUnlockedForSlot2;

	private int maxMissionUnlockedForSlot3;

	private int maxMissionUnlockedForSlot4;

	private bool overwritingSlot1;

	private bool overwritingSlot2;

	private bool overwritingSlot3;

	private bool overwritingSlot4;

	private int creditsIndex = 1;

	private bool showingCredits;

	private MenuState currentMenuState;

	private int currentSlot = 1;

	private bool showContinueButton;

	public Transform mainMenuPanelEndTransformContinue;

	public Transform mainMenuPanelEndTransform;

	public Transform mainMenuPanelStartTransform;

	public Transform avpMenuPanelStartTransform;

	public Transform avpMenuPanelEndTransform;

	public MeshRenderer avpBannerRenderer;

	public TextMesh avpButtonText;

	public Texture avpBannerTexture;

	public Texture predators3dBannerTexture;

	public Transform extrasPanelTop;

	public Transform extrasPanelBottom;

	public Transform charactersPanelTop;

	public Transform charactersPanelBottom;

	private bool musicOn = true;

	private bool musicButActive = true;

	private bool bloodOn = true;

	private TextMesh crystalTextMesh;

	public Transform mainMenuButtonsFrame;

	private bool avpSlidePanelVisible;

	private Dictionary<SystemLanguage, string> languageToSymbol = new Dictionary<SystemLanguage, string>
	{
		{
			SystemLanguage.English,
			"en"
		},
		{
			SystemLanguage.French,
			"fr"
		},
		{
			SystemLanguage.Italian,
			"it"
		},
		{
			SystemLanguage.German,
			"de"
		},
		{
			SystemLanguage.Spanish,
			"es"
		}
	};

	private bool sfxOn
	{
		get
		{
			return GameConstants.sfxOn;
		}
		set
		{
			GameConstants.sfxOn = value;
		}
	}

	private GameObject InstructionsGroup
	{
		get
		{
			return instructionsMenu.InstructionsGroup;
		}
	}

	private GameObject InstructionsGroup2
	{
		get
		{
			return instructionsMenu.InstructionsGroup2;
		}
	}

	private TextMesh instructionsRangedWeapon
	{
		get
		{
			return instructionsMenu.instructionsRangedWeapon;
		}
	}

	private TextMesh instructionsMeleeWeapon
	{
		get
		{
			return instructionsMenu.instructionsMeleeWeapon;
		}
	}

	private TextMesh instructionsHealthBarTextIPad
	{
		get
		{
			return instructionsMenu.instructionsHealthBarTextIPad;
		}
	}

	private TextMesh instructionsPauseMenuTextIPad
	{
		get
		{
			return instructionsMenu.instructionsPauseMenuTextIPad;
		}
	}

	private TextMesh instructionsMoveStickTextIPad
	{
		get
		{
			return instructionsMenu.instructionsMoveStickTextIPad;
		}
	}

	private TextMesh instructionsMeleeWeaponSelectTextIPad
	{
		get
		{
			return instructionsMenu.instructionsMeleeWeaponSelectTextIPad;
		}
	}

	private TextMesh instructionsBlockDashGrabTextIPad
	{
		get
		{
			return instructionsMenu.instructionsBlockDashGrabTextIPad;
		}
	}

	private TextMesh instructionsAttackTextIPad
	{
		get
		{
			return instructionsMenu.instructionsAttackTextIPad;
		}
	}

	private TextMesh instructionsEnergyTextIPad
	{
		get
		{
			return instructionsMenu.instructionsEnergyTextIPad;
		}
	}

	private TextMesh instructionsRangedWeaponSelectTextIPad
	{
		get
		{
			return instructionsMenu.instructionsRangedWeaponSelectTextIPad;
		}
	}

	private TextMesh instructionsShootTargetTextIPad
	{
		get
		{
			return instructionsMenu.instructionsShootTargetTextIPad;
		}
	}

	private TextMesh instructionsThermalVisionTextIPad
	{
		get
		{
			return instructionsMenu.instructionsThermalVisionTextIPad;
		}
	}

	private TextMesh instructionsCloakTextIPad
	{
		get
		{
			return instructionsMenu.instructionsCloakTextIPad;
		}
	}

	private bool ButtonHarwareBackPressed
	{
		get
		{
			return Input.GetKeyDown(KeyCode.Escape);
		}
	}

	private void LoadResourcesTextures()
	{
	}

	private void initText()
	{
		CreditsGroup1.GetComponent<TextMesh>().text = "TM & © 2010\nTwentieth Century Fox\nFilm Corporation.\n" + Language.GetTxt("ALL_RIGHTS_RESERVED") + "\n\n" + Language.GetTxt("DEVELOPED_BY_ANGRY_MOB_GAMES") + "\n\nDan Bojan\nBogdan Iliesiu\nAndrei Szasz\nPaul Szanto\nVictor Tero-Vescan\nZoltan Csomai";
		CreditsGroup2.GetComponent<TextMesh>().text = Language.GetTxt("MUSIC_BY") + " Soundreel\n\n" + Language.GetTxt("SPECIAL_THANKS_TO") + "\n\nMihai Cimpean\nAlexandru Gombos\nMihai Deac\nLaura Gatea\nJose Nieto\nHugo Arman\nHope-Valerie Pashos";
		charactersBackText.text = Language.GetTxt("BACK");
		textExtrasBack.text = Language.GetTxt("BACK");
		textExtrasInstructions.text = Language.GetTxt("INSTRUCTIONS");
		textExtrasMenuName.text = Language.GetTxt("EXTRAS_WORD");
		optionsMenuNameText.text = Language.GetTxt("OPTIONS_WORD");
		maxMissionUnlockedForSlot1 = EncryptedPlayerPrefs.GetInt("PR_LastMissionUnlocked_S" + 1, 0);
		maxMissionUnlockedForSlot2 = EncryptedPlayerPrefs.GetInt("PR_LastMissionUnlocked_S" + 2, 0);
		maxMissionUnlockedForSlot3 = EncryptedPlayerPrefs.GetInt("PR_LastMissionUnlocked_S" + 3, 0);
		maxMissionUnlockedForSlot4 = EncryptedPlayerPrefs.GetInt("PR_LastMissionUnlocked_S" + 4, 0);
		if (maxMissionUnlockedForSlot1 == 0)
		{
			slot1Text.text = string.Empty;
		}
		else
		{
			slot1Text.text = Language.GetTxt("HUNT") + " " + EncryptedPlayerPrefs.GetInt("PR_LastMissionUnlocked_S" + 1);
		}
		if (maxMissionUnlockedForSlot2 == 0)
		{
			slot2Text.text = string.Empty;
		}
		else
		{
			slot2Text.text = Language.GetTxt("HUNT") + " " + EncryptedPlayerPrefs.GetInt("PR_LastMissionUnlocked_S" + 2);
		}
		if (maxMissionUnlockedForSlot3 == 0)
		{
			slot3Text.text = string.Empty;
		}
		else
		{
			slot3Text.text = Language.GetTxt("HUNT") + " " + EncryptedPlayerPrefs.GetInt("PR_LastMissionUnlocked_S" + 3);
		}
		if (maxMissionUnlockedForSlot4 == 0)
		{
			slot4Text.text = string.Empty;
		}
		else
		{
			slot4Text.text = Language.GetTxt("HUNT") + " " + EncryptedPlayerPrefs.GetInt("PR_LastMissionUnlocked_S" + 4);
		}
		textMainMenuContinue.text = Language.GetTxt("CONTINUE");
		textMainMenuNewGame.text = Language.GetTxt("NEW_GAME");
		textOptions.text = Language.GetTxt("OPTIONS_WORD");
		textMainMenuExtras.text = Language.GetTxt("EXTRAS_WORD");
		((TextMesh)slotsBackButton.transform.GetChild(0).GetComponent(typeof(TextMesh))).text = Language.GetTxt("BACK");
		((TextMesh)optionsLanguageButton.transform.GetChild(0).GetComponent(typeof(TextMesh))).text = Language.GetTxt("LANGUAGE");
		((TextMesh)optionsBackButton.transform.GetChild(0).GetComponent(typeof(TextMesh))).text = Language.GetTxt("BACK");
		if (musicOn)
		{
			((TextMesh)optionsMusicButton.transform.GetChild(0).GetComponent(typeof(TextMesh))).text = Language.GetTxt("MUSIC") + Language.GetTxt("ON");
		}
		else
		{
			((TextMesh)optionsMusicButton.transform.GetChild(0).GetComponent(typeof(TextMesh))).text = Language.GetTxt("MUSIC") + Language.GetTxt("OFF");
		}
		if (sfxOn)
		{
			((TextMesh)optionsSfxButton.transform.GetChild(0).GetComponent(typeof(TextMesh))).text = Language.GetTxt("SFX") + Language.GetTxt("ON");
		}
		else
		{
			((TextMesh)optionsMusicButton.transform.GetChild(0).GetComponent(typeof(TextMesh))).text = Language.GetTxt("MUSIC") + Language.GetTxt("OFF");
			((TextMesh)optionsSfxButton.transform.GetChild(0).GetComponent(typeof(TextMesh))).text = Language.GetTxt("SFX") + Language.GetTxt("OFF");
		}
		if (bloodOn)
		{
			((TextMesh)optionsBloodButton.transform.GetChild(0).GetComponent(typeof(TextMesh))).text = Language.GetTxt("BLOOD") + Language.GetTxt("ON");
		}
		else
		{
			((TextMesh)optionsBloodButton.transform.GetChild(0).GetComponent(typeof(TextMesh))).text = Language.GetTxt("BLOOD") + Language.GetTxt("OFF");
		}
		initSlotsMenu();
		((TextMesh)optionsLanguagePickButton.transform.GetChild(0).GetComponent(typeof(TextMesh))).text = Language.GetTxt(Language.CurrentLang.ToString());
		buttonQuit.buttonText.text = Language.GetTxt("QUIT");
		buttonQuitCancel.buttonText.text = Language.GetTxt("NO");
		buttonQuitConfirm.buttonText.text = Language.GetTxt("YES");
		textQuitMessageConfirm.text = Language.GetTxt("ARE_YOU_SURE_YOU_WANT_TO_QUIT");
		buttonBackEULA.transform.GetChild(0).GetComponent<TextMesh>().text = Language.GetTxt("BACK");
		buttonEULA.transform.GetChild(0).GetComponent<TextMesh>().text = Language.GetTxt("EULA");
		buttonPP.transform.GetChild(0).GetComponent<TextMesh>().text = Language.GetTxt("PP");
		buttonTOS.transform.GetChild(0).GetComponent<TextMesh>().text = Language.GetTxt("TOS");
		instructionsPauseMenuTextIPad.text = Language.GetTxt("HEAVY_ATTACK");
		instructionsAttackTextIPad.text = Language.GetTxt("ATTACK");
		instructionsBlockDashGrabTextIPad.text = Language.GetTxt("BLOCK") + "/" + Language.GetTxt("DASH") + "/" + Language.GetTxt("GRAB");
		instructionsCloakTextIPad.text = Language.GetTxt("CLOAK");
		instructionsEnergyTextIPad.text = Language.GetTxt("ENERGY");
		instructionsMeleeWeaponSelectTextIPad.text = Language.GetTxt("MELEE_WEAPON_SELECT");
		instructionsMoveStickTextIPad.text = Language.GetTxt("MOVEMENT_STICK");
		instructionsRangedWeaponSelectTextIPad.text = Language.GetTxt("RANGED_WEAPON_SELECT");
		instructionsShootTargetTextIPad.text = Language.GetTxt("SHOOT_TARGET");
		instructionsThermalVisionTextIPad.text = Language.GetTxt("THERMAL_VISION");
		instructionsHealthBarTextIPad.text = Language.GetTxt("HEALTH_BAR");
		instructionsRangedWeapon.text = Language.GetTxt("RANGED_WEAPON");
		instructionsMeleeWeapon.text = Language.GetTxt("MELEE_WEAPON");
		PlatformDependent.SetCrystalButtonName(crystalTextMesh);
	}

	private void FirstTimeLaunchedV13()
	{
		for (currentSlot = 1; currentSlot <= 4; currentSlot++)
		{
			EncryptedPlayerPrefs.SetInt("PR_WhipLevel_S" + currentSlot, 0);
			EncryptedPlayerPrefs.SetInt("PR_MaskType4Unlocked_S" + currentSlot, 0);
			EncryptedPlayerPrefs.SetInt("PR_PlayerKnowsMaskType4Unlocked_S" + currentSlot, 0);
			EncryptedPlayerPrefs.SetInt("PR_PlayerKnowsWhipUnlocked_S" + currentSlot, 0);
			EncryptedPlayerPrefs.SetInt("PR_BlackPredatorKills_S" + currentSlot, 0);
			EncryptedPlayerPrefs.SetInt("PR_BerserkerKills_S" + currentSlot, 0);
			EncryptedPlayerPrefs.SetInt("PR_DogKills_S" + currentSlot, 0);
			EncryptedPlayerPrefs.SetInt("PR_BlackPredatorTrophies_S" + currentSlot, 0);
			EncryptedPlayerPrefs.SetInt("PR_DogTrophies_S" + currentSlot, 0);
			EncryptedPlayerPrefs.SetInt("PR_BerserkerTrophies_S" + currentSlot, 0);
			EncryptedPlayerPrefs.SetInt("PR_VerticalSplits_S" + currentSlot, 0);
			EncryptedPlayerPrefs.SetInt("PR_TotalKillsWristblade_S" + currentSlot, EncryptedPlayerPrefs.GetInt("PR_TotalKillsWristblade", 0));
			EncryptedPlayerPrefs.SetInt("PR_TotalKillsCombistick_S" + currentSlot, EncryptedPlayerPrefs.GetInt("PR_TotalKillsCombistick", 0));
			EncryptedPlayerPrefs.SetInt("PR_TotalKillsPlasmagun_S" + currentSlot, EncryptedPlayerPrefs.GetInt("PR_TotalKillsPlasmagun", 0));
			EncryptedPlayerPrefs.SetInt("PR_TotalKillsNetgun_S" + currentSlot, EncryptedPlayerPrefs.GetInt("PR_TotalKillsNetgun", 0));
			EncryptedPlayerPrefs.SetInt("PR_TotalKillsWhip_S" + currentSlot, 0);
		}
	}

	private void ActivateMainMenu()
	{
		MainMenuGroup.SetActiveRecursively(true);
		avpBanner.gameObject.SetActiveRecursively(false);
		buttonAVPStore.gameObject.SetActiveRecursively(false);
		buttonAVPTrailer.gameObject.SetActiveRecursively(false);
		if ((bool)RRPresentsLogo)
		{
			if (Language.CurrentLang == SystemLanguage.English)
			{
				RRPresentsLogo.gameObject.SetActiveRecursively(true);
			}
			else
			{
				RRPresentsLogo.gameObject.SetActiveRecursively(false);
			}
		}
	}

	private void Awake()
	{
		StartCoroutine(PlatformDependent.GetPromoPeriodData());
		OnPromoDataReceived(PlatformDependent.Predators_3D_Promo_Period);
		PlatformDependent.onPromoDataReceived = (Action<bool>)Delegate.Combine(PlatformDependent.onPromoDataReceived, new Action<bool>(OnPromoDataReceived));
		LanguageButton[] array = languageButtons;
		foreach (LanguageButton languageButton in array)
		{
			languageButton.onPressBegin += delegate(object sender, EventArgs ea)
			{
				Language.LoadLang((sender as LanguageButton).language);
				optionsLanguagePickButton.gameObject.SetActiveRecursively(true);
				optionsLanguagePickGroup.gameObject.SetActiveRecursively(false);
				FontManager[] array2 = UnityEngine.Object.FindObjectsOfType(typeof(FontManager)) as FontManager[];
				FontManager[] array3 = array2;
				foreach (FontManager fontManager in array3)
				{
					fontManager.ChangeFontAccordingToLanguage(Language.CurrentLang);
				}
				initText();
			};
		}
		PlatformDependent.SetScreenOrientation(false);
		if (PlatformDependent.tablet)
		{
			instructionsMenu = iPadInstructions;
			UnityEngine.Object.Destroy(iPhoneInstructions.gameObject);
		}
		else
		{
			instructionsMenu = iPhoneInstructions;
			UnityEngine.Object.Destroy(iPadInstructions.gameObject);
		}
		crystalTextMesh = mainMenuLeaderboardButton.GetComponentInChildren<TextMesh>();
		PlatformDependent.HideMouseCursor(mouseCursor);
		PlatformDependent.SetPositionForGuerrilaBob(extrasGuerrillaButton, extrasHelsingsFireButton);
		PlatformDependent.HandleIphoneKeyboard();
		//CrystalUnityBasic.Instance.ActivateCrystalSetting(CrystalUnityBasic.CrystalSetting.CrystalSettingEnableGameCenterSupport, "YES");
		//CrystalUnityBasic.Instance.AuthenticateLocalPlayer();
		LoadResourcesTextures();
		if ((bool)RRPresentsLogo)
		{
			if (Language.CurrentLang == SystemLanguage.English)
			{
				RRPresentsLogo.gameObject.SetActiveRecursively(true);
			}
			else
			{
				RRPresentsLogo.gameObject.SetActiveRecursively(false);
			}
		}
		cameraMain = Camera.main;
		currentSlot = EncryptedPlayerPrefs.GetInt("PR_CurrentSlot", 0);
		musicOn = PlayerPrefs.GetInt("PR_MusicOn", 1) == 1;
		sfxOn = PlayerPrefs.GetInt("PR_SfxOn", 1) == 1;
		bloodOn = PlayerPrefs.GetInt("PR_BloodOn", 1) == 1;
		if (musicOn)
		{
			if (sfxOn)
			{
				base.GetComponent<AudioSource>().Play();
			}
			((TextMesh)optionsMusicButton.transform.GetChild(0).GetComponent(typeof(TextMesh))).text = Language.GetTxt("MUSIC") + Language.GetTxt("ON");
		}
		else
		{
			base.GetComponent<AudioSource>().Stop();
			((TextMesh)optionsMusicButton.transform.GetChild(0).GetComponent(typeof(TextMesh))).text = Language.GetTxt("MUSIC") + Language.GetTxt("OFF");
		}
		if (sfxOn)
		{
			musicButActive = true;
			((TextMesh)optionsSfxButton.transform.GetChild(0).GetComponent(typeof(TextMesh))).text = Language.GetTxt("SFX") + Language.GetTxt("ON");
		}
		else
		{
			((TextMesh)optionsMusicButton.transform.GetChild(0).GetComponent(typeof(TextMesh))).text = Language.GetTxt("MUSIC") + Language.GetTxt("OFF");
			musicButActive = false;
			((TextMesh)optionsSfxButton.transform.GetChild(0).GetComponent(typeof(TextMesh))).text = Language.GetTxt("SFX") + Language.GetTxt("OFF");
		}
		if (bloodOn)
		{
			((TextMesh)optionsBloodButton.transform.GetChild(0).GetComponent(typeof(TextMesh))).text = Language.GetTxt("BLOOD") + Language.GetTxt("ON");
		}
		else
		{
			((TextMesh)optionsBloodButton.transform.GetChild(0).GetComponent(typeof(TextMesh))).text = Language.GetTxt("BLOOD") + Language.GetTxt("OFF");
		}
		textMainMenuContinue = (TextMesh)mainMenuContinueButton.transform.GetChild(0).GetComponent(typeof(TextMesh));
		textMainMenuNewGame = (TextMesh)mainMenuNewGameButton.transform.GetChild(0).GetComponent(typeof(TextMesh));
		textMainMenuExtras = (TextMesh)mainMenuExtrasButton.transform.GetChild(0).GetComponent(typeof(TextMesh));
		slot1OverwriteYes = (TextMesh)OverwriteSlot1Yes.transform.GetChild(0).GetComponent(typeof(TextMesh));
		slot1OverwriteNo = (TextMesh)OverwriteSlot1No.transform.GetChild(0).GetComponent(typeof(TextMesh));
		slot2OverwriteYes = (TextMesh)OverwriteSlot2Yes.transform.GetChild(0).GetComponent(typeof(TextMesh));
		slot2OverwriteNo = (TextMesh)OverwriteSlot2No.transform.GetChild(0).GetComponent(typeof(TextMesh));
		slot3OverwriteYes = (TextMesh)OverwriteSlot3Yes.transform.GetChild(0).GetComponent(typeof(TextMesh));
		slot3OverwriteNo = (TextMesh)OverwriteSlot3No.transform.GetChild(0).GetComponent(typeof(TextMesh));
		slot4OverwriteYes = (TextMesh)OverwriteSlot4Yes.transform.GetChild(0).GetComponent(typeof(TextMesh));
		slot4OverwriteNo = (TextMesh)OverwriteSlot4No.transform.GetChild(0).GetComponent(typeof(TextMesh));
		charactersBackText = (TextMesh)charactersBackButton.transform.GetChild(0).GetComponent(typeof(TextMesh));
		textExtrasBack = (TextMesh)extrasBackButton.transform.GetChild(0).GetComponent(typeof(TextMesh));
		textExtrasInstructions = (TextMesh)extrasInstructionsButton.transform.GetChild(0).GetComponent(typeof(TextMesh));
		textOptions = (TextMesh)extrasOptionsButton.transform.GetChild(0).GetComponent(typeof(TextMesh));
		initText();
		if (liteVersion)
		{
			showContinueButton = true;
		}
		else if (currentSlot != 0 && (maxMissionUnlockedForSlot1 > 0 || maxMissionUnlockedForSlot2 > 0 || maxMissionUnlockedForSlot3 > 0 || maxMissionUnlockedForSlot4 > 0))
		{
			showContinueButton = true;
		}
	}

	private void OnPromoDataReceived(bool promoPeriod)
	{
		if (promoPeriod)
		{
			avpButtonText.text = "PREDATOR 3D";
			avpBannerRenderer.material.mainTexture = predators3dBannerTexture;
		}
		else
		{
			avpButtonText.text = "AVP: EVOLUTION";
			avpBannerRenderer.material.mainTexture = avpBannerTexture;
		}
	}

	private void initSlotsMenu()
	{
		slot1ContinueAsk.text = Language.GetTxt("CURRENT_PROGRESS_WILL_BE_LOST") + "\n" + Language.GetTxt("DO_YOU_WANT_TO_CONTINUE");
		slot1OverwriteNo.text = Language.GetTxt("NO");
		slot1OverwriteYes.text = Language.GetTxt("YES");
		slot2ContinueAsk.text = Language.GetTxt("CURRENT_PROGRESS_WILL_BE_LOST") + "\n" + Language.GetTxt("DO_YOU_WANT_TO_CONTINUE");
		slot2OverwriteNo.text = Language.GetTxt("NO");
		slot2OverwriteYes.text = Language.GetTxt("YES");
		slot3ContinueAsk.text = Language.GetTxt("CURRENT_PROGRESS_WILL_BE_LOST") + "\n" + Language.GetTxt("DO_YOU_WANT_TO_CONTINUE");
		slot3OverwriteNo.text = Language.GetTxt("NO");
		slot3OverwriteYes.text = Language.GetTxt("YES");
		slot4ContinueAsk.text = Language.GetTxt("CURRENT_PROGRESS_WILL_BE_LOST") + "\n" + Language.GetTxt("DO_YOU_WANT_TO_CONTINUE");
		slot4OverwriteNo.text = Language.GetTxt("NO");
		slot4OverwriteYes.text = Language.GetTxt("YES");
		if (EncryptedPlayerPrefs.GetInt("PR_LastMissionUnlocked_S" + 1, 0) == 0)
		{
			slot1HonorPointsText.text = Language.GetTxt("EMPTY_SLOT");
		}
		else
		{
			slot1HonorPointsText.text = Language.GetTxt("HONOR_POINTS") + " " + EncryptedPlayerPrefs.GetInt("PR_HonorPoints_S" + 1, 0);
		}
		if (EncryptedPlayerPrefs.GetInt("PR_LastMissionUnlocked_S" + 2, 0) == 0)
		{
			slot2HonorPointsText.text = Language.GetTxt("EMPTY_SLOT");
		}
		else
		{
			slot2HonorPointsText.text = Language.GetTxt("HONOR_POINTS") + " " + EncryptedPlayerPrefs.GetInt("PR_HonorPoints_S" + 2, 0);
		}
		if (EncryptedPlayerPrefs.GetInt("PR_LastMissionUnlocked_S" + 3, 0) == 0)
		{
			slot3HonorPointsText.text = Language.GetTxt("EMPTY_SLOT");
		}
		else
		{
			slot3HonorPointsText.text = Language.GetTxt("HONOR_POINTS") + " " + EncryptedPlayerPrefs.GetInt("PR_HonorPoints_S" + 3, 0);
		}
		if (EncryptedPlayerPrefs.GetInt("PR_LastMissionUnlocked_S" + 4, 0) == 0)
		{
			slot4HonorPointsText.text = Language.GetTxt("EMPTY_SLOT");
		}
		else
		{
			slot4HonorPointsText.text = Language.GetTxt("HONOR_POINTS") + " " + EncryptedPlayerPrefs.GetInt("PR_HonorPoints_S" + 4, 0);
		}
	}

	private void Start()
	{
		AddEventHandlers();
		initSlotsMenu();
		ActivateMainMenu();
		StoryGroup.gameObject.SetActiveRecursively(false);
		OptionsGroup.gameObject.SetActiveRecursively(false);
		SlotSelectGroup.gameObject.SetActiveRecursively(false);
		ExtrasGroup.gameObject.SetActiveRecursively(false);
		InstructionsGroup.gameObject.SetActiveRecursively(false);
		CreditsGroup1.gameObject.SetActiveRecursively(false);
		CreditsGroup2.gameObject.SetActiveRecursively(false);
		CharactersGroup.gameObject.SetActiveRecursively(false);
		CharactersDetailGroup.gameObject.SetActiveRecursively(false);
		StartCoroutine(UpdateCR());
	}

	private void AddEventHandlers()
	{
		buttonAVPSlideIn.onPressBegin += buttonAVP_onPressBegin;
		buttonAVPStore.onPressBegin += buttonAVPStore_onPressBegin;
		buttonAVPTrailer.onPressBegin += buttonAVPTrailer_onPressBegin;
		buttonQuit.onPressBegin += delegate
		{
			buttonQuitConfirm.transform.parent.gameObject.SetActiveRecursively(true);
			buttonQuit.transform.parent.gameObject.SetActiveRecursively(false);
			if (avpSlidePanelVisible)
			{
				avpSlidePanel.gameObject.SetActiveRecursively(false);
			}
		};
		buttonQuitConfirm.onPressBegin += delegate
		{
#if !UNITY_EDITOR
			Application.Quit();
#else
            UnityEditor.EditorApplication.isPlaying = false;
#endif
		};
		buttonQuitCancel.onPressBegin += delegate
		{
			buttonQuitConfirm.transform.parent.gameObject.SetActiveRecursively(false);
			buttonQuit.transform.parent.gameObject.SetActiveRecursively(true);
			if (showContinueButton)
			{
				mainMenuNewGameButton.gameObject.SetActiveRecursively(true);
			}
			else
			{
				mainMenuContinueButton.gameObject.SetActiveRecursively(false);
			}
			if (avpSlidePanelVisible)
			{
				avpSlidePanel.gameObject.SetActiveRecursively(true);
			}
		};
	}

	private void buttonAVPTrailer_onPressBegin(object sender, EventArgs e)
	{
		StartCoroutine(SlideOutAVPPanel());
		if (!PlatformDependent.Predators_3D_Promo_Period)
		{
			PlayAVPTrailer();
		}
		else
		{
			PlayPredators3DTrailer();
		}
	}

	private void PlayPredators3DTrailer()
	{
		Debug.Log("Predators Trailer");
		Application.OpenURL("http://bit.ly/1kbKT0x");
	}

	private void PlayAVPTrailer()
	{
		Debug.Log("AVP Trailer");
		Application.OpenURL("http://www.youtube.com/watch?v=4QA7Q4ONuKo");
	}

	private void buttonAVPStore_onPressBegin(object sender, EventArgs e)
	{
		StartCoroutine(SlideOutAVPPanel());
		if (!PlatformDependent.Predators_3D_Promo_Period)
		{
			Application.OpenURL("https://play.google.com/store/apps/details?id=com.fde.avpevolution");
		}
		else
		{
			PlayPredators3DTrailer();
		}
	}

	private IEnumerator SlideOutAVPPanel()
	{
		if (avpSlidePanelVisible)
		{
			buttonAVPStore.gameObject.SetActiveRecursively(false);
			buttonAVPTrailer.gameObject.SetActiveRecursively(false);
			yield return StartCoroutine(SlidePanelVertical(avpSlidePanel, avpMenuPanelEndTransform.localPosition, avpMenuPanelStartTransform.localPosition, slideMenuTime / 2f));
			avpBanner.gameObject.SetActiveRecursively(false);
			buttonAVPSlideIn.gameObject.SetActiveRecursively(true);
			avpSlidePanelVisible = false;
		}
	}

	private IEnumerator SlideInAVPPanel()
	{
		if (!avpSlidePanelVisible)
		{
			buttonAVPSlideIn.gameObject.SetActiveRecursively(false);
			avpBanner.gameObject.SetActiveRecursively(true);
			yield return StartCoroutine(SlidePanelVertical(avpSlidePanel, avpMenuPanelStartTransform.localPosition, avpMenuPanelEndTransform.localPosition, slideMenuTime / 2f));
			buttonAVPStore.gameObject.SetActiveRecursively(true);
			buttonAVPTrailer.gameObject.SetActiveRecursively(true);
			avpSlidePanelVisible = true;
		}
	}

	private void buttonAVP_onPressBegin(object sender, EventArgs e)
	{
		StartCoroutine(SlideInAVPPanel());
	}

	private IEnumerator StartMission()
	{
		if (PlatformDependent.tablet)
		{
			PlatformDependent.DismissCrystalUI();
		}
		yield return null;
		PlatformDependent.LoadLevelWithLoadingScreen("GamePlay_iPad");
	}

	private void LoadMissionSelect()
	{
		if (PlatformDependent.tablet)
		{
			PlatformDependent.DismissCrystalUI();
		}
		PlatformDependent.LoadLevelWithLoadingScreen("MissionSelect3D");
	}

	private IEnumerator SlidePanelVertical(Transform panel, Vector3 startPosition, Vector3 endPosition, float moveTime)
	{
		float timer = moveTime;
		float aux2 = startPosition.z;
		startPosition = panel.localPosition;
		startPosition.z = aux2;
		panel.localPosition = startPosition;
		aux2 = endPosition.z;
		endPosition = panel.localPosition;
		endPosition.z = aux2;
		while (timer >= 0f)
		{
			panel.localPosition = Vector3.Lerp(endPosition, startPosition, timer / moveTime);
			timer -= Time.deltaTime;
			yield return null;
		}
		panel.localPosition = endPosition;
	}

	private void Update()
	{
		PlatformDependent.SetScreenOrientation(true);
		PlatformDependent.UpdateMouseCursorGUITexture(mouseCursor);
	}

	private IEnumerator UpdateCR()
	{
		if (sfxOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(soundMenuPanelAppear);
		}
		if (EncryptedPlayerPrefs.GetInt("FIRST_RUN_AVP_BANNER", 1) == 1)
		{
			MonoBehaviour.print("Banner In");
			StartCoroutine(SlideInAVPPanel());
			EncryptedPlayerPrefs.SetInt("FIRST_RUN_AVP_BANNER", 0);
		}
		if (showContinueButton)
		{
			mainMenuContinueButton.gameObject.SetActiveRecursively(true);
			mainMenuNewGameButton.gameObject.SetActiveRecursively(true);
			yield return StartCoroutine(SlidePanelVertical(mainMenuSlidePanel, mainMenuPanelStartTransform.localPosition, mainMenuPanelEndTransform.localPosition, slideMenuTime));
		}
		else
		{
			mainMenuNewGameButton.gameObject.SetActiveRecursively(true);
			mainMenuContinueButton.gameObject.SetActiveRecursively(false);
			yield return StartCoroutine(SlidePanelVertical(mainMenuSlidePanel, mainMenuPanelStartTransform.localPosition, mainMenuPanelEndTransform.localPosition, slideMenuTime));
		}
		while (true)
		{
			if (ButtonHarwareBackPressed)
			{
				switch (currentMenuState)
				{
				case MenuState.ExtrasMenu:
					Debug.Log("Exit extras meniu");
					yield return StartCoroutine(OnButtonExtrasBackPressed());
					break;
				case MenuState.OptionsMenu:
					yield return StartCoroutine(OnButtonBackOptionsPressed());
					break;
				case MenuState.CharactersMenu:
					yield return StartCoroutine(OnBUttonBackCharactersPressed());
					break;
				case MenuState.SlotSelectMenu:
					yield return StartCoroutine(OnButtonBackSLotSelectPressed());
					break;
				}
			}
			if (InputGUI.touchCount > 0)
			{
				TouchGUI touch = InputGUI.GetTouch(0);
				touchPosition = touch.position;
				if (touch.phase == TouchPhase.Began)
				{
					switch (currentMenuState)
					{
					case MenuState.MainMenu:
						hitCollider = InputGUI.GetHitCollider(touchPosition, cameraMain);
						if (!(hitCollider != null))
						{
							break;
						}
						if (buttonEULA == hitCollider)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
							}
							Application.OpenURL("http://tos.ea.com/legalapp/mobileeula/US/" + languageToSymbol[Language.CurrentLang] + "/OTHER/");
							yield return null;
						}
						if (buttonPP == hitCollider)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
							}
							Application.OpenURL("http://tos.ea.com/legalapp/WEBPRIVACY/US/" + languageToSymbol[Language.CurrentLang] + "/PC/");
							yield return null;
						}
						if (buttonTOS == hitCollider)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
							}
							Application.OpenURL("http://tos.ea.com/legalapp/WEBTERMS/US/" + languageToSymbol[Language.CurrentLang] + "/PC/");
							yield return null;
						}
						if (buttonBackEULA == hitCollider)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClickBack);
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuPanelDisappear);
							}
							yield return StartCoroutine(SlidePanelVertical(menuEULASlidePanel, extrasPanelBottom.localPosition, extrasPanelTop.localPosition, slideMenuTime / 2f));
							EULAGroup.gameObject.SetActiveRecursively(false);
							ActivateMainMenu();
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuPanelAppear);
							}
							if (showContinueButton)
							{
								mainMenuContinueButton.gameObject.SetActiveRecursively(true);
								mainMenuNewGameButton.gameObject.SetActiveRecursively(true);
								yield return StartCoroutine(SlidePanelVertical(mainMenuSlidePanel, mainMenuPanelStartTransform.localPosition, mainMenuPanelEndTransform.localPosition, slideMenuTime));
							}
							else
							{
								mainMenuNewGameButton.gameObject.SetActiveRecursively(true);
								mainMenuContinueButton.gameObject.SetActiveRecursively(false);
								yield return StartCoroutine(SlidePanelVertical(mainMenuSlidePanel, mainMenuPanelStartTransform.localPosition, mainMenuPanelEndTransform.localPosition, slideMenuTime));
							}
						}
						if (mainMenuNewGameButton == hitCollider)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuPanelDisappear);
							}
							StartCoroutine(SlideOutAVPPanel());
							yield return StartCoroutine(SlidePanelVertical(mainMenuSlidePanel, mainMenuSlidePanel.localPosition, mainMenuPanelStartTransform.localPosition, slideMenuTime / 2f));
							while (avpSlidePanelVisible)
							{
								yield return null;
							}
							if (liteVersion)
							{
								ResetPlayerPrefsForNewGame(1);
								OverwriteSlot1.gameObject.SetActiveRecursively(false);
								OverwriteSlot2.gameObject.SetActiveRecursively(false);
								OverwriteSlot3.gameObject.SetActiveRecursively(false);
								OverwriteSlot4.gameObject.SetActiveRecursively(false);
								overwritingSlot1 = false;
								overwritingSlot2 = false;
								overwritingSlot3 = false;
								overwritingSlot4 = false;
								currentMenuState = MenuState.StoryMenu;
								storySlideIndex = 0;
								SlotSelectGroup.gameObject.SetActiveRecursively(false);
								StoryGroup.gameObject.SetActiveRecursively(true);
								MainMenuGroup.SetActiveRecursively(false);
							}
							else
							{
								currentMenuState = MenuState.SlotSelectMenu;
								SlotSelectGroup.SetActiveRecursively(true);
								slotsMenuNameText.text = Language.GetTxt("NEW_GAME");
								currentMenuState = MenuState.SlotSelectMenu;
								MainMenuGroup.SetActiveRecursively(false);
								newGameMode = true;
							}
						}
						if (mainMenuContinueButton == hitCollider && showContinueButton)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
							}
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuPanelDisappear);
							}
							StartCoroutine(SlideOutAVPPanel());
							yield return StartCoroutine(SlidePanelVertical(mainMenuSlidePanel, mainMenuSlidePanel.localPosition, mainMenuPanelStartTransform.localPosition, slideMenuTime / 2f));
							while (avpSlidePanelVisible)
							{
								yield return null;
							}
							currentMenuState = MenuState.SlotSelectMenu;
							SlotSelectGroup.SetActiveRecursively(true);
							slotsMenuNameText.text = Language.GetTxt("RESUME_GAME");
							currentMenuState = MenuState.SlotSelectMenu;
							MainMenuGroup.SetActiveRecursively(false);
							newGameMode = false;
						}
						PlatformDependent.HandleCrystalAndFoxButtons(mainMenuLeaderboardButton, hitCollider, base.GetComponent<AudioSource>(), sfxOn, soundMenuClick, liteVersion);
						if (mainMenuExtrasButton == hitCollider)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuPanelDisappear);
							}
							StartCoroutine(SlideOutAVPPanel());
							yield return StartCoroutine(SlidePanelVertical(mainMenuSlidePanel, mainMenuSlidePanel.localPosition, mainMenuPanelStartTransform.localPosition, slideMenuTime / 2f));
							while (avpSlidePanelVisible)
							{
								yield return null;
							}
							ExtrasGroup.gameObject.SetActiveRecursively(true);
							MainMenuGroup.gameObject.SetActiveRecursively(false);
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuPanelAppear);
							}
							yield return StartCoroutine(SlidePanelVertical(extrasSlidePanel, extrasPanelTop.localPosition, extrasPanelBottom.localPosition, slideMenuTime));
							currentMenuState = MenuState.ExtrasMenu;
						}
						break;
					case MenuState.StoryMenu:
						switch (storySlideIndex)
						{
						case 0:
							storyText.text = Language.GetTxt("STORY_SLIDE_2", 45);
							storyTexture1.active = false;
							storyTexture2.active = true;
							storyTexture3.active = false;
							storyTexture4.active = false;
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundSlide);
							}
							storySlideIndex++;
							break;
						case 1:
							storyText.text = Language.GetTxt("STORY_SLIDE_3", 45);
							storyTexture1.active = false;
							storyTexture2.active = false;
							storyTexture3.active = true;
							storyTexture4.active = false;
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundSlide);
							}
							storySlideIndex++;
							break;
						case 2:
							storyText.text = Language.GetTxt("STORY_SLIDE_4", 45);
							storyTexture1.active = false;
							storyTexture2.active = false;
							storyTexture3.active = false;
							storyTexture4.active = true;
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundSlide);
							}
							storySlideIndex++;
							break;
						case 3:
							EncryptedPlayerPrefs.SetInt("PR_CurrentSlot", currentSlot);
							if (liteVersion)
							{
								EncryptedPlayerPrefs.SetInt("PR_CurrentJungleType", 1);
							}
							else
							{
								EncryptedPlayerPrefs.SetInt("PR_CurrentJungleType", 3);
							}
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundSlide);
							}
							yield return null;
							StartNewGame(currentSlot);
							break;
						}
						break;
					case MenuState.SlotSelectMenu:
						hitCollider = InputGUI.GetHitCollider(touchPosition, cameraMain);
						if (!(hitCollider != null))
						{
							break;
						}
						if (slotsBackButton == hitCollider)
						{
							yield return StartCoroutine(OnButtonBackSLotSelectPressed());
						}
						if (overwritingSlot1)
						{
							if (OverwriteSlot1Yes == hitCollider)
							{
								if (sfxOn)
								{
									base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
								}
								ResetPlayerPrefsForNewGame(1);
								currentMenuState = MenuState.StoryMenu;
								storySlideIndex = 0;
								OverwriteSlot1.gameObject.SetActiveRecursively(false);
								OverwriteSlot2.gameObject.SetActiveRecursively(false);
								OverwriteSlot3.gameObject.SetActiveRecursively(false);
								OverwriteSlot4.gameObject.SetActiveRecursively(false);
								overwritingSlot1 = false;
								overwritingSlot2 = false;
								overwritingSlot3 = false;
								overwritingSlot4 = false;
								SlotSelectGroup.gameObject.SetActiveRecursively(false);
								StoryGroup.gameObject.SetActiveRecursively(true);
							}
							else if (OverwriteSlot1No == hitCollider)
							{
								if (sfxOn)
								{
									base.GetComponent<AudioSource>().PlayOneShot(soundMenuClickBack);
								}
								OverwriteSlot1.gameObject.SetActiveRecursively(false);
								overwritingSlot1 = false;
								slotsSlot1Button.gameObject.active = true;
							}
						}
						else if (slotsSlot1Button == hitCollider)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
							}
							if (newGameMode)
							{
								if (maxMissionUnlockedForSlot1 != 0)
								{
									OverwriteSlot1.gameObject.SetActiveRecursively(true);
									overwritingSlot1 = true;
									slotsSlot1Button.gameObject.active = false;
								}
								else
								{
									ResetPlayerPrefsForNewGame(1);
									OverwriteSlot1.gameObject.SetActiveRecursively(false);
									OverwriteSlot2.gameObject.SetActiveRecursively(false);
									OverwriteSlot3.gameObject.SetActiveRecursively(false);
									OverwriteSlot4.gameObject.SetActiveRecursively(false);
									overwritingSlot1 = false;
									overwritingSlot2 = false;
									overwritingSlot3 = false;
									overwritingSlot4 = false;
									currentMenuState = MenuState.StoryMenu;
									storySlideIndex = 0;
									SlotSelectGroup.gameObject.SetActiveRecursively(false);
									StoryGroup.gameObject.SetActiveRecursively(true);
									EncryptedPlayerPrefs.SetInt("PR_CurrentSlot", 1);
								}
							}
							else if (maxMissionUnlockedForSlot1 != 0)
							{
								EncryptedPlayerPrefs.SetInt("PR_CurrentSlot", 1);
								LoadMissionSelect();
							}
						}
						if (overwritingSlot2)
						{
							if (OverwriteSlot2Yes == hitCollider)
							{
								if (sfxOn)
								{
									base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
								}
								ResetPlayerPrefsForNewGame(2);
								OverwriteSlot1.gameObject.SetActiveRecursively(false);
								OverwriteSlot2.gameObject.SetActiveRecursively(false);
								OverwriteSlot3.gameObject.SetActiveRecursively(false);
								OverwriteSlot4.gameObject.SetActiveRecursively(false);
								overwritingSlot1 = false;
								overwritingSlot2 = false;
								overwritingSlot3 = false;
								overwritingSlot4 = false;
								currentMenuState = MenuState.StoryMenu;
								storySlideIndex = 0;
								SlotSelectGroup.gameObject.SetActiveRecursively(false);
								StoryGroup.gameObject.SetActiveRecursively(true);
							}
							else if (OverwriteSlot2No == hitCollider)
							{
								if (sfxOn)
								{
									base.GetComponent<AudioSource>().PlayOneShot(soundMenuClickBack);
								}
								OverwriteSlot2.gameObject.SetActiveRecursively(false);
								overwritingSlot2 = false;
								slotsSlot2Button.gameObject.active = true;
							}
						}
						else if (slotsSlot2Button == hitCollider)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
							}
							if (newGameMode)
							{
								if (maxMissionUnlockedForSlot2 != 0)
								{
									OverwriteSlot2.gameObject.SetActiveRecursively(true);
									overwritingSlot2 = true;
									slotsSlot2Button.gameObject.active = false;
								}
								else
								{
									ResetPlayerPrefsForNewGame(2);
									OverwriteSlot1.gameObject.SetActiveRecursively(false);
									OverwriteSlot2.gameObject.SetActiveRecursively(false);
									OverwriteSlot3.gameObject.SetActiveRecursively(false);
									OverwriteSlot4.gameObject.SetActiveRecursively(false);
									overwritingSlot1 = false;
									overwritingSlot2 = false;
									overwritingSlot3 = false;
									overwritingSlot4 = false;
									currentMenuState = MenuState.StoryMenu;
									storySlideIndex = 0;
									SlotSelectGroup.gameObject.SetActiveRecursively(false);
									StoryGroup.gameObject.SetActiveRecursively(true);
									EncryptedPlayerPrefs.SetInt("PR_CurrentSlot", 2);
								}
							}
							else if (maxMissionUnlockedForSlot2 != 0)
							{
								EncryptedPlayerPrefs.SetInt("PR_CurrentSlot", 2);
								LoadMissionSelect();
							}
						}
						if (overwritingSlot3)
						{
							if (OverwriteSlot3Yes == hitCollider)
							{
								if (sfxOn)
								{
									base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
								}
								ResetPlayerPrefsForNewGame(3);
								OverwriteSlot1.gameObject.SetActiveRecursively(false);
								OverwriteSlot2.gameObject.SetActiveRecursively(false);
								OverwriteSlot3.gameObject.SetActiveRecursively(false);
								OverwriteSlot4.gameObject.SetActiveRecursively(false);
								overwritingSlot1 = false;
								overwritingSlot2 = false;
								overwritingSlot3 = false;
								overwritingSlot4 = false;
								currentMenuState = MenuState.StoryMenu;
								storySlideIndex = 0;
								SlotSelectGroup.gameObject.SetActiveRecursively(false);
								StoryGroup.gameObject.SetActiveRecursively(true);
							}
							else if (OverwriteSlot3No == hitCollider)
							{
								if (sfxOn)
								{
									base.GetComponent<AudioSource>().PlayOneShot(soundMenuClickBack);
								}
								OverwriteSlot3.gameObject.SetActiveRecursively(false);
								overwritingSlot3 = false;
								slotsSlot3Button.gameObject.active = true;
							}
						}
						else if (slotsSlot3Button == hitCollider)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
							}
							if (newGameMode)
							{
								if (maxMissionUnlockedForSlot3 != 0)
								{
									OverwriteSlot3.gameObject.SetActiveRecursively(true);
									overwritingSlot3 = true;
									slotsSlot3Button.gameObject.active = false;
								}
								else
								{
									ResetPlayerPrefsForNewGame(3);
									OverwriteSlot1.gameObject.SetActiveRecursively(false);
									OverwriteSlot2.gameObject.SetActiveRecursively(false);
									OverwriteSlot3.gameObject.SetActiveRecursively(false);
									OverwriteSlot4.gameObject.SetActiveRecursively(false);
									overwritingSlot1 = false;
									overwritingSlot2 = false;
									overwritingSlot3 = false;
									overwritingSlot4 = false;
									currentMenuState = MenuState.StoryMenu;
									storySlideIndex = 0;
									SlotSelectGroup.gameObject.SetActiveRecursively(false);
									StoryGroup.gameObject.SetActiveRecursively(true);
									EncryptedPlayerPrefs.SetInt("PR_CurrentSlot", 3);
								}
							}
							else if (maxMissionUnlockedForSlot3 != 0)
							{
								EncryptedPlayerPrefs.SetInt("PR_CurrentSlot", 3);
								LoadMissionSelect();
							}
						}
						if (overwritingSlot4)
						{
							if (OverwriteSlot4Yes == hitCollider)
							{
								if (sfxOn)
								{
									base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
								}
								ResetPlayerPrefsForNewGame(4);
								OverwriteSlot1.gameObject.SetActiveRecursively(false);
								OverwriteSlot2.gameObject.SetActiveRecursively(false);
								OverwriteSlot3.gameObject.SetActiveRecursively(false);
								OverwriteSlot4.gameObject.SetActiveRecursively(false);
								overwritingSlot1 = false;
								overwritingSlot2 = false;
								overwritingSlot3 = false;
								overwritingSlot4 = false;
								currentMenuState = MenuState.StoryMenu;
								storySlideIndex = 0;
								SlotSelectGroup.gameObject.SetActiveRecursively(false);
								StoryGroup.gameObject.SetActiveRecursively(true);
							}
							else if (OverwriteSlot4No == hitCollider)
							{
								if (sfxOn)
								{
									base.GetComponent<AudioSource>().PlayOneShot(soundMenuClickBack);
								}
								OverwriteSlot4.gameObject.SetActiveRecursively(false);
								overwritingSlot4 = false;
								slotsSlot4Button.gameObject.active = true;
							}
						}
						else
						{
							if (!(slotsSlot4Button == hitCollider))
							{
								break;
							}
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
							}
							if (newGameMode)
							{
								if (maxMissionUnlockedForSlot4 != 0)
								{
									OverwriteSlot4.gameObject.SetActiveRecursively(true);
									overwritingSlot4 = true;
									slotsSlot4Button.gameObject.active = false;
									break;
								}
								ResetPlayerPrefsForNewGame(4);
								OverwriteSlot1.gameObject.SetActiveRecursively(false);
								OverwriteSlot2.gameObject.SetActiveRecursively(false);
								OverwriteSlot3.gameObject.SetActiveRecursively(false);
								OverwriteSlot4.gameObject.SetActiveRecursively(false);
								overwritingSlot1 = false;
								overwritingSlot2 = false;
								overwritingSlot3 = false;
								overwritingSlot4 = false;
								currentMenuState = MenuState.StoryMenu;
								storySlideIndex = 0;
								SlotSelectGroup.gameObject.SetActiveRecursively(false);
								StoryGroup.gameObject.SetActiveRecursively(true);
								EncryptedPlayerPrefs.SetInt("PR_CurrentSlot", 4);
							}
							else if (maxMissionUnlockedForSlot4 != 0)
							{
								EncryptedPlayerPrefs.SetInt("PR_CurrentSlot", 4);
								LoadMissionSelect();
							}
						}
						break;
					case MenuState.OptionsMenu:
						hitCollider = InputGUI.GetHitCollider(touchPosition, cameraMain);
						if (!(hitCollider != null))
						{
							break;
						}
						if (optionsLanguagePickButton.gameObject.active && optionsLanguagePickButton == hitCollider)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
							}
							optionsLanguagePickButton.gameObject.SetActiveRecursively(false);
							optionsLanguagePickGroup.gameObject.SetActiveRecursively(true);
						}
						if (optionsBloodButton == hitCollider)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
							}
							if (bloodOn)
							{
								PlayerPrefs.SetInt("PR_BloodOn", 0);
								bloodOn = false;
								((TextMesh)optionsBloodButton.transform.GetChild(0).GetComponent(typeof(TextMesh))).text = Language.GetTxt("BLOOD") + Language.GetTxt("OFF");
							}
							else
							{
								PlayerPrefs.SetInt("PR_BloodOn", 1);
								bloodOn = true;
								((TextMesh)optionsBloodButton.transform.GetChild(0).GetComponent(typeof(TextMesh))).text = Language.GetTxt("BLOOD") + Language.GetTxt("ON");
							}
						}
						else if (optionsMusicButton == hitCollider)
						{
							if (musicButActive)
							{
								if (sfxOn)
								{
									base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
								}
								if (musicOn)
								{
									PlayerPrefs.SetInt("PR_MusicOn", 0);
									musicOn = false;
									base.GetComponent<AudioSource>().Stop();
									((TextMesh)optionsMusicButton.transform.GetChild(0).GetComponent(typeof(TextMesh))).text = Language.GetTxt("MUSIC") + Language.GetTxt("OFF");
								}
								else
								{
									PlayerPrefs.SetInt("PR_MusicOn", 1);
									musicOn = true;
									base.GetComponent<AudioSource>().Play();
									((TextMesh)optionsMusicButton.transform.GetChild(0).GetComponent(typeof(TextMesh))).text = Language.GetTxt("MUSIC") + Language.GetTxt("ON");
								}
							}
							else if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClickError);
							}
						}
						else if (optionsSfxButton == hitCollider)
						{
							base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
							if (sfxOn)
							{
								PlayerPrefs.SetInt("PR_SfxOn", 0);
								PlayerPrefs.SetInt("PR_MusicOn", 0);
								musicOn = false;
								sfxOn = false;
								musicButActive = false;
								base.GetComponent<AudioSource>().Stop();
								((TextMesh)optionsSfxButton.transform.GetChild(0).GetComponent(typeof(TextMesh))).text = Language.GetTxt("SFX") + Language.GetTxt("OFF");
								((TextMesh)optionsMusicButton.transform.GetChild(0).GetComponent(typeof(TextMesh))).text = Language.GetTxt("MUSIC") + Language.GetTxt("OFF");
							}
							else
							{
								PlayerPrefs.SetInt("PR_SfxOn", 1);
								PlayerPrefs.SetInt("PR_MusicOn", 1);
								musicOn = true;
								musicButActive = true;
								sfxOn = true;
								base.GetComponent<AudioSource>().Play();
								((TextMesh)optionsMusicButton.transform.GetChild(0).GetComponent(typeof(TextMesh))).text = Language.GetTxt("MUSIC") + Language.GetTxt("ON");
								((TextMesh)optionsSfxButton.transform.GetChild(0).GetComponent(typeof(TextMesh))).text = Language.GetTxt("SFX") + Language.GetTxt("ON");
							}
						}
						else if (optionsBackButton == hitCollider)
						{
							yield return StartCoroutine(OnButtonBackOptionsPressed());
						}
						break;
					case MenuState.ExtrasMenu:
						hitCollider = InputGUI.GetHitCollider(touchPosition, cameraMain);
						if (!(hitCollider != null))
						{
							break;
						}
						if (showingCredits && creditsButtonArea == hitCollider)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
							}
							switch (creditsIndex)
							{
							case 1:
								CreditsGroup1.gameObject.SetActiveRecursively(false);
								CreditsGroup2.gameObject.SetActiveRecursively(true);
								textExtrasMenuName.text = Language.GetTxt("CREDITS");
								creditsIndex = 2;
								break;
							case 2:
								CreditsGroup1.gameObject.SetActiveRecursively(false);
								CreditsGroup2.gameObject.SetActiveRecursively(false);
								creditsIndex = 1;
								textExtrasMenuName.text = Language.GetTxt("EXTRAS_WORD");
								showingCredits = false;
								break;
							}
						}
						if (extrasInstructionsButton == hitCollider)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuPanelDisappear);
							}
							yield return StartCoroutine(SlidePanelVertical(extrasSlidePanel, extrasPanelBottom.localPosition, extrasPanelTop.localPosition, slideMenuTime / 2f));
							if (showingCredits)
							{
								CreditsGroup1.gameObject.SetActiveRecursively(false);
								CreditsGroup2.gameObject.SetActiveRecursively(false);
								textExtrasMenuName.text = Language.GetTxt("EXTRAS_WORD");
								creditsIndex = 1;
								showingCredits = false;
							}
							currentMenuState = MenuState.InstructionsMenu;
							InstructionsGroup.SetActiveRecursively(true);
							instructionSlideIndex = 0;
							ExtrasGroup.gameObject.SetActiveRecursively(false);
						}
						else if (extrasCharactersButton == hitCollider)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuPanelDisappear);
							}
							yield return StartCoroutine(SlidePanelVertical(extrasSlidePanel, extrasPanelBottom.localPosition, extrasPanelTop.localPosition, slideMenuTime / 2f));
							if (showingCredits)
							{
								CreditsGroup1.gameObject.SetActiveRecursively(false);
								CreditsGroup2.gameObject.SetActiveRecursively(false);
								textExtrasMenuName.text = Language.GetTxt("EXTRAS_WORD");
								creditsIndex = 1;
								showingCredits = false;
							}
							currentMenuState = MenuState.CharactersMenu;
							ExtrasGroup.gameObject.SetActiveRecursively(false);
							CharactersGroup.SetActiveRecursively(true);
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuPanelAppear);
							}
							yield return StartCoroutine(SlidePanelVertical(charactersSlidePanel, charactersPanelTop.localPosition, charactersPanelBottom.localPosition, slideMenuTime));
						}
						else if (extrasCreditsButton == hitCollider)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
							}
							showingCredits = true;
							CreditsGroup1.gameObject.SetActiveRecursively(true);
							CreditsGroup2.gameObject.SetActiveRecursively(false);
							textExtrasMenuName.text = Language.GetTxt("CREDITS");
							creditsIndex = 1;
						}
						else if (extrasGuerrillaButton == hitCollider)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
							}
							if (showingCredits)
							{
								CreditsGroup1.gameObject.SetActiveRecursively(false);
								CreditsGroup2.gameObject.SetActiveRecursively(false);
								textExtrasMenuName.text = Language.GetTxt("EXTRAS_WORD");
								creditsIndex = 1;
								showingCredits = false;
							}
							Application.OpenURL("https://play.google.com/store/apps/details?id=com.angrymobgames.guerrillabob");
							yield return null;
						}
						else if (extrasOptionsButton == hitCollider)
						{
							if (showingCredits)
							{
								CreditsGroup1.gameObject.SetActiveRecursively(false);
								CreditsGroup2.gameObject.SetActiveRecursively(false);
								textExtrasMenuName.text = Language.GetTxt("EXTRAS_WORD");
								creditsIndex = 1;
								showingCredits = false;
							}
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuPanelDisappear);
							}
							yield return StartCoroutine(SlidePanelVertical(extrasSlidePanel, extrasPanelBottom.localPosition, extrasPanelTop.localPosition, slideMenuTime / 2f));
							OptionsGroup.SetActiveRecursively(true);
							ExtrasGroup.SetActiveRecursively(false);
							currentMenuState = MenuState.OptionsMenu;
							yield return null;
						}
						else if (extrasBackButton == hitCollider)
						{
							yield return StartCoroutine(OnButtonExtrasBackPressed());
						}
						else if (extrasHelsingsFireButton == hitCollider)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
							}
							if (showingCredits)
							{
								CreditsGroup1.gameObject.SetActiveRecursively(false);
								CreditsGroup2.gameObject.SetActiveRecursively(false);
								textExtrasMenuName.text = Language.GetTxt("EXTRAS_WORD");
								creditsIndex = 1;
								showingCredits = false;
							}
							Application.OpenURL("https://play.google.com/store/apps/details?id=com.angrymobgames.muffinknight");
							yield return null;
						}
						break;
					case MenuState.InstructionsMenu:
						switch (instructionSlideIndex)
						{
						case 0:
							InstructionsGroup.SetActiveRecursively(false);
							InstructionsGroup2.SetActiveRecursively(true);
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundSlide);
							}
							instructionSlideIndex++;
							break;
						case 1:
							yield return null;
							currentMenuState = MenuState.ExtrasMenu;
							InstructionsGroup2.SetActiveRecursively(false);
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundSlide);
							}
							ExtrasGroup.SetActiveRecursively(true);
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuPanelAppear);
							}
							yield return StartCoroutine(SlidePanelVertical(extrasSlidePanel, extrasPanelTop.localPosition, extrasPanelBottom.localPosition, slideMenuTime));
							break;
						}
						break;
					case MenuState.CharactersMenu:
						hitCollider = InputGUI.GetHitCollider(touchPosition, cameraMain);
						if (!(hitCollider != null))
						{
							break;
						}
						if (charactersBackButton == hitCollider)
						{
							yield return StartCoroutine(OnBUttonBackCharactersPressed());
						}
						else if (charactersRoyceButton == hitCollider)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
							}
							charactersNameText.text = "Royce";
							CharactersDetailGroup.gameObject.SetActiveRecursively(false);
							charactersRoyceImage.gameObject.active = true;
							charactersDescriptionText.gameObject.active = true;
							if (PlatformDependent.tablet)
							{
								charactersDescriptionText.text = Language.GetTxt("CHARACTER_DESCRIPTION_ROYCE", charactersLengthDescriptionIpad);
							}
							else
							{
								charactersDescriptionText.text = Language.GetTxt("CHARACTER_DESCRIPTION_ROYCE", charactersLengthDescriptionIphone);
							}
						}
						else if (charactersStansButton == hitCollider)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
							}
							charactersNameText.text = "Stans";
							CharactersDetailGroup.gameObject.SetActiveRecursively(false);
							charactersStansImage.gameObject.active = true;
							charactersDescriptionText.gameObject.active = true;
							if (PlatformDependent.tablet)
							{
								charactersDescriptionText.text = Language.GetTxt("CHARACTER_DESCRIPTION_STANS", charactersLengthDescriptionIpad);
							}
							else
							{
								charactersDescriptionText.text = Language.GetTxt("CHARACTER_DESCRIPTION_STANS", charactersLengthDescriptionIphone);
							}
						}
						else if (charactersCuchilloButton == hitCollider)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
							}
							charactersNameText.text = "Cuchillo";
							CharactersDetailGroup.gameObject.SetActiveRecursively(false);
							charactersCuchilloImage.gameObject.active = true;
							charactersDescriptionText.gameObject.active = true;
							if (PlatformDependent.tablet)
							{
								charactersDescriptionText.text = Language.GetTxt("CHARACTER_DESCRIPTION_CUCHILLO", charactersLengthDescriptionIpad);
							}
							else
							{
								charactersDescriptionText.text = Language.GetTxt("CHARACTER_DESCRIPTION_CUCHILLO", charactersLengthDescriptionIphone);
							}
						}
						else if (charactersIsabelleButton == hitCollider)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
							}
							charactersNameText.text = "Isabelle";
							CharactersDetailGroup.gameObject.SetActiveRecursively(false);
							charactersIsabelleImage.gameObject.active = true;
							charactersDescriptionText.gameObject.active = true;
							if (PlatformDependent.tablet)
							{
								charactersDescriptionText.text = Language.GetTxt("CHARACTER_DESCRIPTION_ISABELLE", charactersLengthDescriptionIpad);
							}
							else
							{
								charactersDescriptionText.text = Language.GetTxt("CHARACTER_DESCRIPTION_ISABELLE", charactersLengthDescriptionIphone);
							}
						}
						else if (charactersHanzoButton == hitCollider)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
							}
							charactersNameText.text = "Hanzo";
							CharactersDetailGroup.gameObject.SetActiveRecursively(false);
							charactersHanzoImage.gameObject.active = true;
							charactersDescriptionText.gameObject.active = true;
							if (PlatformDependent.tablet)
							{
								charactersDescriptionText.text = Language.GetTxt("CHARACTER_DESCRIPTION_HANZO", charactersLengthDescriptionIpad);
							}
							else
							{
								charactersDescriptionText.text = Language.GetTxt("CHARACTER_DESCRIPTION_HANZO", charactersLengthDescriptionIphone);
							}
						}
						else if (charactersNikolaiButton == hitCollider)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
							}
							charactersNameText.text = "Nikolai";
							CharactersDetailGroup.gameObject.SetActiveRecursively(false);
							charactersNikolaiImage.gameObject.active = true;
							charactersDescriptionText.gameObject.active = true;
							if (PlatformDependent.tablet)
							{
								charactersDescriptionText.text = Language.GetTxt("CHARACTER_DESCRIPTION_NIKOLAI", charactersLengthDescriptionIpad);
							}
							else
							{
								charactersDescriptionText.text = Language.GetTxt("CHARACTER_DESCRIPTION_NIKOLAI", charactersLengthDescriptionIphone);
							}
						}
						else if (charactersMombasaButton == hitCollider)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
							}
							charactersNameText.text = "Mombasa";
							CharactersDetailGroup.gameObject.SetActiveRecursively(false);
							charactersMombasaImage.gameObject.active = true;
							charactersDescriptionText.gameObject.active = true;
							if (PlatformDependent.tablet)
							{
								charactersDescriptionText.text = Language.GetTxt("CHARACTER_DESCRIPTION_MOMBASA", charactersLengthDescriptionIpad);
							}
							else
							{
								charactersDescriptionText.text = Language.GetTxt("CHARACTER_DESCRIPTION_MOMBASA", charactersLengthDescriptionIphone);
							}
						}
						else if (charactersNolandButton == hitCollider)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
							}
							charactersNameText.text = "Noland";
							CharactersDetailGroup.gameObject.SetActiveRecursively(false);
							charactersNolandImage.gameObject.active = true;
							charactersDescriptionText.gameObject.active = true;
							if (PlatformDependent.tablet)
							{
								charactersDescriptionText.text = Language.GetTxt("CHARACTER_DESCRIPTION_NOLAND", charactersLengthDescriptionIpad);
							}
							else
							{
								charactersDescriptionText.text = Language.GetTxt("CHARACTER_DESCRIPTION_NOLAND", charactersLengthDescriptionIphone);
							}
						}
						else if (charactersFalconerButton == hitCollider)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
							}
							charactersNameText.text = "Falconer";
							CharactersDetailGroup.gameObject.SetActiveRecursively(false);
							charactersFalconerImage.gameObject.active = true;
							charactersDescriptionText.gameObject.active = true;
							if (PlatformDependent.tablet)
							{
								charactersDescriptionText.text = Language.GetTxt("CHARACTER_DESCRIPTION_FALCONER", charactersLengthDescriptionIpad);
							}
							else
							{
								charactersDescriptionText.text = Language.GetTxt("CHARACTER_DESCRIPTION_FALCONER", charactersLengthDescriptionIphone);
							}
						}
						else if (charactersFlusherButton == hitCollider)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
							}
							charactersNameText.text = "Tracker";
							CharactersDetailGroup.gameObject.SetActiveRecursively(false);
							charactersTrackerImage.gameObject.active = true;
							charactersDescriptionText.gameObject.active = true;
							if (PlatformDependent.tablet)
							{
								charactersDescriptionText.text = Language.GetTxt("CHARACTER_DESCRIPTION_TRACKER", charactersLengthDescriptionIpad);
							}
							else
							{
								charactersDescriptionText.text = Language.GetTxt("CHARACTER_DESCRIPTION_TRACKER", charactersLengthDescriptionIphone);
							}
						}
						else if (charactersBerserkerButton == hitCollider)
						{
							if (sfxOn)
							{
								base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
							}
							charactersNameText.text = "Mr. Black";
							CharactersDetailGroup.gameObject.SetActiveRecursively(false);
							charactersMrBlackImage.gameObject.active = true;
							charactersDescriptionText.gameObject.active = true;
							if (PlatformDependent.tablet)
							{
								charactersDescriptionText.text = Language.GetTxt("CHARACTER_DESCRIPTION_MRBLACK", charactersLengthDescriptionIpad);
							}
							else
							{
								charactersDescriptionText.text = Language.GetTxt("CHARACTER_DESCRIPTION_MRBLACK", charactersLengthDescriptionIphone);
							}
						}
						break;
					}
				}
			}
			yield return null;
		}
	}

	private IEnumerator OnButtonBackSLotSelectPressed()
	{
		if (sfxOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(soundMenuClickBack);
		}
		OverwriteSlot1.gameObject.SetActiveRecursively(false);
		OverwriteSlot2.gameObject.SetActiveRecursively(false);
		OverwriteSlot3.gameObject.SetActiveRecursively(false);
		OverwriteSlot4.gameObject.SetActiveRecursively(false);
		overwritingSlot1 = false;
		overwritingSlot2 = false;
		overwritingSlot3 = false;
		overwritingSlot4 = false;
		SlotSelectGroup.gameObject.SetActiveRecursively(false);
		ActivateMainMenu();
		currentMenuState = MenuState.MainMenu;
		if (sfxOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(soundMenuClick);
			base.GetComponent<AudioSource>().PlayOneShot(soundMenuPanelDisappear);
		}
		if (showContinueButton)
		{
			mainMenuContinueButton.gameObject.SetActiveRecursively(true);
			mainMenuNewGameButton.gameObject.SetActiveRecursively(true);
			yield return StartCoroutine(SlidePanelVertical(mainMenuSlidePanel, mainMenuPanelStartTransform.localPosition, mainMenuPanelEndTransform.localPosition, slideMenuTime));
		}
		else
		{
			mainMenuNewGameButton.gameObject.SetActiveRecursively(true);
			mainMenuContinueButton.gameObject.SetActiveRecursively(false);
			yield return StartCoroutine(SlidePanelVertical(mainMenuSlidePanel, mainMenuPanelStartTransform.localPosition, mainMenuPanelEndTransform.localPosition, slideMenuTime));
		}
	}

	private IEnumerator OnBUttonBackCharactersPressed()
	{
		if (sfxOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(soundMenuClickBack);
			base.GetComponent<AudioSource>().PlayOneShot(soundMenuPanelDisappear);
		}
		currentMenuState = MenuState.ExtrasMenu;
		yield return StartCoroutine(SlidePanelVertical(charactersSlidePanel, charactersPanelBottom.localPosition, charactersPanelTop.localPosition, slideMenuTime / 2f));
		CharactersDetailGroup.gameObject.SetActiveRecursively(false);
		CharactersGroup.gameObject.SetActiveRecursively(false);
		ExtrasGroup.gameObject.SetActiveRecursively(true);
		charactersNameText.text = Language.GetTxt("CHARACTERS");
		if (sfxOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(soundMenuPanelAppear);
		}
		yield return StartCoroutine(SlidePanelVertical(extrasSlidePanel, extrasPanelTop.localPosition, extrasPanelBottom.localPosition, slideMenuTime));
	}

	private IEnumerator OnButtonBackOptionsPressed()
	{
		if (sfxOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(soundMenuClickBack);
		}
		optionsLanguagePickGroup.gameObject.SetActiveRecursively(false);
		OptionsGroup.SetActiveRecursively(false);
		ExtrasGroup.gameObject.SetActiveRecursively(true);
		if (sfxOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(soundMenuPanelAppear);
		}
		yield return StartCoroutine(SlidePanelVertical(extrasSlidePanel, extrasPanelTop.localPosition, extrasPanelBottom.localPosition, slideMenuTime));
		currentMenuState = MenuState.ExtrasMenu;
	}

	private IEnumerator OnButtonExtrasBackPressed()
	{
		if (sfxOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(soundMenuClickBack);
			base.GetComponent<AudioSource>().PlayOneShot(soundMenuPanelDisappear);
		}
		yield return StartCoroutine(SlidePanelVertical(extrasSlidePanel, extrasPanelBottom.localPosition, extrasPanelTop.localPosition, slideMenuTime / 2f));
		if (showingCredits)
		{
			CreditsGroup1.gameObject.SetActiveRecursively(false);
			CreditsGroup2.gameObject.SetActiveRecursively(false);
			textExtrasMenuName.text = Language.GetTxt("EXTRAS_WORD");
			creditsIndex = 1;
			showingCredits = false;
		}
		currentMenuState = MenuState.MainMenu;
		ExtrasGroup.gameObject.SetActiveRecursively(false);
		ActivateMainMenu();
		if (sfxOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(soundMenuPanelAppear);
		}
		if (showContinueButton)
		{
			mainMenuContinueButton.gameObject.SetActiveRecursively(true);
			mainMenuNewGameButton.gameObject.SetActiveRecursively(true);
			yield return StartCoroutine(SlidePanelVertical(mainMenuSlidePanel, mainMenuPanelStartTransform.localPosition, mainMenuPanelEndTransform.localPosition, slideMenuTime));
		}
		else
		{
			mainMenuNewGameButton.gameObject.SetActiveRecursively(true);
			mainMenuContinueButton.gameObject.SetActiveRecursively(false);
			yield return StartCoroutine(SlidePanelVertical(mainMenuSlidePanel, mainMenuPanelStartTransform.localPosition, mainMenuPanelEndTransform.localPosition, slideMenuTime));
		}
	}

	public void ScrollListTo(float pos)
	{
		charactersSlideTexts.Translate(0f, 0f, pos);
	}

	private void setActiveOptionsMenu(bool value)
	{
		optionsLanguageButton.SetActiveRecursively(value);
		optionsBackButton.gameObject.SetActiveRecursively(value);
	}

	private void setActiveLanguageMenu(bool value)
	{
		LanguageButton[] array = languageButtons;
		foreach (LanguageButton languageButton in array)
		{
			languageButton.gameObject.SetActiveRecursively(value);
		}
	}

	private void StartNewGame(int slot)
	{
		StartCoroutine(StartMission());
	}

	public void ResetPlayerPrefsForNewGame(int currentSlotParam)
	{
		storyText.text = Language.GetTxt("STORY_SLIDE_1", 45);
		storyTexture1.active = true;
		storyTexture2.active = false;
		storyTexture3.active = false;
		storyTexture4.active = false;
		currentSlot = currentSlotParam;
		EncryptedPlayerPrefs.SetInt("PR_VerticalSplits_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_PlayerKnowsWhipUnlocked_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_CurrentSlot", currentSlot);
		EncryptedPlayerPrefs.SetInt("PR_CurrentMission_S" + currentSlot, 34);
		EncryptedPlayerPrefs.SetInt("PR_LastMissionUnlocked_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_WristLevel_S" + currentSlot, 1);
		EncryptedPlayerPrefs.SetInt("PR_SpearLevel_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_NetGunLevel_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_PlasmaGunLevel_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_DiskLevel_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_WhipLevel_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_MaskLevel_S" + currentSlot, -1);
		EncryptedPlayerPrefs.SetInt("PR_HonorPointsTotalEver_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_HonorPoints_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt(GameConstants.MASK_NUMBER_S + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_MaskType1Unlocked_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_MaskType2Unlocked_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_MaskType3Unlocked_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_MaskType4Unlocked_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_StealthKills_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_OponentsImpaled_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_BodiesSplit_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetFloat("PR_LongestSurvivalTime_S" + currentSlot, 0f);
		EncryptedPlayerPrefs.SetInt("PR_PredatorDeaths_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_NetGunCaptures_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_MaxDiskCombo_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_PlayerKnowsDiscUnlocked_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_PlayerKnowsMaskType1Unlocked_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_PlayerKnowsMaskType2Unlocked_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_PlayerKnowsMaskType3Unlocked_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_PlayerKnowsMaskType4Unlocked_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_PlayerKnowsNetGunUnlocked_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_PlayerKnowsPlasmaUnlocked_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_PlayerKnowsSpearUnlocked_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_PlayerKnowsSurvival1Unlocked_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_PlayerKnowsSurvival2Unlocked_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_PlayerKnowsSurvival3Unlocked_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_Survival1Unlocked_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_Survival2Unlocked_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_NikolaiKills_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_IsabeleKills_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_RoyceKills_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_CuchilloKills_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_HanzoKills_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_StansKills_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_NolanKills_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_SoldierKills_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_MombasaKills_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_BlackPredatorKills_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_BerserkerKills_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_DogKills_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_NikolaiTrophies_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_IsabeleTrophies_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_RoyceTrophies_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_CuchilloTrophies_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_HanzoTrophies_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_StansTrophies_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_NolanTrophies_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_SoldierTrophies_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_MombasaTrophies_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_BlackPredatorTrophies_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_DogTrophies_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_BerserkerTrophies_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_TotalKillsWristblade_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_TotalKillsCombistick_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_TotalKillsPlasmagun_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_TotalKillsNetgun_S" + currentSlot, 0);
		EncryptedPlayerPrefs.SetInt("PR_TotalKillsWhip_S" + currentSlot, 0);
	}
}
