using System.Collections;
using UnityEngine;

public class Falcon : MonoBehaviour
{
	private const float maxShadowScale = 3f;

	private const float xZDistanceFromPredator = 10f;

	private const float heightFromGround = 10f;

	private const float explodeRadius = 1.5f;

	private const float birdSpeed = 1.5f;

	private const float blobScaleMax = 0.4f;

	public AudioClip soundHitPlayer;

	public AudioClip soundBirdFly;

	public AudioClip soundProximityCircleShow;

	public Transform blobShadow;

	public Transform birdBody;

	public Transform proximityCircle;

	private bool demoAttack;

	private Vector3 localScale;

	private Transform xForm;

	private Transform predatorTransform;

	private Vector3 vecBirdPredator;

	private Vector3 vectorFalconStartPosition;

	private Vector3 proximityCircleInitialScale = Vector3.zero;

	private float horizontalDistanceToPredator;

	private AttackInfo attackInfoFalcon;

	private float sqrExplodeRadius;

	private void Awake()
	{
		xForm = base.transform;
		attackInfoFalcon = new AttackInfo(90f);
	}

	public void Activate(Transform target, bool aDemoAttack)
	{
		demoAttack = aDemoAttack;
		if (!base.gameObject.active)
		{
			proximityCircleInitialScale = proximityCircle.localScale;
			predatorTransform = target;
			sqrExplodeRadius = 2.25f;
			base.gameObject.active = true;
			StopAllCoroutines();
			StartCoroutine(Dive());
		}
	}

	public void Deactivate()
	{
		if (base.gameObject.active)
		{
			StopAllCoroutines();
			base.gameObject.SetActiveRecursively(false);
			blobShadow.gameObject.active = false;
			proximityCircle.gameObject.SetActiveRecursively(false);
			proximityCircle.localScale = proximityCircleInitialScale;
		}
	}

	private IEnumerator ShrinkProximityCircle()
	{
		proximityCircle.position = new Vector3(predatorTransform.position.x, proximityCircle.position.y, predatorTransform.position.z);
		if (!demoAttack)
		{
			proximityCircle.gameObject.SetActiveRecursively(true);
		}
		if (Utils.SfxOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(soundProximityCircleShow);
		}
		blobShadow.localScale = Vector3.zero;
		localScale = proximityCircle.localScale;
		while (proximityCircle.localScale.x > 2f)
		{
			localScale.x -= 0.03f;
			localScale.z -= 0.03f;
			proximityCircle.localScale = localScale;
			yield return null;
		}
	}

	private IEnumerator Dive()
	{
		StartCoroutine(ShrinkProximityCircle());
		yield return new WaitForSeconds(0.5f);
		if (Utils.SfxOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(soundBirdFly);
		}
		Vector3 onUnitSphere = Random.onUnitSphere;
		onUnitSphere.y = 0f;
		onUnitSphere.Normalize();
		vectorFalconStartPosition = onUnitSphere * 10f + proximityCircle.position;
		vectorFalconStartPosition.y = 10f;
		xForm.position = vectorFalconStartPosition;
		vecBirdPredator = proximityCircle.position - xForm.position;
		Quaternion initialRotation = birdBody.rotation;
		birdBody.rotation = Quaternion.LookRotation(vecBirdPredator, birdBody.up);
		horizontalDistanceToPredator = 10f;
		float horizontalVectorMagnitude = new Vector2(vecBirdPredator.x, vecBirdPredator.z).magnitude;
		float blobShadowScale = 0f;
		Vector3 blobShadowPos = blobShadow.position;
		birdBody.gameObject.SetActiveRecursively(true);
		blobShadow.gameObject.active = true;
		while (vecBirdPredator.y <= 40f)
		{
			if (proximityCircle.gameObject.active)
			{
				if (proximityCircle.localScale.x > 1f)
				{
					localScale.x -= 0.03f;
					localScale.z -= 0.03f;
					proximityCircle.localScale = localScale;
				}
				else
				{
					proximityCircle.gameObject.SetActiveRecursively(false);
					proximityCircle.localScale = proximityCircleInitialScale;
				}
			}
			birdBody.rotation = Quaternion.LookRotation(vecBirdPredator, birdBody.up);
			xForm.Translate(vecBirdPredator * Time.deltaTime * 1.5f);
			horizontalDistanceToPredator -= horizontalVectorMagnitude * Time.deltaTime * 1.5f;
			vecBirdPredator.y = (0f - horizontalDistanceToPredator) * 1.8f;
			blobShadowScale = (1f - Mathf.Clamp(xForm.position.y, 0f, 10f) / 10f) * 0.4f;
			blobShadowPos.x = xForm.position.x;
			blobShadowPos.z = xForm.position.z;
			blobShadow.position = blobShadowPos;
			blobShadow.localScale = Vector3.one * Mathf.Clamp(blobShadowScale, 0f, 0.4f);
			if (vecBirdPredator.y < 1f && (predatorTransform.position - xForm.position).sqrMagnitude < sqrExplodeRadius)
			{
				if (Utils.SfxOn)
				{
					base.GetComponent<AudioSource>().PlayOneShot(soundHitPlayer);
				}
				Transform explosionEffect = AManager.instance.GetPoolObject(AManager.PoolObjectType.ExplosionPlasmaGunYellow);
				if ((bool)explosionEffect)
				{
					explosionEffect.position = predatorTransform.position + Vector3.up;
					explosionEffect.rotation = xForm.rotation;
					explosionEffect.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
				}
				Vector3 attackPosition = xForm.position;
				attackPosition.y = 0f;
				attackInfoFalcon.AttackerPosition = attackPosition;
				predatorTransform.SendMessage("ApplyDamage", attackInfoFalcon, SendMessageOptions.DontRequireReceiver);
				proximityCircle.gameObject.active = false;
				blobShadow.gameObject.active = false;
				break;
			}
			yield return null;
		}
		birdBody.rotation = initialRotation;
		Deactivate();
	}
}
