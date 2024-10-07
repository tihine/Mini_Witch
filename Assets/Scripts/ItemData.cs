using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script defines the items collectible in game as ScriptableObjects

[CreateAssetMenu (fileName ="Item", menuName ="Items/New Item")] 
public class ItemData : ScriptableObject
{
    public string name;
    public string description;
    public Sprite visual;
    public GameObject prefab;
}
