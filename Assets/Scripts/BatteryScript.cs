using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BatteryScript : MonoBehaviour {

    public GameObject chargeBar;
    public float charge = 1.0f;
    public bool canAttach = true;
    public IPort port;
    Vector3 barOffest = new Vector3(0, -0.5f, -1.0f);
    public Sprite emptySprite, fullSprite;

	// Use this for initialization
	void Start ()
    {
        chargeBar = Instantiate(chargeBar);
	}
	
    void Update()
    {

    }

    public float GetCharge()
    {
        return charge;
    }

    public void DetachFromPort()
    {
        if(port != null)
        {
            port.DetachBattery();
            port = null;
        }
    }

    public void DrainBattery(float drainRate)
    {
        charge -= drainRate * Time.deltaTime;

        charge = Mathf.Clamp(charge, 0.0f, 1.0f);

        if(charge < 0.05f)
        {
            GetComponent<SpriteRenderer>().sprite = emptySprite;
        }
    }

    public void ChargeBattery(float chargeRate)
    {
        charge += chargeRate * Time.deltaTime;

        charge = Mathf.Clamp(charge, 0.0f, 1.0f);

        if (charge > 0.05f)
        {
            GetComponent<SpriteRenderer>().sprite = fullSprite;
        }
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        chargeBar.transform.position = transform.position + barOffest;
        chargeBar.transform.localScale = new Vector3(charge, 1, 1);
	}
}
