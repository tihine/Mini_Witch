using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("OTHER SCRIPTS REFERENCES")]
    [SerializeField] private ItemActionsSystem itemActionsSystem;

    [Header("INVENTORY SYSTEM VARIABLES")]
    public List<ItemData> content = new List<ItemData>();
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Transform inventorySlotsParent;
    const int InventorySize = 20;
    [SerializeField] private Sprite emptySlotVisual;

    public static Inventory instance;
    private bool inventoryIsOpen=false;

    private void Awake()
    {
        instance = this; 
    }
    private void Start()
    {
        RefreshContent();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(inventoryIsOpen)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }
    }
    public void AddItem(ItemData item)
    {
        content.Add(item);
        RefreshContent();
    }
    public void RemoveItem(ItemData item)
    {
        content.Remove(item);
        RefreshContent();
    }
    public List<ItemData> GetContent()
    {
        return content;
    }
    private void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        inventoryIsOpen= true;
    }
    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        itemActionsSystem.actionPanel.SetActive(false);
        TooltipSystem.instance.Hide();
        inventoryIsOpen = false;
    }
    public void RefreshContent()
    {
        //Delete all visuals
        for (int i = 0; i < inventorySlotsParent.childCount; i++)
        {
            Slot currentSlot = inventorySlotsParent.GetChild(i).GetComponent<Slot>();
            currentSlot.item = null;
            currentSlot.itemVisual.sprite = emptySlotVisual;
        }
        //Add all visuals
        for (int i = 0; i < content.Count; i++)
        {
            Slot currentSlot = inventorySlotsParent.GetChild(i).GetComponent<Slot>();
            currentSlot.item = content[i];
            currentSlot.itemVisual.sprite=content[i].visual;
        }
    }
    public bool IsFull()
    {
        return InventorySize == content.Count;
    }
}
