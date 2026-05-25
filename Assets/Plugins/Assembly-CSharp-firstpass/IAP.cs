using System;
using System.Collections.Generic;
using UnityEngine;

public static class IAP
{
	private const string CONSUMABLE_PAYLOAD = "consume";

	private const string NON_CONSUMABLE_PAYLOAD = "nonconsume";

	private static Action<List<IAPProduct>> _productListReceivedAction;

	private static Action<bool> _purchaseCompletionAction;

	private static Action<string> _purchaseRestorationAction;

	static IAP()
	{
		GoogleIABManager.queryInventorySucceededEvent += delegate(List<GooglePurchase> purchases, List<GoogleSkuInfo> skus)
		{
			List<IAPProduct> list = new List<IAPProduct>();
			foreach (GoogleSkuInfo sku in skus)
			{
				list.Add(new IAPProduct(sku));
			}
			if (_productListReceivedAction != null)
			{
				_productListReceivedAction(list);
			}
		};
		GoogleIABManager.queryInventoryFailedEvent += delegate(string error)
		{
			Debug.Log("fetching prouduct data failed: " + error);
			if (_productListReceivedAction != null)
			{
				_productListReceivedAction(null);
			}
		};
		GoogleIABManager.purchaseSucceededEvent += delegate(GooglePurchase purchase)
		{
			if (purchase.developerPayload == "nonconsume")
			{
				if (_purchaseCompletionAction != null)
				{
					_purchaseCompletionAction(true);
				}
			}
			else
			{
				GoogleIAB.consumeProduct(purchase.productId);
			}
		};
		GoogleIABManager.purchaseFailedEvent += delegate(string error)
		{
			Debug.Log("purchase failed: " + error);
			if (_purchaseCompletionAction != null)
			{
				_purchaseCompletionAction(false);
			}
		};
		GoogleIABManager.consumePurchaseSucceededEvent += delegate
		{
			if (_purchaseCompletionAction != null)
			{
				_purchaseCompletionAction(true);
			}
		};
		GoogleIABManager.consumePurchaseFailedEvent += delegate
		{
			if (_purchaseCompletionAction != null)
			{
				_purchaseCompletionAction(false);
			}
		};
	}

	public static void init(string androidPublicKey)
	{
		GoogleIAB.init(androidPublicKey);
	}

	public static void requestProductData(string[] iosProductIdentifiers, string[] androidSkus, Action<List<IAPProduct>> completionHandler)
	{
		_productListReceivedAction = completionHandler;
		GoogleIAB.queryInventory(androidSkus);
	}

	public static void purchaseConsumableProduct(string productId, Action<bool> completionHandler)
	{
		_purchaseCompletionAction = completionHandler;
		GoogleIAB.purchaseProduct(productId, "consume");
	}

	public static void purchaseNonconsumableProduct(string productId, Action<bool> completionHandler)
	{
		_purchaseCompletionAction = completionHandler;
		GoogleIAB.purchaseProduct(productId, "nonconsume");
	}

	public static void restoreCompletedTransactions(Action<string> completionHandler)
	{
		_purchaseCompletionAction = null;
		_purchaseRestorationAction = completionHandler;
	}
}
