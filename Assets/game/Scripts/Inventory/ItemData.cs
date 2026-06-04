using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private string _name;

    public string ID => _id;
    public string Name => _name;
}