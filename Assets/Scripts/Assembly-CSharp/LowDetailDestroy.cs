using UnityEngine;

public class LowDetailDestroy : MonoBehaviour
{
	private void Start()
	{
		if (!AManager.HighDetail)
		{
			Object.Destroy(base.gameObject);
		}
	}
}
