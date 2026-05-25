using UnityEngine;

public class BlobShadowBillboard : MonoBehaviour
{
	public float GroundOffsetPosition = 0.01f;

	private Transform xForm;

	private Transform parentXForm;

	private Quaternion originalRotation;

	private void Awake()
	{
		xForm = base.transform;
		originalRotation = xForm.rotation;
		parentXForm = base.transform.parent;
	}

	private void LateUpdate()
	{
		xForm.position = new Vector3(parentXForm.position.x, GroundOffsetPosition, parentXForm.position.z);
		xForm.rotation = originalRotation;
	}
}
