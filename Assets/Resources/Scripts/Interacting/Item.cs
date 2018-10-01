using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item :ScriptableObject {

    new public string name = "New Item";
    public Sprite Icon = null;
    public bool isDefaulfItem;
	
    public virtual void Use()
    {
        Debug.Log("using " + name);
    }
    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
