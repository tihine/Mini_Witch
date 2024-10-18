using UnityEngine;

public class InteractBehaviour : MonoBehaviour
{

    private GameObject collectibleItem;
    private GameObject harvestableResource;
    [SerializeField] public Inventory inventory;
    [SerializeField] private GameObject pickupText;
    [SerializeField] private Animator playerAnimator;
    public void CollectItem()
    {
        if (inventory.IsFull())
        {
            Debug.Log("Inventory is full");
            return;
        }
        if (collectibleItem != null)
        {
            Debug.Log("item" + collectibleItem.name);
            inventory.AddItem(collectibleItem.GetComponent<Item>().item);
            pickupText.SetActive(false);
            Destroy(collectibleItem);
        }
    }

    public void Harvest()
    {
        Debug.Log("On interagit avec" + harvestableResource);
        playerAnimator.SetTrigger("Harvest");

    }
    public void BreakHarvestable()
    {
        for (int i = 0; i < harvestableResource.GetComponent<Harvestable>().harvestableItems.Length; i++)
        {
            Resource resource = harvestableResource.GetComponent<Harvestable>().harvestableItems[i];
            for (int y = 0; y < Random.Range(resource.minAmountSpawned,1+resource.maxAmountSpawned); y++)
            {
                GameObject instantiatedResource = GameObject.Instantiate(resource.itemData.prefab);
                instantiatedResource.transform.position = harvestableResource.transform.position;
            }
        }
        Destroy(harvestableResource);
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Item":
                collectibleItem = other.gameObject;
                pickupText.SetActive(true);
                Debug.Log("Item detected ="+collectibleItem);
                break;
            case "Harvestable":
                harvestableResource = other.gameObject;
                Debug.Log("Resource to harvest :" + harvestableResource);
                break;
            default:
                Debug.Log("Tag not found");
                break;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "Item":
                if (collectibleItem == other.gameObject)
                {
                    Debug.Log("Item not accessible anymore =" + collectibleItem); 
                    collectibleItem = null;
                    pickupText.SetActive(false);
                }
                break;
            case "Harvestable":
                if (harvestableResource == other.gameObject)
                {
                    Debug.Log("Resource not accessible anymore =" + harvestableResource);
                    harvestableResource = null;
                    pickupText.SetActive(false);
                }
                break;
            default:
                Debug.Log("Tag not found");
                break;
        }
        
    }
}
