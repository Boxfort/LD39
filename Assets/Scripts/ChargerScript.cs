using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerScript : MonoBehaviour, IPort
{
    GameObject attachedBattery;
    BatteryScript batteryScript;

    float chargeRate = 0.2f;

    AudioSource[] sounds;


    public void AttachBattery(GameObject battery)
    {
        attachedBattery = battery;
        batteryScript = attachedBattery.GetComponent<BatteryScript>();
        batteryScript.port = this;
        battery.transform.position = new Vector3(transform.position.x, transform.position.y, battery.transform.position.z);
        battery.transform.rotation = transform.rotation;
        battery.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        sounds[0].Play();
    }

    public void DetachBattery()
    {
        batteryScript = null;
        attachedBattery.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        attachedBattery = null;

        sounds[0].Play();
    }
    
    // Use this for initialization
    void Start ()
    {
        if(batteryScript != null)
        {
            batteryScript.port = this;
        }
        sounds = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (batteryScript != null)
        {
            batteryScript.ChargeBattery(chargeRate);
            attachedBattery.transform.position = new Vector3(transform.position.x, transform.position.y, attachedBattery.transform.position.z);
            attachedBattery.transform.rotation = transform.rotation;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Battery" && col.GetComponent<BatteryScript>().canAttach && attachedBattery == null)
        {
            AttachBattery(col.gameObject);
        }
    }
}
