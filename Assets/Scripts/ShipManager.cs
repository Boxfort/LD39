using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipManager : MonoBehaviour
{
    float shipSpeed = 0.3f;
    float goalDistance = 100.0f;

    public float currentDistance = 0.0f;

    float shipHealth = 100.0f;
    float crewHealth = 100.0f;

    float crewDamageRate = 2.0f;
    float shipDamageRate = 5.0f;

    public bool powerOn = true;
    public bool thrusterMessage = true;
    public bool canLose = true;
    public bool fiftyShip = true;
    public bool critShip = true;
    public bool fiftyOxygen = true;
    public bool critOxygen = true;
    public bool thrusterFifty = true;
    public bool thrusterFifteen = true;

    public Image vignette;
    public Light mainLight;
    public Light playerLight;

    public CameraScript camera;
    public GameManager gm;

    public Image[] systemIcons = new Image[4];
    public Image[] healthBars = new Image[2];
    public AudioSource[] sounds;
    public PortScript[] ports;

    public MessageboxScript mbox;

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

        foreach (KeyValuePair<SystemType, float> system in systems)
        {
            if (system.Value > 0.5f && powerOn)
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

            if (system.Value <= 0.0f)
            {
                mbox.SystemAlert(system.Key);
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

        if(shipHealth <= 0.0f && canLose)
        {
            gm.GameOverShip();
        }

        if (shipHealth <= 50.0f && fiftyShip)
        {
            mbox.addMessagesToQueue(new string[] { "Hull integrity is at 50%!" });
            fiftyShip = false;
        }
        if (shipHealth <= 25.0f && critShip)
        {
            mbox.addMessagesToQueue(new string[] { "THE SHIP IS GOING DOWN!" });
            critShip = false;
        }
    }

    void DamageCrew()
    {
        crewHealth -= crewDamageRate * Time.deltaTime;

        if (powerOn)
            healthBars[1].fillAmount = crewHealth / 100;

        if (crewHealth <= 0.0f && canLose)
        {
            gm.GameOverOxygen();
        }

        if(crewHealth <= 50.0f && fiftyOxygen)
        {
            mbox.addMessagesToQueue(new string[] { "You've lost half of your oxygen supply, be careful!"});
            fiftyOxygen = false;
        }
        if(crewHealth <= 25.0f && critOxygen)
        {
            mbox.addMessagesToQueue(new string[] { "Oxygen supply is critical!" });
            critOxygen = false;
        }
    }

    void ThrustForward()
    {
        currentDistance += shipSpeed * Time.deltaTime;

        if(currentDistance > 0.0f && thrusterMessage)
        {
            mbox.addMessagesToQueue(new string[] { "Good, the thrusters are back online. You'll be home in no time.",
                                                    "Now make sure you keep the batteries for the other three systems charged too.",
                                                    "Good luck."
                                                    });
            foreach(PortScript port in ports)
            {
                port.isDraining = true;
            }

            thrusterMessage = false;
        }


        if (currentDistance > 50.0f && thrusterFifty)
        {
            mbox.addMessagesToQueue(new string[] { "You're halfway there, keep it up !"});
            thrusterFifty = false;
        }

        if (currentDistance > 15.0f && thrusterFifteen)
        {
            mbox.addMessagesToQueue(new string[] { "You're in the home stretch, don't give up now!" });
            thrusterFifteen = false;
        }

        if (currentDistance >= goalDistance)
        {
            canLose = false;
            gm.WinGame();
        }
    }

    public void UpdateSystem(SystemType type, float value)
    {
        systems[type] = value;
    }
}
