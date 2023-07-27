using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIScript : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabInventorySlot, prefabInventorySprite;
    private GameObject[] inventorySlots;
    private Image[] inventorySprites;
    private Text[] inventoryAmounts;
    private GameManager manager;
    private int inventorySize;

    private void Awake()
    {
        manager = (GameManager)FindObjectOfType(typeof(GameManager));
        manager.inventoryUI = this;
        
    }

    public void InstantiateInventoryUI()
    {
        inventorySize = manager.inventory.Count;
        inventorySlots = new GameObject[inventorySize];
        inventoryAmounts = new Text[inventorySize];
        inventorySprites = new Image[inventorySize];
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i] = Instantiate(prefabInventorySlot, transform);
            inventoryAmounts[i] = inventorySlots[i].GetComponentInChildren<Text>();
            inventorySprites[i] = Instantiate(prefabInventorySprite, inventorySlots[i].transform).GetComponent<Image>();
        }
    }

    private void FixedUpdate()
    {
        //Set the scale of all inventory slots to 1, then increase the scale of only the selected slot.
        for(int i = 0; i < inventorySize; i++)
        {
            inventorySlots[i].transform.localScale = new Vector3(1, 1, 1);
           
        }
        if(inventorySize > 0)
            inventorySlots[manager.selectedSlot].transform.localScale = new Vector3(1.2f, 1.2f, 1);
    }

    public void UpdateInventory()
    {
        //Set the sprite of the inventory slot to the sprite it ought to be based on the ID.
        for(int i = 0; i < inventorySize; i++)
        {
            if (manager.inventory[i].Amount > 1)
                inventoryAmounts[i].text = manager.inventory[i].Amount.ToString();
            else
                inventoryAmounts[i].text = "";
            try
            {
                inventorySprites[i].sprite = manager.itemSprites[manager.inventory[i].ID];
                
            }
            catch
            {
                Debug.Log("There was a problem setting the inventory sprite.");
            }
        }
    }
}
