using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisclaimerController : MonoBehaviour
{
    public float durationToPlay = 4f;

    public float timer = 0;

    public FadeToBlackComponent fade;

    public float timeToEnterNextScene = 4f;
    public float timeToFade = 3f;

    public string nextScene;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= timeToFade) {
            fade.fadeToBlack = true;
        }
        if(timer >= timeToEnterNextScene) {
            goToNextScene(nextScene);
        }
    }


    void goToNextScene(string ns)
    {
        if(ns == "")
        {
            return;
        }

        SceneManager.LoadScene(ns, LoadSceneMode.Single);
    }
}
