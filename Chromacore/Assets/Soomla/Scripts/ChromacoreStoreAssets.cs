using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla;

public class ChromacoreStoreAssets : IStoreAssets {

	public int GetVersion(){
		return 0;
	}

	// No virtual currency needed
	public VirtualCurrency[] GetCurrencies(){
		return new VirtualCurrency[]{ };
	}

	// No virtual goods needed
	public VirtualGood[] GetGoods() {
		return new VirtualGood[] { };
	}

	// No virtual currency packs needed
	public VirtualCurrencyPack[] GetCurrencyPacks() {
		return new VirtualCurrencyPack[] { };
	}

	public VirtualCategory[] GetCategories() {
		return new VirtualCategory[]{GENERAL_CATEGORY};
	}

	public NonConsumableItem[] GetNonConsumableItems() {
		return new NonConsumableItem[]{SKULLKID_SKIN, SCARF_SKIN};
	}

	/** Static Final members **/
	public const string SKULLKID_SKIN_ITEM_ID      = "skull_kid_skin";
	public const string SCARF_SKIN_ITEM_ID         = "scarf_skin";

	/** Virtual Categories **/
	// The muffin rush theme doesn't support categories, so we just put everything under a general category.
	public static VirtualCategory GENERAL_CATEGORY = new VirtualCategory(
		"General", new List<string>(new string[] { SKULLKID_SKIN_ITEM_ID, SCARF_SKIN_ITEM_ID })
		);

	/** Market MANAGED Items **/
	public static NonConsumableItem SKULLKID_SKIN  = new NonConsumableItem(
		"Skull Kid", // name
		"Cosmetic skin for player character.", // description
		"skull_kid_skin", // item id
		new PurchaseWithMarket(new MarketItem(SKULLKID_SKIN_ITEM_ID, MarketItem.Consumable.NONCONSUMABLE , 0.99))
		);

	public static NonConsumableItem SCARF_SKIN  = new NonConsumableItem(
		"Scarf", // name
		"Cosmetic skin for player character.", // description
		"scarf_skin", // item id
		new PurchaseWithMarket(new MarketItem(SCARF_SKIN_ITEM_ID, MarketItem.Consumable.NONCONSUMABLE , 0.99))
		);
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}