using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpawner : MonoBehaviour
{
    
    public static HazardSpawner instance;

    public void Awake() {
        if(instance == null) {

            instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    
    public Transform[] roadHazards;
    public OffRoadHazardSpawner[] offRoadHazards;

    public float offRoadHazardChance = 0.25f;

    public GameObject[] roadHazardList;

    public GameObject[] offroadHazardList;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnRandomHazard() {

        if(Random.Range(0f,1f) < offRoadHazardChance) {
            SpawnOffRoadHazard();
        } else {
            SpawnOnRoadHazard();
        }
    }

    void SpawnOffRoadHazard() {
        OffRoadHazardSpawner offRoadHazard = offRoadHazards[Random.Range(0, offRoadHazards.Length)];

        offRoadHazard.SpawnOffRoadHazard();
    }

    void SpawnOnRoadHazard() {

        GameObject roadHazardSpawner = roadHazardList[Random.Range(0, roadHazardList.Length)];

        Transform hazardLocation = roadHazards[Random.Range(0, roadHazards.Length)];

        GameObject hazard = Instantiate(roadHazardSpawner, hazardLocation.position, hazardLocation.rotation);

        float velocity = Random.Range(GameManager.instance.roadHazardSpeedMultiplierRange.x, GameManager.instance.roadHazardSpeedMultiplierRange.y);

        velocity *= GameManager.instance.roadSpeed;

        hazard.GetComponent<Hazard>().velocity = velocity * -1;
    }

    public GameObject GetRandomOffRoadObstacle() {

        return offroadHazardList[Random.Range(0, offroadHazardList.Length)];
    }
}
