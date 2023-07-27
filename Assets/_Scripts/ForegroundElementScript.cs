using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundElementScript : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;

    private void FixedUpdate()
    {
        sprite.sortingOrder = (int)-transform.position.y;
    }
}
