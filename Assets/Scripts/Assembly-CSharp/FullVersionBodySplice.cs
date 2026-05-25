using System.Collections;
using UnityEngine;

public class FullVersionBodySplice : MonoBehaviour
{
	public GUITexture mouseCursor;

	public Transform BackGroundPlane;

	public Transform transformCheckTextStart;

	public Transform transformCheckTextEnd;

	public Transform BackGroundPlanePosition1;

	public Transform BackGroundPlanePosition1_zoomed;

	public Transform BackGroundPlanePosition2;

	public Transform BackGroundPlanePosition3;

	public Transform BackGroundPlanePosition4;

	public Transform BackGroundPlanePosition5;

	public Transform transformTextBodySpliceInitial;

	public Transform transformTextGetFullVersionInitial;

	public Transform transformTextNowInitial;

	public Transform transformTextBodySpliceFinal;

	public Transform transformTextGetFullVersionFinal;

	public Transform transformTextNowFinal;

	public Transform backFoldPositionIPhone;

	public Transform backFoldPositionIPad;

	public TextMesh textBodySplice;

	public TextMesh textGetFullVersion;

	public TextMesh textNow;

	public TextMesh textCheckFullVersion;

	public TextMesh textViewTrailer;

	public TextMesh textBack;

	public GameObject BloodSplatLong;

	public GameObject BloodSplatRound;

	public AudioClip soundBloodSplat;

	public AudioClip soundTextHit;

	public Collider AppStoreButton;

	public Collider checkFullVersionButton;

	public Collider viewTrailerButton;

	public Collider backButton;

	public Camera cameraMain;

	private Vector2 touchPosition = Vector2.zero;

	private Collider hitCollider;

	private RaycastHit hitInfo;

	private Ray ray;

	private bool sfxOn = true;

	private void Awake()
	{
		PlatformDependent.HideMouseCursor(mouseCursor);
		PlatformDependent.HandleIphoneKeyboard();
		sfxOn = PlayerPrefs.GetInt("PR_SfxOn", 1) == 1;
		PlatformDependent.SetScreenOrientation(false);
		if (PlatformDependent.tablet)
		{
			backButton.transform.localPosition = backFoldPositionIPad.localPosition;
			backButton.transform.localScale = backFoldPositionIPad.localScale;
		}
		else
		{
			backButton.transform.localPosition = backFoldPositionIPhone.localPosition;
			backButton.transform.localScale = backFoldPositionIPhone.localScale;
		}
		if (!cameraMain)
		{
			cameraMain = Camera.main;
		}
		textGetFullVersion.text = Language.GetTxt("GET_FULL_VERSION");
		textNow.text = Language.GetTxt("NOW");
		textBodySplice.text = Language.GetTxt("DONT_MAKE_US_BODY_SPLICE_YOU");
		textCheckFullVersion.text = Language.GetTxt("CHECK_FULL_VERSION");
		textViewTrailer.text = Language.GetTxt("WATCH_GAMEPLAY_TRAILER");
		textBack.text = Language.GetTxt("BACK");
		StartCoroutine(BloodyAnimation());
	}

	public void LoadMainMenu()
	{
		PlatformDependent.LoadLevelWithLoadingScreen("MainMenu3D_iPad");
	}

	private IEnumerator SlidePanel(Transform panel, Transform startTransform, Transform endTransform, float moveTime, bool fadeAlpha)
	{
		float timer = moveTime;
		panel.position = startTransform.position;
		panel.localScale = startTransform.localScale;
		Color initialColor = Color.red;
		Color finalColor = Color.red;
		if (fadeAlpha)
		{
			initialColor = panel.GetComponent<Renderer>().material.color;
			finalColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
			panel.GetComponent<Renderer>().material.color = finalColor;
			panel.gameObject.active = true;
		}
		while (timer >= 0f)
		{
			panel.position = Vector3.Lerp(endTransform.position, startTransform.position, timer / moveTime);
			panel.localScale = Vector3.Lerp(endTransform.localScale, startTransform.localScale, timer / moveTime);
			if (fadeAlpha)
			{
				panel.GetComponent<Renderer>().material.color = Color.Lerp(initialColor, finalColor, timer / moveTime);
			}
			timer -= Time.deltaTime;
			yield return null;
		}
		panel.position = endTransform.position;
		if (fadeAlpha)
		{
			panel.GetComponent<Renderer>().material.color = initialColor;
		}
	}

	private IEnumerator GrowShrink(Transform panel, Transform startTransform, Transform endTransform, float moveTime)
	{
		float timer3 = moveTime;
		panel.position = startTransform.position;
		panel.localScale = startTransform.localScale;
		while (true)
		{
			timer3 = moveTime;
			while (timer3 >= 0f)
			{
				panel.position = Vector3.Lerp(endTransform.position, startTransform.position, timer3 / moveTime);
				panel.localScale = Vector3.Lerp(endTransform.localScale, startTransform.localScale, timer3 / moveTime);
				timer3 -= Time.deltaTime;
				yield return null;
			}
			panel.position = endTransform.position;
			timer3 = moveTime;
			while (timer3 >= 0f)
			{
				panel.position = Vector3.Lerp(startTransform.position, endTransform.position, timer3 / moveTime);
				panel.localScale = Vector3.Lerp(startTransform.localScale, endTransform.localScale, timer3 / moveTime);
				timer3 -= Time.deltaTime;
				yield return null;
			}
			panel.position = startTransform.position;
		}
	}

	private IEnumerator StartAudio()
	{
		yield return new WaitForSeconds(0.5f);
	}

	private IEnumerator StartMovie()
	{
		yield return null;
		yield return null;
	}

	private IEnumerator BloodyAnimation()
	{
		BackGroundPlane.gameObject.active = true;
		yield return StartCoroutine(SlidePanel(BackGroundPlane, BackGroundPlanePosition1, BackGroundPlanePosition2, 2f, false));
		if (!sfxOn)
		{
			base.GetComponent<AudioSource>().Stop();
		}
		yield return StartCoroutine(SlidePanel(BackGroundPlane, BackGroundPlanePosition2, BackGroundPlanePosition3, 2f, false));
		yield return StartCoroutine(SlidePanel(BackGroundPlane, BackGroundPlanePosition3, BackGroundPlanePosition4, 2f, false));
		yield return StartCoroutine(SlidePanel(BackGroundPlane, BackGroundPlanePosition4, BackGroundPlanePosition5, 3f, false));
		yield return new WaitForSeconds(0.5f);
		if (sfxOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(soundTextHit);
		}
		yield return StartCoroutine(SlidePanel(textBodySplice.transform, transformTextBodySpliceInitial, transformTextBodySpliceFinal, 0.3f, true));
		BloodSplatLong.gameObject.active = true;
		if (sfxOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(soundBloodSplat);
		}
		yield return new WaitForSeconds(0.2f);
		if (sfxOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(soundTextHit);
		}
		yield return StartCoroutine(SlidePanel(textGetFullVersion.transform, transformTextGetFullVersionInitial, transformTextGetFullVersionFinal, 0.3f, true));
		yield return new WaitForSeconds(0.1f);
		if (sfxOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(soundTextHit);
		}
		yield return StartCoroutine(SlidePanel(textNow.transform, transformTextNowInitial, transformTextNowFinal, 0.2f, true));
		BloodSplatRound.gameObject.active = true;
		if (sfxOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(soundBloodSplat);
		}
		yield return new WaitForSeconds(2f);
		AppStoreButton.gameObject.active = true;
		yield return new WaitForSeconds(0.2f);
		checkFullVersionButton.gameObject.active = true;
		textCheckFullVersion.gameObject.active = true;
		yield return new WaitForSeconds(0.2f);
		textViewTrailer.gameObject.active = true;
		viewTrailerButton.gameObject.active = true;
		StartCoroutine(GrowShrink(textCheckFullVersion.transform, transformCheckTextStart, transformCheckTextEnd, 2f));
		yield return new WaitForSeconds(1f);
		float timer = 5f;
		float fadeTime = 5f;
		Color initialColor = BackGroundPlane.GetComponent<Renderer>().material.color;
		Color finalColor = new Color(0.64705884f, 0f, 0f);
		while (timer >= 0f)
		{
			BackGroundPlane.GetComponent<Renderer>().material.color = Color.Lerp(finalColor, initialColor, timer / fadeTime);
			timer -= Time.deltaTime;
			yield return null;
		}
		BackGroundPlane.GetComponent<Renderer>().material.color = finalColor;
	}

	private void Update()
	{
		PlatformDependent.UpdateMouseCursorGUITexture(mouseCursor);
		PlatformDependent.SetScreenOrientation(true);
		if (InputGUI.touchCount <= 0 || InputGUI.GetTouch(0).phase != 0)
		{
			return;
		}
		hitCollider = InputGUI.GetHitCollider(touchPosition, cameraMain);
		if (hitCollider != null && !(hitCollider == AppStoreButton) && !(hitCollider == checkFullVersionButton))
		{
			if (hitCollider == viewTrailerButton)
			{
				StartCoroutine(StartMovie());
			}
			else if (hitCollider == backButton)
			{
				LoadMainMenu();
			}
		}
	}
}
