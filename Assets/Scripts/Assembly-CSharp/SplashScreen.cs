using System.Collections;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
	public Renderer loadingScreen;

	public TextMesh loadingMessage;

	private void Awake()
	{
		Application.targetFrameRate = 60;
		PlatformDependent.HandleIphoneKeyboard();
		PlatformDependent.SetScreenOrientation(false);
		PlatformDependent.HandleScreenDarken();
	}

	private IEnumerator Start()
	{
		yield return null;
	}
}
