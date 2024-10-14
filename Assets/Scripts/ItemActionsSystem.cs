using UnityEngine;

public class ItemActionsSystem : MonoBehaviour
{
    [Header("Action Panel References")]
    public GameObject actionPanel;
    [SerializeField] private Transform dropPoint;
    [SerializeField] private GameObject useItemButton;
    [SerializeField] private GameObject equipItemButton;
    [SerializeField] private GameObject dropItemButton;
    [SerializeField] private GameObject destroyItemButton;
    [HideInInspector] public ItemData itemCurrentlySelected;

    public void OpenActionPanel(ItemData item, Vector3 slotPosition)
    {
        itemCurrentlySelected = item; //needed for ActionButtons
        if (item == null)
        {
            actionPanel.SetActive(false);
            return;
        }

        switch (item.itemType)
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
    //not used because the game doesn't have equipments
    public void EquipActionButton()
    {
        print("Equip item on" + itemCurrentlySelected);
        CloseActionPanel();
    }
    public void DropActionButton()
    {
        GameObject instantiatedItem = Instantiate(itemCurrentlySelected.prefab);
        instantiatedItem.transform.position = dropPoint.position;
        Inventory.instance.RemoveItem(itemCurrentlySelected);
        Inventory.instance.RefreshContent();
        CloseActionPanel();
    }
    public void DestroyActionButton()
    {
        Inventory.instance.RemoveItem(itemCurrentlySelected);
        Inventory.instance.RefreshContent();
        CloseActionPanel();
    }
}
