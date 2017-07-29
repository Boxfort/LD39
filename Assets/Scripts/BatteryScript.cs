using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryScript : MonoBehaviour {

    public GameObject chargeBar;
    float charge = 1.0f;

    Vector3 barOffest = new Vector3(0, -0.5f, -1.0f);

	// Use this for initialization
	void Start ()
    {
        chargeBar = Instantiate(chargeBar);
	}
	
    void Update()
    {

    }

    public void DrainBattery(float drainRate)
    {
        charge -= drainRate * Time.deltaTime;
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        chargeBar.transform.position = transform.position + barOffest;
        chargeBar.transform.localScale = new Vector3(charge, 1, 1);
	}
}
