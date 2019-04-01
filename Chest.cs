using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    private bool isOpen = false;

    public GameObject[] foods;
    public Transform player;
    public Sprite newSprite;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isOpen)
            {
                if (Input.GetKey(KeyCode.Z))
                {
                    this.GetComponent<SpriteRenderer>().sprite = newSprite;
                    transform.localScale = new Vector3(.95F, .95f, 0);
                    isOpen = true;
                    for (int i = 0; i < foods.Length; i++)
                    {
                        foods[i].SetActive(true);
                    }
                }
            }
        }
    }
}
