using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomingFire : MonoBehaviour
{
    float minTime = 1.0f;
    float maxTime = 3.0f;

    public ShipManager gameManager;

    // Update is called once per frame
    void Update()
    {
        
    }

    void Fire()
    {
        float time = Random.Range(minTime, maxTime);
        gameManager.DamageShip();
        Invoke("Fire", time);
    }
}