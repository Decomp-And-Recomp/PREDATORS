using UnityEngine;

public class TriangleTarget : MonoBehaviour
{
	public float rotateSpeed = 5f;

	public Vector3 possitionOffset = Vector3.up;

	private Transform xForm;

	public Transform targetXForm;

	public bool predatorTriangle;

	public void SetTarget(Transform newTarget)
	{
		targetXForm = newTarget.transform;
		if (!predatorTriangle)
		{
		}
	}

	private void Start()
	{
		if (predatorTriangle)
		{
		}
		xForm = base.transform;
	}

	public void PlayTriangulateAnimation()
	{
	}

	private void LateUpdate()
	{
		xForm.position = targetXForm.position + possitionOffset;
		xForm.Rotate(0f, (0f - rotateSpeed) * Time.deltaTime, 0f);
	}
}
