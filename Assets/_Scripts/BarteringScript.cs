using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarteringScript : MonoBehaviour
{
    [SerializeField]
    private GameObject itemPrefab, layoutGroup;
    private List<BarterItemScript> items = new List<BarterItemScript>();
    private GameManager manager;
    private void Update()
    {
        if(manager == null)
        {
            manager = GameManager.Instance;

            for(int i = 0; i < manager.inventory.Count; i++)
            {
                items.Add(Instantiate(itemPrefab, layoutGroup.transform).GetComponent<BarterItemScript>());
                items[i].sprite = manager.itemSprites[manager.inventory[i].ID];
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            manager.StopBartering();
        }
    }
}
