using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShip : MonoBehaviour
{
    public Image bar;
    public Image ship;

    public ShipManager sm;

    float progress = 0.0f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!sm.powerOn)
            return; 

        progress = sm.currentDistance / 100;

        bar.fillAmount = progress;

        float shipX = (Screen.width/20) + (Mathf.Clamp(Screen.width * progress, 0, Screen.width * 0.9f));
        ship.rectTransform.position = new Vector3(shipX, ship.rectTransform.position.y, ship.rectTransform.position.z);

        progress = Mathf.Clamp(progress, 0, 1.0f);
	}
}
