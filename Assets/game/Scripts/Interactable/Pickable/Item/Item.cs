using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, IInteractable, IPickable
{
    [SerializeField] private ItemData _data;

    public string Name => _data.Name;

    public UnityEvent OnItemPicked;

    [ContextMenu("Interact Item")]
    public void Interact(PlayerCharacter character)
    {
        Pickup(character);
    }

    public virtual void Pickup(PlayerCharacter character)
    {
        ItemData newData = new ItemData(_data.ID, _data.Name);
        character.Inventory.AddItems(newData);
        OnItemPicked?.Invoke();
        Destroy(gameObject);
    }
}