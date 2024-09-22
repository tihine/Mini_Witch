using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField]
    private float pickupRange = 2.6f;

    //public Inventory inventory;
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward, out hit, pickupRange))
        {
            if (hit.transform.CompareTag("Item"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //inventory.content.Add(hit.transform.gameObject.GetComponent<Item>().item);
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }
}
