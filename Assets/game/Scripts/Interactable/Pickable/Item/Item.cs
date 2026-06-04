using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, IInteractable, IPickable
{
    [SerializeField] private string _id;
    [SerializeField] private string _name;

    public string Id => _id;
    public string Name => _name;

    public UnityEvent OnItemPicked;

    [ContextMenu("Interact Item")]
    public void Interact()
    {
        Pickup();
    }

    public void Pickup()
    {
        OnItemPicked?.Invoke();
        Destroy(gameObject);
    }
}