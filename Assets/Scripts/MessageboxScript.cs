using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageboxScript : MonoBehaviour
{
    float messageDelay = 1.5f;
    float exitDelay = 3.0f;
    float letterDelay = 0.05f;
    float nextDelay = 0.5f;

    public Text messageText;
    public Animator mboxAnim;

    bool isOpen;

	// Use this for initialization
	void Start ()
    {
        
	}

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            showMessages(new string[] {"Engineer! It looks like you're the only one who made it back to the ship.", "You better get those batteries charged up and get the hell out of dodge before the bounty hunters show up."});
        }
    }
	
    public void showMessages(string[] messages)
    {
        IEnumerator coroutine = messageRoutine(messages, false);
        StartCoroutine(coroutine);
    }

    IEnumerator messageRoutine(string[] messages, bool close)
    {
        if(!isOpen)
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

        if (close)
        {
            yield return new WaitForSeconds(exitDelay);
            mboxAnim.SetTrigger("close");
        }
    }
}
