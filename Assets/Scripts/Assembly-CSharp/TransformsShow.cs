using UnityEngine;

public class TransformsShow : MonoBehaviour
{
	public bool alwaysShow = true;

	public float smallSphere = 0.2f;

	public float largeSphere = 1f;

	public void OnDrawGizmosSelected()
	{
		if (!alwaysShow)
		{
			Gizmos.color = Color.green;
			int childCount = base.transform.GetChildCount();
			for (int i = 0; i < childCount; i++)
			{
				Gizmos.DrawWireSphere(base.transform.GetChild(i).position, 0.2f);
				Gizmos.DrawWireSphere(base.transform.GetChild(i).position, 1f);
			}
		}
	}

	public void OnDrawGizmos()
	{
		if (alwaysShow)
		{
			Gizmos.color = Color.green;
			int childCount = base.transform.GetChildCount();
			for (int i = 0; i < childCount; i++)
			{
				Gizmos.DrawWireSphere(base.transform.GetChild(i).position, smallSphere);
				Gizmos.DrawWireSphere(base.transform.GetChild(i).position, largeSphere);
			}
		}
	}
}
