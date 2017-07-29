using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    float camSpeed = 6.0f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Vector3 position = Vector3.Lerp(transform.position, target.position, camSpeed * Time.deltaTime);

        transform.position = new Vector3(position.x, position.y, -10.0f);
	}
}
