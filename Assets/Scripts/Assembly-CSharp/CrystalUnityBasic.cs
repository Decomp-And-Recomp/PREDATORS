using System;
using System.Collections;
using System.Collections.Generic;
using Procurios.Public;
using UnityEngine;

public class CrystalUnityBasic : MonoBehaviour
{
	public enum ActivateUiType
	{
		StandardUi = 0,
		ProfileUi = 1,
		ChallengesUi = 2,
		LeaderboardsUi = 3,
		AchievementsUi = 4,
		AddFriends = 5,
		Settings = 6,
		Gifting = 7,
		VirtualGoods = 8,
		VirtualCurrencies = 9,
		FindFriends = 10,
		InviteFriends = 11,
		GiftsAndMarket = 12,
		SwitchUser = 13
	}

	public enum CrystalLeaderboardCategories
	{
		CLCTop20 = 1,
		CLCTop20Friends = 2,
		CLCRandom20 = 4,
		CLCCurrentUser = 8
	}

	public enum ActivatePullTabUiType
	{
		Profile = 0,
		Challenges = 1,
		Leaderboards = 2,
		Achievements = 3,
		Friends = 4,
		Settings = 5,
		Gifting = 6,
		VirtualGoods = 7,
		VirtualCurrencies = 8,
		FindFriends = 9,
		GiftsAndMarket = 10,
		News = 11
	}

	public enum CrystalLeaderboardType
	{
		CLTGlobal = 0,
		CLTNational = 1,
		CLTLocal = 2,
		CLTThisMonth = 3,
		CLTLastMonth = 4,
		CLTThisWeek = 5,
		CLTLastWeek = 6,
		CLTToday = 7,
		CLTYesterday = 8
	}

	public enum CrystalSetting
	{
		CrystalSettingCocosAchievementWorkaround = 1,
		CrystalSettingAvoidBackgroundActivity = 2,
		CrystalSettingShouldAutorotateWorkaround = 3,
		CrystalSettingSingleiPadPopover = 4,
		CrystalSettingEnableGameCenterSupport = 5
	}

	public delegate void ChallengeNotificationUpdateHandler();

	public delegate void UIDeactivatedHandler();

	public delegate void AchievementsUpdateHandler(bool success, IList<string> achievements);

	public delegate void VirtualGoodsUpdateHandler(bool success, IDictionary<string, int> goods);

	public delegate void VirtualBalancesUpdateHandler(bool success, IDictionary<string, int> balances);

	public delegate void StartedFromPendingUrlHandler();

	public delegate void CrystalPlayerInfoUpdateHandler(bool success, CrystalPlayer player);

	public delegate void PopoversActivatedHandler();

	public delegate void LeaderboardUpdateHandler(bool success, string leaderboardId);

	public string iLastIncomingChallenge = string.Empty;

	public string iStartedFromPendingUrl = string.Empty;

	public bool iPopoversActivated;

	public bool iVirtualGoodsUpdated;

	public bool iPlayerInfoUpdated;

	public string iPlayerInfoUpdate = string.Empty;

	private static CrystalUnityBasic _singleton;

	private CrystalPlayer iPlayer;

	private IDictionary<string, int> iVirtualGoods;

	private IDictionary<string, int> iVirtualBalances;

	private IList<string> iAchievements;

	private CrystalLeaderboardType iLeaderboardType;

	private string iLeaderboardId;

	private CrystalLeaderboardCategories iLeaderboardCategory;

	private IList<LeaderboardEntry> iTop20EntriesForLeaderboard;

	private IList<LeaderboardEntry> iTop20FriendsForLeaderboard;

	private IList<LeaderboardEntry> iRandom20ForLeaderboard;

	private LeaderboardEntry iCurrentUserEntryForLeaderboard = new LeaderboardEntry();

	public static CrystalUnityBasic Instance
	{
		get
		{
			if (_singleton == null)
			{
				Debug.Log("[CrystalUnityBasic] instantiate");
				GameObject gameObject = new GameObject();
				_singleton = gameObject.AddComponent<CrystalUnityBasic>();
				gameObject.name = "CrystalUnityBasicSingleton";
			}
			return _singleton;
		}
	}

	public CrystalPlayer CurrentCrystalPlayer
	{
		get
		{
			return iPlayer;
		}
	}

	public IDictionary<string, int> CurrentVirtualGoods
	{
		get
		{
			return iVirtualGoods;
		}
	}

	public IDictionary<string, int> CurrentVirtualBalances
	{
		get
		{
			return iVirtualBalances;
		}
	}

	public IList<string> CurrentAchievements
	{
		get
		{
			return iAchievements;
		}
	}

	public IList<LeaderboardEntry> CurrentTop20EntriesForLeaderboard
	{
		get
		{
			return iTop20EntriesForLeaderboard;
		}
	}

	public IList<LeaderboardEntry> CurrentTop20FriendsForLeaderboard
	{
		get
		{
			return iTop20FriendsForLeaderboard;
		}
	}

	public IList<LeaderboardEntry> CurrentRandom20ForLeaderboard
	{
		get
		{
			return iRandom20ForLeaderboard;
		}
	}

	public LeaderboardEntry CurrentUserEntryForLeaderboard
	{
		get
		{
			return iCurrentUserEntryForLeaderboard;
		}
	}

	public CrystalLeaderboardType DownloadedLeaderboardType
	{
		get
		{
			return iLeaderboardType;
		}
	}

	public string DownloadedLeaderboardId
	{
		get
		{
			return iLeaderboardId;
		}
	}

	public CrystalLeaderboardCategories DownloadedLeaderboardCategory
	{
		get
		{
			return iLeaderboardCategory;
		}
	}

	public event ChallengeNotificationUpdateHandler ChallengeNotificationEvent;

	public event UIDeactivatedHandler UIDeactivatedEvent;

	public event AchievementsUpdateHandler AchievementsUpdateEvent;

	public event VirtualGoodsUpdateHandler VirtualGoodsUpdateEvent;

	public event VirtualBalancesUpdateHandler VirtualBalancesUpdateEvent;

	public event StartedFromPendingUrlHandler StartedFromPendingUrlEvent;

	public event CrystalPlayerInfoUpdateHandler CrystalPlayerInfoUpdateEvent;

	public event PopoversActivatedHandler PopoversActivatedEvent;

	public event LeaderboardUpdateHandler LeaderBoardUpdatedEvent;

	private bool isOkToRun()
	{
		return false;
	}

	public void Start()
	{
	}

	public void Update()
	{
		int num = Time.frameCount % 30;
		switch (num)
		{
		case 0:
		{
			string string3 = PlayerPrefs.GetString("CCChallengeNotification");
			if (string3 != string.Empty)
			{
				iLastIncomingChallenge = string3;
				PlayerPrefs.SetString("CCChallengeNotification", string.Empty);
				if (this.ChallengeNotificationEvent != null)
				{
					this.ChallengeNotificationEvent();
				}
			}
			string string4 = PlayerPrefs.GetString("CCUIDeactivated");
			if (string4 != string.Empty)
			{
				Debug.Log("[CrystalUnityBasic] UI Deactivated");
				PlayerPrefs.SetString("CCUIDeactivated", string.Empty);
				if (this.UIDeactivatedEvent != null)
				{
					this.UIDeactivatedEvent();
				}
			}
			string string5 = PlayerPrefs.GetString("CCAchievementsUpdated");
			if (string5 != string.Empty)
			{
				Debug.Log("[CrystalUnityBasic] Achievments activated " + string5);
				bool success3 = PopulateAchievements(string5);
				PlayerPrefs.SetString("CCAchievementsUpdated", string.Empty);
				if (this.AchievementsUpdateEvent != null)
				{
					this.AchievementsUpdateEvent(success3, iAchievements);
				}
			}
			break;
		}
		case 5:
		{
			string @string = PlayerPrefs.GetString("CCVirtualGoodsUpdated");
			string string2 = PlayerPrefs.GetString("CCVirtualBalancesUpdated");
			if (@string != string.Empty)
			{
				Debug.Log("[CrystalUnityBasic] virtualgoods [goods] activated " + @string);
				bool success = PopulateVirtualGoods(@string);
				PlayerPrefs.SetString("CCVirtualGoodsUpdated", string.Empty);
				if (this.VirtualGoodsUpdateEvent != null)
				{
					this.VirtualGoodsUpdateEvent(success, iVirtualGoods);
				}
			}
			if (string2 != string.Empty)
			{
				Debug.Log("[CrystalUnityBasic] virtualgoods [balances] activated " + string2);
				bool success2 = PopulateVirtualBalances(string2);
				PlayerPrefs.SetString("CCVirtualBalancesUpdated", string.Empty);
				if (this.VirtualBalancesUpdateEvent != null)
				{
					this.VirtualBalancesUpdateEvent(success2, iVirtualBalances);
				}
			}
			break;
		}
		case 10:
			iStartedFromPendingUrl = PlayerPrefs.GetString("CCStartedFromPendingUrl");
			if (iStartedFromPendingUrl != string.Empty)
			{
				PlayerPrefs.SetString("CCStartedFromPendingUrl", string.Empty);
				if (this.StartedFromPendingUrlEvent != null)
				{
					this.StartedFromPendingUrlEvent();
				}
			}
			break;
		}
		switch (num)
		{
		case 15:
		{
			string string7 = PlayerPrefs.GetString("CCPlayerInfoUpdated");
			if (string7 != string.Empty)
			{
				Debug.Log("[CrystalUnityBasic] player info activated " + string7);
				bool success4 = PopulateCrystalPlayer(string7);
				PlayerPrefs.SetString("CCPlayerInfoUpdated", string.Empty);
				if (this.CrystalPlayerInfoUpdateEvent != null)
				{
					this.CrystalPlayerInfoUpdateEvent(success4, iPlayer);
				}
			}
			break;
		}
		case 20:
		{
			string string6 = PlayerPrefs.GetString("CCPopoversActivated");
			bool flag = ((string6 == "YES") ? true : false);
			if (flag != iPopoversActivated)
			{
				Debug.Log("[CrystalUnityBasic] popovers activated " + flag);
				iPopoversActivated = flag;
				if (this.PopoversActivatedEvent != null)
				{
					this.PopoversActivatedEvent();
				}
			}
			break;
		}
		}
	}

	public void Leaderboardupdated(string leaderboardData)
	{
		bool success = PopulateLeaderBoardEntries(leaderboardData);
		if (this.LeaderBoardUpdatedEvent != null)
		{
			this.LeaderBoardUpdatedEvent(success, iLeaderboardId);
		}
	}

	public bool AppWasStartedFromPendingUrl()
	{
		if (iStartedFromPendingUrl != string.Empty)
		{
			return true;
		}
		return false;
	}

	public bool HaveIncomingChallengeFromCrystal()
	{
		if (iLastIncomingChallenge != string.Empty)
		{
			return true;
		}
		return false;
	}

	public void ActivateUi()
	{
		Debug.Log("[CrystalUnityBasic] ActivateUi");
		ActivateUi(ActivateUiType.StandardUi);
	}

	public void ActivateUi(ActivateUiType type)
	{
		if (isOkToRun())
		{
			Debug.Log(string.Concat("[CrystalUnityBasic] ActivateUi, ", type, ", isOkToRun"));
			AddCommand("ActivateUi|" + (int)type);
		}
		else
		{
			Debug.Log("[CrystalUnityBasic] ActivateUi, " + type);
		}
	}

	public void ActivateUiAtLeadboardWithId(string leaderboardId)
	{
		if (isOkToRun())
		{
			AddCommand("ActivateUiAtLeadboardWithId|" + leaderboardId);
		}
		else
		{
			Debug.Log("[CrystalUnityBasic] ActivateUiAtLeadboardWithId" + leaderboardId);
		}
	}

	public void DeactivateUi()
	{
		if (isOkToRun())
		{
			AddCommand("DeactivateUi");
		}
		else
		{
			Debug.Log("[CrystalUnityBasic] DeactivateUi");
		}
	}

	public void ActivatePullTabUi(ActivatePullTabUiType type, string edge)
	{
		ActivatePullTabUi(type, edge, true);
	}

	public void ActivatePullTabUi(ActivatePullTabUiType type, string edge, bool closedState)
	{
		if (isOkToRun())
		{
			Debug.Log(string.Concat("[CrystalUnityBasic] ActivatePullTabUi, ", type, ", ", edge, ", ", closedState, ", isOkToRun"));
			AddCommand("ActivatePullTabUi|" + (int)type + "|" + edge + "|" + closedState);
		}
		else
		{
			Debug.Log(string.Concat("[CrystalUnityBasic] ActivatePullTabUi, ", type, ", ", edge, ",  ", closedState));
		}
	}

	public void ActivatePullTabUiAtLeadboardWithId(string leaderboardId, string edge)
	{
		ActivatePullTabUiAtLeadboardWithId(leaderboardId, edge, true);
	}

	public void ActivatePullTabUiAtLeadboardWithId(string leaderboardId, string edge, bool closedState)
	{
		if (isOkToRun())
		{
			Debug.Log("[CrystalUnityBasic] ActivatePullTabUiAtLeadboardWithId, " + leaderboardId + ", " + edge + ", " + closedState + ", isOkToRun");
			AddCommand("ActivatePullTabUiAtLeadboardWithId|" + leaderboardId + "|" + edge + "|" + closedState);
		}
		else
		{
			Debug.Log("[CrystalUnityBasic] ActivatePullTabUiAtLeadboardWithId" + leaderboardId + ",  " + closedState);
		}
	}

	public void ActivateCrystalPullTabOn(IList<string> tabs, string edge)
	{
		ActivateCrystalPullTabOn(tabs, null, edge, true);
	}

	public void ActivateCrystalPullTabOn(IList<string> tabs, string edge, bool closedState)
	{
		ActivateCrystalPullTabOn(tabs, null, edge, closedState);
	}

	public void ActivateCrystalPullTabOn(IList<string> tabs, string leaderboardId, string edge)
	{
		ActivateCrystalPullTabOn(tabs, leaderboardId, edge, true);
	}

	public void ActivateCrystalPullTabOn(IList<string> tabs, string leaderboardId, string edge, bool closedState)
	{
		if (isOkToRun())
		{
			string text = null;
			if (tabs != null)
			{
				ArrayList arrayList = new ArrayList();
				foreach (string tab in tabs)
				{
					arrayList.Add(tab);
				}
				text = JSON.JsonEncode(arrayList);
			}
			AddCommand("ActivateCrystalPullTabOn|" + text + "|" + leaderboardId + "|" + edge + "|" + closedState);
		}
		else
		{
			Debug.Log("[CrystalUnityBasic] ActivateCrystalPullTabOn, " + leaderboardId + ", " + edge + ",  " + closedState);
		}
	}

	public void DeactivatePullTabUi()
	{
		if (isOkToRun())
		{
			AddCommand("DeactivatePullTabUi");
		}
		else
		{
			Debug.Log("[CrystalUnityBasic] DeactivatePullTabUi");
		}
	}

	public void ResetCrystalPullTabState()
	{
		if (isOkToRun())
		{
			AddCommand("ResetCrystalPullTabState");
		}
		else
		{
			Debug.Log("[CrystalUnityBasic] ResetCrystalPullTabState");
		}
	}

	public void PostChallengeResultForLastChallenge(double result, bool doDialog)
	{
		if (isOkToRun())
		{
			AddCommand("PostChallengeResultForLastChallenge|" + result + "|" + doDialog);
		}
		else
		{
			Debug.Log("[CrystalUnityBasic] PostChallengeResultForLastChallenge " + result + ", " + doDialog);
		}
	}

	public void PostAchievement(string achievementId, bool wasObtained, string description, bool alwaysPopup)
	{
		PostAchievement(achievementId, wasObtained, description, alwaysPopup, achievementId);
	}

	public void PostLeaderboardResult(string leaderboardId, float result, bool lowestValFirst, bool isTimeBased)
	{
		PostLeaderboardResult(leaderboardId, result, lowestValFirst, leaderboardId, isTimeBased);
	}

	public void PostAchievement(string achievementId, bool wasObtained, string description, bool alwaysPopup, string gcAchievementId)
	{
		if (isOkToRun())
		{
			AddCommand("PostAchievement|" + achievementId + "|" + wasObtained + "|" + description + "|" + alwaysPopup + "|" + gcAchievementId);
		}
		else
		{
			Debug.Log("[CrystalUnityBasic] PostAchievement" + achievementId + ", " + wasObtained + ", " + description + ", " + alwaysPopup + ", " + gcAchievementId);
		}
	}

	public void PostLeaderboardResult(string leaderboardId, float result, bool lowestValFirst, string gcLeaderboardId, bool isTimeBased)
	{
		if (isOkToRun())
		{
			AddCommand("PostLeaderboardResult|" + leaderboardId + "|" + result + "|" + lowestValFirst + "|" + gcLeaderboardId + "|" + isTimeBased);
		}
		else
		{
			Debug.Log("[CrystalUnityBasic] PostLeaderboardResult" + leaderboardId + ", " + result + ", " + lowestValFirst + ", " + gcLeaderboardId + ", " + isTimeBased);
		}
	}

	public void PostAchievementProgressWithId(string crystalId, string gameCenterId, double percentageComplete, string achievementDescription)
	{
		if (isOkToRun())
		{
			AddCommand("PostAchievementProgressWithId|" + crystalId + "|" + gameCenterId + "|" + percentageComplete + "|" + achievementDescription);
		}
		else
		{
			Debug.Log("[CrystalUnityBasic] PostAchievementProgressWithId, " + crystalId + ", " + gameCenterId + ", " + percentageComplete + ", " + achievementDescription);
		}
	}

	[Obsolete("Use iOS APIs in AppController+Crystal.mm file to set the orientations")]
	public void LockToOrientation(ScreenOrientation orientation)
	{
		string text = ScreenOrientationToString(orientation);
		if (isOkToRun() && text != null)
		{
			AddCommand("LockToOrientation|" + text);
		}
		else
		{
			Debug.Log("[CrystalUnityBasic] LockToOrientation" + text);
		}
	}

	public void ActivateCrystalSetting(CrystalSetting setting, string settingValue)
	{
		if (isOkToRun() && settingValue != null)
		{
			AddCommand("ActivateCrystalSetting|" + (int)setting + "|" + settingValue);
		}
		else
		{
			Debug.Log("[CrystalUnityBasic] ActivateCrystalSetting, " + (int)setting + ", " + settingValue);
		}
	}

	public void AuthenticateLocalPlayer()
	{
		if (isOkToRun())
		{
			AddCommand("AuthenticateLocalPlayer");
		}
		else
		{
			Debug.Log("[CrystalUnityBasic] AuthenticateLocalPlayer");
		}
	}

	public void CloseCrystalSession()
	{
		if (isOkToRun())
		{
			AddCommand("CloseCrystalSession");
		}
		else
		{
			Debug.Log("[CrystalUnityBasic] CloseCrystalSession");
		}
	}

	public void WillRotateToInterfaceWithOrientation(ScreenOrientation orientation, double orientationDuration)
	{
		string text = ScreenOrientationToString(orientation);
		if (isOkToRun() && text != null)
		{
			AddCommand("WillRotateToInterfaceWithOrientation|" + text + "|" + orientationDuration);
		}
		else
		{
			Debug.Log("[CrystalUnityBasic] WillRotateToInterfaceWithOrientation" + text + ", " + orientationDuration);
		}
	}

	public void DidRotateFromInterfaceOrientation(ScreenOrientation orientation)
	{
		string text = ScreenOrientationToString(orientation);
		if (isOkToRun() && text != null)
		{
			AddCommand("DidRotateFromInterfaceOrientation|" + text);
		}
		else
		{
			Debug.Log("[CrystalUnityBasic] DidRotateFromInterfaceOrientation" + text);
		}
	}

	[Obsolete("Use public void DownloadLeaderBoardDataForID(string leaderboardId, CrystalLeaderboardType leaderboardType)")]
	public void DownloadLeaderBoardDataForID(string leaderboardId)
	{
		if (isOkToRun())
		{
			AddCommand("DownloadLeaderboardDataForID|" + leaderboardId);
		}
		else
		{
			Debug.Log("[CrystalUnityBasic] DownloadLeaderboardDataForID, " + leaderboardId);
		}
	}

	public void DownloadLeaderBoardDataForID(string leaderboardId, CrystalLeaderboardType leaderboardType)
	{
		DownloadLeaderBoardDataForID(leaderboardId, leaderboardType, false);
	}

	public void DownloadLeaderBoardDataForID(string leaderboardId, CrystalLeaderboardType leaderboardType, bool force)
	{
		if (isOkToRun())
		{
			AddCommand("DownloadLeaderboardDataForID|" + leaderboardId + "|" + (int)leaderboardType + "|" + force);
		}
		else
		{
			Debug.Log(string.Concat("[CrystalUnityBasic] DownloadLeaderboardDataForID, ", leaderboardId, ", ", leaderboardType, ", ", force));
		}
	}

	public void PostVirtualGoods(IDictionary<string, int> goods)
	{
		if (isOkToRun() && goods != null && goods.Count != 0)
		{
			Debug.Log("[CrystalUnityBasic] PostVirtualGoods");
			Hashtable hashtable = new Hashtable();
			foreach (KeyValuePair<string, int> good in goods)
			{
				hashtable.Add(good.Key, good.Value);
			}
			string text = JSON.JsonEncode(hashtable);
			AddCommand("PostVirtualGoods|" + text);
		}
		else
		{
			Debug.Log("[CrystalUnityBasic] PostVirtualGoods, ");
		}
	}

	public void SetLockedGoods(IList<string> lockedGoods)
	{
		if (isOkToRun())
		{
			Debug.Log("[CrystalUnityBasic] SetLockedGoods");
			string text = null;
			if (lockedGoods != null && lockedGoods.Count != 0)
			{
				ArrayList arrayList = new ArrayList();
				foreach (string lockedGood in lockedGoods)
				{
					arrayList.Add(lockedGood);
				}
				text = JSON.JsonEncode(arrayList);
			}
			AddCommand("SetLockedGoods|" + text);
		}
		else
		{
			Debug.Log("> CrystalUnityBasic SetLockedGoods, ");
		}
	}

	public void PostVirtualBalances(IDictionary<string, int> balances)
	{
		if (isOkToRun() && balances != null && balances.Count != 0)
		{
			Debug.Log("[CrystalUnityBasic] PostVirtualBalances");
			Hashtable hashtable = new Hashtable();
			foreach (KeyValuePair<string, int> balance in balances)
			{
				hashtable.Add(balance.Key, balance.Value);
			}
			string text = JSON.JsonEncode(hashtable);
			AddCommand("PostVirtualBalances|" + text);
		}
		else
		{
			Debug.Log("[CrystalUnityBasic] PostVirtualBalances, ");
		}
	}

	public void CrystalPlayerStartUpdating()
	{
		if (isOkToRun())
		{
			AddCommand("CrystalPlayerStartUpdating");
		}
		else
		{
			Debug.Log("[CrystalUnityBasic] CrystalPlayerStartUpdating");
		}
	}

	public void VirtualGoodsStartUpdating()
	{
		if (isOkToRun())
		{
			AddCommand("VirtualGoodsStartUpdating");
		}
		else
		{
			Debug.Log("[CrystalUnityBasic] VirtualGoodsStartUpdating");
		}
	}

	public void VirtualGoodsUpdateNow()
	{
		if (isOkToRun())
		{
			AddCommand("VirtualGoodsUpdateNow");
		}
		else
		{
			Debug.Log("[CrystalUnityBasic] VirtualGoodsUpdateNow");
		}
	}

	public void RequestAchievementData()
	{
		if (isOkToRun())
		{
			AddCommand("RequestAchievementData");
		}
		else
		{
			Debug.Log("[CrystalUnityBasic] RequestAchievementData");
		}
	}

	public bool IsCrystalPlayerSignedIn()
	{
		bool result = false;
		Debug.Log("[CrystalUnityBasic] IsCrystalPlayerSignedIn");
		if (isOkToRun())
		{
			PlayerPrefs.SetString("CCCommandWithReturn", "isCrystalPlayerSignedIn");
			string @string = PlayerPrefs.GetString("Result");
			result = ((@string == "YES") ? true : false);
		}
		return result;
	}

	private void AddCommand(string newCommand)
	{
	}

	private bool PopulateCrystalPlayer(string incomingPlayerInfo)
	{
		bool flag = false;
		Hashtable hashtable = (Hashtable)JSON.JsonDecode(incomingPlayerInfo);
		if (hashtable == null)
		{
			return flag;
		}
		if (hashtable.ContainsKey("success"))
		{
			flag = (bool)hashtable["success"];
			if (!flag)
			{
				return flag;
			}
		}
		if (hashtable.ContainsKey("player"))
		{
			string json = (string)hashtable["player"];
			Hashtable hashtable2 = (Hashtable)JSON.JsonDecode(json);
			if (hashtable2.ContainsKey("data"))
			{
				if (iPlayer == null)
				{
					iPlayer = new CrystalPlayer();
				}
				Hashtable hashtable3 = hashtable2["data"] as Hashtable;
				if (hashtable3.ContainsKey("facebookuser"))
				{
					iPlayer.isFacebookUser = (bool)hashtable3["facebookuser"];
				}
				if (hashtable3.ContainsKey("twitteruser"))
				{
					iPlayer.isTwitterUser = (bool)hashtable3["twitteruser"];
				}
				if (hashtable3.ContainsKey("crystalfriends"))
				{
					iPlayer.numCrystalFriends = Convert.ToInt32((double)hashtable3["crystalfriends"]);
				}
				if (hashtable3.ContainsKey("gamefriends"))
				{
					iPlayer.numCrystalFriendsWithGame = Convert.ToInt32((double)hashtable3["gamefriends"]);
				}
				if (iPlayer.gifts != null)
				{
					iPlayer.gifts.Clear();
				}
				else
				{
					iPlayer.gifts = new List<string>();
				}
				if (hashtable3.ContainsKey("gifts"))
				{
					ArrayList arrayList = (ArrayList)hashtable3["gifts"];
					if (arrayList != null)
					{
						foreach (double item2 in arrayList)
						{
							iPlayer.gifts.Add(Convert.ToString(item2));
						}
					}
				}
				if (iPlayer.giftsLeftToSend != null)
				{
					iPlayer.giftsLeftToSend.Clear();
				}
				else
				{
					iPlayer.giftsLeftToSend = new Dictionary<string, int>();
				}
				if (hashtable3.ContainsKey("giftsLeft"))
				{
					Hashtable hashtable4 = (Hashtable)hashtable3["giftsLeft"];
					if (hashtable4 != null)
					{
						ArrayList arrayList2 = new ArrayList(hashtable4.Keys);
						ArrayList arrayList3 = new ArrayList(hashtable4.Values);
						for (int i = 0; i < arrayList2.Count; i++)
						{
							iPlayer.giftsLeftToSend.Add((string)arrayList2[i], Convert.ToInt32(arrayList3[i]));
						}
					}
				}
				if (hashtable3.ContainsKey("crystaluser"))
				{
					iPlayer.alias = (string)hashtable3["crystaluser"];
				}
				if (hashtable3.ContainsKey("guestuser"))
				{
					if ((bool)hashtable3["guestuser"])
					{
						iPlayer.isCrystaluser = false;
					}
					else
					{
						iPlayer.isCrystaluser = true;
					}
				}
				if (hashtable3.ContainsKey("badge"))
				{
					iPlayer.badgeNumber = Convert.ToInt32((double)hashtable3["badge"]);
				}
				if (iPlayer.crystalfriendIds != null)
				{
					iPlayer.crystalfriendIds.Clear();
				}
				else
				{
					iPlayer.crystalfriendIds = new List<string>();
				}
				if (hashtable3.ContainsKey("crystalfriendids"))
				{
					ArrayList arrayList4 = (ArrayList)hashtable3["crystalfriendids"];
					if (arrayList4 != null)
					{
						foreach (string item3 in arrayList4)
						{
							iPlayer.crystalfriendIds.Add(item3);
						}
					}
				}
				if (hashtable3.ContainsKey("crystalplayerid"))
				{
					iPlayer.crystalPlayerId = Convert.ToString((double)hashtable3["crystalplayerid"]);
				}
			}
			else
			{
				Debug.Log("Data not found!!");
			}
		}
		return flag;
	}

	private bool PopulateVirtualGoods(string incomingVirtualGoods)
	{
		bool flag = false;
		Hashtable hashtable = (Hashtable)JSON.JsonDecode(incomingVirtualGoods);
		if (hashtable == null)
		{
			return flag;
		}
		if (hashtable.ContainsKey("success"))
		{
			flag = (bool)hashtable["success"];
			if (!flag)
			{
				return flag;
			}
		}
		if (hashtable.ContainsKey("goods"))
		{
			Hashtable hashtable2 = hashtable["goods"] as Hashtable;
			if (iVirtualGoods != null)
			{
				iVirtualGoods.Clear();
			}
			else
			{
				iVirtualGoods = new Dictionary<string, int>();
			}
			ArrayList arrayList = new ArrayList(hashtable2.Keys);
			ArrayList arrayList2 = new ArrayList(hashtable2.Values);
			for (int i = 0; i < arrayList.Count; i++)
			{
				iVirtualGoods.Add((string)arrayList[i], Convert.ToInt32(arrayList2[i]));
			}
		}
		return flag;
	}

	private bool PopulateVirtualBalances(string incomingVirtualBalances)
	{
		bool flag = false;
		Hashtable hashtable = (Hashtable)JSON.JsonDecode(incomingVirtualBalances);
		if (hashtable == null)
		{
			return flag;
		}
		if (hashtable.ContainsKey("success"))
		{
			flag = (bool)hashtable["success"];
			if (!flag)
			{
				return flag;
			}
		}
		if (hashtable.ContainsKey("balances"))
		{
			Hashtable hashtable2 = hashtable["balances"] as Hashtable;
			if (iVirtualBalances != null)
			{
				iVirtualBalances.Clear();
			}
			else
			{
				iVirtualBalances = new Dictionary<string, int>();
			}
			ArrayList arrayList = new ArrayList(hashtable2.Keys);
			ArrayList arrayList2 = new ArrayList(hashtable2.Values);
			for (int i = 0; i < arrayList.Count; i++)
			{
				iVirtualBalances.Add((string)arrayList[i], Convert.ToInt32(arrayList2[i]));
			}
		}
		return flag;
	}

	private bool PopulateAchievements(string incomingAchievements)
	{
		bool flag = false;
		Hashtable hashtable = (Hashtable)JSON.JsonDecode(incomingAchievements);
		if (hashtable == null)
		{
			return flag;
		}
		if (hashtable.ContainsKey("success"))
		{
			flag = (bool)hashtable["success"];
			if (!flag)
			{
				return flag;
			}
		}
		if (hashtable.ContainsKey("achievements"))
		{
			ArrayList arrayList = (ArrayList)hashtable["achievements"];
			if (arrayList != null)
			{
				if (iAchievements != null)
				{
					iAchievements.Clear();
				}
				else
				{
					iAchievements = new List<string>();
				}
				for (int i = 0; i < arrayList.Count; i++)
				{
					iAchievements.Add(arrayList[i].ToString());
				}
			}
		}
		return flag;
	}

	private bool PopulateLeaderBoardEntries(string incomingLeaderboardData)
	{
		bool flag = false;
		Hashtable hashtable = (Hashtable)JSON.JsonDecode(incomingLeaderboardData);
		if (hashtable == null)
		{
			return flag;
		}
		if (hashtable.ContainsKey("success"))
		{
			flag = (bool)hashtable["success"];
			if (!flag)
			{
				return flag;
			}
		}
		if (hashtable.ContainsKey("leaderboardId"))
		{
			string text = (string)hashtable["leaderboardId"];
			iLeaderboardId = text;
		}
		if (hashtable.ContainsKey("leaderboardType"))
		{
			double num = (double)hashtable["leaderboardType"];
			iLeaderboardType = (CrystalLeaderboardType)num;
		}
		if (hashtable.ContainsKey("category"))
		{
			double num2 = (double)hashtable["category"];
			iLeaderboardCategory = (CrystalLeaderboardCategories)num2;
			if (hashtable.ContainsKey("leaderboardData"))
			{
				ArrayList leaderboardListData = (ArrayList)hashtable["leaderboardData"];
				PopulateLeaderBoardEntries(leaderboardListData, iLeaderboardCategory);
			}
		}
		return flag;
	}

	private void PopulateLeaderBoardEntries(ArrayList leaderboardListData, CrystalLeaderboardCategories aType)
	{
		ArrayList arrayList = new ArrayList();
		if (leaderboardListData == null)
		{
			return;
		}
		for (int i = 0; i < leaderboardListData.Count; i++)
		{
			Hashtable hashtable = (Hashtable)leaderboardListData[i];
			LeaderboardEntry leaderboardEntry = new LeaderboardEntry();
			if (hashtable.ContainsKey("username"))
			{
				leaderboardEntry.username = (string)hashtable["username"];
			}
			if (hashtable.ContainsKey("crystalplayerid"))
			{
				leaderboardEntry.crystalplayerid = (string)hashtable["crystalplayerid"];
			}
			if (hashtable.ContainsKey("position"))
			{
				leaderboardEntry.position = (string)hashtable["position"];
			}
			if (hashtable.ContainsKey("score"))
			{
				leaderboardEntry.score = (double)hashtable["score"];
			}
			if (hashtable.ContainsKey("percentile"))
			{
				leaderboardEntry.percentile = (string)hashtable["percentile"];
			}
			arrayList.Add(leaderboardEntry);
		}
		switch (aType)
		{
		case CrystalLeaderboardCategories.CLCTop20:
			if (iTop20EntriesForLeaderboard != null)
			{
				iTop20EntriesForLeaderboard.Clear();
			}
			else
			{
				iTop20EntriesForLeaderboard = new List<LeaderboardEntry>();
			}
			{
				foreach (LeaderboardEntry item3 in arrayList)
				{
					iTop20EntriesForLeaderboard.Add(item3);
				}
				break;
			}
		case CrystalLeaderboardCategories.CLCTop20Friends:
			if (iTop20FriendsForLeaderboard != null)
			{
				iTop20FriendsForLeaderboard.Clear();
			}
			else
			{
				iTop20FriendsForLeaderboard = new List<LeaderboardEntry>();
			}
			{
				foreach (LeaderboardEntry item4 in arrayList)
				{
					iTop20FriendsForLeaderboard.Add(item4);
				}
				break;
			}
		case CrystalLeaderboardCategories.CLCRandom20:
			if (iRandom20ForLeaderboard != null)
			{
				iRandom20ForLeaderboard.Clear();
			}
			else
			{
				iRandom20ForLeaderboard = new List<LeaderboardEntry>();
			}
			{
				foreach (LeaderboardEntry item5 in arrayList)
				{
					iRandom20ForLeaderboard.Add(item5);
				}
				break;
			}
		case CrystalLeaderboardCategories.CLCCurrentUser:
		{
			LeaderboardEntry leaderboardEntry2 = (LeaderboardEntry)arrayList[0];
			iCurrentUserEntryForLeaderboard.username = leaderboardEntry2.username;
			iCurrentUserEntryForLeaderboard.crystalplayerid = leaderboardEntry2.crystalplayerid;
			iCurrentUserEntryForLeaderboard.position = leaderboardEntry2.position;
			iCurrentUserEntryForLeaderboard.score = leaderboardEntry2.score;
			iCurrentUserEntryForLeaderboard.percentile = leaderboardEntry2.percentile;
			break;
		}
		case (CrystalLeaderboardCategories)3:
		case (CrystalLeaderboardCategories)5:
		case (CrystalLeaderboardCategories)6:
		case (CrystalLeaderboardCategories)7:
			break;
		}
	}

	private string ScreenOrientationToString(ScreenOrientation orientation)
	{
		string result = null;
		switch (orientation)
		{
		case ScreenOrientation.Portrait:
			result = "portrait";
			break;
		case ScreenOrientation.LandscapeLeft:
			result = "landscapeLeft";
			break;
		case ScreenOrientation.LandscapeRight:
			result = "landscapeRight";
			break;
		case ScreenOrientation.PortraitUpsideDown:
			result = "portraitUpsideDown";
			break;
		}
		return result;
	}
}
