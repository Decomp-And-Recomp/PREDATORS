using System.Collections;
using UnityEngine;

public class EndGameScene : MonoBehaviour
{
	public GUITexture mouseCursor;

	public Transform BackGroundPlane;

	public Transform BackGroundPlanePosition1;

	public Transform BackGroundPlanePosition2;

	public Transform BackGroundPlanePosition3;

	public Transform transformTextInitial;

	public Transform transformTextFinal;

	public TextMesh textStoryEnd;

	public Camera cameraMain;

	public float BackGroundSlideInitialDelay = 2f;

	public float BackGroundSlideTime = 5f;

	public float ShowTextDelay = 1f;

	public float FadeTextAnimTime = 1f;

	public float CanSwitchToMainMenuDelay = 1f;

	public float FadeToRedTime = 10f;

	private bool sfxOn = true;

	private bool canSwitchToMainMenu;

	private void Awake()
	{
		PlatformDependent.HideMouseCursor(mouseCursor);
		PlatformDependent.HandleIphoneKeyboard();
		sfxOn = PlayerPrefs.GetInt("PR_SfxOn", 1) == 1;
		PlatformDependent.SetScreenOrientation(false);
		if (!cameraMain)
		{
			cameraMain = Camera.main;
		}
		if (Language.CurrentLang != SystemLanguage.English)
		{
			textStoryEnd.text = Language.GetTxt("STORY_END", 65);
		}
		else
		{
			textStoryEnd.text = Language.GetTxt("STORY_END");
		}
		StartCoroutine(BloodyAnimation());
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

	private IEnumerator BloodyAnimation()
	{
		BackGroundPlane.gameObject.active = true;
		if (!sfxOn)
		{
			base.GetComponent<AudioSource>().Stop();
		}
		yield return StartCoroutine(SlidePanel(BackGroundPlane, BackGroundPlanePosition1, BackGroundPlanePosition2, BackGroundSlideInitialDelay, false));
		yield return StartCoroutine(SlidePanel(BackGroundPlane, BackGroundPlanePosition2, BackGroundPlanePosition3, BackGroundSlideTime, false));
		yield return new WaitForSeconds(ShowTextDelay);
		yield return StartCoroutine(SlidePanel(textStoryEnd.transform, transformTextInitial, transformTextFinal, FadeTextAnimTime, true));
		yield return new WaitForSeconds(CanSwitchToMainMenuDelay);
		canSwitchToMainMenu = true;
	}

	private void Update()
	{
		PlatformDependent.SetScreenOrientation(true);
		if (InputGUI.touchCount > 0 && canSwitchToMainMenu && InputGUI.GetTouch(0).phase == TouchPhase.Began)
		{
			PlatformDependent.LoadLevelWithLoadingScreen("MissionSelect3D");
		}
	}
}
