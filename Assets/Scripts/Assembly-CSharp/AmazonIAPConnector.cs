using UnityEngine;

public class AmazonIAPConnector : MonoBehaviour
{
	public delegate void IABAndroidEmptyDelegate();

	public delegate void IABAndroidPurchaseDelegate(string productId);

	public delegate void IABAndroidErrorDelegate(string error);

	public delegate void IABAndroidBillingSupportedDelegate(bool isSupported);

	public static AmazonIAPConnector Instance;

	public AndroidJavaClass amazonIAPClass;

	private AndroidJavaObject amazonIAPInstance;

	public event IABAndroidPurchaseDelegate purchaseSucceededEvent;

	public event IABAndroidErrorDelegate purchaseFailedEvent;

	public event IABAndroidErrorDelegate restoreTransactionsFailedEvent;

	public event IABAndroidEmptyDelegate onrestoreTransactionSuccessfulEvent;

	private void Awake()
	{
		if (GameConstants.AmazonAppstore)
		{
			amazonIAPClass = new AndroidJavaClass("com.angrymobgames.amazoniap.AmazonIAPUnity");
			amazonIAPInstance = amazonIAPClass.CallStatic<AndroidJavaObject>("getInstance", new object[0]);
		}
		Instance = this;
	}

	public void PurchaseProduct(string sku)
	{
		amazonIAPInstance.Call("purchaseProduct", sku);
	}

	public void RestoreTransactions()
	{
		amazonIAPInstance.Call("restoreTransactions");
	}

	public bool IsBillingAvailable()
	{
		return amazonIAPInstance.Call<bool>("isPurchaseAvailable", new object[0]);
	}

	public void OnPurchaseSuccessfull(string sku)
	{
		if (this.purchaseSucceededEvent != null)
		{
			this.purchaseSucceededEvent(sku);
		}
	}

	public void OnPurchaseFailed(string message)
	{
		if (this.purchaseFailedEvent != null)
		{
			this.purchaseFailedEvent(message);
		}
	}

	public void OnRestoreTransactionsFailed(string message)
	{
		if (this.restoreTransactionsFailedEvent != null)
		{
			this.restoreTransactionsFailedEvent(message);
		}
	}

	public void OnRestoreTransactionsSuccessful()
	{
		if (this.onrestoreTransactionSuccessfulEvent != null)
		{
			this.onrestoreTransactionSuccessfulEvent();
		}
	}
}
