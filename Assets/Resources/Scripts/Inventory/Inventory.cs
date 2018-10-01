using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    #region Singelton

    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("more then one Inventory");
            return;
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;

    public int slots;

    public List<Item> items = new List<Item>();

    public bool Add(Item item)
    {
        if (!item.isDefaulfItem)
        {
            if (items.Count >= slots)
            {
                Debug.Log("Not Enough Room!");
                return false;
            }
            items.Add(item);

            if (OnItemChangedCallback != null)
                OnItemChangedCallback.Invoke();
        }
        return true;
    }
    public void Remove(Item item)
    {
        items.Remove(item);

        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();
    }
}
