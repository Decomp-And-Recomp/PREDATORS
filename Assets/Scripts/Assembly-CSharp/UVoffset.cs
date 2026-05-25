using UnityEngine;

public class UVoffset : MonoBehaviour
{
	public float scrollSpeed = 0.5f;

	public bool verticalOffset;

	private void Start()
	{
		base.enabled = false;
	}

	private void Update()
	{
		float num = Time.time * scrollSpeed;
		if (verticalOffset)
		{
			base.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0f, num % 1f));
		}
		else
		{
			base.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(num % 1f, 0f));
		}
	}

	private void OnBecameVisible()
	{
		base.enabled = true;
	}

	private void OnBecameInvisible()
	{
		base.enabled = false;
	}
}
