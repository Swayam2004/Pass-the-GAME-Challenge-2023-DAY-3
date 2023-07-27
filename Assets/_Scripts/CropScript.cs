using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropScript : MonoBehaviour
{

    [SerializeField]
    SpriteRenderer sprite;
    [SerializeField]
    private short minAmount, maxAmount, ID, waitingTime;
    private short waitedTime, currentStage;
    [SerializeField]
    private Sprite[] stages;
    private GameManager manager;
    [SerializeField]
    private ParticleSystem particles;

    private void Awake()
    {
        manager = (GameManager)FindObjectOfType(typeof(GameManager));
    }

    private void FixedUpdate()
    {
        if (manager.wetPitLocations.Contains(transform.position))
        {
            if(!particles.isPlaying)
                particles.Play();
            waitedTime++;
            if (waitedTime >= waitingTime && currentStage < stages.Length - 1)
            {
                currentStage++;
                manager.wetPitLocations.Remove(transform.position);
                waitedTime = 0;
            }
        }
        else
            particles.Stop();
        sprite.sprite = stages[currentStage];
        if(currentStage == stages.Length - 1)
        {
            Debug.Log("Fully grown");
            if(!manager.grownCrops.Contains(this))
            {
                manager.grownCrops.Add(this);
                manager.grownCropLocations.Add(transform.position);
            }
        }
    }

    public short GetID()
    {
        return ID;
    }

    public short GetAmount()
    {
        return (short)Random.Range(minAmount, maxAmount);
    }
}
