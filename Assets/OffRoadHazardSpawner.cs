using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffRoadHazardSpawner : MonoBehaviour
{
    public HazardSign warningSign;

    public float instantiateDelay = 0.5f;

    public bool busy;

    float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(timer > 0f && busy == true) {
            timer -= Time.deltaTime;
            warningSign.flashing = true;
        } else if (timer <= 0f && busy == true) {
            warningSign.flashing = false;
            busy = false;
            ReleaseObstacle();
        }
    }

    public void SpawnOffRoadHazard() {

        if(busy == false) {
            PrepareObstacleSpawn();
        }
    }

    void PrepareObstacleSpawn() {

        busy = true;
        timer = instantiateDelay;

    }

    void ReleaseObstacle() {

        GameObject objectToSpawn = HazardSpawner.instance.GetRandomOffRoadObstacle();

        GameObject spawnThis = Instantiate(objectToSpawn, this.transform.position, this.transform.rotation);

        spawnThis.GetComponent<Hazard>().velocity = GameManager.instance.roadSpeed * GameManager.instance.offRoadHazardSpeedMultiplier * -1f;
    }

}
