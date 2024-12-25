using UnityEngine;

public enum ItemType
{
    HeathIncrease,
    ExpInCrease,
    Key1,
    Key2
}

[System.Serializable]
public class ItemDrop
{
    public GameObject itemPrefab; // Prefab của item
    [Range(0, 100)] public float dropRate; // Tỷ lệ rơi (0 - 100%)
}