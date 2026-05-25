using System;
using System.Collections;
using UnityEngine;

public class XperiaPhone : MonoBehaviour
{
	public PlayerController playerController;

	private AndroidJavaObject currentConfig;

	public bool phoneSlided;

	private void Awake()
	{
		if ((SystemInfo.deviceModel.Contains("Sony Ericsson") && SystemInfo.deviceModel.Contains("R800")) || SystemInfo.deviceModel.Contains("Z1i"))
		{
			StartCoroutine(CheckForPhoneSlidedStatus(OnPhoneSlidOpen, OnPhoneSlidClosed));
			playerController.hud.buttonPause.pollForKey = KeyCode.Menu;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void OnPhoneSlidOpen(object sender, EventArgs ea)
	{
		playerController.hud.AttackStick.GetComponent<Renderer>().enabled = false;
		playerController.hud.BlockStick.GetComponent<Renderer>().enabled = false;
		playerController.hud.LeftStick.GetComponent<Renderer>().enabled = false;
		playerController.hud.LeftControlPad.GetComponent<Renderer>().enabled = false;
		playerController.inputDevice = PlayerController.InputDevice.XperiaPlay;
	}

	private void OnPhoneSlidClosed(object sender, EventArgs ea)
	{
		playerController.hud.AttackStick.GetComponent<Renderer>().enabled = true;
		playerController.hud.BlockStick.GetComponent<Renderer>().enabled = true;
		playerController.hud.LeftStick.GetComponent<Renderer>().enabled = true;
		playerController.hud.LeftControlPad.GetComponent<Renderer>().enabled = true;
		playerController.inputDevice = PlayerController.InputDevice.Touch;
	}

	private void InitAndroidConfigLink()
	{
		using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			currentConfig = @static.Call<AndroidJavaObject>("getResources", new object[0]).Call<AndroidJavaObject>("getConfiguration", new object[0]);
		}
	}

	public IEnumerator CheckForPhoneSlidedStatus(EventHandler phoneSlidOpen, EventHandler phoneSlidClose)
	{
		InitAndroidConfigLink();
		while (true)
		{
			int nav = currentConfig.Get<int>("navigationHidden");
			if (nav == 2 || nav == 0)
			{
				if (phoneSlided && phoneSlidClose != null)
				{
					phoneSlidClose(null, null);
				}
				phoneSlided = false;
			}
			else
			{
				if (!phoneSlided && phoneSlidOpen != null)
				{
					phoneSlidOpen(null, null);
				}
				phoneSlided = true;
			}
			yield return new WaitForSeconds(1f);
		}
	}
}
