using System;
using System.Collections.Generic;
using Soomla;
using UnityEngine;
using System.IO;

public class ChromacoreEventHandler : MonoBehaviour
{
	bool skullKidPurchasedp = false;
	bool scarfPurchasedp = false;

	GameObject shopMenu;

	void Start(){
		shopMenu = GameObject.Find("Shop Text");
	}
	
	public ChromacoreEventHandler ()
	{
		StoreEvents.OnMarketPurchase += onMarketPurchase;
		StoreEvents.OnMarketRefund += onMarketRefund;
		StoreEvents.OnItemPurchased += onItemPurchased;
		StoreEvents.OnGoodEquipped += onGoodEquipped;
		StoreEvents.OnGoodUnEquipped += onGoodUnequipped;
		StoreEvents.OnGoodUpgrade += onGoodUpgrade;
		StoreEvents.OnBillingSupported += onBillingSupported;
		StoreEvents.OnBillingNotSupported += onBillingNotSupported;
		StoreEvents.OnMarketPurchaseStarted += onMarketPurchaseStarted;
		StoreEvents.OnItemPurchaseStarted += onItemPurchaseStarted;
		StoreEvents.OnUnexpectedErrorInStore += onUnexpectedErrorInStore;
		StoreEvents.OnCurrencyBalanceChanged += onCurrencyBalanceChanged;
		StoreEvents.OnGoodBalanceChanged += onGoodBalanceChanged;
		StoreEvents.OnMarketPurchaseCancelled += onMarketPurchaseCancelled;
		StoreEvents.OnRestoreTransactionsStarted += onRestoreTransactionsStarted;
		StoreEvents.OnRestoreTransactionsFinished += onRestoreTransactionsFinished;
		StoreEvents.OnStoreControllerInitialized += onStoreControllerInitialized;
		#if UNITY_ANDROID && !UNITY_EDITOR
		StoreEvents.OnIabServiceStarted += onIabServiceStarted;
		StoreEvents.OnIabServiceStopped += onIabServiceStopped;
		#endif
	}
	
	public void onMarketPurchase(PurchasableVirtualItem pvi, string purchaseToken) {
		if(pvi.ItemId == "skull_kid_skin"){
			skullKidPurchasedp = true;
		}else if(pvi.ItemId == "scarf_skin"){
			scarfPurchasedp = true;
		}else{
			skullKidPurchasedp = false;
			scarfPurchasedp = false;
		}

		shopMenu.SendMessage("skullKid_skinBought", skullKidPurchasedp);
		shopMenu.SendMessage("scarf_skinBought", scarfPurchasedp);
	}
	
	public void onMarketRefund(PurchasableVirtualItem pvi) {
		
	}
	
	public void onItemPurchased(PurchasableVirtualItem pvi) {
		if(pvi.ItemId == "skull_kid_skin"){
			skullKidPurchasedp = true;
		}else if(pvi.ItemId == "scarf_skin"){
			scarfPurchasedp = true;
		}else{
			skullKidPurchasedp = false;
			scarfPurchasedp = false;
		}
		
		shopMenu.SendMessage("skullKid_skinBought", skullKidPurchasedp);
		shopMenu.SendMessage("scarf_skinBought", scarfPurchasedp);
	}
	
	public void onGoodEquipped(EquippableVG good) {
		
	}
	
	public void onGoodUnequipped(EquippableVG good) {
		
	}
	
	public void onGoodUpgrade(VirtualGood good, UpgradeVG currentUpgrade) {
		
	}
	
	public void onBillingSupported() {
		
	}
	
	public void onBillingNotSupported() {
		
	}
	
	public void onMarketPurchaseStarted(PurchasableVirtualItem pvi) {
		
	}
	
	public void onItemPurchaseStarted(PurchasableVirtualItem pvi) {
		
	}
	
	public void onMarketPurchaseCancelled(PurchasableVirtualItem pvi) {
		
	}
	
	public void onUnexpectedErrorInStore(string message) {
		
	}
	
	public void onCurrencyBalanceChanged(VirtualCurrency virtualCurrency, int balance, int amountAdded) {
		ChromacoreLocalStoreInfo.UpdateBalances();
	}
	
	public void onGoodBalanceChanged(VirtualGood good, int balance, int amountAdded) {
		ChromacoreLocalStoreInfo.UpdateBalances();
	}
	
	public void onRestoreTransactionsStarted() {
		
	}
	
	public void onRestoreTransactionsFinished(bool success) {
		
	}
	
	public void onStoreControllerInitialized() {
		ChromacoreLocalStoreInfo.Init();
		
		// some usage examples for add/remove currency
		// some examples
		if (ChromacoreLocalStoreInfo.VirtualCurrencies.Count>0) {
			try {
				StoreInventory.GiveItem(ChromacoreLocalStoreInfo.VirtualCurrencies[0].ItemId,4000);
				StoreUtils.LogDebug("SOOMLA ExampleEventHandler", "Currency balance:" + StoreInventory.GetItemBalance(ChromacoreLocalStoreInfo.VirtualCurrencies[0].ItemId));
			} catch (VirtualItemNotFoundException ex){
				StoreUtils.LogError("SOOMLA ExampleEventHandler", ex.Message);
			}
		}
	}
	
	#if UNITY_ANDROID && !UNITY_EDITOR
	public void onIabServiceStarted() {
		
	}
	public void onIabServiceStopped() {
		
	}
	#endif
}

