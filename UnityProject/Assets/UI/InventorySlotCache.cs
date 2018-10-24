﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
///     A pseudo-array/dictionary for retrieving inventory slots.
///     Supports multiple interactions to make it easier to access the slot you want to reference.
/// </summary>
/// <example>
///     var beltSlot = inventorySlotCache.BeltSlot;
/// </example>
/// <example>
///     var firstSlot = inventorySlotCache[0];
/// </example>
/// <example>
///     var hatSlot = inventorySlotCache[ItemType.Hat];
/// </example>
/// <example>
///     var idSlot = inventorySlotCache["id"];
/// </example>
/// <example>
///     foreach (var slot in inventorySlotCache)
/// </example>
/// <example>
///     inventorySlotCache.GetSlotByItem(CurrentSlot.Item)
/// </example>
public class InventorySlotCache : MonoBehaviour
{
	public static List<UI_ItemSlot> InventorySlots = new List<UI_ItemSlot>();

	public UI_ItemSlot this [int index] => InventorySlots[index];

	public UI_ItemSlot this [ItemType type] => GetSlotByItemType(type);

	public UI_ItemSlot this [string eventName] => GetSlotByEvent(eventName);

	public int Length => InventorySlots != null ? InventorySlots.Count : 0;

	private void Awake()
	{
		InventorySlots.Clear();
		var childSlots = GetComponentsInChildren<UI_ItemSlot>();

		for (int i = 0; i < childSlots.Length; i++)
		{
			InventorySlots.Add(childSlots[i]);
		}
	}

	public ItemType GetItemType(GameObject obj)
	{
		ItemAttributes item = obj.GetComponent<ItemAttributes>();
		return item.type;
	}

	public SpriteType GetItemMasterType(GameObject obj)
	{
		ItemAttributes item = obj.GetComponent<ItemAttributes>();
		return item.spriteType;
	}

	/// <summary>
	///     Returns the most fitting slot for a given item to be equipped.
	/// </summary>
	/// <remarks>
	///     Returns the left pocket for non-equippable items.
	/// </remarks>
	public UI_ItemSlot GetSlotByItem(GameObject obj)
	{
		ItemAttributes item = obj.GetComponent<ItemAttributes>();
		return GetSlotByItemType(item.type);
	}

	public void Add(UI_ItemSlot item)
	{
		InventorySlots.Add(item);
	}

	public void Remove(UI_ItemSlot item)
	{
		if (InventorySlots.Contains(item))
		{
			InventorySlots.Remove(item);
		}
	}

	public UI_ItemSlot GetSlotByItemType(ItemType type)
	{
		string eventName = "storage02";
		switch (type)
		{
			case ItemType.Back:
				eventName = "back";
				break;
			case ItemType.Belt:
				eventName = "belt";
				break;
			case ItemType.Ear:
				eventName = "ear";
				break;
			case ItemType.Glasses:
				eventName = "glasses";
				break;
			case ItemType.Gloves:
				eventName = "gloves";
				break;
			case ItemType.Hat:
				eventName = "hat";
				break;
			case ItemType.ID:
				eventName = "id";
				break;
			case ItemType.PDA:
				eventName = "id";
				break;
			case ItemType.Mask:
				eventName = "mask";
				break;
			case ItemType.Neck:
				eventName = "neck";
				break;
			case ItemType.Shoes:
				eventName = "feet";
				break;
			case ItemType.Suit:
				eventName = "suit";
				break;
			case ItemType.Uniform:
				eventName = "uniform";
				break;
			case ItemType.Gun:
				eventName = "suitStorage";
				break;
		}

		return GetSlotByEvent(eventName);
	}

	public UI_ItemSlot GetSlotByEvent(string eventName)
	{
		int indexSearch = InventorySlots.FindIndex(x => x.eventName == eventName);
		if (indexSearch != -1)
		{
			return InventorySlots[indexSearch];
		}
		else
		{
			return null;
		}
	}
}