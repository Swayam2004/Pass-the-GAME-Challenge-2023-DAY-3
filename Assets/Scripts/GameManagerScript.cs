using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public Sprite[] itemSprites;
    public bool playerCanMove;
    public List<Item> inventory = new List<Item>();
    private List<int> inventoryIDs = new List<int>();
    private List<Vector2> pitLocations = new List<Vector2>(), filledPitLocations = new List<Vector2>(), occupiedLocations = new List<Vector2>();
    public List<Vector2> wetPitLocations = new List<Vector2>(), grownCropLocations = new List<Vector2>();
    public List<CropScript> grownCrops = new List<CropScript>();
    public Item selectedItem = new Item(0, 0);
    public short selectedSlot, feedbackIndex;

    [SerializeField]
    private GameObject barteringScreen;

    [SerializeField]
    private SpriteRenderer mouseFeedbackSquare;

    [SerializeField]
    private GameObject pitPrefab, cornCropPrefab;

    private Camera cam;

    private Vector2 snappedMousePos;

    public InventoryUIScript inventoryUI;

    private bool instantiatedInventoryUI;

    private void Awake()
    {
        barteringScreen.SetActive(false);
        //Make the farmhouse occupied terrain so nothing can be placed there.
        for(int x = -5; x < 6; x++)
        {
            for(int y = 1; y < 9; y++)
            {
                occupiedLocations.Add(new Vector2(x, y));
            }
        }
        cam = Camera.main;
        //Creates 9 empty inventory spaces.
        for (int i = 0; i < 9; i++)
        {
            inventory.Add(new Item(0, 0));
            inventoryIDs.Add(0);
        }
        

    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Alpha1))
        {
            selectedSlot = 0;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            selectedSlot = 1;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            selectedSlot = 2;
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            selectedSlot = 3;
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            selectedSlot = 4;
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
            selectedSlot = 5;
        }
        if (Input.GetKey(KeyCode.Alpha7))
        {
            selectedSlot = 6;
        }
        if (Input.GetKey(KeyCode.Alpha8))
        {
            selectedSlot = 7;
        }
        if (Input.GetKey(KeyCode.Alpha9))
        {
            selectedSlot = 8;
        }
        if (Input.mouseScrollDelta.y > 0)
            selectedSlot++;
        if (Input.mouseScrollDelta.y < 0)
            selectedSlot--;
        selectedSlot = (short)Mathf.Repeat(selectedSlot, inventory.Count);

        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        snappedMousePos = new Vector2(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y));
        mouseFeedbackSquare.transform.position = snappedMousePos;
        if(grownCropLocations.Contains(snappedMousePos))
        {
            CropScript script = grownCrops[grownCropLocations.IndexOf(snappedMousePos)];
            Item cropItem = new Item(script.GetID(), script.GetAmount());
            if (Input.GetMouseButtonDown(1))
            {
                if(AddItem(cropItem))
                {
                    grownCrops.Remove(script);
                    grownCropLocations.Remove(script.transform.position);
                    filledPitLocations.Remove(script.transform.position);
                    Destroy(script.gameObject);
                }
            }
        }
        switch(selectedItem.ID)
        {
            //If the farmer uses the hoe, the place the hoe is used should be noted so that only one pit can be created there.
            //If the hoe is already used on a square, the farmer is not allowed to use it again.
            case 1:
                if (pitLocations.Contains(snappedMousePos) || occupiedLocations.Contains(snappedMousePos))
                    feedbackIndex = 1;
                else
                    feedbackIndex = 0;
                if (Input.GetMouseButtonDown(0) && feedbackIndex == 0 && playerCanMove)
                {
                   
                    Instantiate(pitPrefab, snappedMousePos, Quaternion.identity);
                    pitLocations.Add(snappedMousePos);
                }
                break;
                    //The watering can waters pits, regardless of whether there is currently a crop in it or not.
            case 2:
                if (pitLocations.Contains(snappedMousePos))
                {
                    feedbackIndex = 0;
                }
                else
                    feedbackIndex = 2;
                if(feedbackIndex == 0 && Input.GetMouseButtonDown(0))
                {
                    wetPitLocations.Add(snappedMousePos);
                }
                break;
                //If there is a pit that has not been filled, the farmer may click their corn seed.
                case 3:
                if(pitLocations.Contains(snappedMousePos) && !filledPitLocations.Contains(snappedMousePos) && !occupiedLocations.Contains(snappedMousePos))
                {
                    feedbackIndex = 0;
                }
                else
                {
                    feedbackIndex = 1;
                }
                if (Input.GetMouseButtonDown(0) && feedbackIndex == 0)
                {
                    if (RemoveItem(selectedSlot))
                    {
                        Instantiate(cornCropPrefab, snappedMousePos, Quaternion.identity);
                        filledPitLocations.Add(snappedMousePos);
                    }
                }

                break;
        }
        //if the player can click, green. If the player cannot click, red. Special case, invisible.
        switch(feedbackIndex)
        {
            case 0:
                mouseFeedbackSquare.color = new Color(0, 1, 0, .5f);
                break;
            case 1:
                mouseFeedbackSquare.color = new Color(1, 0, 0, .5f);
                break;
            case 2:
                mouseFeedbackSquare.color = new Color(0, 0, 0, 0);
                break;
        }

    }

    private void FixedUpdate()
    {

        //Wait for the inventoryUIScript to make contact with the GameManager. Then let it create inventory slots and add a hoe and a watering can.
        if(inventoryUI != null && !instantiatedInventoryUI)
        {
            inventoryUI.InstantiateInventoryUI();

            AddItem(new Item(1, 1));
            AddItem(new Item(2, 1));
            instantiatedInventoryUI = true;
        }
        selectedItem = inventory[selectedSlot];
    }

    public void StartBartering()
    {
        playerCanMove = false;
        barteringScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void StopBartering()
    {
        playerCanMove = true;
        barteringScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public bool AddItem(Item item)
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i].ID == item.ID)
            {
                inventory[i].amount += item.amount;
                inventoryUI.UpdateInventory();
                return true;
            }
        }
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].ID == 0)
            {
                Debug.Log(i);

                inventory[i] = item;
                inventoryIDs[i] = item.ID;
                inventoryUI.UpdateInventory();
                return true;
            }
        }
        return false;
    }
    public bool RemoveItem(short inventoryIndex, short amount = 1)
    {
       
        if(inventory[inventoryIndex].amount - amount >= 0)
        {
            inventory[inventoryIndex].amount -= amount;
            if(inventory[inventoryIndex].amount == 0)
            {
                inventory[inventoryIndex].ID = 0;
                inventoryIDs[inventoryIndex] = 0;
            }
            inventoryUI.UpdateInventory();
            return true;
        }
        return false;
    }
}
