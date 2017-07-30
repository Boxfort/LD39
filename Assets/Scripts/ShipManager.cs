using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipManager : MonoBehaviour
{
    float shipSpeed = 0.5f;
    float goalDistance = 100.0f;

    public float currentDistance = 0.0f;

    float shipHealth = 100.0f;
    float crewHealth = 100.0f;

    float crewDamageRate = 2.0f;
    float shipDamageRate = 5.0f;

    bool powerOn = true;

    bool powerBeenOn = false;

    public Image vignette;
    public Light mainLight;
    public Light playerLight;

    public CameraScript camera;

    public Image[] systemIcons = new Image[4];

    public Image[] healthBars = new Image[2];

    public AudioSource[] sounds;

    public Dictionary<SystemType, float> systems = new Dictionary<SystemType, float>()
    {
        {SystemType.thrusters, 100.0f},
        {SystemType.shields, 100.0f},
        {SystemType.oxygen, 100.0f},
        {SystemType.lights, 100.0f}
    };
	
    void Start()
    {
        sounds = GetComponents<AudioSource>();
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
        if(systems[SystemType.lights] <= 0 && powerOn)
        {
            PowerOff();
        }
        else if(systems[SystemType.lights] > 0 && !powerOn)
        {
            PowerOn();
        }

        foreach(KeyValuePair<SystemType, float> system in systems)
        {
            if(system.Value > 0.5f && powerOn)
            {
                systemIcons[(int)system.Key].color = new Color32(255, 255, 255, 255);
            }
            else if (system.Value <= 0.0f || !powerOn)
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

    public void PowerOff()
    {
        powerOn = false;
        mainLight.enabled = false;
        playerLight.enabled = true;
        vignette.enabled = true;
        healthBars[0].color = new Color(0.25f, 0.25f, 0.25f);
        healthBars[1].color = new Color(0.25f, 0.25f, 0.25f);

        if(powerBeenOn)
            sounds[0].Play();
    }

    public void PowerOn()
    {
        powerOn = true;
        mainLight.enabled = true;
        playerLight.enabled = false;
        vignette.enabled = false;
        healthBars[0].color = Color.white;
        healthBars[1].color = Color.white;

        powerBeenOn = true;
    }

    public void DamageShip()
    {
        if (systems[SystemType.shields] <= 0.0f)
        {
            shipHealth -= shipDamageRate;
            camera.ShakeCamera();
            sounds[1].Play();
        }

        if(powerOn)
            healthBars[0].fillAmount = shipHealth / 100;

        
    }

    void DamageCrew()
    {
        crewHealth -= crewDamageRate * Time.deltaTime;

        if (powerOn)
            healthBars[1].fillAmount = crewHealth / 100;
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
