using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
	public float speed = 4f;

	private void Update()
	{
		Plane plane = new Plane(Vector3.up, base.transform.position);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float enter = 0f;
		if (plane.Raycast(ray, out enter))
		{
			Vector3 point = ray.GetPoint(enter);
			Quaternion to = Quaternion.LookRotation(point - base.transform.position);
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, to, speed * Time.deltaTime);
		}
	}
}
