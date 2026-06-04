using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, IInteractable, IPickable
{
    [SerializeField] private string _id;
    [SerializeField] private string _name;
    [SerializeField] private ItemData _data;

    public string Id => _id;
    public string Name => _name;

    public UnityEvent OnItemPicked;

    [ContextMenu("Interact Item")]
    public void Interact()
    {
        Pickup();
    }

    public void Pickup(PlayerCharacter character)
    {
        ItemData newData = new ItemData(_data.ID, _data.Name);
        character.Inventory.AddItems(newData);
        OnItemPicked?.Invoke();
        Destroy(gameObject);
    }
}