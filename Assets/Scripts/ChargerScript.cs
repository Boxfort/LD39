using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerScript : MonoBehaviour, IPort
{
    GameObject attachedBattery;
    BatteryScript batteryScript;

    float chargeRate = 0.2f;

    public void AttachBattery(GameObject battery)
    {
        attachedBattery = battery;
        batteryScript = attachedBattery.GetComponent<BatteryScript>();
        batteryScript.port = this;
        battery.transform.position = new Vector3(transform.position.x, transform.position.y, battery.transform.position.z);
        battery.transform.rotation = transform.rotation;
        battery.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    public void DetachBattery()
    {
        batteryScript = null;
        attachedBattery.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        attachedBattery = null;
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (batteryScript != null)
        {
            batteryScript.ChargeBattery(chargeRate);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Battery" && col.GetComponent<BatteryScript>().canAttach)
        {
            AttachBattery(col.gameObject);
        }
    }
}
