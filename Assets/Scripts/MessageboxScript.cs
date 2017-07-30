using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageboxScript : MonoBehaviour
{
    float messageDelay = 1.5f;
    float exitDelay = 3.0f;
    float letterDelay = 0.05f;

    public Text messageText;
    public Animator mboxAnim;

	// Use this for initialization
	void Start ()
    {
        
	}

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            showMessage("This is a test message, pretty snazzy huh? Your ship is exploding btw, bye.");
        }
    }
	
    public void showMessage(string message)
    {
        IEnumerator coroutine = messageRoutine(message);
        StartCoroutine(coroutine);
    }

    IEnumerator messageRoutine(string message)
    {
        Debug.Log("show the message FAM");
        mboxAnim.SetTrigger("open");
        yield return new WaitForSeconds(messageDelay);

        foreach (char letter in message.ToCharArray())
        {
            messageText.text += letter;
            yield return new WaitForSeconds(letterDelay);
        }

        yield return new WaitForSeconds(exitDelay);
        mboxAnim.SetTrigger("close");
    }

}
