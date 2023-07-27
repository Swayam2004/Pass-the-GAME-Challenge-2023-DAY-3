using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarterItemScript : MonoBehaviour
{
    public Sprite sprite;

    [SerializeField]
    private Image image;

    private void Update()
    {
        image.sprite = sprite;
    }
}
