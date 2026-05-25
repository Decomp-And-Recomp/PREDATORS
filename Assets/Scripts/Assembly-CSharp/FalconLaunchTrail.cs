using System;
using System.Collections;
using UnityEngine;

public class FalconLaunchTrail : MonoBehaviour
{
	private const float speed = 2f;

	private const float maxDistortAmount = 5f;

	private const float radius = 2f;

	private const float maxHeight = 20f;

	private Transform xForm;

	private Vector3 randomDistortDirection = Vector3.right;

	private void OnEnable()
	{
		xForm = base.transform;
		StartCoroutine(GoUp3());
	}

	private IEnumerator GoUp3()
	{
		Vector3 initialPosition = xForm.position;
		Vector3 randomOnUnitCircle = UnityEngine.Random.onUnitSphere;
		randomOnUnitCircle.y = 0f;
		randomOnUnitCircle.Normalize();
		randomDistortDirection = Vector3.Cross(Vector3.up, randomOnUnitCircle);
		randomDistortDirection.Normalize();
		randomDistortDirection *= 5f;
		Vector3 amountToDecrease = -randomDistortDirection;
		while (xForm.position.y < 20f)
		{
			xForm.Translate((Vector3.up * 10f + randomDistortDirection) * Time.deltaTime * 2f);
			randomDistortDirection += amountToDecrease * Time.deltaTime;
			yield return null;
		}
		xForm.position = initialPosition;
		base.gameObject.active = false;
	}

	private IEnumerator GoUp()
	{
		Vector3 initialPosition = xForm.position;
		yield return new WaitForSeconds(UnityEngine.Random.value / 2f);
		while (xForm.position.y < 5f)
		{
			Vector3 randomDir = UnityEngine.Random.onUnitSphere;
			randomDir.y = 0f;
			xForm.position += (randomDir + Vector3.up) * 2f;
			yield return null;
		}
		xForm.position = initialPosition;
		base.gameObject.active = false;
	}

	private IEnumerator GoUp2()
	{
		float bombTrajectoryHeight = 2f;
		float bombTimerThrowArcIncrement = 1f;
		Vector3 initialTargetPosition2 = UnityEngine.Random.onUnitSphere;
		initialTargetPosition2.y = 0f;
		initialTargetPosition2.Normalize();
		initialTargetPosition2 = xForm.position + Vector3.up * 5f + initialTargetPosition2 * 2f;
		xForm.rotation = new Quaternion(0.1f, -0.7f, -0.7f, -0.1f);
		float timerThrow = (xForm.position - initialTargetPosition2).magnitude;
		float cTime = timerThrow;
		Vector3 startPos = xForm.position;
		float trajectoryHeight = bombTrajectoryHeight * timerThrow / 5f;
		while (cTime > 0f)
		{
			cTime -= bombTimerThrowArcIncrement * Time.deltaTime;
			Vector3 currentPos = Vector3.Lerp(initialTargetPosition2, startPos, cTime / timerThrow);
			currentPos.y += trajectoryHeight * Mathf.Sin(cTime / timerThrow * (float)Math.PI);
			if (currentPos.y < 0.007f)
			{
				currentPos.y = 0.007f;
			}
			xForm.position = currentPos;
			yield return null;
		}
	}
}
