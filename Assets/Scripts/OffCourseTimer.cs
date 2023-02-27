using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffCourseTimer : MonoBehaviour
{
    public SpriteRenderer sr;

    public float timeToCrash = 3f;

    float crashTimer;

    public bool offCourse;

    Coroutine shakeCoroutine;

    public Transform objectToShake;

    public float shakeValue = 0.05f;
    
    // Start is called before the first frame update
    void Start()
    {
        shakeCoroutine = null;
        offCourse = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(offCourse) {
            crashTimer += Time.deltaTime;
        } else if (!offCourse && crashTimer > 0) {
            crashTimer -= Time.deltaTime;
        }

        //OffCourseEffects();

        OffCourseShake();
    }

    void OffCourseEffects() {

        float colorValue = 255 * (crashTimer / timeToCrash);

        sr.color = new Color(255, 255 - colorValue, 255 - colorValue);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "OffCourseZone") {
            Debug.Log("Off Course!");
            offCourse = true;
        }

    }

    void OffCourseShake() {

        if(offCourse) {
            shakeCoroutine = StartCoroutine(Shake());
        } else if (!offCourse && shakeCoroutine != null){
            StopCoroutine(shakeCoroutine);
            shakeCoroutine = null;

            objectToShake.position = transform.position;
        }
    }

    IEnumerator Shake() {

        Vector3 shakeVector = new Vector3(Random.Range(-shakeValue, shakeValue), Random.Range(-shakeValue, shakeValue), 0);

        objectToShake.transform.position += shakeVector;
        yield return new WaitForSeconds(0.01f);

        objectToShake.transform.position -= shakeVector;
        yield return new WaitForSeconds(0.01f);
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "OffCourseZone") {
            offCourse = false;
        }

    }
}
