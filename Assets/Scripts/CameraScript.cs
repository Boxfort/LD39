using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    float camSpeed = 6.0f;
    float shakeDuration = 0.25f;
    float shakeMagnitude = 0.05f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Vector3 position = Vector3.Lerp(transform.position, target.position, camSpeed * Time.deltaTime);
        transform.position = new Vector3(position.x, position.y, -10.0f);
	}

    public void ShakeCamera()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float elapsed = 0.0f;

        Vector3 originalCamPos = Camera.main.transform.position;

        while (elapsed < shakeDuration)
        {

            elapsed += Time.deltaTime;

            float percentComplete = elapsed / shakeDuration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= shakeMagnitude * damper;
            y *= shakeMagnitude * damper;

            Camera.main.transform.position = new Vector3(originalCamPos.x + x, originalCamPos.y + y, originalCamPos.z);

            originalCamPos = Vector3.Lerp(originalCamPos, target.position, camSpeed * Time.deltaTime);

            yield return null;
        }

        Camera.main.transform.position = originalCamPos;
    }
}
