using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour {

    float pickupDistance = 0.6f;

    GameObject heldItem;

	// Use this for initialization
	void Start ()
    {
	}

    void pickupItem(GameObject item)
    { 
        heldItem = item;
        heldItem.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        heldItem.transform.parent = transform;
        heldItem.transform.localPosition = new Vector3(0, 1.0f, 0) * pickupDistance;
        heldItem.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90.0f));
    }

    void dropItem()
    {
        Vector3 itemPos = heldItem.transform.position;
        heldItem.transform.parent = null;
        heldItem.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        heldItem.transform.position = itemPos;
        heldItem = null;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.DrawRay(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position, Color.red, 1.0f);

        if (Input.GetButtonDown("Fire1"))
        {
            if(heldItem != null)
            {
                dropItem();
            }
            else
            {
                //Check for a grabbable item
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position, pickupDistance);
                foreach(RaycastHit2D hit in hits)
                {
                    if (hit.collider.tag == "Battery")
                    {
                        pickupItem(hit.collider.gameObject);
                        break;
                    }
                }
            }
        }
	}
}
