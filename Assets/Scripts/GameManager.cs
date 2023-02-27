using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct SpeedInterval {
    public long seconds;
    public float speed;
    public float minDelayBetweenHazards;
    public float maxDelayBetweenHazards;
}

public class GameManager : MonoBehaviour
{
    
    //Singleton Object;

    public static GameManager instance;

    
    public void Awake() {
        if(instance == null) {

            instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    
    
    
    public float timer;

    public float runTimer;

    public long seconds;

    public ParallaxScroller road;

    public float roadSpeed = 1.5f;

    float targetSpeed;

    public SpeedInterval[] speedIntervals;

    public float speedIncreaseRate = 0.10f;

    


    [Header("LifeSystem")]
    public int currentLives;
    public int startingLives = 3;
    public int maxLives = 6;

    [Header("Hazard Speeds")]
    public float offRoadHazardSpeedMultiplier = 3f;
    public Vector2 roadHazardSpeedMultiplierRange;
    ObstacleGenerator hazardSpawner;
    
    [Header("Player Spawn")] 
    public PlayerSpawner playerSpawner;

    [Header("Player Respawn")]
    public bool playerDown;

    // Start is called before the first frame update
    void Start()
    {
        if(speedIntervals.Length > 0) {
            roadSpeed = speedIntervals[0].speed;
            targetSpeed = roadSpeed;
        }
        
        seconds = 0;
    
        road.moveSpeed.y = roadSpeed;

        hazardSpawner = GetComponent<ObstacleGenerator>();

        SpawnPlayer();
        currentLives = startingLives;
        UIManager.instance.SetLives(currentLives);
    }

    // Update is called once per frame
    void Update()
    {
        
        
        timer += Time.deltaTime;

        if(timer >= 1f) {
            timer -= 1f;
            seconds++;
            UpdateRoadSpeed();
            UpdateDelayBetweenHazards();
        }

            if(roadSpeed <= targetSpeed) {
                roadSpeed += speedIncreaseRate * Time.deltaTime;
                road.moveSpeed.y = roadSpeed;
            }

              
    }

    void UpdateRoadSpeed() {

        for (int x = speedIntervals.Length-1 ; x >= 0 ; x--) {
            if(seconds >= speedIntervals[x].seconds) {
                targetSpeed = speedIntervals[x].speed;
                break;
            }
        }
        if(seconds <= speedIntervals[0].seconds) {
            targetSpeed = speedIntervals[0].speed;
        }
    }

    void UpdateDelayBetweenHazards() {

        for (int x = speedIntervals.Length-1 ; x >= 0 ; x--) {
            if(seconds >= speedIntervals[x].seconds) {
                
                hazardSpawner.minDelayBetweenHazards = speedIntervals[x].minDelayBetweenHazards;
                hazardSpawner.maxDelayBetweenHazards = speedIntervals[x].maxDelayBetweenHazards;
                break;
            }
        }
        if(seconds <= speedIntervals[0].seconds) {
            hazardSpawner.minDelayBetweenHazards = speedIntervals[0].minDelayBetweenHazards;
            hazardSpawner.maxDelayBetweenHazards = speedIntervals[0].maxDelayBetweenHazards;

        }
    }



    public void SpawnPlayer() {
        if(playerSpawner != null) {
            playerSpawner.SpawnPlayer();
        }
    }

    public void HandlePlayerDeath() {
        Debug.Log("HandlePlayerDeath called");
        if(currentLives <= 0) {
            //Handle Game Over Scenario
            StartCoroutine(GameOverCoroutine());
            
        } else {
            
            Debug.Log("Player has lives remaining, RespawnCoroutine called");
            //We need to remove a life and reset the player.
            StartCoroutine(RespawnCoroutine());
        }
    }


    IEnumerator GameOverCoroutine() {

        
        yield return new WaitForSeconds(2.5f);
        UIManager.instance.SetGameOver();
    }

    IEnumerator RespawnCoroutine() {

        GetComponent<ObstacleGenerator>().spawnObstacles = false;
        yield return new WaitForSeconds(3f);

        Debug.Log("Calling SpawnPlayer");
        playerSpawner.SpawnPlayer();
        

        GetComponent<ObstacleGenerator>().spawnObstacles = true;

    }
}
