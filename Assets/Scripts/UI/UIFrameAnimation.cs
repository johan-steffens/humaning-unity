using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFrameAnimation : MonoBehaviour
{

    public Sprite[] frames;
    public float frameSpeed;
    public bool shouldAnimate = true;

    private Image image;
    
    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(Animate());
    }

    public IEnumerator Animate()
    {
        while(shouldAnimate) for (int i = 0; i < frames.Length; i++)
        {
            image.sprite = frames[i];
            yield return new WaitForSeconds(frameSpeed);
        }
    }
}
