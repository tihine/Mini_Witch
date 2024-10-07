using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<ItemData> content = new List<ItemData>();
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Transform inventorySlotsParent;
    const int InventorySize = 20;

    private void Start()
    {
        RefreshContent();
    }
    public void AddItem(ItemData item)
    {
        content.Add(item);
        RefreshContent();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }
    private void RefreshContent()
    {
        for (int i = 0; i < content.Count; i++)
        {
            inventorySlotsParent.GetChild(i).GetChild(0).GetComponent<Image>().sprite = content[i].visual;
        }
    }
    public bool IsFull()
    {
        return InventorySize == content.Count;
    }
}
