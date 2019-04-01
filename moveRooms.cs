using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveRooms : MonoBehaviour {

    public GameObject stairsToGoToLocation;
    public GameObject playerObject;

    void OnTriggerEnter2D(Collider2D col){
        if (col.CompareTag("Player"))
        {
            playerObject.transform.position = stairsToGoToLocation.transform.position;
        }
	}

}
