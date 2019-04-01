using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skull : MonoBehaviour {

    private Transform player;

    public GameObject key;

    private void Start()
    {
        // UI Off
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        // while colliding, if player presses enter, search item inventory, collect item.
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        /*
        if(Player){

            //showMessage();

        }
        */

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.Z))
            {
                if (key != null)
                {
                    Destroy(key);
                    player.SendMessage("GainKey");
                }
            }
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {


    }

    void OnCollisonExit2D(Collision2D col){

        // turnOffMessage();

    }

    private void showMessage(){

        //show UI message

    }

    private void turnOffMessage(){

        //unshow UI message

    }
}
