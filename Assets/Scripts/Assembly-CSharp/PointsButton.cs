public class PointsButton : Button
{
	public int points = 1000;

	public bool setText = true;

	protected override void Awake()
	{
		base.Awake();
		if (setText)
		{
			buttonText.text = points + "\n" + Language.GetTxt("POINTS");
		}
	}
}
