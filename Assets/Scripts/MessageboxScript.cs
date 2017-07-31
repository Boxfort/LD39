using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageboxScript : MonoBehaviour
{
    float messageDelay = 1.5f;
    float exitDelay = 2.0f;
    float letterDelay = 0.05f;
    float nextDelay = 1.0f;

    public Text messageText;
    public Animator mboxAnim;

    bool oxygenTriggered = false, shieldTriggered = false, lightsTriggered = false;

    bool readingMessage = false;

    Queue<string[]> messageQueue = new Queue<string[]>();

    // Use this for initialization
    void Start ()
    {
        addMessagesToQueue(new string[] {"Engineer! It looks like you're the only one who made it back to the ship.",
                                        "You better get those batteries charged up and get the hell out of dodge before the bounty hunters show up.",
                                        "Pick up those batteries and put them into the charging stations beside you, then take them to the engine room and get this thing moving!",
                                        "Remember, you've gotta keep recharging the batteries when they run outa juice."
                                        });
    }

    void Update()
    {
        if(messageQueue.Count > 0 && !readingMessage)
        {
            showMessages(messageQueue.Dequeue());
        }
    }

    public void SystemAlert(SystemType type)
    {
        switch (type)
        {
            case SystemType.shields:
                if(!shieldTriggered)
                {
                    addMessagesToQueue(new string[] { "The shields are down! We're gonna start taking damage from those bounty hunters. Get them back online!" });
                    shieldTriggered = true;
                }
                break;
            case SystemType.oxygen:
                if(!oxygenTriggered)
                {
                    addMessagesToQueue(new string[] { "Auxillary power is out, you're not going to be able to see the status of any of your systems, or five feet in front of you." });
                    oxygenTriggered = true;
                }
                break;
            case SystemType.lights:
                if(!lightsTriggered)
                {
                    addMessagesToQueue(new string[] { "Auxillary power is out, you're not going to be able to see the status of any of your systems, or five feet in front of you." });
                    lightsTriggered = true;
                }
                break;
        }
    }

    public void addMessagesToQueue(string[] messages)
    {
        messageQueue.Enqueue(messages);
    }
	
    public void showMessages(string[] messages)
    {
        IEnumerator coroutine = messageRoutine(messages);
        StartCoroutine(coroutine);
    }

    IEnumerator messageRoutine(string[] messages)
    {
        readingMessage = true;
        mboxAnim.SetTrigger("open");
        yield return new WaitForSeconds(messageDelay);

        foreach(string message in messages)
        {
            messageText.text = "";

            foreach (char letter in message.ToCharArray())
            {
                messageText.text += letter;
                yield return new WaitForSeconds(letterDelay);
            }

            yield return new WaitForSeconds(nextDelay);
        }

        yield return new WaitForSeconds(exitDelay);
        messageText.text = "";
        mboxAnim.SetTrigger("close");
        readingMessage = false;
    }
}
