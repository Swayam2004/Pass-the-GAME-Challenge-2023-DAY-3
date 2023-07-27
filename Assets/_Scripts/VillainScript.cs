using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillainScript : MonoBehaviour
{
    [SerializeField]
    private GameObject player, E;
    [SerializeField]
    private SpriteRenderer shade;

    private bool currentlyDark;
    private GameManager manager;

    private void Awake()
    {
        manager = (GameManager)FindObjectOfType(typeof(GameManager));
    }

    private void FixedUpdate()
    {
        if(Vector2.Distance(player.transform.position, transform.position) < 10 && !currentlyDark && transform.localScale.y > .9f)
        {
            StartCoroutine(MakeDark());
        }
        else if(currentlyDark && Vector2.Distance(player.transform.position, transform.position) > 10)
        {
            StartCoroutine(MakeBright());
        }
    }

    private void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position) < 2 && manager.PlayerCanMove)
        {
            E.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E))
            {
                manager.StartBartering();
            }
        }
        else
            E.SetActive(false);
    }

    private IEnumerator MakeDark()
    {
        Debug.Log("Make Dark");
        currentlyDark = true;
        
        while(shade.color.a < .85f && currentlyDark)
        {
            yield return new WaitForFixedUpdate();
            shade.color = new Color(shade.color.r, shade.color.g, shade.color.b, shade.color.a + .01f);
        }
        shade.color = new Color(shade.color.r, shade.color.g, shade.color.b, .85f);

    }

    private IEnumerator MakeBright()
    {
        Debug.Log("Make Bright");

        currentlyDark = false;
        while (shade.color.a > .01f && !currentlyDark)
        {
            yield return new WaitForFixedUpdate();
            shade.color = new Color(shade.color.r, shade.color.g, shade.color.b, shade.color.a * .8f);
        }
        shade.color = new Color(shade.color.r, shade.color.g, shade.color.b, 0);
    }
}
