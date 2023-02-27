using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject prefabToInstantiate;

    public Transform startingPosition;

    public Transform endingPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlayer() {
        GameObject player = Instantiate(prefabToInstantiate, startingPosition.position, startingPosition.rotation);

        player.GetComponent<PlayerBehavior>().endingPosition = endingPosition;
    }
}
