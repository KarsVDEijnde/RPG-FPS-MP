using UnityEngine;

public class Interactable : MonoBehaviour {
    
    public virtual void Interact()
    {
        // method will be overwritten by item
        // Debug.Log("Interacting with " + transform.name);
    }
}
