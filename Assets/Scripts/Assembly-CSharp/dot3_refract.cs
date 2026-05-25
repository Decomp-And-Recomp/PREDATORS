using UnityEngine;

public class dot3_refract : MonoBehaviour
{
	public Material refract_material;

	public float offsetSpeed = 0.025f;

	private void Start()
	{
		base.GetComponent<Renderer>().material = refract_material;
	}

	private void Update()
	{
		float num = Mathf.Repeat(Time.time * offsetSpeed, 1f);
		refract_material.SetTextureOffset("_NormalMap", new Vector2(0f - num, num));
		refract_material.SetTextureOffset("_NormalMap2", new Vector2(num, 0f - num));
	}
}
