using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    float shipSpeed = 0.5f;
    float goalDistance = 100.0f;
    float currentDistance = 0.0f;

    float shipHealth = 100.0f;
    float crewHealth = 100.0f;

    float crewDamageRate = 2.0f;
    float shipDamageRate = 5.0f;

    public CameraScript camera;

    public Image[] systemIcons = new Image[4];

    public Image[] healthBars = new Image[2];

    Dictionary<SystemType, float> systems = new Dictionary<SystemType, float>()
    {
        {SystemType.thrusters, 100.0f},
        {SystemType.shields, 100.0f},
        {SystemType.oxygen, 100.0f},
        {SystemType.lights, 100.0f}
    };

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(systems[SystemType.thrusters] > 0)
        {
            ThrustForward();
        }
        if(systems[SystemType.oxygen] <= 0)
        {
            DamageCrew();
        }

        foreach(KeyValuePair<SystemType, float> system in systems)
        {
            if(system.Value > 0.5f)
            {
                systemIcons[(int)system.Key].color = new Color32(255, 255, 255, 255);
            }
            else if (system.Value <= 0.0f)
            {
                systemIcons[(int)system.Key].color = new Color32(85, 85, 85, 255);
            }
            else if (system.Value < 0.25f)
            {
                systemIcons[(int)system.Key].color = new Color32(255, 107, 107, 255);
            }
            else if (system.Value < 0.5f)
            {
                systemIcons[(int)system.Key].color = new Color32(210, 187, 0, 255);
            }
        }
    }

    public void DamageShip()
    {
        if (systems[SystemType.shields] <= 0.0f)
        {
            shipHealth -= shipDamageRate;
            camera.ShakeCamera();
        }

        healthBars[0].fillAmount = shipHealth / 100;
    }

    void DamageCrew()
    {
        crewHealth -= crewDamageRate * Time.deltaTime;

        healthBars[0].fillAmount = crewHealth / 100;
    }

    void ThrustForward()
    {
        currentDistance += shipSpeed * Time.deltaTime;
    }

    public void UpdateSystem(SystemType type, float value)
    {
        systems[type] = value;
    }
}
