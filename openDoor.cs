using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoor : MonoBehaviour {

    public bool Locked;
    public GameObject player;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (!Locked)
            {
                DestroyDoor();
            }
            else if (Locked)
            {
                player.SendMessage("ThisDoor", gameObject);
                player.SendMessage("UnlockDoor");
            }
        }
    }

    void DestroyDoor(){
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
    
}
