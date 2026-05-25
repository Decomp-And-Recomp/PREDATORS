using System.Collections;
using UnityEngine;

public class SimpleFollowCamera : MonoBehaviour
{
	private const float COORDINATE_X_LANDSCAPE_MARGIN = 18f;

	private float bossDistance = 7f;

	public PlayerController playerController;

	public float distance = 4.8f;

	public float height = 6.5f;

	public float smoothLag = 0.2f;

	public float maxSpeed = 10f;

	public float currentRespawnPointZ;

	public float frontZConstrain = 5000f;

	public float leftXConstraint = -60f;

	public float rightXConstraint = 60f;

	public float volumeFadeSpeed = 1f;

	public LayerMask cameraCullingMaskThermalNoGUI = -131073;

	public LayerMask cameraCullingMaskThermalWithGUI = -131073;

	public Camera pauseCamera;

	public GameObject textBossName;

	public GameObject waveForm;

	public Transform cameraEndTarget;

	public AudioClip soundShowBossName;

	private float zVelocity;

	private float xVelocity;

	private Vector3 newTargetPos;

	private Transform xform;

	private Transform target;

	private float zoomIncrement = 0.05f;

	private float initialLocalScale = 1f;

	private float zoomedInLocalScale = 0.63f;

	private float zoomedInOrthoSize = 2.4f;

	private float initialOrthoSize = 3.810001f;

	private bool zoomedIn;

	private Quaternion initialCameraRotation = Quaternion.identity;

	private bool musicOn;

	private bool sfxOn;

	private void Awake()
	{
		float magnitude = (base.GetComponent<Camera>().ViewportToWorldPoint(Vector3.zero) - base.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1f, 0f, 0f))).magnitude;
		rightXConstraint = 18f - magnitude / 2f;
		leftXConstraint = 0f - rightXConstraint;
		initialCameraRotation = base.transform.rotation;
		xform = base.transform;
		if (!playerController)
		{
			playerController = (PlayerController)GameObject.FindWithTag("Player").GetComponent(typeof(PlayerController));
		}
		target = playerController.transform;
		musicOn = PlayerPrefs.GetInt("PR_MusicOn", 1) == 1;
		sfxOn = PlayerPrefs.GetInt("PR_SfxOn", 1) == 1;
		switch (EncryptedPlayerPrefs.GetInt("PR_CurrentJungleType", 1))
		{
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
			base.GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Predators_JungleMission_part1", typeof(AudioClip));
			break;
		case 7:
		case 8:
		case 9:
		case 10:
		case 11:
			base.GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Predators_NightJungle_part1", typeof(AudioClip));
			break;
		case 12:
		case 13:
		case 14:
			base.GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Predators_JungleMission_part2", typeof(AudioClip));
			break;
		case 15:
			base.GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Predators_NightJungle_part2", typeof(AudioClip));
			break;
		case 16:
		case 17:
			base.GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Predators_BossTrack_part2", typeof(AudioClip));
			break;
		case 18:
		case 19:
		case 20:
		case 21:
			base.GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Predators_NightJungle_part2", typeof(AudioClip));
			break;
		case 22:
		case 23:
		case 24:
			base.GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Predators_BossTrack_part1", typeof(AudioClip));
			break;
		case 25:
			base.GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Predators_JungleMission_part1", typeof(AudioClip));
			break;
		default:
			base.GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Predators_JungleMission_part1", typeof(AudioClip));
			break;
		}
		if (musicOn && sfxOn && (bool)base.GetComponent<AudioSource>().clip)
		{
			base.GetComponent<AudioSource>().Play();
		}
	}

	public void ZoomIn()
	{
		if (!zoomedIn)
		{
			StartCoroutine(ZoomInCR());
		}
	}

	public void FocusOnCharacter(Transform characterTransform)
	{
		StartCoroutine(FocusOnCharacterCR(characterTransform));
	}

	public void FocusOnSceneElement(Transform characterTransform)
	{
		StartCoroutine(FocusOnSceneElementCR(characterTransform));
	}

	private IEnumerator FocusOnSceneElementCR(Transform characterTransform)
	{
		target = characterTransform;
		AManager.instance.CinematicInProgress = true;
		playerController.StopTriangulatingTarget();
		smoothLag *= 2f;
		if (playerController.ThermalVisionMode)
		{
			base.GetComponent<Camera>().cullingMask = cameraCullingMaskThermalNoGUI;
		}
		else
		{
			base.GetComponent<Camera>().cullingMask = -16385;
		}
		while ((cameraEndTarget.position - target.position).sqrMagnitude > bossDistance * bossDistance)
		{
			yield return new WaitForSeconds(0.1f);
		}
		ZoomIn();
		yield return new WaitForSeconds(3f);
		if (playerController.ThermalVisionMode)
		{
			base.GetComponent<Camera>().cullingMask = cameraCullingMaskThermalWithGUI;
		}
		else
		{
			base.GetComponent<Camera>().cullingMask = -1073741825;
		}
		smoothLag /= 2f;
		target = playerController.transform;
		ZoomOut();
		AManager.instance.CinematicInProgress = false;
	}

	private IEnumerator FocusOnCharacterCR(Transform characterTransform)
	{
		target = characterTransform;
		AManager.instance.CinematicInProgress = true;
		playerController.StopTriangulatingTarget();
		smoothLag *= 2f;
		if (playerController.ThermalVisionMode)
		{
			base.GetComponent<Camera>().cullingMask = cameraCullingMaskThermalNoGUI;
		}
		else
		{
			base.GetComponent<Camera>().cullingMask = -16385;
		}
		while ((cameraEndTarget.position - target.position).sqrMagnitude > bossDistance * bossDistance)
		{
			yield return new WaitForSeconds(0.1f);
		}
		ZoomIn();
		if ((bool)pauseCamera)
		{
			bool wasInThermalVisionMode = false;
			PlatformDependent.SetActivePC_MAC(playerController.mouseCursor, false);
			pauseCamera.enabled = true;
			waveForm.gameObject.SetActiveRecursively(true);
			if (!playerController.ThermalVisionMode)
			{
				playerController.SwitchThermalVisionMode();
				base.GetComponent<Camera>().cullingMask = cameraCullingMaskThermalNoGUI;
			}
			else
			{
				wasInThermalVisionMode = true;
			}
			yield return new WaitForSeconds(1f);
			textBossName.gameObject.active = true;
			if (Utils.SfxOn)
			{
				base.GetComponent<AudioSource>().PlayOneShot(soundShowBossName);
			}
			yield return new WaitForSeconds(3f);
			pauseCamera.enabled = false;
			PlatformDependent.SetActivePC_MAC(playerController.mouseCursor, true);
			waveForm.gameObject.SetActiveRecursively(false);
			textBossName.gameObject.active = false;
			if (!wasInThermalVisionMode)
			{
				playerController.SwitchThermalVisionMode();
				base.GetComponent<Camera>().cullingMask = -1073741825;
			}
			yield return new WaitForSeconds(1f);
		}
		if (playerController.ThermalVisionMode)
		{
			base.GetComponent<Camera>().cullingMask = cameraCullingMaskThermalWithGUI;
		}
		else
		{
			base.GetComponent<Camera>().cullingMask = -1073741825;
		}
		smoothLag /= 2f;
		target = playerController.transform;
		ZoomOut();
		AManager.instance.CinematicInProgress = false;
	}

	public void ZoomOut()
	{
		if (zoomedIn)
		{
			StartCoroutine(ZoomOutCR());
		}
	}

	private IEnumerator ZoomInCR()
	{
		float startLocalScale = xform.localScale.x;
		float startOrthoSize = base.GetComponent<Camera>().orthographicSize;
		float t = 0f;
		while (t <= 1f)
		{
			base.GetComponent<Camera>().orthographicSize = Mathf.Lerp(startOrthoSize, zoomedInOrthoSize, t);
			xform.localScale = Mathf.Lerp(startLocalScale, zoomedInLocalScale, t) * Vector3.one;
			t += zoomIncrement;
			yield return null;
		}
		zoomedIn = true;
	}

	private IEnumerator ZoomOutCR()
	{
		float startLocalScale = xform.localScale.x;
		float startOrthoSize = base.GetComponent<Camera>().orthographicSize;
		float t = 0f;
		while (t <= 1f)
		{
			base.GetComponent<Camera>().orthographicSize = Mathf.Lerp(startOrthoSize, initialOrthoSize, t);
			xform.localScale = Mathf.Lerp(startLocalScale, initialLocalScale, t) * Vector3.one;
			t += zoomIncrement * 2f;
			yield return null;
		}
		zoomedIn = false;
	}

	public void StartMusic()
	{
		if ((bool)base.GetComponent<AudioSource>().clip)
		{
			base.GetComponent<AudioSource>().Play();
		}
	}

	public void TurnDownVolume(float fadeTime)
	{
		StartCoroutine(TurnDownVolumeCR(fadeTime));
	}

	public void TurnUpVolume(float fadeTime)
	{
		StartCoroutine(TurnUpVolumeCR(fadeTime));
	}

	private IEnumerator TurnDownVolumeCR(float fadeTime)
	{
		float audioVolume = base.GetComponent<AudioSource>().volume;
		while (base.GetComponent<AudioSource>().volume > 0f)
		{
			base.GetComponent<AudioSource>().volume -= Time.deltaTime * fadeTime;
			yield return null;
		}
		base.GetComponent<AudioSource>().Stop();
		base.GetComponent<AudioSource>().volume = audioVolume;
	}

	private IEnumerator TurnUpVolumeCR(float fadeTime)
	{
		float audioVolume = base.GetComponent<AudioSource>().volume;
		base.GetComponent<AudioSource>().volume = 0f;
		base.GetComponent<AudioSource>().Play();
		while (base.GetComponent<AudioSource>().volume <= audioVolume)
		{
			base.GetComponent<AudioSource>().volume += Time.deltaTime * fadeTime;
			yield return null;
		}
	}

	public void StopMusic()
	{
		if ((bool)base.GetComponent<AudioSource>().clip)
		{
			base.GetComponent<AudioSource>().Stop();
		}
	}

	public void ShakeCamera(float maxIntensity)
	{
		StartCoroutine(ShakeCameraCR(maxIntensity));
	}

	private IEnumerator ShakeCameraCR(float maxIntensity)
	{
		float sign = -1f;
		for (int i = 0; i < 2; i++)
		{
			float rotateAngle = Random.Range(0.2f, maxIntensity) * sign;
			sign *= -1f;
			xform.RotateAround(target.position, Vector3.right, rotateAngle);
			yield return new WaitForSeconds(0.05f);
		}
		xform.rotation = initialCameraRotation;
	}

	private void LateUpdate()
	{
		newTargetPos = target.position - Vector3.forward * distance;
		if (newTargetPos.x < leftXConstraint)
		{
			newTargetPos.x = leftXConstraint;
		}
		if (newTargetPos.x > rightXConstraint)
		{
			newTargetPos.x = rightXConstraint;
		}
		if (newTargetPos.z < currentRespawnPointZ)
		{
			newTargetPos.z = currentRespawnPointZ;
		}
		if (newTargetPos.z > frontZConstrain)
		{
			newTargetPos.z = frontZConstrain;
		}
		newTargetPos.y = target.position.y + height;
		Vector3 vector = newTargetPos;
		newTargetPos.x = Mathf.SmoothDamp(xform.position.x, newTargetPos.x, ref xVelocity, smoothLag, maxSpeed);
		newTargetPos.z = Mathf.SmoothDamp(xform.position.z, newTargetPos.z, ref zVelocity, smoothLag, maxSpeed);
		if (!((double)newTargetPos.x <= 0.0) && !((double)newTargetPos.x >= 0.0))
		{
			newTargetPos.x = vector.x;
		}
		if (!((double)newTargetPos.z <= 0.0) && !((double)newTargetPos.z >= 0.0))
		{
			newTargetPos.z = vector.z;
		}
		xform.position = newTargetPos;
	}
}
