using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEyes : MonoBehaviour
{
    Vector3 originalScale;
    Vector3 targetScale;

    float blinkCounter = 2f;

    private void Awake()
    {
        originalScale = transform.localScale;
        targetScale = new Vector3(originalScale.x, originalScale.y * 0.2f, originalScale.z);
    }

    private void Update()
    {
        blinkCounter -= Time.deltaTime;
        if (blinkCounter <= 0)
        {
            StartCoroutine(blink());
            blinkCounter = Random.Range(0.5f,4f);
        }
    }



    IEnumerator blink()
    {
        transform.localScale = targetScale;
        yield return new WaitForSeconds(0.1f);
        transform.localScale = originalScale;
    }
}
