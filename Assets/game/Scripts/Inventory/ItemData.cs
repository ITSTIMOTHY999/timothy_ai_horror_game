using UnityEngine;

[System.Serializable]

public class ItemData
{
    [SerializeField] private string _id;
    [SerializeField] private string _name;

    public string ID => _id;
    public string Name => _name;

    public ItemData(string id, string name)
    {
        _id = id;
        _name = name;
    }
}