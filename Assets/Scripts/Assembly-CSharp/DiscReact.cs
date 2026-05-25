using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DiscReact : MonoBehaviour
{
	public float returnToPlayerSecs = 1f;

	public AudioClip soundBounceEnvironment;

	public AudioClip soundReturnToPlayer;

	private Transform xForm;

	private Transform playerXForm;

	private bool hitEnvironment;

	private void Awake()
	{
		xForm = base.transform;
		playerXForm = GameObject.FindWithTag("Player").transform;
	}

	private void OnEnable()
	{
		base.GetComponent<Animation>().Play();
		StopCoroutine("ReturnToPlayer");
		hitEnvironment = false;
		StartCoroutine("ReturnToPlayer");
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == 8)
		{
			hitEnvironment = true;
			if (Utils.SfxOn)
			{
				AudioSource.PlayClipAtPoint(soundBounceEnvironment, xForm.position);
			}
		}
	}

	private IEnumerator ReturnToPlayer()
	{
		float cTime = returnToPlayerSecs;
		while (cTime > 0f && !hitEnvironment)
		{
			cTime -= 2f * Time.deltaTime;
			yield return null;
		}
		base.GetComponent<Rigidbody>().velocity = Vector3.zero;
		float timerThrow = (xForm.position - playerXForm.position).magnitude;
		cTime = timerThrow;
		Vector3 startPos = xForm.position;
		float trajectoryHeight = 3f * timerThrow / 11f;
		if (UnityEngine.Random.value > 0.5f)
		{
			trajectoryHeight *= -1f;
		}
		if (Utils.SfxOn)
		{
			AudioSource.PlayClipAtPoint(soundReturnToPlayer, xForm.position);
		}
		while (cTime > 0f)
		{
			cTime -= 20f * Time.deltaTime;
			Vector3 currentPos = Vector3.Lerp(playerXForm.position, startPos, cTime / timerThrow);
			currentPos.x += trajectoryHeight * Mathf.Sin(cTime / timerThrow * (float)Math.PI);
			xForm.position = currentPos;
			yield return null;
		}
		base.gameObject.active = false;
	}
}
