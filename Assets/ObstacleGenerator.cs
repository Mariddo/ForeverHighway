using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public bool spawnObstacles;

    public HazardSpawner hazardSpawner;

    public Coroutine spawnCoroutine;

    public float minDelayBetweenHazards = 1f;
    public float maxDelayBetweenHazards = 2f;


    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnCoroutine == null && spawnObstacles == true) {
            spawnCoroutine = StartCoroutine(SpawnHazardCoroutine());
        } else if(spawnObstacles == false && spawnCoroutine != null) {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    IEnumerator SpawnHazardCoroutine() {

        while(true) {
            SpawnHazard();

            yield return new WaitForSeconds(Random.Range(minDelayBetweenHazards, maxDelayBetweenHazards));
        }

    } 


    void SpawnHazard() {
        hazardSpawner.SpawnRandomHazard();
    }

}
