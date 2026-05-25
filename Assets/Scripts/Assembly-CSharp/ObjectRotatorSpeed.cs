using UnityEngine;

public class ObjectRotatorSpeed : MonoBehaviour
{
	public float speed = 5f;

	private void Update()
	{
		base.transform.Rotate(0f, 0f, (0f - speed) * Time.deltaTime);
	}
}
