using UnityEngine;

public class Item : MonoBehaviour, IInteractable, IPickable
{
    [SerializeField]
    private string _name;

    public string Name => _name;

    public void Interact()
    {
        Pickup();
    }

    public void Pickup()
    {
        //nanti
    }
}