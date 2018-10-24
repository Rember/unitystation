﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StorageHandler : MonoBehaviour
{
	private GameObject inventorySlotPrefab;
	private StorageItem storageCache;
	private List<UI_ItemSlot> localSlotCache = new List<UI_ItemSlot>();

	void Awake()
	{
		inventorySlotPrefab = Resources.Load("InventorySlot")as GameObject;
	}

	public void OpenStorageUI(StorageItem storageItem)
	{
		storageCache = storageItem;
		storageCache.clientUpdatedDelegate += StorageUpdatedEvent;
		PopulateInventorySlots();
	}

	private void PopulateInventorySlots()
	{
		localSlotCache.Clear();
		for (int i = 0; i < storageCache.storageProps.slotCount; i++)
		{
			GameObject newSlot = Instantiate(inventorySlotPrefab, Vector3.zero, Quaternion.identity);
			newSlot.transform.parent = transform;
			var itemSlot = newSlot.GetComponentInChildren<UI_ItemSlot>();
			itemSlot.eventName = "inventory" + i;
			localSlotCache.Add(itemSlot);
			UIManager.InventorySlots.Add(itemSlot);
		}
	}

	public void CloseStorageUI()
	{
		storageCache.clientUpdatedDelegate -= StorageUpdatedEvent;
		storageCache = null;

		for (int i = localSlotCache.Count - 1; i >= 0; i--)
		{
			UIManager.InventorySlots.Remove(localSlotCache[i]);
			Destroy(localSlotCache[i].gameObject);
		}
		localSlotCache.Clear();
	}

	private void StorageUpdatedEvent()
	{
		Debug.Log("Storage updated while open");
	}
}