using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManagerScript : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private int textDelay;
    [SerializeField]
    private Image image;

    private bool skipLine;

    private Camera cam;

    private float VillainScaleY;

    private GameManagerScript manager;

    [SerializeField]
    private GameObject villain;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            skipLine = true;
        if (Input.GetMouseButtonUp(0))
            skipLine = false;
    }

    private void FixedUpdate()
    {
        villain.transform.localScale = new Vector3(1, VillainScaleY, 1);
    }

    private void Awake()
    {
        cam = Camera.main;
        manager = (GameManagerScript)FindObjectOfType(typeof(GameManagerScript));
        text.text = "";
        image.color = new Color(0, 0, 0, 0);
        Dialogue[] intro = new Dialogue[16];
        intro[0] = new Dialogue("", -1, 1);
        intro[1] = new Dialogue("Wait...", 0);
        intro[2] = new Dialogue("Oh, dagnabbit! Why did I build a farmhouse in the middle of the ocean?", 0);
        intro[3] = new Dialogue("I'm so far away, I can't even see a coast.", 0);
        intro[4] = new Dialogue("How am I ever going to get home?", 0);
        intro[5] = new Dialogue("Perhaps I can be of service.", 1, 4);
        intro[6] = new Dialogue("What peril you've found yourself in, farmer.", 1, 2);
        intro[7] = new Dialogue("500 miles off the nearest coast. Nary a living soul to aid.", 1);
        intro[8] = new Dialogue("Yet, I'm here for you. As recompense I beseech you for your particular skillset.", 1);
        intro[9] = new Dialogue("What? You want me to farm vegetables for you?", 0, 5);
        intro[10] = new Dialogue("Your mortal worts are of... limited use to me.", 1, 4);
        intro[11] = new Dialogue("Give me what I desire and freedom shall finally be yours.", 1);
        intro[12] = new Dialogue("Well then, creepy stranger, what do you 'desire'?", 0, 5);
        intro[13] = new Dialogue("Use your imagination. You'll figure it out.", 1, 4);
        intro[14] = new Dialogue("These morsels shall feed the soil. Barter me its retribution.", 1, 6);

        intro[15] = new Dialogue("", 1, 3);

        StartCoroutine(StartDialogue(intro));
    }

    //Write a sequence of dialogues to the dialogue box. Makes the dialogue box visible on the first line of dialogue that is not empty. Makes the dialogue box invisible when done.
    public IEnumerator StartDialogue(Dialogue[] dialogues)
    {
        for (int i = 0; i < dialogues.Length; i++)
        {
            switch (dialogues[i].eventIndex)
            {
                case 0:
                    break;
                case 1:
                    yield return new WaitForSeconds(1);
                    yield return StartCoroutine(Zoom());
                    yield return new WaitForSeconds(.5f);
                    break;
                case 2:
                    yield return new WaitForSeconds(1);
                    yield return StartCoroutine(GrowVillain());
                        break;
                case 3:
                    yield return StartCoroutine(MoveCam(new Vector2(0, 0)));
                    manager.playerCanMove = true;
                    break;
                case 4:
                    yield return StartCoroutine(MoveCam(new Vector2(6, 0)));
                    break;
                case 5:
                    yield return StartCoroutine(MoveCam(new Vector2(0, 0)));
                    break;
                case 6:
                    manager.AddItem(new Item(3, 5));
                    break;
            }
            if(dialogues[i].line != "")
                image.color = new Color(1, 1, 1, 1);
            yield return StartCoroutine(WriteDialogue(dialogues[i]));
        }
        image.color = new Color(0, 0, 0, 0);
        text.text = "";
    }

    //Write a single string to the dialogue box letter by letter. The punctuation marks '.', ',', '?' and '!' take twice as long. Skip with the left mouse button. Awaits a left mouse button release before continuing.
    private IEnumerator WriteDialogue(Dialogue dialogue)
    {
        text.text = "";
        string writtenDialogue = "";
        yield return new WaitForFixedUpdate();
        for(int i = 0; i < dialogue.line.Length; i++)
        {
            for (int j = 0; j < textDelay; j++)
            {
                if(skipLine)
                {
                    writtenDialogue = dialogue.line;
                    i = dialogue.line.Length;
                }

                yield return new WaitForFixedUpdate();
            }
            if (i < dialogue.line.Length)
            {
                writtenDialogue += dialogue.line[i];
                text.text = writtenDialogue;
                if (dialogue.line[i] == '.' || dialogue.line[i] == ',' || dialogue.line[i] == '?' || dialogue.line[i] == '!')
                {
                    for (int k = 0; k < textDelay; k++)
                    {
                        if (skipLine)
                        {
                            writtenDialogue = dialogue.line;
                            i = dialogue.line.Length;
                        }
                        yield return new WaitForFixedUpdate();
                    }
                }
            }
            text.text = writtenDialogue;


        }

        //Awaits a click only if there is a line of dialogue.
        while (!Input.GetMouseButtonDown(0) && text.text != "")
        {
            yield return null;
        }
        while (!Input.GetMouseButtonUp(0) && text.text != "")
        {
            yield return null;
        }
    }

    private IEnumerator Zoom()
    {

        cam.orthographicSize = 40;
        while (cam.orthographicSize > 5)
        {
            yield return new WaitForFixedUpdate();
            cam.orthographicSize *= .9f;
        }
        cam.orthographicSize = 5;
    }

    private IEnumerator MoveCam(Vector2 pos)
    {
        while((Vector2)cam.transform.localPosition != pos)
        {
            yield return new WaitForFixedUpdate();
            cam.transform.localPosition = (Vector3)Vector2.MoveTowards(cam.transform.localPosition, pos, .2f) + new Vector3(0,0,-10);
        }
    }

    private IEnumerator GrowVillain()
    {
        while(VillainScaleY < 1)
        {
            VillainScaleY += .1f;
            yield return new WaitForFixedUpdate();
        }
        VillainScaleY = 1;
    }
}
