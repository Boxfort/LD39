using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortScript : MonoBehaviour, IPort
{
    GameObject attachedBattery;
    BatteryScript batteryScript;
    public GameManager gameManager;
    public SystemType type;
    public float drainRate = 0.1f;

    AudioSource[] sounds;

	// Use this for initialization
	void Start ()
    {
        sounds = GetComponents<AudioSource>();
	}

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

    // Update is called once per frame
    void Update ()
    {
        if (batteryScript != null)
        {
            batteryScript.DrainBattery(drainRate);
            attachedBattery.transform.position = new Vector3(transform.position.x, transform.position.y, attachedBattery.transform.position.z);
            attachedBattery.transform.rotation = transform.rotation;
        }

        if (attachedBattery != null)
        {
            gameManager.UpdateSystem(type, batteryScript.GetCharge());
        }
        else
        {
            gameManager.UpdateSystem(type, 0.0f);
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
