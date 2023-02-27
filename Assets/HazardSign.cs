using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSign : MonoBehaviour
{
    public bool flashing = false;

    SpriteRenderer sr;

    public float flashSwapDuration = 0.15f;

    float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(flashing == false) {
            sr.enabled = false;
        } else {
            FlashRoutine();
        }
    }

    void FlashRoutine() {
        timer += Time.deltaTime;

        if(timer >= flashSwapDuration) {
            timer = 0;
            sr.enabled = sr.enabled ? false : true;
        }
    }
}
