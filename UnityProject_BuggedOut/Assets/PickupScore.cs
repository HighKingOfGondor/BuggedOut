using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScore : PooledObject
{
    [Header("Settings")]
    public int scoreOnPickup = 10;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.GetComponentInParent<PlayerController>() != null)
        {
            LevelManager.instance.scoreCurrent += scoreOnPickup;
            DestroyThisObject();
        }
    }

}
