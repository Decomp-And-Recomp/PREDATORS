using System.Collections.Generic;
using UnityEngine;

public class FontManager : MonoBehaviour
{
	public List<SystemLanguage> supportedLanguages;

	public List<Font> correspondingFonts;

	public List<float> correspondingCharSizes;

	public List<float> correspoindingYTransforms;

	public List<Material> correspondingMaterials;

	private Dictionary<TextMesh, float> previousScaleFactor = new Dictionary<TextMesh, float>();

	private Dictionary<TextMesh, float> previousYTransform = new Dictionary<TextMesh, float>();

	public List<TextMesh> allTextMeshes;

	private void Awake()
	{
		ChangeFontAccordingToLanguage(Language.CurrentLang);
	}

	private void MoveTextMeshDown(float amount, TextMesh tm)
	{
	}

	public void ChangeFontAccordingToLanguage(SystemLanguage aLanguage)
	{
		int index = 0;
		for (int i = 0; i < supportedLanguages.Count; i++)
		{
			if (supportedLanguages[i] == aLanguage)
			{
				index = i;
				break;
			}
		}
		foreach (TextMesh allTextMesh in allTextMeshes)
		{
			if ((bool)allTextMesh)
			{
				allTextMesh.font = correspondingFonts[index];
				allTextMesh.GetComponent<Renderer>().material = correspondingMaterials[index];
				float num = correspoindingYTransforms[index] * allTextMesh.transform.localScale.y;
				if (previousScaleFactor.ContainsKey(allTextMesh))
				{
					allTextMesh.characterSize /= previousScaleFactor[allTextMesh];
					MoveTextMeshDown(previousYTransform[allTextMesh], allTextMesh);
				}
				else
				{
					previousScaleFactor.Add(allTextMesh, correspondingCharSizes[index]);
					previousYTransform.Add(allTextMesh, num);
				}
				allTextMesh.characterSize *= correspondingCharSizes[index];
				previousScaleFactor[allTextMesh] = correspondingCharSizes[index];
				MoveTextMeshDown(0f - num, allTextMesh);
				previousYTransform[allTextMesh] = num;
			}
		}
	}
}
