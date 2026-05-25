using UnityEngine;

public class AnimationDebug : MonoBehaviour
{
	public GUIText text;

	public string[] animationNames;

	public bool show = true;

	private static string FloatToProgressbar(float value)
	{
		string text = string.Empty;
		int num = (int)(value * 10f);
		num = ((num <= 9) ? num : 9);
		for (int i = 0; i < 10; i++)
		{
			text += ((i != num) ? " " : "|");
		}
		return text;
	}

	private void Update()
	{
		if (this.text == null)
		{
			return;
		}
		if (!show)
		{
			this.text.text = string.Empty;
			return;
		}
		string text = string.Empty;
		if (animationNames != null)
		{
			text += "E Wght Time Time           L A Name\n";
			string[] array = animationNames;
			foreach (string text2 in array)
			{
				AnimationState animationState = base.GetComponent<Animation>()[text2];
				float num = animationState.normalizedTime % 2f;
				if (num < 0f)
				{
					num += 2f;
				}
				switch (animationState.wrapMode)
				{
				case WrapMode.Once:
					num = ((!(animationState.normalizedTime > 1f)) ? num : 0f);
					break;
				case WrapMode.ClampForever:
					num = ((!(animationState.normalizedTime >= 1f)) ? num : 1f);
					break;
				case WrapMode.PingPong:
					num = ((!(num > 1f)) ? num : (2f - num));
					break;
				default:
					num %= 1f;
					break;
				}
				text += string.Format("{0} {1:0.00} {2:0.00} <{5}> {3,3:G} {6} {4}\n", (!animationState.enabled) ? " " : "X", animationState.weight, num, animationState.layer, animationState.name, FloatToProgressbar(num), (animationState.blendMode != AnimationBlendMode.Additive) ? " " : "X");
			}
		}
		this.text.text = text;
	}
}
