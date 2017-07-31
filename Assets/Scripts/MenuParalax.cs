using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuParalax : MonoBehaviour
{

    Vector2 mousepos;
    public GameObject background;

    Vector2 backgroundPos;

    float magnitude = 30.0f;

    // Use this for initialization
    void Start ()
    {
        backgroundPos = background.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 screenMiddle = new Vector2(Screen.width / 2, Screen.height / 2);

        Vector2 difference = (Vector2)Input.mousePosition - screenMiddle;

        Vector2 normalisedVector = new Vector2(difference.x / Screen.width, difference.y / Screen.height);

        background.transform.position = backgroundPos - (normalisedVector * magnitude);
	}
}
