﻿using UnityEngine;

public class InventoryUI : MonoBehaviour {


    public static bool IsOn = false;
    public Transform itemsParent;
    public GameObject inventoryUI;

    Inventory inventory;

    InventorySlot[] slots;

	// Use this for initialization
	void Start () {
        inventory = Inventory.instance;
        inventory.OnItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Inventory"))
        {
            //Add going in iventory Animation
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            IsOn = !IsOn;
        }
	}

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
