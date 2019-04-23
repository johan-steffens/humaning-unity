using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashBackground : MonoBehaviour
{
    public float flashInterval = 0.5f;

    private float totalSeconds = 0f;
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }
    
    void Update()
    {
        totalSeconds += Time.deltaTime;
        if(totalSeconds >= flashInterval)
        {
            totalSeconds = 0;
            image.enabled = !image.enabled;
        }
    }
}
