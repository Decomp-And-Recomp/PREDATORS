using System.Collections;
using UnityEngine;

public class EmptySceneLoader : MonoBehaviour
{
	public TextMesh textLoading;

	public static string sceneToLoad = "MainMenu3D_iPad";

	private void Awake()
	{
		if ((bool)textLoading)
		{
			textLoading.text = Language.GetTxt("LOADING");
		}
		PlatformDependent.SetScreenOrientation(false);
		PlatformDependent.HandleScreenDarken();
	}

	private IEnumerator OnLevelWasLoaded()
	{
		yield return null;
		Application.LoadLevel(sceneToLoad);
	}
}
