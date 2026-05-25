using UnityEngine;

public class IAPConnector : MonoBehaviour
{
	private static bool iapAvailable;

	public static bool IAPavailable
	{
		get
		{
			return iapAvailable;
		}
	}

	static IAPConnector()
	{
		GoogleIABManager.billingSupportedEvent += OnBillingsuported;
	}

	private static void OnBillingsuported()
	{
		iapAvailable = true;
	}
}
