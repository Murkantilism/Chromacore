/*
 * Copyright (C) 2012 Soomla Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Soomla.Example {
	public class ChromacoreStore : IStoreAssets{ // Make sure to change to your actual class name

		public int GetVersion() {
			return 0;
		}

		public VirtualCurrency[] GetCurrencies() {
	        return  new VirtualCurrency[] {
	            GOLD_COINS_CURRENCY
	        };
		}

	    public VirtualGood[] GetGoods() {
	        return new VirtualGood[] {
/* SingleUseVGs     --> */
/* LifetimeVGs      --> */
/* EquippableVGs    --> */    SKULL_KID_SKIN_GOOD, SCARF_SKIN_GOOD,
/* SingleUsePackVGs --> */
/* UpgradeVGs       --> */
	        };
		}

	    public VirtualCurrencyPack[] GetCurrencyPacks() {
	        return new VirtualCurrencyPack[] {
	            
	        };
		}

	    public VirtualCategory[] GetCategories() {
	        return new VirtualCategory[]{
	            SKINS_CATEGORY
	        };
		}

	    public NonConsumableItem[] GetNonConsumableItems() {
			return new NonConsumableItem[]{
			    
			};
		}


        /** Static Final members **/

        // Currencies
    public const string GOLD_COINS_CURRENCY_ITEM_ID = "currency_gc";

        // Goods
    public const string SKULL_KID_SKIN_GOOD_ITEM_ID = "skull_kid_skin";
#if UNITY_ANDROID
	public const string SKULL_KID_SKIN_PRODUCT_ID = "skull_kid_skin";
#else
	public const string SKULL_KID_SKIN_PRODUCT_ID = "skull_kid_skin";
#endif
	
    public const string SCARF_SKIN_GOOD_ITEM_ID = "scarf_skin";
#if UNITY_ANDROID
    public const string SCARF_SKIN_PRODUCT_ID = "scarf_skin";
#else
	public const string SCARF_SKIN_PRODUCT_ID = "scarf_skin";
#endif
        

        // Currency Packs

        // Non Consumables


        /** Virtual Currencies **/

        
        public static VirtualCurrency GOLD_COINS_CURRENCY = new VirtualCurrency(
                 "Gold Coins", // name
                 "", // description
                 GOLD_COINS_CURRENCY_ITEM_ID); // item id
        


        /** Virtual Currency Packs **/

        


        /** Virtual Goods **/

        /* SingleUseVGs */
        
        /* LifetimeVGs */
        
        /* EquippableVGs */
        
    public static VirtualGood SKULL_KID_SKIN_GOOD = new EquippableVG(
            EquippableVG.EquippingModel.CATEGORY, // The equipping type
            "skull_kid_skin", // name
            "", // description
            SKULL_KID_SKIN_GOOD_ITEM_ID, // item id
            new PurchaseWithMarket(new MarketItem(SKULL_KID_SKIN_PRODUCT_ID, MarketItem.Consumable.CONSUMABLE, 0.99))
); // the way this virtual good is purchased

    public static VirtualGood SCARF_SKIN_GOOD = new EquippableVG(
            EquippableVG.EquippingModel.CATEGORY, // The equipping type
            "scarf_skin", // name
            "Cosmetic skin for player character", // description
            SCARF_SKIN_GOOD_ITEM_ID, // item id
            new PurchaseWithMarket(new MarketItem(SCARF_SKIN_PRODUCT_ID, MarketItem.Consumable.CONSUMABLE, 0.99))
); // the way this virtual good is purchased

        /* SingleUsePackVGs */
        
        /* UpgradeVGs */
        


        /** Virtual Categories **/

        
        public static VirtualCategory SKINS_CATEGORY = new VirtualCategory (
                "Skins", // name
                new List<string>(
                    new string[] { SKULL_KID_SKIN_GOOD_ITEM_ID, SCARF_SKIN_GOOD_ITEM_ID }
                    )
                );
        


        /** Non Consumable Items **/
        

	}

}