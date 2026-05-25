using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FullVersionDetails : MonoBehaviour
{
	public GameObject Slide1Background;

	public GameObject Slide2Background;

	public GameObject Slide3Background;

	public Collider nextButton;

	public TextMesh nextButtonText;

	public TextMesh textSlide1;

	public TextMesh textTitle1;

	public TextMesh textSlide2;

	public TextMesh textSlide2Weapons;

	public TextMesh textSlide3;

	public Collider AppStoreButton;

	public AudioClip clickSound;

	private bool slide1Menu = true;

	private bool slide2Menu;

	private bool slide3Menu;

	public Camera cameraMain;

	private Collider hitCollider;

	private RaycastHit hitInfo;

	private Ray ray;

	private void Awake()
	{
		PlatformDependent.HandleIphoneKeyboard();
		PlatformDependent.SetScreenOrientation(false);
		if (!cameraMain)
		{
			cameraMain = Camera.main;
		}
		nextButton.gameObject.active = true;
		nextButtonText.gameObject.active = true;
		nextButtonText.text = Language.GetTxt("NEXT");
		textSlide1.text = Language.GetTxt("FULL_VERSION_FEATURES1", 80);
		textTitle1.text = Language.GetTxt("FULL_VERSION_TITLE1");
		textSlide2.text = Language.GetTxt("FULL_VERSION_FEATURES2", 80);
		textSlide2Weapons.text = Language.GetTxt("FULL_VERSION_WEAPONS", 80);
		textSlide3.text = Language.GetTxt("FULL_VERSION_FEATURES3", 75);
	}

	private void Update()
	{
		PlatformDependent.SetScreenOrientation(true);
		if (InputGUI.touchCount <= 0)
		{
			return;
		}
		TouchGUI touch = InputGUI.GetTouch(0);
		if (touch.phase != 0)
		{
			return;
		}
		hitCollider = InputGUI.GetHitCollider(touch.position, cameraMain);
		if (hitCollider != null && !(hitCollider == AppStoreButton) && hitCollider == nextButton)
		{
			if (slide1Menu)
			{
				base.GetComponent<AudioSource>().PlayOneShot(clickSound);
				slide1Menu = false;
				slide2Menu = true;
				slide3Menu = false;
				Slide1Background.gameObject.SetActiveRecursively(false);
				Slide2Background.gameObject.SetActiveRecursively(true);
				Slide3Background.gameObject.SetActiveRecursively(false);
			}
			else if (slide2Menu)
			{
				base.GetComponent<AudioSource>().PlayOneShot(clickSound);
				slide1Menu = false;
				slide2Menu = false;
				slide3Menu = true;
				Slide1Background.gameObject.SetActiveRecursively(false);
				Slide2Background.gameObject.SetActiveRecursively(false);
				Slide3Background.gameObject.SetActiveRecursively(true);
				nextButtonText.text = Language.GetTxt("CHECK_FULL_VERSION");
			}
			else if (slide3Menu)
			{
				base.GetComponent<AudioSource>().PlayOneShot(clickSound);
			}
		}
	}
}
