using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ItemData item;
    public Image itemVisual;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            TooltipSystem.instance.Show(item.description, item.name);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.instance.Hide();
    }

    public void ClickOnSlot()
    {
        Inventory.instance.OpenActionPanel(item, transform.position);
    }
}
