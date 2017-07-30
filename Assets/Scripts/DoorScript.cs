﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Animator am;
    BoxCollider2D collide;

    AudioSource[] sounds;

	// Use this for initialization
	void Start ()
    {
        am = transform.parent.GetComponent<Animator>();
        collide = transform.parent.GetComponent<BoxCollider2D>();

        sounds = transform.parent.GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            am.SetTrigger("open");
            collide.enabled = false;
            sounds[0].Play();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            am.SetTrigger("close");
            collide.enabled = true;
        }
    }
}
