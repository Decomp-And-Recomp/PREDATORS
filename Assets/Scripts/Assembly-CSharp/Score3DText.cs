using System.Collections;
using UnityEngine;

public class Score3DText : MonoBehaviour
{
	public float timerStartFading = 1f;

	public float moveIncrement = 0.01f;

	public float timerFade = 0.3f;

	private Transform xForm;

	private Material textMaterial;

	private TextMesh textMesh;

	private void Awake()
	{
		xForm = base.transform;
		textMaterial = base.GetComponent<Renderer>().material;
		textMesh = (TextMesh)GetComponent(typeof(TextMesh));
		if (!textMesh)
		{
			Debug.LogError("no textMesh found on 3D text");
		}
	}

	public void SetTextValue(int value)
	{
		textMesh.text = "+" + value;
	}

	private IEnumerator MoveUp()
	{
		float currentTimer = timerFade;
		while (currentTimer > 0f)
		{
			xForm.position = new Vector3(xForm.position.x, xForm.position.y + moveIncrement, xForm.position.z);
			textMaterial.SetColor("_Color", new Color(1f, 1f, 1f, 1f - currentTimer / timerFade));
			currentTimer -= Time.deltaTime;
			yield return null;
		}
		while (currentTimer < timerStartFading)
		{
			xForm.position = new Vector3(xForm.position.x, xForm.position.y + moveIncrement, xForm.position.z);
			currentTimer += Time.deltaTime;
			yield return null;
		}
		currentTimer = timerFade;
		while (currentTimer > 0f)
		{
			xForm.position = new Vector3(xForm.position.x, xForm.position.y + moveIncrement, xForm.position.z);
			textMaterial.SetColor("_Color", new Color(1f, 1f, 1f, currentTimer / timerFade));
			currentTimer -= Time.deltaTime;
			yield return null;
		}
		base.gameObject.active = false;
	}

	public void OnEnable()
	{
		StartCoroutine(MoveUp());
	}
}
