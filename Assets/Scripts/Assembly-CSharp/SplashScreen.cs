using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Video;

public class SplashScreen : MonoBehaviour
{
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
		yield return StartCoroutine(PlaySplashVideo());
		PlatformDependent.LoadLevelWithLoadingScreen("MainMenu3D_iPad");
	}

	private IEnumerator PlaySplashVideo()
	{
		string videoPath = Path.Combine(Application.streamingAssetsPath, "SplashVid.m4v");
		if (!File.Exists(videoPath))
		{
			yield break;
		}
		Camera cam = Camera.main;
		if (cam == null)
		{
			cam = (Camera)Object.FindObjectOfType(typeof(Camera));
		}
		if (cam == null)
		{
			yield break;
		}
		AudioSource audioSource = base.gameObject.GetComponent<AudioSource>();
		if (audioSource == null)
		{
			audioSource = base.gameObject.AddComponent<AudioSource>();
		}
		audioSource.playOnAwake = false;
		audioSource.spatialBlend = 0f;
		audioSource.volume = 1f;
		audioSource.mute = false;
		audioSource.bypassEffects = false;
		audioSource.bypassListenerEffects = false;
		audioSource.loop = false;
		VideoPlayer vp = base.gameObject.AddComponent<VideoPlayer>();
		vp.playOnAwake = false;
		vp.renderMode = VideoRenderMode.CameraNearPlane;
		vp.targetCamera = cam;
		vp.aspectRatio = VideoAspectRatio.FitInside;
		vp.audioOutputMode = VideoAudioOutputMode.AudioSource;
		vp.url = videoPath;
		vp.isLooping = false;
		bool finished = false;
		bool errored = false;
		vp.loopPointReached += delegate
		{
			finished = true;
		};
		vp.errorReceived += delegate(VideoPlayer src, string msg)
		{
			errored = true;
		};
		vp.Prepare();
		float prepareTimeout = 5f;
		while (!vp.isPrepared && prepareTimeout > 0f && !errored)
		{
			if (Input.anyKeyDown)
			{
				Destroy(vp);
				yield break;
			}
			prepareTimeout -= Time.unscaledDeltaTime;
			yield return null;
		}
		if (!vp.isPrepared || errored)
		{
			Destroy(vp);
			yield break;
		}
		double estimatedLength = (vp.frameRate > 0f) ? ((double)vp.frameCount / vp.frameRate) : 0.0;
		ushort audioTrackCount = vp.audioTrackCount;
		for (ushort i = 0; i < audioTrackCount; i++)
		{
			vp.EnableAudioTrack(i, true);
			vp.SetTargetAudioSource(i, audioSource);
		}
		vp.Play();
		while (!finished && !errored)
		{
			if (Input.anyKeyDown)
			{
				break;
			}
			yield return null;
		}
		vp.Stop();
		Destroy(vp);
	}
}
