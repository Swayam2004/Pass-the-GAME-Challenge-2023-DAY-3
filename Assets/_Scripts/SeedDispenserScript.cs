using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedDispenserScript : MonoBehaviour
{
    [SerializeField]
    private GameObject player, E;
    private DialogueManagerScript dialogueManager;
    private GameManager manager;

    private void Awake()
    {
        manager = (GameManager)FindObjectOfType(typeof(GameManager));
        dialogueManager = (DialogueManagerScript)FindObjectOfType(typeof(DialogueManagerScript));
        E.SetActive(false);
    }

    private void Update()
    {
        if(Vector2.Distance(player.transform.position, transform.position) < 2)
        {
            E.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E) && manager.PlayerCanMove)
            {
                switch (manager.selectedItem.ID)
                {
                    case 4:
                        short amount = 0;
                        int rng = (short)Random.Range(0, 100);
                        if(rng < 10)
                        {
                            amount = 0;
                        }
                        else if(rng < 25)
                        {
                            amount = 1;
                        }
                        else if (rng < 50)
                        {
                            amount = 2;
                        }
                        else if(rng < 75)
                        {
                            amount = 3;
                        }
                        else
                        {
                            amount = 4;
                        }
                            
                        if(manager.AddItem(new Item(3, amount)))
                        {
                            manager.RemoveItem(manager.selectedSlot);
                        }
                        break;
                    default:
                        manager.PlayerCanMove = false;
                        Dialogue[] explanation = new Dialogue[2];
                        explanation[0] = new Dialogue("Insert a crop to create seeds!", 2);
                        explanation[1] = new Dialogue("", 2, 3);
                        StartCoroutine(dialogueManager.StartDialogue(explanation));
                        break;
                }
            }
        }
        else
        {
            E.SetActive(false);
        }
    }

}
