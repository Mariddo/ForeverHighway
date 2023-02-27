using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public Image [] lifeImages;
    public TMP_Text gameOverText;

    public static UIManager instance;

    public FadeToBlackComponent fadeToBlack;

    public float gameOverFadeRate = 0.4f;

    public void Awake() {
        if(instance == null) {

            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLives(int value) {


    }

    public void UpdateScore(long value) {

        scoreText.text = value.ToString();
    }

    public void SetGameOver() {

        gameOverText.enabled = true;
        fadeToBlack.fadeRate = gameOverFadeRate;
        fadeToBlack.fadeToBlack = true;
    }

    public void SetLives(int value) {
        for (int x = 0 ; x < lifeImages.Length ; x++) {
            if(value <= x) {
                lifeImages[x].enabled = false;
            } else {
                lifeImages[x].enabled = true;
            }
        }
    }
}
