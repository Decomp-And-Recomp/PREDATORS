using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Material))]
public class SwitchEmissiveColor : MonoBehaviour
{
	private Color originalColor;

	private void Start()
	{
		originalColor = base.GetComponent<Renderer>().material.GetColor("_Color");
	}

	public void SwitchEmissiveColorMethod()
	{
		StartCoroutine(SwitchColorDamage());
	}

	private IEnumerator SwitchColorDamage()
	{
		base.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
		yield return new WaitForSeconds(0.1f);
		base.GetComponent<Renderer>().material.SetColor("_Color", originalColor);
	}
}
