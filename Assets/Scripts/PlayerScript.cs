using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    float moveSpeed = 6.75f;
    float sprintBonus = 1.5f;
    float activeSprintBonus = 1.0f;

    Rigidbody2D rb;
    PickupScript ps;

    public Sprite deathSprite;

    bool dead;

    public GameManager gm;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        ps = GetComponent<PickupScript>();
	}

    public void kill()
    {
        GetComponent<SpriteRenderer>().sprite = deathSprite;
        dead = true;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (gm.paused || dead)
            return;

        if(Input.GetKey(KeyCode.LeftShift) && ps.heldItem == null)
        {
            activeSprintBonus = sprintBonus;
        }
        else
        {
            activeSprintBonus = 1.0f;
        }

        //Turn player towards the mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rotation = Quaternion.LookRotation(transform.position - mousePos, Vector3.forward);
        transform.rotation = rotation;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

        //Move player
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * activeSprintBonus;
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
	}
}
