using UnityEngine;

[RequireComponent(typeof(GUIText))]
public class FPS : MonoBehaviour
{
	public float updateInterval = 0.5f;

	private float accum;

	private int frames;

	private float timeleft;

	private void Start()
	{
		if (!base.GetComponent<GUIText>())
		{
			MonoBehaviour.print("FramesPerSecond needs a GUIText component!");
			base.enabled = false;
		}
		else
		{
			timeleft = updateInterval;
		}
	}

	private void Update()
	{
		timeleft -= Time.deltaTime;
		accum += Time.timeScale / Time.deltaTime;
		frames++;
		if (timeleft <= 0f)
		{
			base.GetComponent<GUIText>().text = string.Empty + (accum / (float)frames).ToString("00");
			timeleft = updateInterval;
			accum = 0f;
			frames = 0;
		}
	}
}
