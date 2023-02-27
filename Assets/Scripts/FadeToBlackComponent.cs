using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class FadeToBlackComponent : MonoBehaviour
{
    Image image;

    public float currentAlpha;

    public bool fadeToBlack;

    public float fadeRate = 0.10f;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        
        Color color = image.color;

        color.a = currentAlpha;

        image.color = color;

    }

    // Update is called once per frame
    void Update()
    {

        float fadeThisMuch = fadeRate * Time.deltaTime;

        if(fadeToBlack && currentAlpha < 1f)
        {
            currentAlpha += fadeThisMuch;
        }
        else if(!fadeToBlack && currentAlpha > 0f)
        {
            currentAlpha -= fadeThisMuch;
        }

        Color color = image.color;

        color.a = currentAlpha;

        image.color = color;
    }
}
