using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBattery : MonoBehaviour {

    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = 2.5f;
        rb.velocity = new Vector2(5.0f, 9.3f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
