using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvestable : MonoBehaviour
{
    [SerializeField] public Resource[] harvestableItems;
}

[System.Serializable]
public class Resource
{
    public ItemData itemData;
    public int minAmountSpawned;
    public int maxAmountSpawned;
}
