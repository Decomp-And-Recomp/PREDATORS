using UnityEngine;

public class Whip : MonoBehaviour
{
	public Transform[] whipBezierPoints;

	public LineRenderer whipLineRenderer;

	public int whipSegmentCount = 6;

	private Transform grappeledEnemy;

	private float increaseAmount;

	private float t;

	private Vector3[] intermediateSegments;

	private Vector3[] tempControlPointsPositions;

	private Vector3[] tempControlPointsPositions2;

	private Vector3[] tempControlPointsPositions3;

	private int whipBezierPointsLength;

	private bool enemyIsGrappeled;

	private bool whipVisible = true;

	private Animation anim;

	public Transform GrappeledEnemy
	{
		set
		{
			if (value != null)
			{
				enemyIsGrappeled = true;
				grappeledEnemy = value;
			}
			else
			{
				enemyIsGrappeled = false;
			}
		}
	}

	public void CrossFadeAnim(string animationName, float crossFadeTime)
	{
		anim.Stop();
		anim[animationName].time = 0f;
		anim.CrossFade(animationName, crossFadeTime);
	}

	public void ShowWhip()
	{
		whipVisible = true;
		whipLineRenderer.gameObject.active = true;
	}

	public void HideWhip()
	{
		whipVisible = false;
		whipLineRenderer.gameObject.active = false;
	}

	private void Start()
	{
		intermediateSegments = new Vector3[whipSegmentCount];
		tempControlPointsPositions = new Vector3[whipSegmentCount];
		tempControlPointsPositions2 = new Vector3[whipSegmentCount];
		tempControlPointsPositions3 = new Vector3[whipSegmentCount];
		increaseAmount = 1f / (float)whipSegmentCount;
		whipLineRenderer.SetVertexCount(whipSegmentCount + 1);
		whipBezierPointsLength = whipBezierPoints.Length;
		SetAnimationWrapMode();
	}

	public void SetAnimationWrapMode()
	{
		if (!anim)
		{
			anim = base.GetComponent<Animation>();
		}
		if ((bool)anim)
		{
			anim.wrapMode = WrapMode.Once;
			anim["whip_grapple_dash_pose"].wrapMode = WrapMode.ClampForever;
			anim["whip_idle"].wrapMode = WrapMode.Loop;
			anim["whip_attack_heavy_R_charge"].wrapMode = WrapMode.Once;
			anim["whip_attack_heavy_R_charge"].speed = 0.5f;
			anim["whip_retract"].wrapMode = WrapMode.Loop;
			anim["whip_attack_light_R"].wrapMode = WrapMode.Once;
		}
		else
		{
			Debug.LogError("WhipWeapon Animation not set right");
		}
	}

	private Vector3 PointOnBezierCurveAt(float t, Vector3[] bezierControlPointsPositions)
	{
		intermediateSegments[0] = bezierControlPointsPositions[0];
		intermediateSegments[whipBezierPointsLength - 1] = bezierControlPointsPositions[whipBezierPointsLength - 1];
		for (int i = 1; i < whipBezierPointsLength - 1; i++)
		{
			intermediateSegments[i] = bezierControlPointsPositions[i];
		}
		for (int j = 1; j < whipBezierPointsLength; j++)
		{
			for (int k = 0; k < whipBezierPointsLength - j; k++)
			{
				intermediateSegments[k] = (1f - t) * intermediateSegments[k] + t * intermediateSegments[k + 1];
			}
		}
		return intermediateSegments[0];
	}

	private void Update()
	{
		if (whipVisible)
		{
			whipLineRenderer.SetPosition(0, whipBezierPoints[0].position);
			whipLineRenderer.SetPosition(whipSegmentCount, whipBezierPoints[whipBezierPointsLength - 1].position);
			t = increaseAmount;
			int num = 1;
			for (int i = 0; i < whipBezierPointsLength; i++)
			{
				tempControlPointsPositions[i] = whipBezierPoints[i].position;
			}
			while (t < 1f)
			{
				whipLineRenderer.SetPosition(num, PointOnBezierCurveAt(t, tempControlPointsPositions));
				num++;
				t += increaseAmount;
			}
		}
	}

	private void LateUpdate()
	{
		if (whipVisible && enemyIsGrappeled)
		{
			if ((bool)grappeledEnemy)
			{
				whipBezierPoints[whipBezierPointsLength - 1].position = grappeledEnemy.position + Vector3.up;
			}
			else
			{
				Debug.LogError("Enemy was grappeled, but no transform was set");
			}
		}
	}
}
