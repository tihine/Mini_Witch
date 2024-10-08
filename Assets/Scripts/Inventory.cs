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
    [SerializeField] private Sprite emptySlotVisual;
    [SerializeField] private Transform dropPoint;

    [Header("Action Panel References")]
    [SerializeField] private GameObject actionPanel;
    [SerializeField] private GameObject useItemButton;
    [SerializeField] private GameObject equipItemButton;
    [SerializeField] private GameObject dropItemButton;
    [SerializeField] private GameObject destroyItemButton;

    private ItemData itemCurrentlySelected;

    public static Inventory instance;
    private void Awake()
    {
        instance = this; 
    }
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

    public void OpenActionPanel(ItemData item, Vector3 slotPosition)
    {
        itemCurrentlySelected = item; //needed for ActionButtons
        if (item == null)
        {
            actionPanel.SetActive(false);
            return;
        }

        switch(item.itemType)
        {
            case ItemType.Ressource:
                useItemButton.SetActive(false);
                equipItemButton.SetActive(false);
                break;
            case ItemType.Equipment: 
                useItemButton.SetActive(false);
                equipItemButton.SetActive(true);
                break;
            case ItemType.Consumable:
                useItemButton.SetActive(true);
                equipItemButton.SetActive(false);
                break;
            case ItemType.Potion:
                useItemButton.SetActive(true);
                equipItemButton.SetActive(false);
                break;
        }
        actionPanel.transform.position = slotPosition;
        actionPanel.SetActive(true);
    }

    public void CloseActionPanel()
    {
        actionPanel.SetActive(false);
        itemCurrentlySelected = null;
    }

    public void UseActionButton()
    {
        print("Use item on" + itemCurrentlySelected);
        CloseActionPanel();
        
    }
    public void EquipActionButton()
    {
        print("Equip item on" + itemCurrentlySelected);
        CloseActionPanel();
    }
    public void DropActionButton()
    {
        GameObject instantiatedItem = Instantiate(itemCurrentlySelected.prefab);
        instantiatedItem.transform.position = dropPoint.position;
        content.Remove(itemCurrentlySelected);
        RefreshContent();
        CloseActionPanel();
    }
    public void DestroyActionButton()
    {
        content.Remove(itemCurrentlySelected);
        RefreshContent();
        CloseActionPanel();
    }
}
